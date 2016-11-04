using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E911_Tools
{
    class clsE911Globals
    {

        public static IApplication arcApplication
        {
            get;
            set;
        }

        public static IMap pMap
        {
            get;
            set;
        }

        public static IMxDocument pMxDocument
        {
            get;
            set;
        }


        public static IActiveView pActiveView
        {
            get;
            set;
        }

        public static IFeature arcFeatureRoadSegment
        {
            get;
            set;
        }

        public static IPoint arcPoint
        {
            get;
            set;
        }

        public static IEditor3 arcEditor
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_PSAPRoads
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_CountiesSGID
        {
            get;
            set;
        }

        public static IFeature arcFeature_CountyPoly
        {
            get;
            set;
        }

    }
}
