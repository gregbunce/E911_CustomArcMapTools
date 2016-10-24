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

namespace E911_Tools
{
    public partial class frmEtlRoads : Form
    {
        // variables with class scope
        IFeatureClass pUtransFeatureClass; 



        public frmEtlRoads()
        {
            InitializeComponent();
        }


        private void GetRoadsData_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "UTRANS Editor tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                "UTRANS Editor tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        // this method is run when the user clicks the form's button
        private void btnETLtoPSAP_Click(object sender, EventArgs e)
        {
            try
            {
                //////////// open arcCatalog dialog box so user can select the feature class that contains the psap's schema and unique, non-utrans segments
                //////////IGxDialog pGxDialog = new GxDialog();
                //////////pGxDialog.AllowMultiSelect = false;
                //////////pGxDialog.Title = "Select Feature Class for PSAP Schema";
                //////////IGxObjectFilter pGxObjectFilter = new GxFilterFGDBFeatureClasses();
                //////////pGxDialog.ObjectFilter = pGxObjectFilter;
                ////////////pGxDialog.Name = "";  //clears out any text in the feature class name

                //////////IEnumGxObject pEnumGxObject;

                ////////////open dialog so user can select the feature class
                //////////Boolean CancelBrowser; //cancel the dialog if button is clicked
                //////////CancelBrowser = pGxDialog.DoModalOpen(0, out pEnumGxObject); //.DoModalSave(0); //opens the dialog to save data

                ////////////if cancel was clicked, exit the method
                //////////if (CancelBrowser == false)
                //////////{
                //////////    return;
                //////////}

                ////////////MessageBox.Show(pEnumGxObject.Next().FullName);




                //// get access to workspace based on user selected feature class
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
                "Custom E911 Tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }



        // this method inserts new rows in the newly-created feature class
        private void insertNewFeaturesFromUtrans()
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;


                // set up the progress bar on the form to show progress
                pBar.Visible = true;
                lblProgressBar.Visible = true;
                pBar.Minimum = 1;
                pBar.Value = 1;
                pBar.Step = 1;



                //get access to the newly-created feature class with psap's schema
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IFeatureWorkspace arcFeatWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb", 0);
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
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString());
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")).ToString());
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")).ToString());
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")).ToString());
                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")).ToString());




                    // store the new row/feature
                    arcFeatureNewSchemaFeat.Store();

                    // preform the increment of the progress bar
                    pBar.PerformStep();
                }


                pBar.Visible = false;
                lblProgressBar.Visible = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "UTRANS Editor tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        strCountyList = "COFIPS = 49053 and addr_sys = 'IVINS' and cartocode = 8";
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
                "UTRANS Editor tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }




    }
}
