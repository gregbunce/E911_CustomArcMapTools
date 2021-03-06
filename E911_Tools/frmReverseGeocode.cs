﻿using ESRI.ArcGIS.Carto;
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
        //IGeoFeatureLayer pGFlayer = null;
        IFeatureLayer arcFeatLayer = null;

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
                string strGeocoderName;
                string strGeocoderDateField;

                if (radioHWYNAME.Checked)
                {

                    strGeocoderName = "E911ADMIN.RevGeocoderTOC_HWYNAME";
                    strGeocoderDateField = "RevGeoHWYNAME_Date";
                }
                else if (radioSTREETNAME.Checked)
                {
                    strGeocoderName = "E911ADMIN.RevGeocoderTOC_STREETNAME";
                    strGeocoderDateField = "RevGeoSTREETNAME_Date";
                }
                else
                {
                    MessageBox.Show("Please select a Geocoder.", "Choose Geocoder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;


                // get access to the layer that is specified in the choose layer dropdown box
                //pGFlayer = null;
                arcFeatLayer = null;
                // loop through the map's layers and check for the layer with the targeted name (based on the choose layer combobox)
                for (int i = 0; i < clsE911Globals.pMap.LayerCount; i++)
                {
                    if (clsE911Globals.pMap.Layer[i].Name == cboChooseLayer.Text)
                    {
                        //pGFlayer = (IGeoFeatureLayer)clsE911Globals.pMap.Layer[i];
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
                //ILocatorWorkspace locatorWorkspace = clsE911StaticClass.GetFileGDBLocatorWorkspace(@"K:\AGRC Projects\E911_Editing\Locators\AddressLocatorData.gdb");
                //IReverseGeocoding reverseGeocoding = (IReverseGeocoding)locatorWorkspace.GetLocator("UtransHwys_AddrLocator_HWYNAME");

                ILocatorWorkspace locatorWorkspace = clsE911StaticClass.GetSDELocatorWorkspace("", "sde:sqlserver:e911.agrc.utah.gov", "E911", "OSA", "sde.DEFAULT");
                IReverseGeocoding reverseGeocoding = (IReverseGeocoding)locatorWorkspace.GetLocator(strGeocoderName);


                //ILocatorWorkspace locatorWorkspace = clsE911StaticClass.GetSDELocatorWorkspace("", "sde:sqlserver:sgid.agrc.utah.gov", "SGID10", "DBMS", "sde.DEFAULT", "agrc", "agrc");
                //IReverseGeocoding reverseGeocoding = (IReverseGeocoding)locatorWorkspace.GetLocator("TRANSPORTATION.Locator_RoadsAddrSys_COMPOSITE");

                // Set the search tolerance for reverse geocoding
                IReverseGeocodingProperties reverseGeocodingProperties = (IReverseGeocodingProperties)reverseGeocoding;
                //reverseGeocodingProperties.SearchDistance = 3048;
                reverseGeocodingProperties.SearchDistance = Convert.ToDouble(txtSearchRadius.Text.Trim());
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

                    // get the "Street or Intersection" field that was returned from the reverse geocoder (three fields are returned, this is the first.. the others are place and zip)
                    IField addressField = addressFields.get_Field(0);

                    string strReverseGeocodeAddress = string.Empty;
                    strReverseGeocodeAddress = addressProperties.GetProperty(addressField.Name).ToString().Trim();




                    // check if the address is not null and then get the first word which should be the house number
                    if (strReverseGeocodeAddress != "")
                    {
                        // assign the address field the returned value
                        arcFeature.set_Value(arcFeature.Fields.FindField(cboAddressField.Text), strReverseGeocodeAddress);


                        // parse the address into an array
                        string[] arrReverseGeocodeAddress = strReverseGeocodeAddress.Split(' ');

                        // get the first item of of the array
                        string strHseNum = arrReverseGeocodeAddress[0];

                        // get the last item in the array
                        string strHwyName = arrReverseGeocodeAddress[arrReverseGeocodeAddress.Length - 1];

                        // get all items in the arrry except first
                        string strResultMinusNumber = "";
                        if (radioHWYNAME.Checked)
                        {
                            for (int i = 0; i < arrReverseGeocodeAddress.Length; i++)
			                {
			                    if (i != 0)
	                            {
                                    // check if value is N, S, E, or W - if so don't add them to the string
                                    if (arrReverseGeocodeAddress[i].ToString() == "N" | arrReverseGeocodeAddress[i].ToString() == "S" | arrReverseGeocodeAddress[i].ToString() == "E" | arrReverseGeocodeAddress[i].ToString() == "W")
                                    {
                                        // don't add the directionals
                                    }
                                    else
                                    {
                                        strResultMinusNumber = strResultMinusNumber + arrReverseGeocodeAddress[i].ToString();
                                    }
	                            }
			                }                            
                        }
                        if (radioSTREETNAME.Checked)
                        {
                            for (int i = 0; i < arrReverseGeocodeAddress.Length; i++)
                            {
                                if (i != 0)
                                {
                                    strResultMinusNumber = strResultMinusNumber + " " + arrReverseGeocodeAddress[i].ToString();
                                }
                            }  
                        }
                        strResultMinusNumber.TrimStart(' ');
                        strResultMinusNumber.TrimEnd(' ');
                        strResultMinusNumber.Trim();
                        

                        // check if the strHseNum is an int before we write it to the feature class address number field
                        int intHseNum;
                        bool isNumeric = int.TryParse(strHseNum, out intHseNum);

                        ////int intHwyName;
                        ////bool isHwyNameNumeric = int.TryParse(strHwyName, out intHwyName);

                        // if the result has an address
                        if (isNumeric)
                        {
                            arcFeature.set_Value(arcFeature.Fields.FindField(cboChooseAddressNumber.Text), intHseNum);
                        }
                        else
                        {
                            arcFeature.set_Value(arcFeature.Fields.FindField(cboChooseAddressNumber.Text), "Verify RevGeo Result");
                        }

                        // if the result has a highway name
                        ////if (isHwyNameNumeric)
                        ////{
                        ////    arcFeature.set_Value(arcFeature.Fields.FindField(cboChooseHwyName.Text), intHwyName);
                        ////}
                        ////else
                        ////{
                        ////    arcFeature.set_Value(arcFeature.Fields.FindField(cboChooseHwyName.Text), "Verify RevGeo Result");
                        ////}

                        // populate the street/hwy name field
                        if (strResultMinusNumber != "")
                        {
                            arcFeature.set_Value(arcFeature.Fields.FindField(cboChooseHwyName.Text), strResultMinusNumber);
                        }
                        
                    }
                    else
                    {
                        arcFeature.set_Value(arcFeature.Fields.FindField(cboAddressField.Text), "NotFound");
                    }
                    
                    //for (int i = 0; i < addressFields.FieldCount; i++)
                    //{
                    //    IField addressField = addressFields.get_Field(i);

                    //    //arcFeature.set_Value(arcFeature.Fields.FindField(cboAddressField.Text), addressProperties.GetProperty(addressField.Name));

                    //    MessageBox.Show(addressField.AliasName + ": " + addressProperties.GetProperty(addressField.Name));
                    //}

                    // update the run date field
                    arcFeature.set_Value(arcFeature.Fields.FindField(strGeocoderDateField), DateTime.Now);

                    arcFeature.Store();

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

        private void txtSearchRadius_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }




        // the user changed or choose a new layer in the layer drop down menu
        private void cboChooseLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
			{
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // clear out the old field names
                cboAddressField.Items.Clear();
                cboChooseAddressNumber.Items.Clear();
                cboChooseHwyName.Items.Clear();


                for (int i = 0; i < clsE911Globals.pMap.LayerCount; i++)
                {
                    if (clsE911Globals.pMap.Layer[i].Name == cboChooseLayer.Text)
                    {
                        //pGFlayer = (IGeoFeatureLayer)clsE911Globals.pMap.Layer[i];
                        arcFeatLayer = (IFeatureLayer)clsE911Globals.pMap.Layer[i];
                    }
                }

                //update the comboboxes with the currently selected layer's field names
                for (int i = 0; i < arcFeatLayer.FeatureClass.Fields.FieldCount; i++)
                {

                    cboAddressField.Items.Add(arcFeatLayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboChooseAddressNumber.Items.Add(arcFeatLayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboChooseHwyName.Items.Add(arcFeatLayer.FeatureClass.Fields.Field[i].Name.ToString());
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


        // ensure only numbers in the search radius text box
        private void txtSearchRadius_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }



        // spatially assign the city codes from the dispatch center's city sde layer
        private void btnAssignCityCD_Click(object sender, EventArgs e)
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // get access to the layer that is specified in the choose layer dropdown box
                //pGFlayer = null;
                arcFeatLayer = null;
                // loop through the map's layers and check for the layer with the targeted name (based on the choose layer combobox)
                for (int i = 0; i < clsE911Globals.pMap.LayerCount; i++)
                {
                    if (clsE911Globals.pMap.Layer[i].Name == cboChooseLayer.Text)
                    {
                        //pGFlayer = (IGeoFeatureLayer)clsE911Globals.pMap.Layer[i];
                        arcFeatLayer = (IFeatureLayer)clsE911Globals.pMap.Layer[i];
                    }
                }

                // check to assure the user chose a point layer
                if (arcFeatLayer.FeatureClass.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                {
                    MessageBox.Show("You must choose a Point Layer to Reverse Geocode.", "Must Be Point Layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (clsE911Globals.arcFeatClass_CityCD == null)
                {
                    // get access to the e911 database  - connect to sde
                    IWorkspace workspaceE911 = clsE911StaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:e911.agrc.utah.gov", "E911", "OSA", "sde.DEFAULT");
                    IFeatureWorkspace featureWorkspaceE911 = (IFeatureWorkspace)workspaceE911;

                    clsE911Globals.arcFeatClass_CityCD = featureWorkspaceE911.OpenFeatureClass("E911.E911ADMIN.TOC_CITYCD");
                }


                // now loop through the map's point feature layer and intersect point with city boundary to get city code
                IFeatureCursor arcFeatCur = arcFeatLayer.Search(null, false);
                IFeature arcFeature;

                while ((arcFeature = arcFeatCur.NextFeature()) != null)
                {

                    IPoint arcPoint = (IPoint)arcFeature.Shape;

                    // do the intersect
                    ISpatialFilter arcSpatialFilter = new SpatialFilter();
                    arcSpatialFilter.Geometry = arcPoint;
                    arcSpatialFilter.GeometryField = "SHAPE";
                    arcSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    arcSpatialFilter.SubFields = "*";

                    IFeatureCursor arcFeatCurIntersect = clsE911Globals.arcFeatClass_CityCD.Search(arcSpatialFilter, false);
                    IFeature arcFeatureIntersected = arcFeatCurIntersect.NextFeature();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(arcFeatCurIntersect);

                    string strIntersectedCityCD = "";
                    if (arcFeatureIntersected != null)
                    {
                        strIntersectedCityCD = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("CITYCD")).ToString().Trim();
                    }
                    else
                    {
                        strIntersectedCityCD = "";
                    }
                    
                    // populate the table with the city code
                    arcFeature.set_Value(arcFeature.Fields.FindField("CITYCD"), strIntersectedCityCD);
                    arcFeature.set_Value(arcFeature.Fields.FindField("CITYCD_Date"), DateTime.Now);
                    arcFeature.Store();
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
