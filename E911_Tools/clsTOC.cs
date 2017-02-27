using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E911_Tools
{
    class clsTOC
    {

        // THIS METHOD IS OBSOLETE - NOT USED ANY MORE vvvvvv
        // this method inserts TOC specific values into the etl feature class
        public void insertTocIntoEtlFeatureClass(IFeatureCursor arcFeatCurRoadsUtrans, int intFeatureCount, bool blnAssignSyntheticAddressRanges, bool blnUpdateReverseGeocodeData)
        {
            try
            {
                //IFeature arcFeatureUtrans;

                //// loop through this psap's utrans road segments and load them into the newly-created filegeodatabase feature class (psap schema)
                //while ((arcFeatureUtrans = arcFeatCurRoadsUtrans.NextFeature()) != null)
                //{
                //    // create a new row in the newly-created feature class
                //    IFeature arcFeatureNewSchemaFeat = clsE911Globals.arcFeatClassNewSchemaFeat.CreateFeature();

                //    // add the shape/geometry from utrans into the psap's schema feature class
                //    arcFeatureNewSchemaFeat.Shape = arcFeatureUtrans.Shape;

                //    // add field values to the newly-created feature class
                //    string strSTREETNAME = "";
                //    strSTREETNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();
                //    if (strSTREETNAME != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), DBNull.Value);
                //        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETNAME"), null);

                //    }


                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")) != DBNull.Value)
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")));
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_F_ADD"), 0);
                //    }

                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")) != DBNull.Value)
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")));
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("L_T_ADD"), 0);
                //    }

                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")) != DBNull.Value)
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")));
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_F_ADD"), 0);
                //    }

                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")) != DBNull.Value)
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")));
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("R_T_ADD"), 0);
                //    }

                //    // get address ranges values double
                //    double dblRT_ADD = 0;
                //    double dblRF_ADD = 0;
                //    double dblLT_ADD = 0;
                //    double dblLF_ADD = 0;

                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")).ToString().Trim() != "")
                //    {
                //        dblRT_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_T_ADD")));
                //    }
                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")).ToString().Trim() != "")
                //    {
                //        dblRF_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("R_F_ADD")));
                //    }
                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")).ToString().Trim() != "")
                //    {
                //        dblLT_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_T_ADD")));
                //    }
                //    if (arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")).ToString().Trim() != "")
                //    {
                //        dblLF_ADD = Convert.ToDouble(arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("L_F_ADD")));
                //    }


                //    bool blnAddressRangeAllZeros = false;
                //    // check if address ranges are zeros
                //    if (dblRT_ADD == 0 & dblRF_ADD == 0 & dblLT_ADD == 0 & dblLF_ADD == 0)
                //    {
                //        blnAddressRangeAllZeros = true;
                //    }

                //    string strCartoCode = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("CARTOCODE")).ToString().Trim();
                //    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("CARTOCODE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("CARTOCODE")).ToString().Trim());

                //    string strPREDIR = "";
                //    strPREDIR = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim();
                //    if (strPREDIR != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"), DBNull.Value);
                //        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("PREDIR"), null);
                //    }

                //    string strSTREETTYPE = "";
                //    strSTREETTYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim();
                //    if (strSTREETTYPE != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETTYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREETTYPE"), DBNull.Value);
                //    }

                //    string strSUFDIR = "";
                //    strSUFDIR = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                //    if (strSUFDIR != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SUFDIR"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SUFDIR"), DBNull.Value);
                //    }

                //    string strALIAS1 = "";
                //    strALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                //    if (strALIAS1 != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1"), DBNull.Value);
                //    }

                //    string strALIAS1TYPE = "";
                //    strALIAS1TYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim();
                //    if (strALIAS1TYPE != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1TYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS1TYPE"), DBNull.Value);
                //    }

                //    string strALIAS2 = "";
                //    strALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                //    if (strALIAS2 != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2"), DBNull.Value);
                //    }

                //    string strALIAS2TYPE = "";
                //    strALIAS2TYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim();
                //    if (strALIAS2TYPE != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2TYPE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ALIAS2TYPE"), DBNull.Value);
                //    }

                //    string strACSALIAS = "";
                //    strACSALIAS = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSALIAS")).ToString().Trim();
                //    if (strACSALIAS != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSALIAS"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSALIAS")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSALIAS"), DBNull.Value);
                //    }

                //    string strACSNAME = "";
                //    strACSNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim();
                //    if (strACSNAME != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSNAME"), DBNull.Value);
                //    }

                //    string strACSSUF = "";
                //    strACSSUF = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim();
                //    if (strACSSUF != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSSUF"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ACSSUF"), DBNull.Value);
                //    }

                //    string strZIPLEFT = "";
                //    strZIPLEFT = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPLEFT")).ToString().Trim();
                //    if (strZIPLEFT != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ZIPLEFT"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPLEFT")).ToString().Trim());
                //    }

                //    string strZIPRIGHT = "";
                //    strZIPRIGHT = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPRIGHT")).ToString().Trim();
                //    if (strZIPRIGHT != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("ZIPRIGHT"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ZIPRIGHT")).ToString().Trim());
                //    }

                //    string strCOFIPS = "";
                //    strCOFIPS = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("COFIPS")).ToString().Trim();
                //    if (strCOFIPS != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("COFIPS"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("COFIPS")).ToString().Trim());
                //    }

                //    string strHWYNAME = "";
                //    strHWYNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("HWYNAME")).ToString().Trim();
                //    if (strHWYNAME != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("HWYNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("HWYNAME")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("HWYNAME"), DBNull.Value);
                //    }

                //    string strDOTRTNAME = "";
                //    strDOTRTNAME = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_RTNAME")).ToString().Trim();
                //    if (strDOTRTNAME != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_RTNAME"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_RTNAME")).ToString().Trim());
                //    }

                //    string strDOTFMILE = "";
                //    strDOTFMILE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_F_MILE")).ToString().Trim();
                //    if (strDOTFMILE != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_F_MILE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_F_MILE")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_F_MILE"), 0);
                //    }

                //    string strDOTTMILE = "";
                //    strDOTTMILE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_T_MILE")).ToString().Trim();
                //    if (strDOTTMILE != "")
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_T_MILE"), arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("DOT_T_MILE")).ToString().Trim());
                //    }
                //    else
                //    {
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("DOT_T_MILE"), 0);
                //    }


 



                //    // check what dispatch center we are working with, and format the certain fields as needed
                //    // set up the concatination for the STREET field
                //    string strSTREET = "";
                //    int intNumber;
                //    bool blnIsNumeric = int.TryParse(strSTREETNAME, out intNumber);

                //    // cartocode is 1 or 7 (interstates or ramps)
                //    # region if cartocode is 1 or 7
                //    if (strCartoCode == "1" | strCartoCode == "7")
                //    {
                //        // check what county the segment is in
                //        if (strCOFIPS == "49049") // UTAH COUNTY //
                //        {
                //            if (strCartoCode == "1")
                //            {
                //                // they use this format "S I15 NB"
                //                strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                //                // replace values
                //                strSTREET = strSTREET.Replace("  ", " ");
                //                strSTREET = strSTREET.Replace("-", "");
                //                // trim the whole street concatination
                //                strSTREET = strSTREET.Trim();


                //                // populate salias1 and salias2 fields
                //                if (strALIAS1 != "")
                //                {
                //                    string strSALIAS1 = "";
                //                    strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                //                    strSALIAS1 = strSALIAS1.Replace("  ", " ");
                //                    // check for the word "HIGHWAY" and replace it with "SR-"
                //                    strSALIAS1 = strSALIAS1.Replace("-", "");
                //                    strSALIAS1 = strSALIAS1.Trim();
                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1);
                //                }
                //                if (strALIAS2 != "")
                //                {
                //                    string strSALIAS2 = "";
                //                    strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                //                    strSALIAS2 = strSALIAS2.Replace("  ", " ");
                //                    // check for the word "HIGHWAY" and replace it with "SR-"
                //                    strSALIAS2 = strSALIAS2.Replace("-", "");
                //                    strSALIAS2 = strSALIAS2.Trim();
                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2);
                //                }
                //            }
                //            else if (strCartoCode == "7")
                //            {
                //                // they use the " I15 SB 4800 S OFR, I89 WB 5600 W ONR"
                //                strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                //                // reformat the alias2 field to rearrange the order of exit info for TOC format
                //                strSTREET = clsE911StaticClass.ReformatStringForExitFeatures(strSTREET);

                //                // populate salias1 and salias2 fields
                //                if (strALIAS1 != "")
                //                {
                //                    string strSALIAS1 = "";
                //                    strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();

                //                    // reformat the alias2 field to rearrange the order of exit info for TOC format
                //                    string strSALIAS1_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS1);

                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1_reformatted);
                //                }
                //                if (strALIAS2 != "")
                //                {
                //                    string strSALIAS2 = "";
                //                    strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();

                //                    // reformat the alias2 field to rearrange the order of exit info for TOC format
                //                    string strSALIAS2_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS2);

                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2_reformatted);
                //                }
                //            }


                //        }
                //        else if (strCOFIPS == "49035") // SALT LAKE COUNTY //
                //        {
                //            if (strCartoCode == "1")
                //            {
                //                // they use this format "S I15 NB"
                //                strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                //                    arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                //                // replace values
                //                strSTREET = strSTREET.Replace("  ", " ");
                //                strSTREET = strSTREET.Replace("-", "");
                //                // trim the whole street concatination
                //                strSTREET = strSTREET.Trim();


                //                // populate salias1 and salias2 fields
                //                if (strALIAS1 != "")
                //                {
                //                    string strSALIAS1 = "";
                //                    strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                //                    strSALIAS1 = strSALIAS1.Replace("  ", " ");
                //                    // check for the word "HIGHWAY" and replace it with "SR-"
                //                    strSALIAS1 = strSALIAS1.Replace("-", "");
                //                    strSALIAS1 = strSALIAS1.Trim();
                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1);
                //                }
                //                if (strALIAS2 != "")
                //                {
                //                    string strSALIAS2 = "";
                //                    strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                //                    strSALIAS2 = strSALIAS2.Replace("  ", " ");
                //                    // check for the word "HIGHWAY" and replace it with "SR-"
                //                    strSALIAS2 = strSALIAS2.Replace("-", "");
                //                    strSALIAS2 = strSALIAS2.Trim();
                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2);
                //                }

                //            }
                //            else if (strCartoCode == "7")
                //            {
                //                // they use the " I15 SB 4800 S OFR, I89 WB 5600 W ONR"
                //                strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                //                // reformat the alias2 field to rearrange the order of exit info for TOC format
                //                strSTREET = clsE911StaticClass.ReformatStringForExitFeatures(strSTREET);

                //                // populate salias1 and salias2 fields
                //                if (strALIAS1 != "")
                //                {
                //                    string strSALIAS1 = "";
                //                    strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();

                //                    // reformat the alias2 field to rearrange the order of exit info for TOC format
                //                    string strSALIAS1_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS1);

                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1_reformatted);
                //                }
                //                if (strALIAS2 != "")
                //                {
                //                    string strSALIAS2 = "";
                //                    strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();

                //                    // reformat the alias2 field to rearrange the order of exit info for TOC format
                //                    string strSALIAS2_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS2);

                //                    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2_reformatted);
                //                }
                //            }
                //        }
                //    }
                //    #endregion
                //    else // cartocode was not 1 or 7
                //    {
                //        //string strStreetName = "";
                //        //strStreetName = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();
                //        //string strSTREETTYPE = "";
                //        //strSTREETTYPE = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim();

                //        // check if streetname is numeric and if it has a streetype of cir if so format it with the circle after the sufdir
                //        if (strSTREETTYPE.ToUpper() == "CIR" & blnIsNumeric)
                //        {
                //            strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                //                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim() + " " +
                //                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                //        }
                //        else // the streetname does not contain a number with the streettype being "cir"
                //        {
                //            strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                //                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim() + " " +
                //                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim() + " " +
                //                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                //        }

                //        // replace double spaces with one
                //        strSTREET = strSTREET.Replace("  ", " ");



                //        // check if this segment is a highway, and if so.. what type
                //        if (strHWYNAME != "" & strSTREETNAME.Contains("HIGHWAY"))
                //        {
                //            if (strHWYNAME.Contains("SR"))
                //            {
                //                strSTREET = strSTREET.Replace("HIGHWAY ", "SR");
                //            }
                //            if (strHWYNAME.Contains("US"))
                //            {
                //                strSTREET = strSTREET.Replace("HIGHWAY ", "US");
                //            }
                //        }
                //        // check for the word "HIGHWAY" and replace it with "SR-"
                //        //strSTREET = strSTREET.Replace("HIGHWAY ", "SR-");
                //        //strSTREET = strSTREET.Replace("OLD SR-", "OLD HWY ");
                //        // trim the whole street concatination
                //        strSTREET = strSTREET.Trim();

                //        //arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREET"), strSTREET);
                //    }

                //    // make sure string is not greater than the field allows
                //    IFields fields = arcFeatureNewSchemaFeat.Fields;
                //    IField fieldLength = fields.get_Field(fields.FindField("STREET"));
                //    if (strSTREET.Length > fieldLength.Length)
                //    {
                //        strSTREET = "error: length > " + fieldLength.Length.ToString();
                //    }

                //    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("STREET"), strSTREET);

                //    // populate the location field and salias4 - the value in this field is placed after a colon on the calltakers screen to show them the numberic location of the call
                //    string strAcsName = "";
                //    strAcsName = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim();
                //    if (strAcsName != "")
                //    {
                //        string strLOCATION = "";
                //        strLOCATION = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSNAME")).ToString().Trim() + " " +
                //            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ACSSUF")).ToString().Trim();
                //        strLOCATION = strLOCATION.Replace("  ", " ");
                //        strLOCATION = strLOCATION.Trim();
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("LOCATION"), strLOCATION);
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS4"), strLOCATION);
                //    }

                //    // populate salias1
                //    string strAlias1 = "";
                //    strAlias1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                //    if (strALIAS1 != "")
                //    {
                //        string strSALIAS1 = "";
                //        strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim() + " " +
                //            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim();
                //        strSALIAS1 = strSALIAS1.Replace("  ", " ");
                //        // check for the word "HIGHWAY" and replace it with "SR-"
                //        strSALIAS1 = strSALIAS1.Replace("HIGHWAY ", "HWY");
                //        strSALIAS1 = strSALIAS1.Replace("SR ", "SR");
                //        strSALIAS1 = strSALIAS1.Replace("US ", "US");
                //        //strSTREET = strSTREET.Replace("OLD SR-", "OLD HWY ");
                //        strSALIAS1 = strSALIAS1.Trim();
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1);
                //    }

                //    // salias2 fields
                //    string strAlias2 = "";
                //    strAlias2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                //    if (strALIAS2 != "")
                //    {
                //        string strSALIAS2 = "";
                //        strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim() + " " +
                //             arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim();
                //        strSALIAS2 = strSALIAS2.Replace("  ", " ");
                //        // check for the word "HIGHWAY" and replace it with "SR-"
                //        strSALIAS2 = strSALIAS2.Replace("HIGHWAY ", "HWY");
                //        strSALIAS2 = strSALIAS2.Replace("SR ", "SR");
                //        strSALIAS2 = strSALIAS2.Replace("US ", "US");
                //        //strSTREET = strSTREET.Replace("OLD SR-", "OLD HWY ");
                //        strSALIAS2 = strSALIAS2.Trim();
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2);
                //    }



                //    // populate the salias3 field of street name or the alias1 or alias2 fields contain HIGHWAY... then replace with HWY
                //    bool blnStreetNameContainsHwy = strSTREETNAME.Contains("HIGHWAY");
                //    bool blnAlias1ContainsHwy = strALIAS1.Contains("HIGHWAY");
                //    bool blnAlias2ContainsHwy = strALIAS2.Contains("HIGHWAY");
                //    string strSALIAS3 = "";

                //    if (blnStreetNameContainsHwy) // if highway in streetname
                //    {
                //        strSTREETNAME = strSTREETNAME.Replace("HIGHWAY", "HWY");
                //        strSALIAS3 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                //            strSTREETNAME + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETTYPE")).ToString().Trim() + " " +
                //            arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                //        strSALIAS3 = strSALIAS3.Replace("  ", " ");
                //        strSALIAS3 = strSALIAS3.Trim();
                //        arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS3"), strSALIAS3);
                //    }
                //    else
                //    {
                //        if (blnAlias1ContainsHwy) // if highway in alias1
                //        {
                //            strALIAS1 = strALIAS1.Replace("HIGHWAY", "HWY");
                //            strSALIAS3 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                //            strALIAS1 + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1TYPE")).ToString().Trim() + " " +
                //                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                //            strSALIAS3 = strSALIAS3.Replace("  ", " ");
                //            strSALIAS3 = strSALIAS3.Trim();
                //            arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS3"), strSALIAS3);
                //        }
                //        else // if highway in allias2
                //        {
                //            if (blnAlias2ContainsHwy)
                //            {
                //                strALIAS2 = strALIAS2.Replace("HIGHWAY", "HWY");
                //                strSALIAS3 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                //                   strALIAS2 + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2TYPE")).ToString().Trim() + " " +
                //                       arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("SUFDIR")).ToString().Trim();
                //                strSALIAS3 = strSALIAS3.Replace("  ", " ");
                //                strSALIAS3 = strSALIAS3.Trim();
                //                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS3"), strSALIAS3);
                //            }
                //        }
                //    }

                //    // update the etl run date
                //    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("RunETL_Date"), DateTime.Now);

                //    arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("FULLNAME"), "");

                //    // store the new row/feature
                //    // check if geometry is empty before store
                //    if (!(arcFeatureUtrans.Extent.Envelope.IsEmpty))
                //    {
                //        arcFeatureNewSchemaFeat.Store();
                //    }

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "UTRANS Editor tool error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        // THIS METHOD IS OBSOLETE - NOT USED ANY MORE ^^^^^^



        // this method formats the STREET field for TOC specifics 
        public static string TocSTREET(string strSTREET, string strHWYNAME, string strSTREETNAME)
        {
            try
            {
                // check if this segment is a highway, and if so.. what type
                if (strHWYNAME != "" & strSTREETNAME.Contains("HIGHWAY"))
                {
                    if (strHWYNAME.Contains("SR"))
                    {
                        strSTREET = strSTREET.Replace("HIGHWAY ", "SR");
                    }
                    if (strHWYNAME.Contains("US"))
                    {
                        strSTREET = strSTREET.Replace("HIGHWAY ", "US");
                    }
                }

                strSTREET = strSTREET.Trim();

                return strSTREET;
            }
            catch (Exception)
            {
                return "Error TocSTREET";
            }
        }


        // this method formats the SALIAS1 field for TOC specifics 
        public static string TocSALIAS1(string strSALIAS1, string strHwyName2)
        {
            try
            {
                // check if SALIAS1 contains "HIGHWAY"
                if (strSALIAS1.Contains("HIGHWAY") | strSALIAS1.Contains("HWY"))
                {
                    // check if an SR or US highway
                    if (strHwyName2.Contains("US"))
                    {
                        // rename and remove leading space
                        strSALIAS1 = strSALIAS1.Replace("HIGHWAY ", "US");
                        strSALIAS1 = strSALIAS1.Replace("HWY ", "US");
                    }
                    else if (strHwyName2.Contains("SR"))
                    {
                        // rename and remove leading space
                        strSALIAS1 = strSALIAS1.Replace("HIGHWAY ", "SR");
                        strSALIAS1 = strSALIAS1.Replace("HWY ", "SR");
                    }
                }
                else // is not a highway alias
                {

                }
                
                // trim the string and return the result
                strSALIAS1 = strSALIAS1.Trim();

                return strSALIAS1;
            }
            catch (Exception)
            {
                return "Error TocSALIAS1";
            }
        }


        // this method formats the SALILAS2 field for TOC specifics 
        public static string TocSALIAS2(string strSALIAS2, string strHwyName3)
        {
            try
            {
                // check if SALIAS1 contains "HIGHWAY"
                if (strSALIAS2.Contains("HIGHWAY") | strSALIAS2.Contains("HWY"))
                {
                    // check if an SR or US highway
                    if (strHwyName3.Contains("US"))
                    {
                        // rename and remove leading space
                        strSALIAS2 = strSALIAS2.Replace("HIGHWAY ", "US");
                        strSALIAS2 = strSALIAS2.Replace("HWY ", "US");
                    }
                    else if (strHwyName3.Contains("SR"))
                    {
                        // rename and remove leading space
                        strSALIAS2 = strSALIAS2.Replace("HIGHWAY ", "SR");
                        strSALIAS2 = strSALIAS2.Replace("HWY ", "SR");
                    }
                }
                else // is not a highway alias
                {

                }

                // trim the string and return the result
                strSALIAS2 = strSALIAS2.Trim();

                return strSALIAS2;
            }
            catch (Exception)
            {
                return "Error TocSALIAS2";
            }
        }


        // this method formats freeways (if cartocode is 1 or 7) - currently i'm not calling this method as david has these as custom segments (so they get appended), but maybe in the future we will want to get freeways fresh from utrans and then we can use this code
        public static void TocIfCartocodeOneorSeven() 
        {
            try
            {
                ////if (strCartoCode == "1" | strCartoCode == "7")
                ////{
                ////    // check what county the segment is in
                ////    if (strCOFIPS == "49049") // UTAH COUNTY //
                ////    {
                ////        if (strCartoCode == "1")
                ////        {
                ////            // they use this format "S I15 NB"
                ////            strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                ////            // replace values
                ////            strSTREET = strSTREET.Replace("  ", " ");
                ////            strSTREET = strSTREET.Replace("-", "");
                ////            // trim the whole street concatination
                ////            strSTREET = strSTREET.Trim();


                ////            // populate salias1 and salias2 fields
                ////            if (strALIAS1 != "")
                ////            {
                ////                string strSALIAS1 = "";
                ////                strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                ////                strSALIAS1 = strSALIAS1.Replace("  ", " ");
                ////                // check for the word "HIGHWAY" and replace it with "SR-"
                ////                strSALIAS1 = strSALIAS1.Replace("-", "");
                ////                strSALIAS1 = strSALIAS1.Trim();
                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1);
                ////            }
                ////            if (strALIAS2 != "")
                ////            {
                ////                string strSALIAS2 = "";
                ////                strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                ////                strSALIAS2 = strSALIAS2.Replace("  ", " ");
                ////                // check for the word "HIGHWAY" and replace it with "SR-"
                ////                strSALIAS2 = strSALIAS2.Replace("-", "");
                ////                strSALIAS2 = strSALIAS2.Trim();
                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2);
                ////            }
                ////        }
                ////        else if (strCartoCode == "7")
                ////        {
                ////            // they use the " I15 SB 4800 S OFR, I89 WB 5600 W ONR"
                ////            strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                ////            // reformat the alias2 field to rearrange the order of exit info for TOC format
                ////            strSTREET = clsE911StaticClass.ReformatStringForExitFeatures(strSTREET);

                ////            // populate salias1 and salias2 fields
                ////            if (strALIAS1 != "")
                ////            {
                ////                string strSALIAS1 = "";
                ////                strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();

                ////                // reformat the alias2 field to rearrange the order of exit info for TOC format
                ////                string strSALIAS1_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS1);

                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1_reformatted);
                ////            }
                ////            if (strALIAS2 != "")
                ////            {
                ////                string strSALIAS2 = "";
                ////                strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();

                ////                // reformat the alias2 field to rearrange the order of exit info for TOC format
                ////                string strSALIAS2_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS2);

                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2_reformatted);
                ////            }
                ////        }


                ////    }
                ////    else if (strCOFIPS == "49035") // SALT LAKE COUNTY //
                ////    {
                ////        if (strCartoCode == "1")
                ////        {
                ////            // they use this format "S I15 NB"
                ////            strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " +
                ////                arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                ////            // replace values
                ////            strSTREET = strSTREET.Replace("  ", " ");
                ////            strSTREET = strSTREET.Replace("-", "");
                ////            // trim the whole street concatination
                ////            strSTREET = strSTREET.Trim();


                ////            // populate salias1 and salias2 fields
                ////            if (strALIAS1 != "")
                ////            {
                ////                string strSALIAS1 = "";
                ////                strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();
                ////                strSALIAS1 = strSALIAS1.Replace("  ", " ");
                ////                // check for the word "HIGHWAY" and replace it with "SR-"
                ////                strSALIAS1 = strSALIAS1.Replace("-", "");
                ////                strSALIAS1 = strSALIAS1.Trim();
                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1);
                ////            }
                ////            if (strALIAS2 != "")
                ////            {
                ////                string strSALIAS2 = "";
                ////                strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("PREDIR")).ToString().Trim() + " " + arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();
                ////                strSALIAS2 = strSALIAS2.Replace("  ", " ");
                ////                // check for the word "HIGHWAY" and replace it with "SR-"
                ////                strSALIAS2 = strSALIAS2.Replace("-", "");
                ////                strSALIAS2 = strSALIAS2.Trim();
                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2);
                ////            }

                ////        }
                ////        else if (strCartoCode == "7")
                ////        {
                ////            // they use the " I15 SB 4800 S OFR, I89 WB 5600 W ONR"
                ////            strSTREET = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("STREETNAME")).ToString().Trim();

                ////            // reformat the alias2 field to rearrange the order of exit info for TOC format
                ////            strSTREET = clsE911StaticClass.ReformatStringForExitFeatures(strSTREET);

                ////            // populate salias1 and salias2 fields
                ////            if (strALIAS1 != "")
                ////            {
                ////                string strSALIAS1 = "";
                ////                strSALIAS1 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS1")).ToString().Trim();

                ////                // reformat the alias2 field to rearrange the order of exit info for TOC format
                ////                string strSALIAS1_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS1);

                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS1"), strSALIAS1_reformatted);
                ////            }
                ////            if (strALIAS2 != "")
                ////            {
                ////                string strSALIAS2 = "";
                ////                strSALIAS2 = arcFeatureUtrans.get_Value(arcFeatureUtrans.Fields.FindField("ALIAS2")).ToString().Trim();

                ////                // reformat the alias2 field to rearrange the order of exit info for TOC format
                ////                string strSALIAS2_reformatted = clsE911StaticClass.ReformatStringForExitFeatures(strSALIAS2);

                ////                arcFeatureNewSchemaFeat.set_Value(arcFeatureNewSchemaFeat.Fields.FindField("SALIAS2"), strSALIAS2_reformatted);
                ////            }
                ////        }
                ////    }
                ////}
            }
            catch (Exception ex)
            {
            }
        
        }
    }
}
