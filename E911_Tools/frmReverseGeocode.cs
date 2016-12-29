using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E911_Tools
{
    public partial class frmReverseGeocode : Form
    {
        //IWorkspace workspaceSGID;
        //IFeatureWorkspace featureWorkspaceSGID;

        public frmReverseGeocode()
        {
            InitializeComponent();
        }

        // form load method
        private void frmReverseGeocode_Load(object sender, EventArgs e)
        {
            try
            {
                // get access to the current arcmap variables
                clsE911StaticClass.GetCurrentMapDocVariables();

                // load the choose layer combobox with the map's layer names
                for (int i = 0; i < clsE911Globals.pMap.LayerCount; i++)
                {
                    cboChooseLayer.Items.Add(clsE911Globals.pMap.Layer[i].Name.ToString());
                }

                // get access to sgid database
                //workspaceSGID = clsE911StaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:sgid.agrc.utah.gov", "SGID10", "DBMS", "sde.DEFAULT", "agrc", "agrc");
                //featureWorkspaceSGID = (IFeatureWorkspace)workspaceSGID;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 Tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        // reverse geocode button
        private void btnReverseGeocode_Click(object sender, EventArgs e)
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;


                // get access to the layer that is specified in the choose layer dropdown box
                IGeoFeatureLayer pGFlayer = null;
                IFeatureLayer arcFeatLayer = null;
                // loop through the map's layers and check for the layer with the targeted name (based on the choose layer combobox)
                for (int i = 0; i < clsE911Globals.pMap.LayerCount; i++)
                {
                    if (clsE911Globals.pMap.Layer[i].Name == cboChooseLayer.Text)
                    {
                        pGFlayer = (IGeoFeatureLayer)clsE911Globals.pMap.Layer[i];
                        arcFeatLayer = (IFeatureLayer)clsE911Globals.pMap.Layer[i];
                    }
                }

                // check to assure the user chose a point layer
                if (arcFeatLayer.FeatureClass.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                {
                    MessageBox.Show("You must choose a Point Layer to Reverse Geocode.", "Must Be Point Layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

  
                // Get a locator from the locator Workspace
                // Set up the SDE connection properties 
                IPropertySet connectionProperties = new PropertySetClass();
                connectionProperties.SetProperty("SERVER", "");
                //propertySet.SetProperty("DBCLIENT", dbclient);
                connectionProperties.SetProperty("INSTANCE", "sde:sqlserver:sgid.agrc.utah.gov");
                connectionProperties.SetProperty("DATABASE", "SGID10");
                connectionProperties.SetProperty("AUTHENTICATION_MODE", "DBMS");
                connectionProperties.SetProperty("VERSION", "sde.DEFAULT");
                connectionProperties.SetProperty("USER", "agrc");
                connectionProperties.SetProperty("PASSWORD", "agrc");

                // Get the Workspace
                System.Object obj = Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory"));
                IWorkspaceFactory2 workspaceFactory = obj as IWorkspaceFactory2;
                IWorkspace workspace = workspaceFactory.Open(connectionProperties, 0);

                obj = Activator.CreateInstance(Type.GetTypeFromProgID("esriLocation.LocatorManager"));
                ILocatorManager2 locatorManager = obj as ILocatorManager2;
                ILocatorWorkspace locatorWorkspace = locatorManager.GetLocatorWorkspace(workspace);
                //IDatabaseLocatorWorkspace databaseLocatorWorkspace = (IDatabaseLocatorWorkspace)locatorWorkspace;

                IReverseGeocoding reverseGeocoding = (IReverseGeocoding)locatorWorkspace.GetLocator("TRANSPORTATION.Locator_AddressPtsAddrSys");

                // Set the search tolerance for reverse geocoding
                IReverseGeocodingProperties reverseGeocodingProperties = (IReverseGeocodingProperties)reverseGeocoding;
                reverseGeocodingProperties.SearchDistance = 1000;
                reverseGeocodingProperties.SearchDistanceUnits = esriUnits.esriFeet;

                

                // now loop through the point feature layer
                IFeatureCursor arcFeatCur = arcFeatLayer.Search(null, false);
                IFeature arcFeature;
                
                while ((arcFeature = arcFeatCur.NextFeature()) != null)
                {

                    IPoint arcPoint = (IPoint)arcFeature.Shape;

                    // Create a Point at which to find the address
                    IAddressGeocoding addressGeocoding = (IAddressGeocoding)reverseGeocoding;
                    IFields matchFields = addressGeocoding.MatchFields;
                    IField shapeField = matchFields.get_Field(matchFields.FindField("Shape"));
                    IPoint point = new PointClass();
                    point.SpatialReference = shapeField.GeometryDef.SpatialReference;
                    point.X = arcPoint.X;
                    point.Y = arcPoint.Y;


                    // Find the address nearest the Point
                    IPropertySet addressProperties = reverseGeocoding.ReverseGeocode(point, false);

                    // Print the address properties
                    IAddressInputs addressInputs = (IAddressInputs)reverseGeocoding;
                    IFields addressFields = addressInputs.AddressFields;
                    for (int i = 0; i < addressFields.FieldCount; i++)
                    {
                        IField addressField = addressFields.get_Field(i);
                        MessageBox.Show(addressField.AliasName + ": " + addressProperties.GetProperty(addressField.Name));
                    }




                }







            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 Tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }
}
