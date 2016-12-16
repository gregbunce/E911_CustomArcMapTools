using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E911_Tools
{
    public static class clsE911StaticClass
    {


        //connect to sde - method
        #region "Connect to SDE"
        public static ESRI.ArcGIS.Geodatabase.IWorkspace ConnectToTransactionalVersion(String server, String instance, String database, String authenication, String version)
        {
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("SERVER", server);
            //propertySet.SetProperty("DBCLIENT", dbclient);
            propertySet.SetProperty("INSTANCE", instance);
            propertySet.SetProperty("DATABASE", database);
            propertySet.SetProperty("AUTHENTICATION_MODE", authenication);
            propertySet.SetProperty("VERSION", version);

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.Open(propertySet, 0);
        }
        #endregion

        //connect to sde - method (this method has the same name so we can use method overloading)
        #region "Connect to SDE"
        public static ESRI.ArcGIS.Geodatabase.IWorkspace ConnectToTransactionalVersion(String server, String instance, String database, String authenication, String version, String username, String pass)
        {
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("SERVER", server);
            //propertySet.SetProperty("DBCLIENT", dbclient);
            propertySet.SetProperty("INSTANCE", instance);
            propertySet.SetProperty("DATABASE", database);
            propertySet.SetProperty("AUTHENTICATION_MODE", authenication);
            propertySet.SetProperty("VERSION", version);
            propertySet.SetProperty("USER", username);
            propertySet.SetProperty("PASSWORD", pass);

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.Open(propertySet, 0);
        }
        #endregion




        // reformat the string for ramp/exit features for TOC
        public static string ReformatStringForExitFeatures(String strRampField)
        {
            try
            {
                string strAssembled = "";
                strRampField = strRampField.Replace("  ", " ");
                // check for the word "HIGHWAY" and replace it with "SR-"
                strRampField = strRampField.Replace("-", "");
                strRampField = strRampField.Replace("ON", "ONR");
                strRampField = strRampField.Replace("OFF", "OFR");

                // check if string contains the letter "X" next to a number
                if (Regex.IsMatch(strRampField, @" X\d"))
                {
                    strRampField = strRampField.Replace(" X", " ");
                }

                // shift everthing after the last ONR or OFR and move it to the  
                if (strRampField.Contains("ONR") | strRampField.Contains("OFR"))
                {
                    if (strRampField.Contains("ONR"))
                    {
                        string strRoadName = strRampField.Substring(strRampField.LastIndexOf("ONR") + 3);
                        strRoadName = strRoadName.Trim();

                        // now remove those road name from the salias2 string
                        strRampField = strRampField.Remove(strRampField.IndexOf("ONR") + 3);

                        // insert the road name into an array of string 
                        string[] arrRampField = strRampField.Split(' ');

                        // loop through each word in the 
                        for (int i = 0; i < arrRampField.Length; i++)
                        {
                            // create new assembled string
                            if (i == 2)
                            {
                                strAssembled = strAssembled + strRoadName + " " + arrRampField[i] + " ";
                            }
                            else
                            {
                                strAssembled = strAssembled + arrRampField[i] + " ";
                            }
                        }
                    }
                    if (strRampField.Contains("OFR"))
                    {
                        string strRoadName = strRampField.Substring(strRampField.LastIndexOf("OFR") + 3);
                        strRoadName = strRoadName.Trim();

                        // now remove those road name from the salias2 string
                        strRampField = strRampField.Remove(strRampField.IndexOf("OFR") + 3);

                        // insert the road name into an array of string 
                        string[] arrRampField = strRampField.Split(' ');

                        // loop through each word in the 
                        for (int i = 0; i < arrRampField.Length; i++)
                        {
                            // create new assembled string
                            if (i == 2)
                            {
                                strAssembled = strAssembled + strRoadName + " " + arrRampField[i] + " ";
                            }
                            else
                            {
                                strAssembled = strAssembled + arrRampField[i] + " ";
                            }
                        }
                    }
                }

                strRampField = strAssembled.Trim();

                return strRampField;
            }
            catch (Exception ex)
            {
                
                //MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                //"Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                //"Error Location:" + Environment.NewLine + ex.StackTrace,
                //"UTRANS Editor tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return "error";
            }
        
        }

    }
}
