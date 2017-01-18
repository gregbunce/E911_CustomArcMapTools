using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;

namespace E911_Tools
{
    public partial class frmEtlRoads : Form
    {
        //https://social.msdn.microsoft.com/Forums/en-US/7091c37d-64ee-422e-9552-92455b525f00/how-to-change-a-progressbar-from-another-class?forum=Vsexpressvcs



        string strDispatchSchema;
        string strDispatchEtlName;
        string strSourceWorkkSpace;
        string strTargetWorkSpace;
        IDataset arcDataSetETL2;

        // variables with class scope
        IFeatureClass pUtransFeatureClass;
        //IMap pMap;
        //IMxDocument pMxDocument;
        //IActiveView pActiveView;
        IFeatureClass arcFeatClass_CustomSegs;
        IFeatureClass arcFeatClass_CustomMMSegs;
        IFeatureClass arcFeatClass_CustomFwySegs;
        IFeatureClass arcFeatClass_RevGecodeData;
        IFeatureClass arcFeatClass_CityCd;
        IFeatureClass arcFeatClass_EmsZone;
        IFeatureClass arcFeatClass_FireZone;
        IFeatureClass arcFeatClass_LawZone;
        IFeatureClass arcFeatClass_Campus1;
        IFeatureClass arcFeatClass_CampusBld1;
        IFeatureClass arcFeatClass_CampusBld2;
        IFeatureClass arcFeatClass_SplitStreets;
        IFeatureClass arcFeatClass_DNR_Unique;
        IFeatureClass arcFeatClass_DOC_Unique;
        IFeatureClass arcFeatClass_DPS_Unique;

        //get access to the newly-created feature class with psap's schema
        Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
        IWorkspaceFactory workspaceFactoryETL; // = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
        IFeatureWorkspace arcFeatWorkspaceETL; // = (IFeatureWorkspace)workspaceFactory.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb", 0);
        IWorkspaceFactory workspaceFactorySchema;
        IFeatureWorkspace arcFeatWorkspaceSchema;
        IWorkspace workspaceE911;
        IFeatureWorkspace featureWorkspaceE911;
        IWorkspace workspaceSGID;
        IFeatureWorkspace featureWorkspaceSGID;


        public frmEtlRoads()
        {
            InitializeComponent();
        }


        private void GetRoadsData_Load(object sender, EventArgs e)
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // set the global application variable - from the button appliation hook
                clsE911Globals.arcApplication = btnEtlRoads.m_application;

                //get access to the document and the active view
                //pMxDocument = (IMxDocument)btnEtlRoads.m_application.Document;
                clsE911Globals.pMxDocument = (IMxDocument)clsE911Globals.arcApplication.Document;
                clsE911Globals.pMap = clsE911Globals.pMxDocument.FocusMap;
                clsE911Globals.pActiveView = clsE911Globals.pMxDocument.ActiveView;  //pActiveView = (IActiveView)pMap;

                // get access to the feature workspace
                ////workspaceFactoryETL = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                ////arcFeatWorkspaceETL = (IFeatureWorkspace)workspaceFactoryETL.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\StGeorgeE911ETL.gdb", 0);
                ////workspaceFactorySchema = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                ////arcFeatWorkspaceSchema = (IFeatureWorkspace)workspaceFactoryETL.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SchemaFGDBs\StGeorgeE911Schema.gdb", 0);


                // get access to the e911 database  - connect to sde
                workspaceE911 = clsE911StaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:e911.agrc.utah.gov", "E911", "OSA", "sde.DEFAULT");
                featureWorkspaceE911 = (IFeatureWorkspace)workspaceE911;

                workspaceSGID = clsE911StaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:sgid.agrc.utah.gov", "SGID10", "DBMS", "sde.DEFAULT", "agrc", "agrc");
                featureWorkspaceSGID = (IFeatureWorkspace)workspaceSGID;

                //get the editor extension
                UID arcUID = new UID();
                arcUID.Value = "esriEditor.Editor";
                clsE911Globals.arcEditor = clsE911Globals.arcApplication.FindExtensionByCLSID(arcUID) as IEditor3;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        // this method is run when the user clicks the form's button
        private void btnETLtoPSAP_Click(object sender, EventArgs e)
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // get the counties feature class to use as a buffer for selecting road segments
                clsE911Globals.arcFeatClass_CountiesSGID = featureWorkspaceSGID.OpenFeatureClass("SGID10.BOUNDARIES.Counties");

                // check what dispatch center we're working on
                strDispatchSchema = "";
                strDispatchEtlName = "";
                strSourceWorkkSpace = "";
                strTargetWorkSpace = "";

                // create a list of feature classes that we will be loading into the etl feature class
                List<IFeatureClass> listFeatureClasses = new List<IFeatureClass>();

                // create dispatch variables based user selection in combobox 
                switch (cboPSAPname.Text)
                {
                    case "StGeorge":
                        strDispatchSchema = "StGeorgeSchema";
                        strDispatchEtlName = "StGeorgeStreetsETL";
                        strSourceWorkkSpace = @"K:\AGRC Projects\E911_Editing\SchemaFGDBs\StGeorgeE911Schema.gdb";
                        strTargetWorkSpace = @"K:\AGRC Projects\E911_Editing\SaintGeorge\StGeorgeE911ETL.gdb";
                        // get access to utrans roads as feature class
                        arcFeatClass_CustomMMSegs = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.StGeorge_CustomMMSegments");
                        arcFeatClass_CustomSegs = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.StGeorge_CustomSegments");
                        arcFeatClass_CustomFwySegs = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.StGeorge_CustomFwySegments");

                        // add the feature classes into a list of feature classes
                        listFeatureClasses.Add(arcFeatClass_CustomFwySegs);
                        listFeatureClasses.Add(arcFeatClass_CustomMMSegs);
                        listFeatureClasses.Add(arcFeatClass_CustomSegs);
                        
                        //clsE911Globals.arcFeatClass_CityCD = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.StGeorge_CITYCD"); // global variable b/c i need access in reverse geocode form
                        //arcFeatClass_EmsZone = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.StGeorge_EMS_Zones");
                        //arcFeatClass_FireZone = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.StGeorge_Fire_Zones");
                        //arcFeatClass_LawZone = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.StGeorge_Law_Zones");
                        workspaceFactoryETL = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                        arcFeatWorkspaceETL = (IFeatureWorkspace)workspaceFactoryETL.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\StGeorgeE911ETL.gdb", 0);
                        workspaceFactorySchema = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                        arcFeatWorkspaceSchema = (IFeatureWorkspace)workspaceFactoryETL.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SchemaFGDBs\StGeorgeE911Schema.gdb", 0);
                        break;
                    case "TOC":
                        strDispatchSchema = "TOCRoadsSchema";
                        strDispatchEtlName = "TOCStreetsETL";
                        strSourceWorkkSpace = @"K:\AGRC Projects\E911_Editing\SchemaFGDBs\TocE911Schema.gdb";
                        strTargetWorkSpace = @"K:\AGRC Projects\E911_Editing\TOC\Data\TOC_E911_ETL.gdb";
                        // get access to utrans roads as feature class
                        arcFeatClass_CustomFwySegs = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.TOC_Streets_Freeways");
                        arcFeatClass_Campus1 = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.TOC_Streets_Campus");
                        arcFeatClass_CampusBld1 = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.BYU_Campus_Bldgs");
                        arcFeatClass_CampusBld2 = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.SLCC_Bldgs");
                        arcFeatClass_SplitStreets = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.Split_Streets");
                        arcFeatClass_DNR_Unique = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.TOC_Streets_DNR_Unique");
                        //arcFeatClass_DOC_Unique = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.TOC_Streets_DOC_Unique");  david wants to append these by hand after the spillman tools b/c they are outside of his toc polygon boundaries 
                        arcFeatClass_DPS_Unique = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.TOC_Streets_DPS_Unique");
                        arcFeatClass_CustomSegs = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.Streets_Custom_Segments");
                        arcFeatClass_RevGecodeData = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.HwyReverseGeocode");
                        clsE911Globals.arcFeatClass_CityCD = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.TOC_CITYCD"); // global variable b/c i need access in reverse geocode form


                        // add the feature classes into a list of feature classes
                        listFeatureClasses.Add(arcFeatClass_CustomFwySegs);
                        listFeatureClasses.Add(arcFeatClass_Campus1);
                        listFeatureClasses.Add(arcFeatClass_CampusBld1);
                        listFeatureClasses.Add(arcFeatClass_CampusBld2);
                        listFeatureClasses.Add(arcFeatClass_SplitStreets);
                        listFeatureClasses.Add(arcFeatClass_DNR_Unique);
                        listFeatureClasses.Add(arcFeatClass_DOC_Unique);
                        listFeatureClasses.Add(arcFeatClass_DPS_Unique);
                        listFeatureClasses.Add(arcFeatClass_CustomSegs);

                        workspaceFactoryETL = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                        arcFeatWorkspaceETL = (IFeatureWorkspace)workspaceFactoryETL.OpenFromFile(@"K:\AGRC Projects\E911_Editing\TOC\Data\TOC_E911_ETL.gdb", 0);
                        workspaceFactorySchema = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                        arcFeatWorkspaceSchema = (IFeatureWorkspace)workspaceFactoryETL.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SchemaFGDBs\TocE911Schema.gdb", 0);
                        break;
                }


                // get access to the dispacth center's schema file
                IFeatureClass arcFeatureClassSchema = arcFeatWorkspaceSchema.OpenFeatureClass(strDispatchSchema);

                // see if the etl feature class already exists (maybe from last run of the tool) - if so rename it
                IWorkspace2 arcWorkspace2_ETL = (IWorkspace2)arcFeatWorkspaceETL;
                if (arcWorkspace2_ETL.get_NameExists(esriDatasetType.esriDTFeatureClass, strDispatchEtlName) == true)
                {
                    IFeatureClass arcFC_ETL = arcFeatWorkspaceETL.OpenFeatureClass(strDispatchEtlName);
                    IDataset arcDataSetETL = (IDataset)arcFC_ETL;
                    arcDataSetETL.Rename(strDispatchEtlName + "_OldOn" + DateTime.Now.ToString("yyyyMMdd"));
                }
                
                
                // transfer the feature class to the staging ground fgdb so we can load data into it
                // Create workspace name objects.
                IWorkspaceName sourceWorkspaceName = new WorkspaceNameClass();
                IWorkspaceName targetWorkspaceName = new WorkspaceNameClass();
                IName targetName = (IName)targetWorkspaceName;

                // set the source workspace and feature class (this should eventually be in an enterprise database) - this is the fc for e911 roads editing, the odd segments we don't want in utrans
                sourceWorkspaceName.PathName = strSourceWorkkSpace;
                sourceWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";

                // set the target workspace for the extracted and merged roads data
                targetWorkspaceName.PathName = strTargetWorkSpace;
                targetWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";


                // Create a name object for the source feature class.
                IFeatureClassName featureClassNameSource = new FeatureClassNameClass();

                // Set the featureClassName properties.
                IDatasetName sourceDatasetName = (IDatasetName)featureClassNameSource;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = strDispatchSchema;
                IName sourceName = (IName)sourceDatasetName;

                // Create an enumerator for source datasets.
                IEnumName sourceEnumName = new NamesEnumeratorClass();
                IEnumNameEdit sourceEnumNameEdit = (IEnumNameEdit)sourceEnumName;

                // Add the name object for the source class to the enumerator.
                sourceEnumNameEdit.Add(sourceName);

                // Create a GeoDBDataTransfer object and a null name mapping enumerator.
                IGeoDBDataTransfer geoDBDataTransfer = new GeoDBDataTransferClass();
                IEnumNameMapping enumNameMapping = null;

                // Use the data transfer object to create a name mapping enumerator.
                Boolean conflictsFound = geoDBDataTransfer.GenerateNameMapping(sourceEnumName, targetName, out enumNameMapping);
                enumNameMapping.Reset();

                // Check for conflicts.
                if (conflictsFound)
                {
                    // Iterate through each name mapping.
                    INameMapping nameMapping = null;
                    while ((nameMapping = enumNameMapping.Next()) != null)
                    {
                        // Resolve the mapping's conflict (if there is one).
                        if (nameMapping.NameConflicts)
                        {
                            nameMapping.TargetName = nameMapping.GetSuggestedName(targetName);
                        }

                        // See if the mapping's children have conflicts.
                        IEnumNameMapping childEnumNameMapping = nameMapping.Children;
                        if (childEnumNameMapping != null)
                        {
                            childEnumNameMapping.Reset();

                            // Iterate through each child mapping.
                            INameMapping childNameMapping = null;
                            while ((childNameMapping = childEnumNameMapping.Next()) != null)
                            {
                                if (childNameMapping.NameConflicts)
                                {
                                    childNameMapping.TargetName = childNameMapping.GetSuggestedName
                                        (targetName);
                                }
                            }
                        }
                    }
                }

                // Start the transfer.
                geoDBDataTransfer.Transfer(enumNameMapping, targetName);


                // rename the newly copied feature class
                IFeatureClass arcFC_ETL2 = arcFeatWorkspaceETL.OpenFeatureClass(strDispatchSchema);
                arcDataSetETL2 = (IDataset)arcFC_ETL2;
                arcDataSetETL2.Rename(strDispatchEtlName);


                //load the custom segments and the mile marker segments to the blank streets etl feature class
                foreach (var featureClass in listFeatureClasses)
                {
                    loadCustomSegmentsFromE911(featureClass, arcDataSetETL2);
                }
                
                // call the method to load (insert) segments data from utrans
                insertNewFeaturesFromUtrans();

                MessageBox.Show("The E911 Tool finished the roads ETL!  The output feature class from this process is located here: " + strTargetWorkSpace + ".", "Finished!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        // load the custom segments into the etl feature class
        private void loadCustomSegmentsFromE911(IFeatureClass arcFcInput, IDataset arcFcTarget)
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                Geoprocessor geoProcessor = new Geoprocessor();
                ESRI.ArcGIS.DataManagementTools.Append append = new ESRI.ArcGIS.DataManagementTools.Append();
                append.inputs = arcFcInput; // e911 custom segs
                append.target = arcFcTarget; // the newly created etl dataset
                append.schema_type = "TEST";  // NO_TEST will not give you errors but you might not get all the data
                
                IGeoProcessorResult result = (IGeoProcessorResult)geoProcessor.Execute(append, null);

                // see if there were any messages
                if (result.MessageCount > 0)
                {
                    // loop though the messages and report them
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        // this method inserts new rows in the newly-created feature class
        private void insertNewFeaturesFromUtrans()
        {
            try
            {
                lblProgressBar.Visible = true;

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // set up the progress bar on the form to show progress
                pBar.Visible = true;
                pBar.Minimum = 1;
                pBar.Value = 1;
                pBar.Step = 1;


                ////get access to the newly-created feature class with psap's schema
                //Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                //IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                //IFeatureWorkspace arcFeatWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb", 0);
                //IFeatureWorkspace arcFeatWorkspace = (IFeatureWorkspace)workspaceFactory;
                //IFeatureClass arcFeatClassNewSchemaFeat = arcFeatWorkspace.OpenFeatureClass(cboPSAPname.Text.ToString() + "_ETL_" + DateTime.Now.ToString("yyyyMMdd"));
                clsE911Globals.arcFeatClassNewSchemaFeat = arcFeatWorkspaceETL.OpenFeatureClass(strDispatchEtlName);

                // get access to utrans database and the centerline feature class
                //connect to sde
                //IWorkspace workspace = clsE911StaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:utrans.agrc.utah.gov", "UTRANS", "OSA", "sde.DEFAULT");
                IWorkspace workspace = clsE911StaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:utrans.agrc.utah.gov", "UTRANS", "OSA", "TRANSADMIN.EDIT");
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;

                // get access to utrans roads as feature class
                pUtransFeatureClass = featureWorkspace.OpenFeatureClass("UTRANS.TRANSADMIN.StatewideStreets");

                // create a feature cursor to get that psap's related county roads
                var tupleFeatCurInt = getFeatureCursurForPSAP(cboPSAPname.Text, pUtransFeatureClass);

                // get utrans feature cursor for looping through
                IFeatureCursor arcFeatCurRoadsUtrans = tupleFeatCurInt.Item1;
                // get feature count for progress bar
                int intFeatureCount = tupleFeatCurInt.Item2;


                pBar.Maximum = intFeatureCount;

                IFeature arcFeatureUtrans;

                // loop through this psap's utrans road segments and load them into the newly-created filegeodatabase feature class (psap schema)
                while ((arcFeatureUtrans = arcFeatCurRoadsUtrans.NextFeature()) != null)
                {
                    // create a new row in the newly-created feature class
                    IFeature arcFeatureNewSchemaFeat = clsE911Globals.arcFeatClassNewSchemaFeat.CreateFeature();

                    // add the shape/geometry from utrans into the psap's schema feature class
                    arcFeatureNewSchemaFeat.Shape = arcFeatureUtrans.Shape;


                    // set the values for the generic fields - that are the same no matter what dispatch center //

                    // STREETNAME
                    string strSTREETNAME = "";
                    strSTREETNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();
                    if (strSTREETNAME != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), DBNull.Value);
                        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), null);
                    }

                    // L_F_ADD
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")));
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), 0);
                    }

                    // L_T_ADD
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")));
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), 0);
                    }

                    // R_F_ADD
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")));
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), 0);
                    }

                    // R_T_ADD
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")));
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), 0);
                    }

                    // check if all address values are zero
                    // convert address ranges values to double
                    double dblRT_ADD = 0;
                    double dblRF_ADD = 0;
                    double dblLT_ADD = 0;
                    double dblLF_ADD = 0;
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")).ToString().Trim() != "")
                    {
                        dblRT_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")));
                    }
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")).ToString().Trim() != "")
                    {
                        dblRF_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")));
                    }
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")).ToString().Trim() != "")
                    {
                        dblLT_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")));
                    }
                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")).ToString().Trim() != "")
                    {
                        dblLF_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")));
                    }
                    bool blnAddressRangeAllZeros = false;
                    // check if address ranges are zeros
                    if (dblRT_ADD == 0 & dblRF_ADD == 0 & dblLT_ADD == 0 & dblLF_ADD == 0)
                    {
                        blnAddressRangeAllZeros = true;
                    }

                    // CARTOCODE
                    string strCartoCode = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("CARTOCODE")).ToString().Trim();
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("CARTOCODE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("CARTOCODE")).ToString().Trim());

                    // PREDIR
                    string strPREDIR = "";
                    strPREDIR = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim();
                    if (strPREDIR != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"), DBNull.Value);
                        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"), null);
                    }

                    // STREETTYPE
                    string strSTREETTYPE = "";
                    strSTREETTYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim();
                    if (strSTREETTYPE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETTYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETTYPE"), DBNull.Value);
                    }

                    // SUFDIR
                    string strSUFDIR = "";
                    strSUFDIR = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                    if (strSUFDIR != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SUFDIR"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SUFDIR"), DBNull.Value);
                    }
                     
                    // ALIAS1
                    string strALIAS1 = "";
                    strALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                    if (strALIAS1 != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1"), DBNull.Value);
                    }

                    // ALIAS1TYPE
                    string strALIAS1TYPE = "";
                    strALIAS1TYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim();
                    if (strALIAS1TYPE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1TYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1TYPE"), DBNull.Value);
                    }

                    // ALIAS2
                    string strALIAS2 = "";
                    strALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                    if (strALIAS2 != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2"), DBNull.Value);
                    }

                    // ALIAS2TYPE
                    string strALIAS2TYPE = "";
                    strALIAS2TYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim();
                    if (strALIAS2TYPE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2TYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2TYPE"), DBNull.Value);
                    }

                    // ACSALIAS
                    string strACSALIAS = "";
                    strACSALIAS = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSALIAS")).ToString().Trim();
                    if (strACSALIAS != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSALIAS"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSALIAS")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSALIAS"), DBNull.Value);
                    }

                    // ACSNAME
                    string strACSNAME = "";
                    strACSNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim();
                    if (strACSNAME != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSNAME"), DBNull.Value);
                    }

                    // ACSSUF
                    string strACSSUF = "";
                    strACSSUF = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim();
                    if (strACSSUF != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSSUF"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSSUF"), DBNull.Value);
                    }


                    // ZIPLEFT
                    string strZIPLEFT = "";
                    strZIPLEFT = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPLEFT")).ToString().Trim();
                    if (strZIPLEFT != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ZIPLEFT"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPLEFT")).ToString().Trim());
                    }

                    // ZIPRIGHT
                    string strZIPRIGHT = "";
                    strZIPRIGHT = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPRIGHT")).ToString().Trim();
                    if (strZIPRIGHT != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ZIPRIGHT"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPRIGHT")).ToString().Trim());
                    }

                    // COFIPS
                    string strCOFIPS = "";
                    strCOFIPS = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("COFIPS")).ToString().Trim();
                    if (strCOFIPS != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("COFIPS"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("COFIPS")).ToString().Trim());
                    }

                    // HWYNAME
                    string strHWYNAME = "";
                    strHWYNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("HWYNAME")).ToString().Trim();
                    if (strHWYNAME != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("HWYNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("HWYNAME")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("HWYNAME"), DBNull.Value);
                    }

                    // DOT_RTNAME
                    string strDOTRTNAME = "";
                    strDOTRTNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_RTNAME")).ToString().Trim();
                    if (strDOTRTNAME != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_RTNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_RTNAME")).ToString().Trim());
                    }

                    // DOT_F_MILE
                    string strDOTFMILE = "";
                    strDOTFMILE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_F_MILE")).ToString().Trim();
                    if (strDOTFMILE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_F_MILE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_F_MILE")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_F_MILE"), 0);
                    }

                    // DOT_T_MILE
                    string strDOTTMILE = "";
                    strDOTTMILE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_T_MILE")).ToString().Trim();
                    if (strDOTTMILE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_T_MILE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_T_MILE")).ToString().Trim());
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_T_MILE"), 0);
                    }

                    // SALIAS3
                    // populate the salias3 field if street name or the alias1 or alias2 fields contain HIGHWAY... then replace with HWY
                    bool blnStreetNameContainsHwy = strSTREETNAME.Contains("HIGHWAY");
                    bool blnAlias1ContainsHwy = strALIAS1.Contains("HIGHWAY");
                    bool blnAlias2ContainsHwy = strALIAS2.Contains("HIGHWAY");
                    string strSALIAS3 = "";
                    if (blnStreetNameContainsHwy) // if highway in streetname
                    {
                        strSTREETNAME = strSTREETNAME.Replace("HIGHWAY", "HWY");
                        strSALIAS3 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                            strSTREETNAME + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                        strSALIAS3 = strSALIAS3.Replace("  ", " ");
                        strSALIAS3 = strSALIAS3.Trim();
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS3"), strSALIAS3);
                    }
                    else
                    {
                        if (blnAlias1ContainsHwy) // if highway in alias1
                        {
                            strALIAS1 = strALIAS1.Replace("HIGHWAY", "HWY");
                            strSALIAS3 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                            strALIAS1 + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim() + " " +
                                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                            strSALIAS3 = strSALIAS3.Replace("  ", " ");
                            strSALIAS3 = strSALIAS3.Trim();
                            arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS3"), strSALIAS3);
                        }
                        else // if highway in allias2
                        {
                            if (blnAlias2ContainsHwy)
                            {
                                strALIAS2 = strALIAS2.Replace("HIGHWAY", "HWY");
                                strSALIAS3 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                                   strALIAS2 + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim() + " " +
                                       arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                                strSALIAS3 = strSALIAS3.Replace("  ", " ");
                                strSALIAS3 = strSALIAS3.Trim();
                                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS3"), strSALIAS3);
                            }
                        }
                    }

                    // LOCATION and SALIAS4 (the value in the LOCATION field is placed after a colon on the calltakers screen to show them the numberic location of the call)
                    if (strACSNAME != "")
                    {
                        string strLOCATION = "";
                        strLOCATION = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim();
                        strLOCATION = strLOCATION.Replace("  ", " ");
                        strLOCATION = strLOCATION.Trim();
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("LOCATION"), strLOCATION);
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS4"), strLOCATION);
                    }

                    #region "Assign Synthetic Address Ranges"
                    // CHECK TO SEE IF USER WANTS TO CREATE SYNTHETIC ADDRESS RANGES, IF RANGES ARE ZERO
                    if (strHWYNAME != "" & blnAddressRangeAllZeros == true & chkAssignSythetics.Checked & strDOTFMILE != "" & strDOTTMILE != "")
                    {
                        double dblDOT_F_Mile = Convert.ToDouble(strDOTFMILE);
                        double dblDOT_T_Mile = Convert.ToDouble(strDOTTMILE);

                        dblDOT_F_Mile = dblDOT_F_Mile * 1000;
                        dblDOT_T_Mile = dblDOT_T_Mile * 1000;

                        double dblSynth_L_F;
                        double dblSynth_L_T;
                        double dblSynth_R_F;
                        double dblSynth_R_T;

                        // assign the from miles
                        if (clsE911StaticClass.IsOdd(dblDOT_F_Mile))
                        {
                            dblSynth_L_F = dblDOT_F_Mile;
                            dblSynth_R_F = dblDOT_F_Mile - 1;
                        }
                        else
                        {
                            dblSynth_L_F = dblDOT_F_Mile - 3;
                            dblSynth_R_F = dblDOT_F_Mile;
                        }

                        // assign the to miles
                        if (clsE911StaticClass.IsOdd(dblDOT_T_Mile))
                        {
                            dblSynth_L_T = dblDOT_T_Mile;
                            dblSynth_R_T = dblDOT_T_Mile - 1;
                        }
                        else
                        {
                            dblSynth_L_T = dblDOT_T_Mile - 3;
                            dblSynth_R_T = dblDOT_T_Mile;
                        }

                        // check if the values were zero and then got converted to negative, if so give them a positive value 
                        if (dblSynth_L_F == -3)
                        {
                            dblSynth_L_F = dblSynth_L_F + 6;
                        }
                        if (dblSynth_L_T == -3)
                        {
                            dblSynth_L_T = dblSynth_L_T + 6;
                        }
                        if (dblSynth_R_F == -1)
                        {
                            dblSynth_R_F = dblSynth_R_F + 2;
                        }
                        if (dblSynth_R_T == -1)
                        {
                            dblSynth_R_T = dblSynth_R_T + 2;
                        }

                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), dblSynth_L_F);
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), dblSynth_L_T);
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), dblSynth_R_F);
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), dblSynth_R_T);

                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SyntheticAddrRange"), "Y");
                    }
                    #endregion


                    // NOW POPULATE FIELDS THAT ARE SPECIFIC TO THE DISPATCH CENTERS
                    // get the specific field values in string format so we can manipulate them
                    
                    // get needed info for STREET formatting
                    string strSTREET = "";
                    int intNumber;
                    bool blnIsNumeric = int.TryParse(strSTREETNAME, out intNumber);
                    // check if streetname is numeric and if it has a streetype of cir if so format it with the circle after the sufdir
                    if (strSTREETTYPE.ToUpper() == "CIR" & blnIsNumeric) // this is for st george b/c they add cir to numberic streets
                    {
                        strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                    }
                    else // the streetname does not contain a number with the streettype being "cir"
                    {
                        strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                    }
                    // replace double spaces with one
                    strSTREET = strSTREET.Replace("  ", " ");

                    // get neded info for SALLIAS1 formatting
                    string strAlias1_ = "";
                    string strSALIAS1 = "";
                    strAlias1_ = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                    if (strAlias1_ != "")
                    {
                        strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim();
                        strSALIAS1 = strSALIAS1.Replace("  ", " ");
                    }

                    // get needed info for SALIAS2 formatting
                    string strAlias2_ = "";
                    string strSALIAS2 = "";
                    strAlias2_ = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                    if (strAlias2_ != "")
                    {
                        strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim() + " " +
                             arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim();
                        strSALIAS2 = strSALIAS2.Replace("  ", " ");
                    }


                    // get formatting for unique fields - call the appropriate dispatch center's class.method and format as needed //
                    switch (cboPSAPname.Text.ToString())
                    {
                        case "StGeorge":
                            // STREET
                            strSTREET = clsStGeorge.StGeorgeSTREET(strSTREET);
                            // SALIAS1
                            if (strSALIAS1 != "")
                            {
                                strSALIAS1 = clsStGeorge.StGeorgeSALIAS1(strSALIAS1);
                            }
                            // SALIAS2
                            if (strSALIAS2 != "")
                            {
                                strSALIAS2 = clsStGeorge.StGeorgeSALIAS2(strSALIAS2);
                            }
                            break;
                        case "TOC":
                            // STREET
                            strSTREET = clsTOC.TocSTREET(strSTREET, strHWYNAME, strSTREETNAME);
                            // SALIAS1
                            if (strSALIAS1 != "")
                            {
                                strSALIAS1 = clsTOC.TocSALIAS1(strSALIAS1);
                            }
                            // SALIAS2
                            if (strSALIAS2 != "")
                            {
                                strSALIAS2 = clsTOC.TocSALIAS2(strSALIAS2);
                            }
                            break;
                    }


                    // SET THE VALUES FOR THE UNIQUE FIELDS BASED ON WHAT WAS RETURNED FROM THE METHODS CALLED ABOVE
                    // set_Value for STREET
                    // make sure string is not greater than the field allows
                    IFields fields = arcFeatureNewSchemaFeat.Fields;
                    IField fieldLength = fields.get_Field(fields.FindField("STREET"));
                    if (strSTREET.Length > fieldLength.Length)
                    {
                        strSTREET = "error: length > " + fieldLength.Length.ToString();
                    }
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREET"), strSTREET);

                    // set_Value for SALIAS1
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1);
                    
                    // set_Value for SALIAS2
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2);

                    // RunETL_Date
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("RunETL_Date"), DateTime.Now);

                    // FULLNAME
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("FULLNAME"), "");

                    // Store() the new row/feature
                    // check if geometry is empty before store
                    if (!(arcFeatureUtrans.Extent.Envelope.IsEmpty))
                    {
                        arcFeatureNewSchemaFeat.Store();
                    }

                    // preform the increment of the progress bar
                    pBar.PerformStep();


                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    // check if we need to update the reverse geocode data on sde (CREATE SEPERATE METHOD FOR THIS BLOCK)
                    if (chkUpdateRevGeocodeData.Checked)
                    {
                        IDataset arcSDEDataSet_RevGeocoder = null;
                        IFeatureClass featureClass = null;

                        if (cboPSAPname.Text.ToString() == "TOC")
                        {
                            // make idataset from the reverse geocode sde feature class    
                            arcSDEDataSet_RevGeocoder = (IDataset)arcFeatClass_RevGecodeData;

                            IQueryDef queryDef = arcFeatWorkspaceETL.CreateQueryDef();
                            //provide list of tables to join
                            queryDef.Tables = strDispatchEtlName;
                            //retrieve the fields from all tables
                            queryDef.SubFields = "*";
                            //set up join
                            queryDef.WhereClause = @"CARTOCODE NOT IN ('99', '7', '1') and STREETTYPE <> 'FWY' AND HWYNAME <> ''
                                                AND FULLNAME NOT LIKE  '% SB %' AND  FULLNAME NOT LIKE  '% NB %' AND FULLNAME NOT LIKE  
                                                '% EB %' AND  FULLNAME NOT LIKE  '% WB %' AND FULLNAME NOT LIKE  '% SB' AND  FULLNAME NOT LIKE  
                                                 '% NB' AND FULLNAME NOT LIKE  '% EB' AND  FULLNAME NOT LIKE  '% WB'";

                            //Create FeatureDataset. Note the use of .OpenFeatureQuery.
                            //The name "MyJoin" is the name of the restult of the query def and
                            //is used in place of a feature class name.
                            IFeatureDataset featureDataset = arcFeatWorkspaceETL.OpenFeatureQuery("ReverseGeocodeData", queryDef);
                            //open layer to test against
                            IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;
                            featureClass = featureClassContainer.get_ClassByName("ReverseGeocodeData");

                            //IFeatureClass arcFeatClassWithQueryDef = clsE911StaticClass.GetFeatureClassWithQueryDef();  //IDataset arcDataSet, IFeatureWorkspace arcFeatureWS, string strDispatchEtlName, string strWhereClause
                            //loadCustomSegmentsFromE911(arcFeatClassWithQueryDef, arcSDEDataSet_RevGeocoder);
                        }

                        // check to see if we are deleting existing features in the reverse geocode data dataset (the source data for the reverse address locator)
                        if (radioLoad.Checked == true)
                        {
                            // do nothing...
                        }
                        else if (radioTruncateLoad.Checked == true)
                        {
                            // delete the existing features in the dataset
                            IFeatureCursor arcFeatCur_delete = arcFeatClass_RevGecodeData.Search(null, false);
                            IFeature arcFeat_delete;

                            while ((arcFeat_delete = arcFeatCur_delete.NextFeature()) != null)
                            {
                                arcFeatCur_delete.DeleteFeature();
                            }
                        }
                        // call the laod data function to load the data
                        loadCustomSegmentsFromE911(featureClass, arcSDEDataSet_RevGeocoder);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        // this method returns a feature cursor and an int count of the feature cursor of utrans roads based on the user defined psap
        private Tuple<IFeatureCursor, int> getFeatureCursurForPSAP(String strPSAPName, IFeatureClass arcFC)
        {
            try
            {
                string strCountyList = "";
                string strCountyPolyWhereClause = "";


                // get list of counties for this psap
                switch (strPSAPName)
                {
                    case "StGeorge":
                        strCountyList = "CARTOCODE <> 99 and streettype not in ('FWY','RAMP')";
                        //strCountyList = "COFIPS = '49053' AND STREETNAME IS NOT NULL AND (( L_F_ADD IS NOT NULL AND L_T_ADD IS NOT NULL AND R_F_ADD IS NOT NULL AND R_T_ADD IS NOT NULL) AND (L_F_ADD <> 0 AND L_T_ADD <> 0 AND R_F_ADD <> 0 AND R_T_ADD <> 0))";
                        strCountyPolyWhereClause = "FIPS_STR = 49053";
                        //strCountyPolyWhereClause = "FIPS_STR in (49053, 49021)"; // testing to see if more than one poly can be buffered via union
                        break;
                    case "TOC":
                        // this query restrict the segments to ones that david does not concider freeways (divided highways and interstates)
                        strCountyList = @"CARTOCODE NOT IN (99, 7, 1) and STREETTYPE <> 'FWY'
                                        AND FULLNAME NOT LIKE  '% SB %' AND  FULLNAME NOT LIKE  '% NB %' AND FULLNAME NOT LIKE  
                                        '% EB %' AND  FULLNAME NOT LIKE  '% WB %' AND FULLNAME NOT LIKE  '% SB' AND  FULLNAME NOT LIKE  
                                        '% NB' AND FULLNAME NOT LIKE  '% EB' AND  FULLNAME NOT LIKE  '% WB'";

                        //                            // this query gets only the highways from utrans... that are not considered freeways (per David) - just highways only
                        //                            strCountyList = @"CARTOCODE NOT IN (99, 7, 1) and STREETTYPE <> 'FWY' AND HWYNAME <> ''
                        //                                        AND FULLNAME NOT LIKE  '% SB %' AND  FULLNAME NOT LIKE  '% NB %' AND FULLNAME NOT LIKE  
                        //                                        '% EB %' AND  FULLNAME NOT LIKE  '% WB %' AND FULLNAME NOT LIKE  '% SB' AND  FULLNAME NOT LIKE  
                        //                                        '% NB' AND FULLNAME NOT LIKE  '% EB' AND  FULLNAME NOT LIKE  '% WB'";
                        
                        strCountyPolyWhereClause = "FIPS_STR in ('49049', '49035')";
                        break;
                }
                
                // select the county/counties based on the selected dispatch center
                IQueryFilter arcQF_CountyPolys = new QueryFilter();
                arcQF_CountyPolys.WhereClause = strCountyPolyWhereClause;
                IFeatureCursor arcFeatCur_CountyPolys = clsE911Globals.arcFeatClass_CountiesSGID.Search(arcQF_CountyPolys, false);

                // actually get them all in a geometry bag so we can buffer the geometry bag/collection
                //clsE911Globals.arcFeature_CountyPoly = arcFeatCur_CountyPolys.NextFeature();


                IGeometryCollection pGeoColl = new GeometryBag() as IGeometryCollection;
                //object obj = Type.Missing;

                // loop through all the polygons in the cursor and add them to the geometry collection
                while ((clsE911Globals.arcFeature_CountyPoly = arcFeatCur_CountyPolys.NextFeature()) != null)
                {
                    IPolygon arcPolygon = (IPolygon)clsE911Globals.arcFeature_CountyPoly.Shape;
                    // add the polygon to the collection
                    pGeoColl.AddGeometry(arcPolygon);
                    //pGeoColl.AddGeometry(arcPolygon, ref obj, ref obj);
                }

                //set the geometry bag equal to the geometry collection
                IGeometryBag pGeoBag = pGeoColl as IGeometryBag;

                // set the bag's spatial reference
                //ISpatialReference arcSpatialReference;
                IGeoDataset arcGeoDataset = (IGeoDataset)clsE911Globals.arcFeatClass_CountiesSGID;
                pGeoBag.SpatialReference = arcGeoDataset.SpatialReference;


                // buffer the geometry bag a user-defined distance
                //IGeometry arcGeomFromGeoBag = (IGeometry)pGeoBag;
                //arcGeomFromGeoBag.SpatialReference = arcGeoDataset.SpatialReference;

                ////////clsE911Globals.arcFeature_CountyPoly = arcFeatCur_CountyPolys.NextFeature();
                ////////IPolygon arcPoly = (IPolygon)clsE911Globals.arcFeature_CountyPoly.Shape;

                IPolygon arcPoly = null;
                
                if (pGeoColl.GeometryCount == 1) 
                {
                    arcPoly = (IPolygon)pGeoColl.Geometry[0];               
                }
                else if (pGeoColl.GeometryCount > 1)
                {
                    ITopologicalOperator arcTopoOpUnion = new PolygonClass();
                    arcTopoOpUnion.ConstructUnion(pGeoBag as IEnumGeometry);
                    // (ITopologicalOperator)pGeoBag;
                    //arcPoly = arcTopoOp.Union(pGeoColl.Geometry[0]);
                    arcPoly = (IPolygon)arcTopoOpUnion;

                }
                else
                {
                    MessageBox.Show("Counld not find the county polygon boundary to buffer.", "Must have polygon county boundary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }


                // run the buffer on the county/counties (convert miles to meters for code)  meters = miles/0.00062137
                int intTxtBoxBufferMiles = Convert.ToInt16(txtBuffer.Text);
                double dblBufferMeters = intTxtBoxBufferMiles / 0.00062137;

                //MessageBox.Show("Meters: " + intTxtBoxBufferMiles.ToString() + ", Miles: " + dblBufferMeters.ToString());
                //return null; 

                ITopologicalOperator arcTopoOp2 = (ITopologicalOperator)arcPoly;
                IPolygon arcPolyCountyBuffer = arcTopoOp2.Buffer(dblBufferMeters) as IPolygon;


                // create a spatial filter to get the utrans segments with a query filter to exclude ramps, freeways, and cartocode 99
                ISpatialFilter spatialFilter = new SpatialFilterClass();
                spatialFilter.Geometry = arcPolyCountyBuffer;
                spatialFilter.GeometryField = arcFC.ShapeFieldName;
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                spatialFilter.WhereClause = strCountyList;

                // get a cursor of utrans road segments that intersect the polygon buffer boundary
                IFeatureCursor arcFeatCur = arcFC.Search(spatialFilter, false);

                // get feature count
                int intFeatureCount = arcFC.FeatureCount(spatialFilter);

                return Tuple.Create(arcFeatCur, intFeatureCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }



        // select polylines that intersect polygon boundaries (based on roads layer selected in map's TOC)
        private void btnSelectIntersectSegs_Click(object sender, EventArgs e)
        {
            try
            {
                // open the out-of-the-box select by location tool

                UID pUID = new UID();
                pUID.Value = "{82B9951B-DD63-11D1-AA7F-00C04FA37860}";
                //82B9951B-DD63-11D1-AA7F-00C04FA37860  esriArcMapUI.SelectByLayerCommand

                ICommandBars commandBars = clsE911Globals.arcApplication.Document.CommandBars;
                ICommandItem item = commandBars.Find(pUID) as ICommandItem;

                item.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        // split the selected segments by way of calling the custom split line class
        private void btnSplitSelectSeg_Click(object sender, EventArgs e)
        {
            try
			{
                // check if zone is selected...
                if (cboSelectLawZone.SelectedItem == null)
                {
                    MessageBox.Show("Please select a boundary from the drop-down menu to base split on.", "Must select boundary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // check if editing first
                if (clsE911Globals.arcEditor.EditState == ESRI.ArcGIS.Editor.esriEditState.esriStateNotEditing)
                {
                    MessageBox.Show("You must be editing the psap roads in order to split lines.  Please start editing and then try again.", "Must Be Editing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // get minimun split legnth from txtbox
                int intMinLength;
                if (txtLengthMin.Text != "")
                {
                    intMinLength = Convert.ToInt16(txtLengthMin.Text);
                }
                else
                {
                    intMinLength = 0;
                }

             
                bool blnAllSegsSplit = true;
                bool blnNoZeroRanges = true;
                int intCountTotal = 0;
                int intCountSkipped = 0;

                // get access to the boundaries feature class
                string strSplitBoundaryName = "";
                //string strDispatchCenterName = "";

                // check what dispactch center we're working with
                if (cboPSAPname.SelectedItem != null)
                {
                    switch (cboPSAPname.Text.ToString())
                    {
                        case "StGeorge":
                            switch (cboSelectLawZone.Text.ToString())
                            {
                                case "City CD":
                                    strSplitBoundaryName = "E911.E911ADMIN.StGeorge_CITYCD";
                                    break;
                                case "EMS Zones":
                                    strSplitBoundaryName = "E911.E911ADMIN.StGeorge_EMS_Zones";
                                    break;
                                case "Fire Zones":
                                    strSplitBoundaryName = "E911.E911ADMIN.StGeorge_Fire_Zones";
                                    break;
                                case "Law Zones":
                                    strSplitBoundaryName = "E911.E911ADMIN.StGeorge_Law_Zones";
                                    break;
                            }  
                            break;
                        case "TOC":
                            
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a dispactch center from top drop-down list to base split boundaries on.", "Must select dispatch center", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // get access to the selected boundary from the selected dispatch center
                IFeatureClass arcFeatClassSplitBoundary = featureWorkspaceE911.OpenFeatureClass(strSplitBoundaryName);

                // loop through the maps layer and get the highlighted layer and make sure it's a polyline layer
                //////get access to the document and the active view
                ////pMxDocument = (IMxDocument)btnEtlRoads.m_application.Document;
                ////pMap = pMxDocument.FocusMap;
                ////pActiveView = pMxDocument.ActiveView;  //pActiveView = (IActiveView)pMap;

                //make sure the user has selected a polyline layer
                if (clsE911Globals.pMxDocument.SelectedLayer == null)
                {
                    MessageBox.Show("Please select the roads layer in the TOC.", "Select Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!(clsE911Globals.pMxDocument.SelectedLayer is IFeatureLayer))
                {
                    MessageBox.Show("Please select polyline layer.", "Must be PolyLine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //cast the selected layer as a feature layer
                IGeoFeatureLayer pGFlayer = (IGeoFeatureLayer)clsE911Globals.pMxDocument.SelectedLayer;
                clsE911Globals.arcFeatClass_PSAPRoads = pGFlayer.FeatureClass;


                //check if the feaure layer is a polyline layer
                if (pGFlayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                {
                    MessageBox.Show("Please select a polyline layer.", "Must be Polyline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                // get the selected features from the selected roads in the arcmap layer
                ISelectionSet selectedFeatures = ((IFeatureSelection)pGFlayer).SelectionSet;
                 
                IQueryFilter arcQueryFilter = new QueryFilter();
                arcQueryFilter.WhereClause = "";

                ICursor arcCursor;
                selectedFeatures.Search(null, false, out arcCursor);
                
                // create a ComReleaser for cursor management
                using (ComReleaser comReleaser = new ComReleaser())
                {
                    IFeatureCursor arcFeatureCursor = (IFeatureCursor)arcCursor;
                    comReleaser.ManageLifetime(arcFeatureCursor);

                    
                    while ((clsE911Globals.arcFeatureRoadSegment = arcFeatureCursor.NextFeature()) != null)
                    {
                        intCountTotal = intCountTotal + 1;

                        // select the polygon that the current segment intersects
                        // use spatial filter to get segments that intersect polygons
                        // Create the spatial filter and set its spatial constraints.
                        ISpatialFilter spatialFilter = new SpatialFilterClass();
                        spatialFilter.Geometry = (IGeometry)clsE911Globals.arcFeatureRoadSegment.Shape;
                        spatialFilter.GeometryField = arcFeatClassSplitBoundary.ShapeFieldName;
                        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        IFeature arcFeaturePSAPpoly;

                        // Execute the query.
                        using (ComReleaser comReleaser2 = new ComReleaser())
                        {
                            IFeatureCursor featureCursor = arcFeatClassSplitBoundary.Search(spatialFilter, true);
                            comReleaser2.ManageLifetime(featureCursor);
                            arcFeaturePSAPpoly = featureCursor.NextFeature();                       
                        }

                        //// get all the interecting polygons
                        //while ((arcFeature = featureCursor.NextFeature()) != null)
                        //{
                        //}


                        // check if a polygon intersect the current road segment, if not skip and move to the next selected road segment
                        if (arcFeaturePSAPpoly != null)
                        {
                            // create points based on the intersecting locations of the road segment and the psap polygon
                            List<IPoint> resultPoints = new List<IPoint>();
                            ITopologicalOperator topOperator = (ITopologicalOperator)clsE911Globals.arcFeatureRoadSegment.Shape;
                            IGeometry resultGeom = (IGeometry)topOperator.Intersect(arcFeaturePSAPpoly.Shape, esriGeometryDimension.esriGeometry0Dimension);

                            IGeometryCollection pointCollection = (IGeometryCollection)resultGeom;

                            for (int i = 0; i < pointCollection.GeometryCount; i++)
                            {
                                resultPoints.Add((IPoint)pointCollection.get_Geometry(i));
                            }

                            // check how many points are in the point collection before we look for the second one
                            // check how far along the line the first point in the point collection is before we designate the point as a split location
                            // this avoids the error stating that the line could not be split b/c it would result in a zero legnth polyline
                            // it happens when the point from the intersect is at the end of the line, touching the boundary... rather we want the intersect point that is further down the polyline

                            if (pointCollection.GeometryCount == 0)
                            {
                                // exit the loop and get the next selected road segment
                                //MessageBox.Show("continue");
                                continue;
                            }
                            else if (pointCollection.GeometryCount == 1)
                            {
                                clsE911Globals.arcPoint = (IPoint)pointCollection.get_Geometry(0);
                                IPolyline arcPolyLine_RoadSeg = (IPolyline)clsE911Globals.arcFeatureRoadSegment.Shape;

                                //double dblDistAlongLine = getDistanceAlongPoint(clsE911Globals.arcPoint, arcPolyLine_RoadSeg);
                                double dblDistAlongLine = getDistanceAlongPoint(clsE911Globals.arcPoint, arcPolyLine_RoadSeg);
                                if (dblDistAlongLine  < .05 | dblDistAlongLine > .95) // less than 10% or greater than 90% along the line (depending on which way the line is pointed)
                                {
                                    // exit the loop and get the next selected road segment
                                    //MessageBox.Show("continue");
                                    continue;
                                }
                            }
                            else
                            {
                                // there's more than one point in the collection we can move to the next needed
                                clsE911Globals.arcPoint = (IPoint)pointCollection.get_Geometry(0);
                                IPolyline arcPolyLine_RoadSeg = (IPolyline)clsE911Globals.arcFeatureRoadSegment.Shape;

                                double dblDistAlongLine = getDistanceAlongPoint(clsE911Globals.arcPoint, arcPolyLine_RoadSeg);
                                if (dblDistAlongLine < .05 | dblDistAlongLine > .95) // less than 10% or greater than 90% along the line (depending on which way the line is pointed)
                                {
                                    clsE911Globals.arcPoint = (IPoint)pointCollection.get_Geometry(1);

                                    // check the second point in the collection to see if it's also close to the edge of the line
                                    double dblDistAlongLine2 = getDistanceAlongPoint(clsE911Globals.arcPoint, arcPolyLine_RoadSeg);
                                    if (dblDistAlongLine2 < .05 | dblDistAlongLine2 > .95) // less than 10% or greater than 90% along the line (depending on which way the line is pointed)
                                    {
                                        // try to get the third point in the collection
                                        if (pointCollection.GeometryCount > 2)
                                        {
                                            clsE911Globals.arcPoint = (IPoint)pointCollection.get_Geometry(2);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            blnAllSegsSplit = false;
                            intCountSkipped = intCountSkipped + 1;
                        }

                        // make sure we have a point to split on... before we call the split line class
                        if (clsE911Globals.arcPoint != null)
                        {
                            // call agrc's split line tool/class to split the lines at point locations
                            // first ensure address ranges are not zero - b/c these can't be split
                            string strLF = clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("L_F_ADD")).ToString();
                            string strLT = clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("L_T_ADD")).ToString();
                            string strRF = clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("R_F_ADD")).ToString();
                            string strRT= clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("R_F_ADD")).ToString();
                            //string strSegLength = clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("Shape_Length")).ToString();
                            long lngSegLength = Convert.ToInt64(clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("Shape_Length")));
                            if (((strLF == "0" & strLT == "0") | (strRF == "0" & strRT == "0")) | lngSegLength <= intMinLength)
                            {
                                blnNoZeroRanges = false;                       
                            }
                            else
                            {
                                clsSplitLine.SplitLineAtIntersection(); 
                            }                            
                        }
                    }
                    //MessageBox.Show(intCount.ToString());
                }

                MessageBox.Show("Done spliting selected lines! Due to rules in place, the ones still selected did NOT get split.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // show message box if any of the segments did not get split
                if (blnAllSegsSplit == false)
                {
                    MessageBox.Show(intCountSkipped.ToString() + " of the selected " + intCountTotal.ToString() + " segments did not get split becuase no intersecting PSAP boundary was found.", "Not All Segments Spilt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (blnNoZeroRanges == false)
                {
                    MessageBox.Show("One or more of the selected segments either had an address range of zero or the length was too short... so those segments did not get split.", "Not All Segments Spilt", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
			}
			catch (Exception ex)
			{
			MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
			"Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
			"Error Location:" + Environment.NewLine + ex.StackTrace,
            "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
        }




        // make sure only numbers go into the textbox
        private void txtLengthMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }




        // reproject the fgdb to wgs for spillman tools
        private void btnReproject_Click(object sender, EventArgs e)
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // open catalog dialog for selecting fgdb to project data
                IGxDialog pGxDialog = new GxDialog();
                pGxDialog.AllowMultiSelect = false;
                pGxDialog.Title = "Select File Geodatabase to Reproject to WGS84";
                IGxObjectFilter pGxObjectFilter = new GxFilterFileGeodatabases(); // GxFilterFGDBFeatureClasses();
                pGxDialog.ObjectFilter = pGxObjectFilter;
                //pGxDialog.Name = "";  //clears out any text in the feature class name
                IEnumGxObject pEnumGxObject;

                //open dialog so user can select the feature class
                Boolean CancelBrowser; //cancel the dialog if button is clicked
                CancelBrowser = pGxDialog.DoModalOpen(0, out pEnumGxObject); //.DoModalSave(0); //opens the dialog to save data
                //if cancel was clicked, exit the method
                if (CancelBrowser == false)
                {
                    return;
                }

                //IGxObject pGxObject = pEnumGxObject.Next();

                string strFGDBLocation = pEnumGxObject.Next().FullName;

                // Reproject the data..........
                Geoprocessor gp = new Geoprocessor();
                //gp.SetEnvironmentValue("workspace", @"K:\AGRC Projects\E911_Editing\SaintGeorge\StGeorgeReproTest.gdb");
                gp.SetEnvironmentValue("workspace", strFGDBLocation);

                IGpEnumList files = gp.ListFeatureClasses("*", "", "");
                string file; // = files.Next();

                IGpEnumList dfiles = gp.ListDatasets("*", "");
                string dfile = dfiles.Next();

                // loop through all the feature classes in the workspace and reproject them
                while ((file = files.Next()) != "")
                {
                    //MessageBox.Show(file);

                    string f_in = file;
                    //string f_out = @"K:\AGRC Projects\E911_Editing\SaintGeorge\StGeorgeReproTest.gdb\" + file + "_WGS84";
                    string f_out = strFGDBLocation + @"\" + file + "_WGS84";
                    string sys_out = @"K:\AGRC Projects\E911_Editing\Coordinate Systems\WGS 1984.prj";
                    //string geo = "NAD_1983_To_WGS_1984_1";
                    ESRI.ArcGIS.DataManagementTools.Project project = new ESRI.ArcGIS.DataManagementTools.Project();
                    project.in_dataset = file;
                    project.out_coor_system = sys_out;
                    project.out_dataset = f_out;
                    //project.transform_method = geo;

                    IGeoProcessorResult gpresult = gp.Execute(project, null) as IGeoProcessorResult;

                    // check the gp message count to see if any failed
                    if (gpresult.MessageCount > 0)
                    {
                        //for (int Count = 0; Count <= gpresult.MessageCount - 1; Count++)
                        //{
                        //    Console.WriteLine(gpresult.GetMessage(Count));
                        //}
                    }
                }

                MessageBox.Show("Done spliting reprojecting feature classes!", "Done Reprojecting!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        // this method is run when the user changes the selection on the combobox
        private void cboPSAPname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        // check how far the point is along the curve - before splitting - to make sure the split is not going to result in an error ("Split point results in a zero length polyline" returned from IFeatureEdit.Split())
        double getDistanceAlongPoint(IPoint point, IPolyline polyline)
        {
            var outPnt = new PointClass() as IPoint;
            double distAlong = double.NaN;
            double distFrom = double.NaN;
            bool bRight = false;
            polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, point, true, outPnt, ref distAlong, ref distFrom, ref bRight);

            return distAlong;
        }

        private void txtLengthMin_TextChanged(object sender, EventArgs e)
        {

        }


        // make sure the user can only imput numbers for a buffer unit
        private void txtBuffer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

    }
}
