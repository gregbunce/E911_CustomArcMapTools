using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E911_Tools
{
    class clsSplitLine
    {
        static int intStreetName;
        static int intACSName;
        static bool isNumeric_StName;
        static bool isNumeric_ACSName;


        public static void SplitLineAtIntersection()
        {
            try
            {
                // see if the there's anyintersecting segments with a coordinate address to use values in the new number assignments
                List<IFeature> listFeatures = new List<IFeature>();
                listFeatures = checkForIntersectingSegments(clsE911Globals.arcPoint, 1, clsE911Globals.arcFeatClass_PSAPRoads);

                // get the objectid of the selected feature - so we can exclude it from the intesecting search below
                string strSelectedOID = clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("OBJECTID")).ToString();
                //MessageBox.Show("streetoid: " + strSelectedOID + Environment.NewLine + "point x,y: " + clsE911Globals.arcPoint.Y.ToString() + ", " + clsE911Globals.arcPoint.Y);
                isNumeric_StName = false;
                isNumeric_ACSName = false;

                // if see there are any features intersecting (there is always 1 - it's the selected feature to split, so we need to check if it's more than 1)
                if (listFeatures.Count > 1)
                {
                    //bool blnUseCoordinateAddress;

                    // loop through the intesecting features to see if any have coordinate address to use
                    for (int i = 0; i < listFeatures.Count; i++)
                    {
                        // ignore the ifeature that's the selected one - we don't need to check for coordinate values for the selected
                        if (strSelectedOID != listFeatures[i].OID.ToString())
                        {
                            // check if street name is acs/coordinate (numberic)
                            isNumeric_StName = int.TryParse(listFeatures[i].get_Value(listFeatures[i].Fields.FindField("STREETNAME")).ToString(), out intStreetName);

                            isNumeric_ACSName = int.TryParse(listFeatures[i].get_Value(listFeatures[i].Fields.FindField("ACSNAME")).ToString(), out intACSName);

                            // if the value is numeric then capture the number and proceed
                            if (isNumeric_StName == true)
                            {
                                //MessageBox.Show(intStreetName.ToString());
                                break; // breaks from "for loop"
                            }
                            if (isNumeric_ACSName == true)
                            {
                                //MessageBox.Show(intACSName.ToString());
                                break; // breaks from "for loop"
                            }
                        }
                    }
                }

                // call the split centerline method
                doCenterlineSplit();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        // this method splits the centerline
        public static void doCenterlineSplit()
        {
            try
            {
                // set address field range fields
                string strLeftFromName = "L_F_ADD";
                string strLeftToName = "L_T_ADD";
                string strRightFromName = "R_F_ADD";
                string strRightToName = "R_T_ADD";
                long longLeftFromNum;
                long longLeftToNum;
                long longRightFromNum;
                long longRightToNum;


                ICurve arcParentCurve = clsE911Globals.arcFeatureRoadSegment.Shape as ICurve;

                // check if the curve is nothing
                if (arcParentCurve == null)
                {
                    MessageBox.Show("The selected polyline does not have a valid shape.", "Can Not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // check if the curve is nothing
                if (arcParentCurve.IsEmpty == true)
                {
                    MessageBox.Show("The selected polyline has an empty geometry.", "Can Not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // check if the curve is nothing
                if (arcParentCurve.Length == 0)
                {
                    MessageBox.Show("The selected polyline has a length of zero.", "Can Not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // make sure the required fields which contain the house numbers have been specified
                IFields arcFields = clsE911Globals.arcFeatureRoadSegment.Fields;
                longLeftFromNum = arcFields.FindField(strLeftFromName);
                longLeftToNum = arcFields.FindField(strLeftToName);
                longRightFromNum = arcFields.FindField(strRightFromName);
                longRightToNum = arcFields.FindField(strRightToName);

                // check for Null values in the 4 house number fields (modGlobals has comments on g_pExtension)
                if (longLeftFromNum == null | longLeftToNum == null | longRightFromNum == null | longRightToNum == null)
                {
                    MessageBox.Show("One or more of the house numbers are Null for the selected line.", "Can Not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // try to get a valid house number from each of the 4 house number fields.
                // this is especially important if the fields are Text, which is common for geocoding data.
                long lngFrom_Left_HouseNum = TryToGetValidHouseNum(clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("L_F_ADD")).ToString().Trim());
                long lngFrom_Right_HouseNum = TryToGetValidHouseNum(clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("R_F_ADD")).ToString().Trim());
                long lngTo_Left_HouseNum = TryToGetValidHouseNum(clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("L_T_ADD")).ToString().Trim());
                long lngTo_Right_HouseNum = TryToGetValidHouseNum(clsE911Globals.arcFeatureRoadSegment.get_Value(clsE911Globals.arcFeatureRoadSegment.Fields.FindField("R_T_ADD")).ToString().Trim());

                if (lngFrom_Left_HouseNum == -1 | lngFrom_Right_HouseNum == -1 | lngTo_Left_HouseNum == -1 | lngTo_Right_HouseNum == -1)
                {
                    MessageBox.Show("One or more of the house numbers are invalid for the selected line.  Please see the attribute table and verify that address ranges are valid.", "Can Not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                bool blnMixedParity = false;

                // check left address ranges //
                // both of these values should retrun as false (if only odd numbers are on left) - meaning the to and from range for that side of the road is odd
                bool blnLeftFromIsEven = isEven(lngFrom_Left_HouseNum);
                bool blnLeftToIsEven = isEven(lngTo_Left_HouseNum);

                // check if side of the road is both even or both odd
                if (blnLeftToIsEven != blnLeftFromIsEven)
                {
                    MessageBox.Show("The left side of the selected line has both even and odd numbers.  Ensure the left ranges are both either odd or even.", "Mixed Parity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // check right address ranges //
                // both ranges should be same
                bool blnRightFromIsEven = isEven(lngFrom_Right_HouseNum);
                bool blnRightToIsEven = isEven(lngTo_Right_HouseNum);

                // check if side of the road is both even or odd
                if (blnRightFromIsEven != blnRightToIsEven)
                {
                    MessageBox.Show("The right side of the selected line has both even and odd numbers.  Ensure the right ranges are both either odd or even.", "Mixed Parity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // verify the 2 sides of the polyline have opposite parity (unless one side has 2 zeros)
                bool blnLeftIsEven = isEven(lngFrom_Left_HouseNum);
                bool blnOneSideHasZeros;

                // check if any of the range values are zero
                if ((lngFrom_Left_HouseNum == 0 & lngTo_Left_HouseNum == 0) | (lngFrom_Right_HouseNum == 0 & lngTo_Right_HouseNum == 0))
                {
                    blnOneSideHasZeros = true;
                }
                else
                {
                    blnOneSideHasZeros = false;
                }


                if (blnOneSideHasZeros == false)
                {
                    // check if both the right and left "from" ranges are of same parity
                    if (blnLeftFromIsEven == blnRightFromIsEven)
                    {
                        if (blnLeftFromIsEven == false)
                        {
                            MessageBox.Show("Both sides of the selected line begin with odd numbers.  Ensure one side of segment begins with an odd number and the other begins with an even number.", "Mixed Parity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Both sides of the selected line begin with even numbers.  Ensure one side of segment begins with an odd number and the other begins with an even number.", "Mixed Parity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // check if both the right and left "to" ranges are of same parity
                    if (blnLeftToIsEven == blnRightToIsEven)
                    {
                        if (blnLeftToIsEven == false)
                        {
                            MessageBox.Show("Both sides of the selected line end with odd numbers.  Ensure one side of segment ends with an odd number and the other ends with an even number.", "Mixed Parity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Both sides of the selected line end with even numbers.  Ensure one side of segment ends with an odd number and the other ends with an even number.", "Mixed Parity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }


                // create the point that will be used to split the selected polyline //
                if (clsE911Globals.arcPoint == null)
                {
                    MessageBox.Show("A valid split point could not be created. The split point (from user mouse-click) returned null.", "Can not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsE911Globals.arcPoint.IsEmpty == true)
                {
                    MessageBox.Show("A valid split point could not be created. The split point (from user mouse-click) returned IsEmpty.", "Can not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // split the parent feature into 2 offspring features. we use IFeatureEdit::Split since
                // it respects geodatabase behaviour (subtypes, domains, split policies etc). it also maintains
                // M and Z values, and works for geometric networks and topological ArcInfo coverages
                IFeatureEdit arcFeatureEdit = clsE911Globals.arcFeatureRoadSegment as IFeatureEdit;
                ISet arcNewSet;

                // start an edit operation
                clsE911Globals.arcEditor.StartOperation();

                // split the segment
                arcNewSet = arcFeatureEdit.Split(clsE911Globals.arcPoint);

                // make sure the segment was split into 2 segments
                if (arcNewSet.Count != 2)
                {
                    MessageBox.Show("The selected line was not split into two segments -- unknown error.  Please check selected segment and try process again.", "Can not Split", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsE911Globals.arcEditor.AbortOperation();
                    return;
                }


                // we now need to modify the house numbers of the 2 offspring polylines. before doing this,
                // we must be sure pNewFeature1 is the offspring line that contains the parent's from vertex,
                // or our logic will not work. Since ISet::Next does not return the features in any particular
                // order, we must test and switch if needed.
                IFeature arcNewFeature1 = arcNewSet.Next() as IFeature;  // will be wrong 50% of the time
                IFeature arcNewFeature2;
                ICurve arcNewFeatCurve1 = arcNewFeature1.Shape as ICurve;
                ICurve arcNewFeatCurve2;

                // get the from points from each curve
                IPoint arcParentFromPnt = arcParentCurve.FromPoint;
                IPoint arcNewFeature1FromPnt = arcNewFeatCurve1.FromPoint;

                // set up relational operator to check if points are equal
                IRelationalOperator arcRelationalOperator = arcParentFromPnt as IRelationalOperator;
                if (arcRelationalOperator.Equals(arcNewFeature1FromPnt) == true)
                {
                    // no need to switch... just set the from point for the 2nd segment (the other split segment)
                    arcNewFeature2 = arcNewSet.Next() as IFeature;
                    arcNewFeatCurve2 = arcNewFeature2.Shape as ICurve;
                }
                else // will happen 50% of the time, need to switch features
                {
                    arcNewFeature2 = arcNewFeature1;
                    arcNewFeatCurve2 = arcNewFeature2.Shape as ICurve;
                    arcNewFeature1 = arcNewSet.Next() as IFeature;
                    arcNewFeatCurve1 = arcNewFeature1.Shape as ICurve;
                }


                // get the distance along the curve (as a ratio) where the split point falls. we will need
                // this soon for the interpolation of house numbers.
                double dblDistAlongCurve = arcNewFeatCurve1.Length / (arcNewFeatCurve1.Length + arcNewFeatCurve2.Length);

                // fix the 4 house numbers that are not correct (main part of code). the other 4 numbers are
                // already correct (the FROM_LEFT and FROM_RIGHT of the first feature, and the TO_LEFT and TO_RIGHT of the second feature).
                long lngLeftNum = getInterpolatedHouseNumber(lngFrom_Left_HouseNum, lngTo_Left_HouseNum, dblDistAlongCurve);
                long lngRightNum = getInterpolatedHouseNumber(lngFrom_Right_HouseNum, lngTo_Right_HouseNum, dblDistAlongCurve);

                // the following 10 lines set the TO_LEFT and TO_RIGHT numbers of the first feature //
                if (isNumeric_ACSName | isNumeric_StName) // there was an intersecting road with a numberic street name
                {
                    // use the numeric street name to populate the address ranges
                    if (isNumeric_StName) // use numeric values from street name
                    {
                        // check if intersecting numeric street name is even or odd
                        bool blnIsEven_NumericStName = isEven(Convert.ToInt64(intStreetName)); // convert b/c method is expecting long

                        if (blnIsEven_NumericStName) // intersecting street is even
                        {
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("L_T_ADD"), intStreetName - 1);
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("R_T_ADD"), intStreetName - 2);
                        }
                        else // intersecting street is odd
                        {
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("L_T_ADD"), intStreetName - 2);
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("R_T_ADD"), intStreetName - 1);
                        }
                    }
                    else // use numeric values from acs alias field
                    {
                        // check if value is even or odd
                        bool blnIsEvenAcsName = isEven(Convert.ToInt64(intACSName));

                        if (blnIsEvenAcsName) // intersecting street is even
                        {
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("L_T_ADD"), intACSName - 1);
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("R_T_ADD"), intACSName - 2);
                        }
                        else // intersecting street is odd
                        {
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("L_T_ADD"), intACSName - 2);
                            arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("R_T_ADD"), intACSName - 1);
                        }
                    }
                }
                else // there was not an intersecting road with a numberic street name
                {
                    if (lngFrom_Left_HouseNum == lngTo_Left_HouseNum) // if parent had no range of house numbers
                    {
                        arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("L_T_ADD"), lngFrom_Left_HouseNum);
                    }
                    else
                    {
                        arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("L_T_ADD"), lngLeftNum);
                    }
                    if (lngFrom_Right_HouseNum == lngTo_Right_HouseNum)
                    {
                        arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("R_T_ADD"), lngFrom_Right_HouseNum);
                    }
                    else
                    {
                        arcNewFeature1.set_Value(arcNewFeature1.Fields.FindField("R_T_ADD"), lngRightNum);
                    }
                }

                //store feature1
                arcNewFeature1.Store();

                //get field values for address ranges
                long lngFeat1_L_T_ADD = Convert.ToInt64(arcNewFeature1.get_Value(arcNewFeature1.Fields.FindField("L_T_ADD")));
                long lngFeat1_R_T_ADD = Convert.ToInt64(arcNewFeature1.get_Value(arcNewFeature1.Fields.FindField("R_T_ADD")));

                // the following lines set the FROM_LEFT and FROM_RIGHT numbers of the second feature
                // set the left_from
                if (isNumeric_ACSName | isNumeric_StName) // there was an intersecting road with a numberic street name
                {
                    // use the numeric street name to populate the address ranges
                    if (isNumeric_StName) // use numeric values from street name
                    {
                        // check if intersecting numeric street name is even or odd
                        bool blnIsEven_NumericStName = isEven(Convert.ToInt64(intStreetName)); // convert b/c method is expecting long

                        if (blnIsEven_NumericStName) // intersecting street is even
                        {
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), intStreetName + 1);
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("R_F_ADD"), intStreetName);
                        }
                        else // intersecting street is odd
                        {
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), intStreetName);
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("R_F_ADD"), intStreetName + 1);
                        }
                    }
                    else  // use numeric values from acs alias field
                    {
                        // check if value is even or odd
                        bool blnIsEvenAcsName = isEven(Convert.ToInt64(intACSName));

                        if (blnIsEvenAcsName) // intersecting street is even
                        {
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), intACSName + 1);
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("R_F_ADD"), intACSName);
                        }
                        else // intersecting street is odd
                        {
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), intACSName);
                            arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("R_F_ADD"), intACSName + 1);
                        }
                    }
                }
                else // there was not an intersecting road with a numberic street name
                {
                    if (lngFrom_Left_HouseNum < lngTo_Left_HouseNum)
                    {
                        //long intLTADD = Convert.ToInt64(arcNewFeature1.get_Value(arcNewFeature1.Fields.FindField("L_T_ADD")));
                        //arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), arcNewFeature1.get_Value(arcNewFeature1.Fields.FindField("L_T_ADD") + 2));
                        arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), lngFeat1_L_T_ADD + 2);
                    }
                    else if (lngFrom_Left_HouseNum == lngTo_Left_HouseNum) // if parent had no range of house numbers
                    {
                        arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), lngFrom_Left_HouseNum);
                    }
                    else // if house numbers run opposite to the polyline's digitized direction
                    {
                        arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("L_F_ADD"), lngFeat1_L_T_ADD - 2);
                    }

                    // set the right_from for the feature 2
                    if (lngFrom_Right_HouseNum < lngTo_Right_HouseNum)
                    {
                        arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("R_F_ADD"), lngFeat1_R_T_ADD + 2);
                    }
                    else if (lngFrom_Right_HouseNum == lngTo_Right_HouseNum)
                    {
                        arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("R_F_ADD"), lngFrom_Right_HouseNum);
                    }
                    else // if house numbers run opposite to the polyline's digitized direction
                    {
                        arcNewFeature2.set_Value(arcNewFeature2.Fields.FindField("R_F_ADD"), lngFeat1_R_T_ADD - 2);
                    }
                }

                // store the features
                //arcNewFeature1.Store();
                arcNewFeature2.Store();

                // stop the edit operation
                clsE911Globals.arcEditor.StopOperation("AGRC Split Line");


            }
            catch (Exception ex)
            {
                // abort the operation if there's an error
                clsE911Globals.arcEditor.AbortOperation();


                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
        }








        // this method tries to get a valid house number
        public static long TryToGetValidHouseNum(object strHouseNum)
        {
            try
            {
                // attempt to get a valid Long Interger from the supplied candidate
                // returns -1 if not possible
                int intHouseNum;
                bool blnIsNumber = int.TryParse(strHouseNum.ToString().Trim(), out intHouseNum);

                if (blnIsNumber == true)
                {
                    // convert the int to long and return it
                    long longHouseNum = Convert.ToInt64(intHouseNum);

                    // check if value is zero
                    if (longHouseNum != 0)
                    {
                        return longHouseNum;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return 0;
            }
        }



        // check if number is even
        public static bool isEven(long longHseNumber)
        {
            try
            {
                // return True is number is even (could also shorten this code to: "return longHseNumber % 2 == 0;"
                if (longHseNumber % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // retrun false
                return false;
            }
        }


        // this method is called from the 'doCenterlineSplit' method above and returns a long value
        public static long getInterpolatedHouseNumber(long lngFrom, long lngTo, double dblDist)
        {
            try
            {
                // interpolate the next lowest whole number given lngFrom and lngTo. Makes sure it is on the
                // correct side of the street if MixedParity is False.
                // start by returning the raw (Double) interpolated house number.
                double dblHouseNum;
                long lngNextLowestHouseNumber;

                if (lngFrom < lngTo)
                {
                    dblHouseNum = ((lngTo - lngFrom) * dblDist) + lngFrom;
                    lngNextLowestHouseNumber = Convert.ToInt64(dblHouseNum); //this will retrun a long
                }
                else
                {
                    dblHouseNum = lngFrom - ((lngFrom - lngTo) * dblDist);
                    lngNextLowestHouseNumber = Convert.ToInt64(dblHouseNum) + 1; //this will return a long
                }

                // make sure the interpolated number is on the correct side of the street
                bool blnFromIsEven = isEven(lngFrom);
                bool blnCandidateNumberIsEven = isEven(lngNextLowestHouseNumber);


                if (blnFromIsEven != blnCandidateNumberIsEven)
                {
                    if (lngFrom < lngTo)
                    {
                        lngNextLowestHouseNumber = lngNextLowestHouseNumber - 1;
                    }
                    else
                    {
                        lngNextLowestHouseNumber = lngNextLowestHouseNumber + 1;
                    }
                }

                // retrun the value
                return lngNextLowestHouseNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "E911 ArcMap tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return -1;
            }

        }



        // get features that intersect the buffered mouse click and return them as a list of IFeature
        public static List<IFeature> checkForIntersectingSegments(IPoint mousePoint, double buffer, IFeatureClass featureClass)
        {
            var envelope = mousePoint.Envelope;
            envelope.Expand(buffer, buffer, false);
            var geodataset = (IGeoDataset)featureClass;
            string shapeFieldName = featureClass.ShapeFieldName;
            ESRI.ArcGIS.Geodatabase.ISpatialFilter spatialFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilter();
            spatialFilter.Geometry = envelope;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;  // website for other options edndoc.esri.com/arcobjects/9.2/ComponentHelp/esrigeodatabase/esrispatialrelenum.htm
            spatialFilter.set_OutputSpatialReference(shapeFieldName, geodataset.SpatialReference);

            ESRI.ArcGIS.Geodatabase.IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);

            var features = new List<IFeature>();
            ESRI.ArcGIS.Geodatabase.IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
                features.Add(feature);
            return features;
        }

















    }
}
