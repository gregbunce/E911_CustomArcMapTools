using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}
