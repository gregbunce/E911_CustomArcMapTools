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

namespace E911_Tools
{
    public partial class frmEtlRoads : Form
    {
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
                //// open arcCatalog dialog box so user can select the feature class that contains the psap's schema and unique, non-utrans segments
                //IGxDialog pGxDialog = new GxDialog();
                //pGxDialog.AllowMultiSelect = false;
                //pGxDialog.Title = "Select Feature Class for PSAP Schema";
                //IGxObjectFilter pGxObjectFilter = new GxFilterFGDBFeatureClasses();
                //pGxDialog.ObjectFilter = pGxObjectFilter;
                ////pGxDialog.Name = "";  //clears out any text in the feature class name

                //IEnumGxObject pEnumGxObject;

                ////open dialog so user can select the feature class
                //Boolean CancelBrowser; //cancel the dialog if button is clicked
                //CancelBrowser = pGxDialog.DoModalOpen(0, out pEnumGxObject); //.DoModalSave(0); //opens the dialog to save data

                ////if cancel was clicked, exit the method
                //if (CancelBrowser == false)
                //{
                //    return;
                //}

                ////MessageBox.Show(pEnumGxObject.Next().FullName);




                // get access to workspace based on user selected feature class
                // Create workspace name objects.
                IWorkspaceName sourceWorkspaceName = new WorkspaceNameClass();
                IWorkspaceName targetWorkspaceName = new WorkspaceNameClass();
                IName targetName = (IName)targetWorkspaceName;

                // get the source feature class workspace from the form's dropdown menu
                if (cboPSAPname.Text == "StGeorge")
                {
                    // set the source workspace and feature class (this should eventually be in an enterprise database) - this is the fc for e911 roads editing, the odd segments we don't want in utrans
                    sourceWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_Editing.gdb";
                    sourceWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";

                    // set the target workspace for the extracted and merged roads data
                    targetWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb";
                    targetWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                }
                else if (cboPSAPname.Text == "TOC")
                {
                    // set the source workspace and feature class (this should eventually be in an enterprise database) - this is the fc for e911 roads editing, the odd segments we don't want in utrans
                    //sourceWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_Editing.gdb\StGeorge_Dispatch_Streets";
                    //sourceWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";

                    //// set the target workspace for the extracted and merged roads data
                    //targetWorkspaceName.PathName = @"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb";
                    //targetWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                }

                
                // Create a name object for the source feature class.
                IFeatureClassName featureClassNameSource = new FeatureClassNameClass();

                // Set the featureClassName properties.
                IDatasetName sourceDatasetName = (IDatasetName)featureClassNameSource;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = "StGeorge_Dispatch_Streets";
                //sourceDatasetName.Name = cboPSAPname.Text.ToString() + "_ETL_" + DateTime.Now.ToString("yyyyMMdd");
                IName sourceName=(IName)sourceDatasetName;

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
                IWorkspaceFactory arcWorkspaceFactory = new ESRI.ArcGIS.DataSourcesGDB.FileGDBWorkspaceFactory();
                IFeatureWorkspace arcFeatWorkspace = (IFeatureWorkspace)arcWorkspaceFactory.OpenFromFile(@"K:\AGRC Projects\E911_Editing\SaintGeorge\SaintGeorgePSAP_ETL.gdb",0);
                IFeatureClass arcFeatClass_ETL = arcFeatWorkspace.OpenFeatureClass("StGeorge_Dispatch_Streets");
                
                IDataset targetDataset = (IDataset)arcFeatClass_ETL;
                targetDataset.Rename(cboPSAPname.Text.ToString() + "_ETL_" + DateTime.Now.ToString("yyyyMMdd"));



                // get access to utrans database and the centerline feature class








            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Custom E911 Tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }




        }
    }
}
