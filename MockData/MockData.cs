using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MockData
{
    public static class MockData
    {
        public static int GenerateData(string spName)
        {
            int result = 0;
            switch(spName)
            {
                case "USP_InsEmployee": result = 1;
                                        break;
                default: result = 1;
                    break;
            }
            return result;
        }

        public static DataSet GenerateDataSet(string spName)
        {
            DataSet dataSet = new DataSet();
            DataTable[] dataTables = new DataTable[5];

            #region sample code
            switch (spName)
            {
                case "USP_GetEmployee":
                    string[] columnNames = { "Id", "Name", "Age", "Salary" };
                    string[] dataTypes = { "int", "string", "int", "int" };
                    object[] data = { 1, "Jash", 27, 25000 };

                    dataTables[0] = PrepareDataTable(columnNames, dataTypes, data);
                    break;

                //Activity
                case "USP_FetchActivityDesc":
                    string[] FetchActivityColumnNames = { "Column - Local_Act_Description", "Local_Duration_Question", "Local_Help_text" };
                    string[] FetchActivitydataTypes = { "string", "string", "string" };
                    object[] FetchActivitydata = { "testing", "local question1", "local help1" };

                    dataTables[0] = PrepareDataTable(FetchActivityColumnNames, FetchActivitydataTypes, FetchActivitydata);
                    break;

              
                //BLLDuration
                case "USP_FetchDurationCountryDesc":
                    string[] FetchDurationCountryColumnNames = { "Local_Duration_group_Description", "Local_Duration_Group_Sequence", "Local_Duration_Group_line_Code" };
                    string[] FetchDurationCountrydataTypes = { "string", "string", "string" };
                    object[] FetchDurationCountrydata = { "demo", "11", "56" };

                    dataTables[0] = PrepareDataTable(FetchDurationCountryColumnNames, FetchDurationCountrydataTypes, FetchDurationCountrydata);
                    break;
                case "USP_FetchDurationByCategory":
                    string[] FetchDurationByCategoryColumnNames = { "Service", "Duration_Group_Code", "Global_Duration_group_Description", "Global_Duration_group_Sequence", "Global_Duration_group_line_code", "Id", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy" };
                    string[] FetchDurationByCategorydataTypes = { "int", "string", "string", "string", "string", "int", "datetime", "int", "datetime", "int" };
                    object[] FetchDurationByCategorydata = { 2, "d1", "1-2 minutes", "2", "12", 140, "NULL", "NULL", "NULL", "NULL" };
                    string[] FetchDurationByCategoryColumnNames1 = { "Service", "Duration_group_code", "Country", "Local_Duration_group_Sequence", "Local_Duration_group_line_code", "Global_Duration_group_Description", "Local_Duration_group_Description", "Id", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy", "Service", "Duration_Group_Code", "Global_Duration_group_Description", "Global_Duration_group_Sequence", "Global_Duration_group_line_code", "Id", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy" };
                    string[] FetchDurationByCategorydataTypes1 = { "int", "string", "string", "string", "string", "string", "string", "int", "datetime", "int", "datetime", "int", "int", "string", "string", "string", "string", "int", "datetime", "int", "datetime", "int" };
                    object[] FetchDurationByCategorydata1 = { 2, "d1", "GB", "2", "12", "NULL", "1 - 2 minutes", "5286", "NULL", "NULL", "NULL", "NULL", 2, "d1", "1 - 2 minutes", "2", "12", 140, "NULL", "NULL", "NULL", "NULL" };

                    dataTables[0] = PrepareDataTable(FetchDurationByCategoryColumnNames, FetchDurationByCategorydataTypes, FetchDurationByCategorydata);
                    dataTables[1] = PrepareDataTable(FetchDurationByCategoryColumnNames1, FetchDurationByCategorydataTypes1, FetchDurationByCategorydata1);
                    break;

                case "USP_GetDuration":
                    string[] GetDurationColumnNames = { "Service","Duration_Group_Code","Global_Duration_group_Description","Global_Duration_group_Sequence","Global_Duration_group_line_code","Id","CreatedDate","CreatedBy","UpdatedDate","UpdatedBy" };
                    string[] GetDurationdataTypes = { "int","string","string","string","string","int","datetime","int","datetime","int" };
                    object[] GetDurationdata = { 2,"d1","1-2 minutes","2","12",	140,"NULL","NULL","NULL","NULL" };
                    string[] GetDurationColumnNames1 = { "Service","Duration_group_code","Country","Local_Duration_group_Sequence","Local_Duration_group_line_code","Global_Duration_group_Description","Local_Duration_group_Description","Id","CreatedDate","CreatedBy","UpdatedDate","UpdatedBy" };
                    string[] GetDurationdataTypes1 = { "int","string","string","string","string","string","string","int","datetime","int","datetime","int" };
                    object[] GetDurationdata1 = { 2,"d1","GB","1","11","NULL","less than a minute",5285,"NULL","NULL","NULL","NULL" };

                    dataTables[0] = PrepareDataTable(GetDurationColumnNames, GetDurationdataTypes, GetDurationdata);
                    dataTables[1] = PrepareDataTable(GetDurationColumnNames1, GetDurationdataTypes1, GetDurationdata1);
                    break;

                //BLLDictionary
                case "USP_GetExportedDictionaryDetails":
                    string[] ExportedDictionaryColumnNames = { "Id","ExportFileName","ExportStatusId","BlobContainerPath","ExportVirtualPath" };
                    string[] ExportedDictionarydataTypes = { "int","string","int","string","string" };
                    object[] ExportedDictionarydata = { 183,"xxx.dat",6,"comtech - dataexport","NULL"};

                    dataTables[0] = PrepareDataTable(ExportedDictionaryColumnNames, ExportedDictionarydataTypes, ExportedDictionarydata);
                    break;

                //BLLOthersCoding
                case "USP_GetMasterOthersCodingControl":
                    string[] MasterOthersCodingControlColumnNames = { "Name","ServiceId","Id","Shared_Answer","Category","Variable","Active","FilterVariable","FilterWhere"};
                    string[] MasterOthersCodingControldataTypes = { "string","int","int","string","string","string","char","string","string" };
                    object[] MasterOthersCodingControldata = { "Mobile TopUp Shops", 1, 13, "topup_shop", "02", "otherQ06", "1", "NULL", "NULL" };

                    dataTables[0] = PrepareDataTable(MasterOthersCodingControlColumnNames, MasterOthersCodingControldataTypes, MasterOthersCodingControldata);
                    break;

                case "USP_GetMasterOthersCodingByServiceId":
                    string[] MasterOthersCodingByServiceIdColumnNames = { "Id","ServiceId","Name","FileName" };
                    string[] MasterOthersCodingByServiceIddataTypes = { "int","int","string","string" };
                    object[] MasterOthersCodingByServiceIddata = { 15,1,"Wearable Shops","wearshops.csv" };

                    dataTables[0] = PrepareDataTable(MasterOthersCodingByServiceIdColumnNames, MasterOthersCodingByServiceIddataTypes, MasterOthersCodingByServiceIddata);
                    break;

                case "USP_GetOthersDetails2":
                    string[] OthersDetails2ColumnNames = { "ID", "Description", "Answer_code", "ValidPeriod", "Global_Answer", "CreatedDate", "UpdatedDate" };
                    string[] OthersDetails2dataTypes = { "int", "string", "string", "int", "string", "datetime", "datetime" };
                    object[] OthersDetails2data = { 769756, "HTC, EVO", "3155",200101, "HTC~Evo 4G", "NULL", "NULL" };

                    dataTables[0] = PrepareDataTable(OthersDetails2ColumnNames, OthersDetails2dataTypes, OthersDetails2data);
                    break;


                //BLLExtraDefinitions
                case "USP_FetchDataRefExtraDefIDByCountry":
                    string[] FetchDataRefColumnNames = { "ServiceId", "ExtDefinitionID", "Sequence", "ExtraID", "Code", "Global_Desc", "Comment", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy" };
                    string[] FetchDataRefDataTypes = { "int","int","float","string","string","string","string","datetime","int","datetime","int" };
                    object[] FetchDataRefData = { 1,44121,10,"Dynamic_set","101_10","md_prodtrakcomp1|'0'","NULL","NULL","NULL","NULL","NULL" };

                    string[] FetchDataRefColumnNames1 = { "ServiceId","Sequence","ExtDefinitionId","Country","Local_Desc","Id","CreatedDate","CreatedBy","UpdatedDate","UpdatedBy" };
                    string[] FetchDataRefDataTypes1 = { "int","float","int","string","string","int","datetime","int","datetime","int" };
                    object[] FetchDataRefData1 = { 1,10,44121,"AR","md_prodtrakcomp1|'0'",591153,"NULL","NULL","NULL","NULL" };

                    dataTables[0] = PrepareDataTable(FetchDataRefColumnNames, FetchDataRefDataTypes, FetchDataRefData);
                    dataTables[1] = PrepareDataTable(FetchDataRefColumnNames1, FetchDataRefDataTypes1, FetchDataRefData1);
                    break;

                case "USP_GetExtraDefsForExcel":
                    string[] ExtraDefsForExcelColumnNames = { "Sequence","ExtraID","Code","Global_Desc","ExtDefinitionID","CreatedDate","CreatedBy","UpdatedDate","UpdatedBy" };
                    string[] ExtraDefsForExcelDataTypes = { "float","string","string","string","int","datetime","int","datetime","int" };
                    object[] ExtraDefsForExcelData = { 50,"Placeholder","411","401,provider_land",44732,"NULL","NULL","2017-01-03 13:49:07.727",2685 };

                    string[] ExtraDefsForExcelColumnNames1 = { "Local_Desc","Country","ExtDefinitionID","Sequence" };
                    string[] ExtraDefsForExcelDataTypes1 = { "string","string","int","float" };
                    object[] ExtraDefsForExcelData1 = { "401,provider_land","US",44732,50 };

                    dataTables[0] = PrepareDataTable(ExtraDefsForExcelColumnNames, ExtraDefsForExcelDataTypes, ExtraDefsForExcelData);
                    dataTables[1] = PrepareDataTable(ExtraDefsForExcelColumnNames1, ExtraDefsForExcelDataTypes1, ExtraDefsForExcelData1);
                    break;

                case "USP_GetEventsForExcel":
                    string[] EventsForExcelColumnNames = { "ServiceId","Event_Code","Global_Event_Sequence","Global_Event_Description","CreatedDate","CreatedBy","UpdatedDate","UpdatedBy" };
                    string[] EventsForExcelDataTypes = { "int","string","int","string","datetime","int","datetime","int" };
                    object[] EventsForExcelData = { 3,"e1",1,"Event 1","NULL","NULL","NULL","NULL" };

                    string[] EventsForExcelColumnNames1 = { "Local_Event_Description","Country","Event_Code" };
                    string[] EventsForExcelDataTypes1 = { "string","string","string" };
                    object[] EventsForExcelData1 = { "4","US","e4"};

                    dataTables[0] = PrepareDataTable(EventsForExcelColumnNames, EventsForExcelDataTypes, EventsForExcelData);
                    dataTables[1] = PrepareDataTable(EventsForExcelColumnNames1, EventsForExcelDataTypes1, EventsForExcelData1);
                    break;

                //BLLMasterAnswer.cs
                case "USP_CheckDuplicate":
                    string[] CheckDuplicateColumnNames = { "DupCheck"};
                    string[] CheckDuplicateDataTypes = { "int"};
                    object[] CheckDuplicateData = { 0 };

                    string[] CheckDuplicateColumnNames1 = { "Global_Ans_Code1" };
                    string[] CheckDuplicateDataTypes1 = { "int" };
                    object[] CheckDuplicateData1 = { 1 };

                    dataTables[0] = PrepareDataTable(CheckDuplicateColumnNames, CheckDuplicateDataTypes, CheckDuplicateData);
                    dataTables[1] = PrepareDataTable(CheckDuplicateColumnNames1, CheckDuplicateDataTypes1, CheckDuplicateData1);
                    break;

                case "USP_getAttrListForAns":
                    string[] AttrListForAnsColumnNames = { "ServiceID","Shared_Answer","Ans_Code1","Attribute_Number","Attribute_Value","Attributes" };
                    string[] AttrListForAnsDataTypes = { "int","string","int","int","string","string" };
                    object[] AttrListForAnsData = { 1, "phonelist", 14557, 12, "4", "ScreenSize" };

                    dataTables[0] = PrepareDataTable(AttrListForAnsColumnNames, AttrListForAnsDataTypes, AttrListForAnsData);
                    break;

                //BLLMobileManufacturer.cs
                case "USP_FetchMObileManuifacturer":
                    string[] MobileManuifacturerColumnNames = { "ID", "Global_Answer", "Global_Ans_Code1" };
                    string[] MobileManuifacturerDataTypes = { "int", "string", "string" };
                    object[] MobileManuifacturerData = { 1, "aoledior", "1" };

                    dataTables[0] = PrepareDataTable(MobileManuifacturerColumnNames, MobileManuifacturerDataTypes, MobileManuifacturerData);
                    break;

                case "USP_GetAllAttributes":
                    string[] AttributesColumnNames = { "Order", "Attributes", "Attribute_Number" };
                    string[] AttributesDataTypes = { "int", "string", "int" };
                    object[] AttributesData = { 17, "Att_complete", 11 };

                    dataTables[0] = PrepareDataTable(AttributesColumnNames, AttributesDataTypes, AttributesData);
                    break;

                //BLLOperators.cs
                case "USP_GetOperatorsForExcel":
                    string[] OperatorsColumnNames = { "Shared_Answer", "Global_Ans_Code1", "Global_Ans_Ref", "Global_Answer", "Ans_Order", "CreatedDate", "CreatedBy", "UpdatedDate", "UpdatedBy" };
                    string[] OperatorsDataTypes = { "string","string","string","string","int","datetime","int","datetime","int" };
                    object[] OperatorsData = { "network","569","mob_op_569","@NetHome",10,"NULL","NULL","2016-06-10 18:11:26.470",2599 };
                    
                    string[] OperatorsColumnNames1 = { "Local_Answer","Country","Global_Ans_Ref","Shared_Answer","Local_STD_Export","Local_STD_Override" };
                    string[] OperatorsDataTypes1 = { "string","string","string","string","string","string" };
                    object[] OperatorsData1 = { "RCN Online","US","tv_op_654","network_tv","0","0" };

                    dataTables[0] = PrepareDataTable(OperatorsColumnNames, OperatorsDataTypes, OperatorsData);
                    dataTables[1] = PrepareDataTable(OperatorsColumnNames1, OperatorsDataTypes1, OperatorsData1);
                    break;

                case "USP_FetchOperators":
                    string[] FetchOperatorsColumnNames = { "Operator Code","Ans_Order","Operator" };
                    string[] FetchOperatorsDataTypes = { "int", "int", "string"};
                    object[] FetchOperatorsData = { 569,10,"@NetHome" };

                    string[] FetchOperatorsColumnNames1 = { "Col" };
                    string[] FetchOperatorsDataTypes1 = { "string" };
                    object[] FetchOperatorsData1 = { "network" };

                    dataTables[0] = PrepareDataTable(FetchOperatorsColumnNames, FetchOperatorsDataTypes, FetchOperatorsData);
                    dataTables[1] = PrepareDataTable(FetchOperatorsColumnNames1, FetchOperatorsDataTypes1, FetchOperatorsData1);
                    break;


                //BLLPowerView
                case "USP_Power_GetAnswerList":
                    string[] AnswerListColumnNames = { "qus_code" };
                    string[] AnswerListDataTypes = { "string" };
                    object[] AnswerListData = { "10101" };

                    dataTables[0] = PrepareDataTable(AnswerListColumnNames, AnswerListDataTypes, AnswerListData);
                    break;

                //BLLQuestions

                case "USP_GetQuestionsForExcel":
                    string[] QuestionsColumnNames = { "Service","Cat_Code","Global_Sequence","Global_Qus_Code","Qus_Type","Global_Question_Desc","Global_Qus_NextQ","Global_Question_Fillin","Global_Question_Help","Qus_Start_New_Line","Global_Qus_Var_1","Global_Qus_Var_2","Global_Qus_Var_3","Global_Qus_Var_4","Global_Qus_Var_5","Global_Qus_Var_6","Global_Qus_Var_7","Qus_sort_logic","Validate","Parameter","Global_Placeholder","display","Global_prefill","Global_hide","Shared_Answer","id","CreatedDate","CreatedBy","UpdatedDate","UpdatedBy" };
                    string[] QuestionsDataTypes = { "int","string","int","string","string","string","string","string","string","char","string","string","string","string","string","string","string","char","string","string","string","string","string","string","string","int","datetime","int","datetime","int" };
                    object[] QuestionsData = { 1,"07",20,"7_2","VRS","07010 - What is the sex of the person you bought the phone for?","7_3","","","","sex","","","","","","","U","","","","","","","sex","101844","NULL","NULL","NULL","NULL" };

                    string[] QuestionsColumnNames1 = { "qus_code" };
                    string[] QuestionsDataTypes1 = { "string" };
                    object[] QuestionsData1 = { "12_24" };

                    string[] QuestionsColumnNames2 = { "CountryCode" };
                    string[] QuestionsDataTypes2 = { "string" };
                    object[] QuestionsData2 = { "UK" };



                    dataTables[0] = PrepareDataTable(QuestionsColumnNames, QuestionsDataTypes, QuestionsData);
                    dataTables[1] = PrepareDataTable(QuestionsColumnNames1, QuestionsDataTypes1, QuestionsData1);
                    dataTables[2] = PrepareDataTable(QuestionsColumnNames2, QuestionsDataTypes2, QuestionsData2);
                    break;


            }
            #endregion

            #region Prepare Dataset
            foreach (DataTable dt in dataTables)
            {
                if(dt != null && dt.Rows.Count > 0)
                {
                    dataSet.Tables.Add(dt);
                }
            }
            #endregion

            return dataSet;
        }

        public static DataTable GenerateDataTable(string spName)
        {
            DataTable dt = new DataTable();

            #region sample code
           
            if (spName == "USP_GetEmployee")
            {
                string[] columnNames = { "Id", "Name", "Age", "Salary" };
                string[] dataTypes   = { "int", "string", "int", "int" };
                object[] data        = { 1, "Jash", 25, 25000};

                dt = PrepareDataTable(columnNames, dataTypes, data);
            }
            #endregion
            return dt;
        }

        private static DataTable PrepareDataTable(string[] columnNames, string[] dataType, object[] data)
        {
            DataTable dt = new DataTable();
            DataColumn dCol;
            DataRow dataRecord;

            //Preparing Datatable
            for (int i = 0; i < columnNames.Length; i++)
            {
                switch (dataType[i])
                {
                    case "int": dCol = new DataColumn(columnNames[i], typeof(int)); break;
                    case "string": dCol = new DataColumn(columnNames[i], typeof(string)); break;
                    case "char": dCol = new DataColumn(columnNames[i], typeof(char)); break;
                    case "double": dCol = new DataColumn(columnNames[i], typeof(double)); break;
                    case "float": dCol = new DataColumn(columnNames[i], typeof(float)); break;
                    case "datetime": dCol = new DataColumn(columnNames[i], typeof(DateTime)); break;

                    default: dCol = new DataColumn(columnNames[i], typeof(object)); break;
                }

                dt.Columns.Add(dCol);
            }

            //Adding data to datatable
            dataRecord = dt.NewRow();
            for (int i = 0; i < data.Length; i++)
            {
                dataRecord[i] = data[i];
            }

            dt.Rows.Add(dataRecord);

            return dt;
        }
    }
}
