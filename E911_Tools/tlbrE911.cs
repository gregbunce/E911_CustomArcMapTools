using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace E911_Tools
{
    /// <summary>
    /// Summary description for tlbrE911.
    /// </summary>
    [Guid("beb47a34-126d-4da7-89c5-b4e1f8554213")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("E911_Tools.tlbrE911")]
    public sealed class tlbrE911 : BaseToolbar
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public tlbrE911()
        {
            //
            // TODO: Define your toolbar here by adding items
            //
            //AddItem("esriArcMapUI.ZoomInTool");
            //BeginGroup(); //Separator
            //AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            //AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command
            AddItem("{b2410654-129b-45c8-9be2-50c9fabba090}"); // etl roads data from utrans
            AddItem("{04430d22-6276-4b65-abd7-63eb36a13921}");  // elt address points
            AddItem("{14a41c91-a3ec-47dd-ac89-a43014b7d6bc}"); // reverse geocode mile makers

        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "AGRC E911 Toolbar";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "tlbrE911";
            }
        }
    }
}