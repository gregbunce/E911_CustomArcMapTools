using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

namespace E911_Tools
{
    public partial class frmEtlRoads : Form
    {
        // variables with class scope
        IFeatureClass pUtransFeatureClass;
        //IMap pMap;
        //IMxDocument pMxDocument;
        //IActiveView pActiveView;

        //get access to the newly-created feature class with psap's schema
        Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
        IWorkspaceFactory workspaceFactory; // = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
        IFeatureWorkspace arcFeatWorkspace; // = (IFeatureWorkspace)workspaceFactory.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb", 0);


        public frmEtlRoads()
        {
            InitializeComponent();
        }


        private void GetRoadsData_Load(object sender, EventArgs e)
        {
            try
            {
                // set the global application variable - from the button appliation hook
                clsE911Globals.arcApplication = btnEtlRoads.m_application;

                //get access to the document and the active view
                //pMxDocument = (IMxDocument)btnEtlRoads.m_application.Document;
                clsE911Globals.pMxDocument = (IMxDocument)clsE911Globals.arcApplication.Document;
                clsE911Globals.pMap = clsE911Globals.pMxDocument.FocusMap;
                clsE911Globals.pActiveView = clsE911Globals.pMxDocument.ActiveView;  //pActiveView = (IActiveView)pMap;

                // get access to the feature workspace
                workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                arcFeatWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb", 0);

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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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


        // this method is run when the user clicks the form's button
        private void btnETLtoPSAP_Click(object sender, EventArgs e)
        {
            try
            {
        
                //// Create workspace name objects.
                //IWorkspaceName sourceWorkspaceName = new WorkspaceNameClass();
                //IWorkspaceName targetWorkspaceName = new WorkspaceNameClass();
                //IName targetName = (IName)targetWorkspaceName;

                //// get the source feature class workspace from the form's dropdown menu
                //if (cboPSAPname.Text == "StGeorge")
                //{
                //    // set the source workspace and feature class (this should eventually be in an enterprise database) - this is the fc for e911 roads editing, the odd segments we don't want in utrans
                //    sourceWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_Editing.gdb";
                //    sourceWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";

                //    // set the target workspace for the extracted and merged roads data
                //    targetWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb";
                //    targetWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                //}
                //else if (cboPSAPname.Text == "TOC")
                //{
                //    // set the source workspace and feature class (this should eventually be in an enterprise database) - this is the fc for e911 roads editing, the odd segments we don't want in utrans
                //    //sourceWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_Editing.gdb\StGeorge_Dispatch_Streets";
                //    //sourceWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";

                //    //// set the target workspace for the extracted and merged roads data
                //    //targetWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb";
                //    //targetWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                //}

                
                //// Create a name object for the source feature class.
                //IFeatureClassName featureClassNameSource = new FeatureClassNameClass();

                //// Set the featureClassName properties.
                //IDatasetName sourceDatasetName = (IDatasetName)featureClassNameSource;
                //sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                //sourceDatasetName.Name = "StGeorge_Dispatch_Streets";
                ////sourceDatasetName.Name = cboPSAPname.Text.ToString() + "_ETL_" + DateTime.Now.ToString("yyyyMMdd");
                //IName sourceName=(IName)sourceDatasetName;

                //// Create an enumerator for source datasets.
                //IEnumName sourceEnumName = new NamesEnumeratorClass();
                //IEnumNameEdit sourceEnumNameEdit = (IEnumNameEdit)sourceEnumName;

                //// Add the name object for the source class to the enumerator.
                //sourceEnumNameEdit.Add(sourceName);

                //// Create a GeoDBDataTransfer object and a null name mapping enumerator.
                //IGeoDBDataTransfer geoDBDataTransfer = new GeoDBDataTransferClass();
                //IEnumNameMapping enumNameMapping = null;

                //// Use the data transfer object to create a name mapping enumerator.
                //Boolean conflictsFound = geoDBDataTransfer.GenerateNameMapping(sourceEnumName, targetName, out enumNameMapping);
                //enumNameMapping.Reset();

                //// Check for conflicts.
                //if (conflictsFound)
                //{
                //    // Iterate through each name mapping.
                //    INameMapping nameMapping = null;
                //    while ((nameMapping = enumNameMapping.Next()) != null)
                //    {
                //        // Resolve the mapping's conflict (if there is one).
                //        if (nameMapping.NameConflicts)
                //        {
                //            nameMapping.TargetName = nameMapping.GetSuggestedName(targetName);
                //        }

                //        // See if the mapping's children have conflicts.
                //        IEnumNameMapping childEnumNameMapping = nameMapping.Children;
                //        if (childEnumNameMapping != null)
                //        {
                //            childEnumNameMapping.Reset();

                //            // Iterate through each child mapping.
                //            INameMapping childNameMapping = null;
                //            while ((childNameMapping = childEnumNameMapping.Next()) != null)
                //            {
                //                if (childNameMapping.NameConflicts)
                //                {
                //                    childNameMapping.TargetName = childNameMapping.GetSuggestedName
                //                        (targetName);
                //                }
                //            }
                //        }
                //    }
                //}

                //// Start the transfer.
                //geoDBDataTransfer.Transfer(enumNameMapping, targetName);



                //// rename the newly copied feature class
                //IWorkspaceFactory arcWorkspaceFactory = new ESRI.ArcGIS.DataSourcesGDB.FileGDBWorkspaceFactory();
                //IFeatureWorkspace arcFeatWorkspace = (IFeatureWorkspace)arcWorkspaceFactory.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb",0);
                //IFeatureClass arcFeatClass_ETL = arcFeatWorkspace.OpenFeatureClass("StGeorge_Dispatch_Streets");
                
                //IDataset targetDataset = (IDataset)arcFeatClass_ETL;
                //targetDataset.Rename(cboPSAPname.Text.ToString() + "_ETL_" + DateTime.Now.ToString("yyyyMMdd"));



                // insert rows from utrans
                insertNewFeaturesFromUtrans();


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
                IFeatureClass arcFeatClassNewSchemaFeat = arcFeatWorkspace.OpenFeatureClass("StGeorge_ETL_20161021");
                
                // get access to utrans database and the centerline feature class
                //connect to sde
                IWorkspace workspace = clsE911StaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:utrans.agrc.utah.gov", "UTRANS", "OSA", "sde.DEFAULT");
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;

                // get access to utrans roads as feature class
                pUtransFeatureClass = featureWorkspace.OpenFeatureClass("UTRANS.TRANSADMIN.StatewideStreets");

                // get feature count for progress bar
                //int intFeatureCount = pUtransFeatureClass.FeatureCount();

                // create a feature cursor to get that psap's related county roads
                var tupleFeatCurInt = getFeatureCursurForPSAP(cboPSAPname.Text, pUtransFeatureClass);
                IFeatureCursor arcFeatCurRoadsUtrans = tupleFeatCurInt.Item1;
                int intFeatureCount = tupleFeatCurInt.Item2;

                pBar.Maximum = intFeatureCount;

                IFeature arcFeatureUtrans;

                // loop through this psap's utrans road segments and load them into the newly-created filegeodatabase feature class (psap schema)
                while ((arcFeatureUtrans = arcFeatCurRoadsUtrans.NextFeature()) != null)
                {
                    // create a new row in the newly-created feature class
                    IFeature arcFeatureNewSchemaFeat = arcFeatClassNewSchemaFeat.CreateFeature();

                    // add the shape/geometry from utrans into the psap's schema feature class
                    arcFeatureNewSchemaFeat.Shape = arcFeatureUtrans.Shape;

                    // add field values to the newly-created feature class
                    string strSTREETNAME = "";
                    strSTREETNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();
                    if (strSTREETNAME != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim());
                    }
                    else
                    {
                        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), DBNull.Value);
                    }


                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")));
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), 0);
                    }

                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")));                       
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), 0);
                    }

                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")));
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), 0);
                    }

                    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")) != DBNull.Value)
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")));
                    }
                    else
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), 0);
                    }
                    
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("CARTOCODE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("CARTOCODE")).ToString().Trim());

                    string strPREDIR = "";
                    strPREDIR = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim();
                    if (strPREDIR != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim());
                    }
                    else
                    {
                        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"),  DBNull.Value);
                    }

                    string strSTREETTYPE = "";
                    strSTREETTYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim();
                    if (strSTREETTYPE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETTYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim()); 
                    }
                    else
                    {
                        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETTYPE"), DBNull.Value);
                    }

                    string strSUFDIR = "";
                    strSUFDIR = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                    if (strSUFDIR != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SUFDIR"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim());             
                    }
                    else
                    {
                        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SUFDIR"), DBNull.Value);
                    }

                    string strALIAS1 = "";
                    strALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                    if (strALIAS1 != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString());
                    }

                    string strALIAS1TYPE = "";
                    strALIAS1TYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim();
                    if (strALIAS1TYPE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1TYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim());
                    }

                    string strALIAS2 = "";
                    strALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                    if (strALIAS2 != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim());  
                    }

                    string strALIAS2TYPE = "";
                    strALIAS2TYPE =arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim();
                    if (strALIAS2TYPE != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2TYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim());
                    }

                    string strACSALIAS = "";
                    strACSALIAS =arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSALIAS")).ToString().Trim();
                    if (strACSALIAS != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSALIAS"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSALIAS")).ToString().Trim());
                    }

                    string strACSNAME = "";
                    strACSNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim();
                    if (strACSNAME != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim());
                    }
                    
                    string strACSSUF = "";
                    strACSSUF = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim();
                    if (strACSSUF != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSSUF"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim());
                    }

                    string strZIPLEFT = "";
                    strZIPLEFT = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPLEFT")).ToString().Trim();
                    if (strZIPLEFT != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ZIPLEFT"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPLEFT")).ToString().Trim()); 
                    }

                    string strZIPRIGHT = "";
                    strZIPRIGHT = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPRIGHT")).ToString().Trim();
                    if (strZIPRIGHT != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ZIPRIGHT"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPRIGHT")).ToString().Trim());
                    }

                    string strCOFIPS = "";
                    strCOFIPS = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("COFIPS")).ToString().Trim();
                    if (strCOFIPS != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("COFIPS"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("COFIPS")).ToString().Trim());
                    }

                    string strHWYNAME = "";
                    strHWYNAME =arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("HWYNAME")).ToString().Trim();
                    if (strHWYNAME != "")
                    {
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("HWYNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("HWYNAME")).ToString().Trim());
                    }

                    // set up the concatination for the STREET field
                    string strSTREET = "";
                    int intNumber;
                    bool blnIsNumeric = int.TryParse(strSTREETNAME, out intNumber);
                    //string strStreetName = "";
                    //strStreetName = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();
                    //string strSTREETTYPE = "";
                    //strSTREETTYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim();

                    // check if streetname is numeric and if it has a streetype of cir if so format it with the circle after the sufdir
                    if (strSTREETTYPE.ToUpper() == "CIR" & blnIsNumeric)
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
                    // check for the word "HIGHWAY" and replace it with "SR-"
                    strSTREET = strSTREET.Replace("HIGHWAY ", "SR-");
                    strSTREET = strSTREET.Replace("OLD SR-", "OLD HWY ");
                    // trim the whole street concatination
                    strSTREET = strSTREET.Trim();
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREET"), strSTREET);


                    // populate the location field and salias4 - the value in this field is placed after a colon on the calltakers screen to show them the numberic location of the call
                    string strAcsName = ""; 
                    strAcsName = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim();
                    if (strAcsName != "")
                    {
                        string strLOCATION = "";
                        strLOCATION = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim();
                        strLOCATION = strLOCATION.Replace("  ", " ");
                        strLOCATION = strLOCATION.Trim();
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("LOCATION"), strLOCATION);
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS4"), strLOCATION);                             
                    }

                    // populate salias1 and salias2 fields
                    //string strAlias1 = "";
                    //strAlias1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                    if (strALIAS1 != "")
                    {
                        string strSALIAS1 = "";
                        strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim() + " " +
                            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim();
                        strSALIAS1 = strSALIAS1.Replace("  ", " ");
                        // check for the word "HIGHWAY" and replace it with "SR-"
                        strSALIAS1 = strSALIAS1.Replace("HIGHWAY ", "SR-");
                        strSTREET = strSTREET.Replace("OLD SR-", "OLD HWY ");
                        strSALIAS1 = strSALIAS1.Trim();
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1);
                    }
                    //string strAlias2 = "";
                    //strAlias2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                    if (strALIAS2 != "")
                    {
                        string strSALIAS2 = "";
                        strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim() + " " +
                             arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim();
                        strSALIAS2 = strSALIAS2.Replace("  ", " ");
                        // check for the word "HIGHWAY" and replace it with "SR-"
                        strSALIAS2 = strSALIAS2.Replace("HIGHWAY ", "SR-");
                        strSTREET = strSTREET.Replace("OLD SR-", "OLD HWY ");
                        strSALIAS2 = strSALIAS2.Trim();
                        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2);
                    }

                    // populate the salias3 field of street name or the alias1 or alias2 fields contain HIGHWAY... then replace with HWY
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

                    // store the new row/feature
                    arcFeatureNewSchemaFeat.Store();

                    // preform the increment of the progress bar
                    pBar.PerformStep();
                }


                //this.Close();
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

                // get list of counties for this psap
                switch (strPSAPName)
                {
                    case "StGeorge":
                        strCountyList = "COFIPS = 49053 AND CARTOCODE <> 99";
                        //strCountyList = "COFIPS = '49053' AND STREETNAME IS NOT NULL AND (( L_F_ADD IS NOT NULL AND L_T_ADD IS NOT NULL AND R_F_ADD IS NOT NULL AND R_T_ADD IS NOT NULL) AND (L_F_ADD <> 0 AND L_T_ADD <> 0 AND R_F_ADD <> 0 AND R_T_ADD <> 0))";
                        break;
                    case "TOC":
                        strCountyList = "COFIPS in (,,,)";
                        break;
                }

                IQueryFilter arcQueryFilter = new QueryFilter();
                arcQueryFilter.WhereClause = strCountyList;

                // get the feature cursor
                IFeatureCursor arcFeatCur = arcFC.Search(arcQueryFilter, false);

                // get feature count
                int intFeatureCount = arcFC.FeatureCount(arcQueryFilter);

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



                //////// get access to the psap boundaries
                //////IFeatureClass arcFeatClassDispaceCityCD = arcFeatWorkspace.OpenFeatureClass("StGeorge_Dispatch_CITYCD");

                //////// loop through the maps layer and get the highlighted layer and make sure it's a polyline layer
                ////////get access to the document and the active view
                //////pMxDocument = (IMxDocument)btnEtlRoads.m_application.Document;
                //////pMap = pMxDocument.FocusMap;
                //////pActiveView = pMxDocument.ActiveView;  //pActiveView = (IActiveView)pMap;

                ////////make sure the user has selected a polyline layer
                //////if (pMxDocument.SelectedLayer == null)
                //////{
                //////    MessageBox.Show("Please select the roads layer in the TOC.", "Select Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //////    return;
                //////}
                //////if (!(pMxDocument.SelectedLayer is IFeatureLayer))
                //////{
                //////    MessageBox.Show("Please select polyline layer.", "Must be PolyLine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //////    return;
                //////}

                ////////cast the selected layer as a feature layer
                //////IGeoFeatureLayer pGFlayer = (IGeoFeatureLayer)pMxDocument.SelectedLayer;

                ////////check if the feaure layer is a polyline layer
                //////if (pGFlayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                //////{
                //////    MessageBox.Show("Please select a polyline layer.", "Must be Polyline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //////    return;
                //////}




                //////// select the segments that are intersecting the psap boundaries

                //////// the featureClass is the streets
                //////// the envelope is the polygon boundaries
                //////// http://help.arcgis.com/EN/sdk/10.0/ArcObjects_NET/componenthelp/index.html#/esriSpatialRelEnum_Constants/002500000086000000/

                //////// use spatial filter to get segments that intersect polygons
                //////// Create the spatial filter and set its spatial constraints.
                //////ISpatialFilter spatialFilter = new SpatialFilterClass();
                //////spatialFilter.Geometry = arcFeatClassDispaceCityCD as IGeometry;
                //////spatialFilter.GeometryField = pGFlayer.FeatureClass.ShapeFieldName;
                //////spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses; //.esriSpatialRelIntersects;

                //////// Set the attribute constraints and subfields.
                //////// We want to exclude ramps, highways and interstates.
                ////////spatialFilter.WhereClause = "NAME <> 'Ramp' AND PRE_TYPE NOT IN ('Hwy', 'I')";
                ////////spatialFilter.SubFields = "NAME, TYPE";

                //////// Execute the query.
                //////IFeatureCursor featureCursor = pGFlayer.FeatureClass.Search(spatialFilter, true);
                //////IFeature arcFeature;

                //////IFeatureSelection arcFeatSelect = (IFeatureSelection)pGFlayer;
                
                //////IQueryFilter arcQFilter = new QueryFilter();


                //////while ((arcFeature = featureCursor.NextFeature()) != null)
                //////{
                //////    arcQFilter.WhereClause = "";
                //////    arcQFilter.WhereClause = "OBJECTID = " + arcFeature.get_Value(arcFeature.Fields.FindField("OBJECTID"));
                //////    arcFeatSelect.SelectFeatures(arcQFilter, esriSelectionResultEnum.esriSelectionResultAdd, false);
                //////}


                //////clsE911Globals.pActiveView.Refresh();
                //////clsE911Globals.pActiveView.Refresh();




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }

        private void btnSplitSelectSeg_Click(object sender, EventArgs e)
        {
            try
			{
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

                // get access to the psap boundaries
                IFeatureClass arcFeatClassDispaceCityCD = arcFeatWorkspace.OpenFeatureClass("StGeorge_Dispatch_CITYCD");

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
                        spatialFilter.GeometryField = arcFeatClassDispaceCityCD.ShapeFieldName;
                        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        IFeature arcFeaturePSAPpoly;

                        // Execute the query.
                        using (ComReleaser comReleaser2 = new ComReleaser())
                        {
                            IFeatureCursor featureCursor = arcFeatClassDispaceCityCD.Search(spatialFilter, true);
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

                            //MessageBox.Show(pointCollection.GeometryCount.ToString());

                            clsE911Globals.arcPoint = (IPoint)pointCollection.get_Geometry(0);
                            //MessageBox.Show(arcPoint.X.ToString() + ", " + arcPoint.Y.ToString());     
                       
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
                // open catalog dialog for selecting fgdb to project data
                // open arcCatalog dialog box so user can select the feature class that contains the  psap's schema and unique, non-utrans segments
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
                MessageBox.Show(pEnumGxObject.Next().FullName);




                // reproject the data..........
                // http://support.esri.com/technical-article/000010852



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "UTRANS Editor tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




    }
}
