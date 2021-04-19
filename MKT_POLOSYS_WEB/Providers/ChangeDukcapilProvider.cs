using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_WEB.DataAccess;
using MKT_POLOSYS_WEB.Models;
using MKT_POLOSYS_WEB.Models.UploadTaskInquiry;
using Newtonsoft.Json;
using POLO_EXTENSION;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MKT_POLOSYS_WEB.Providers
{
    public class ChangeDukcapilProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();

        public List<ListTaskViewModel> get(ParamListDetail model)
        {
            List<ListTaskViewModel> ListData = new List<ListTaskViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var pPriorityLevel = string.Join(",", model.pPriorityLevel);
                //foreach (var item in model.pPriorityLevel)
                //{
                //    pPriorityLevel = pPriorityLevel + "|" + item;
                //}
                //Declare COnnection                
                var querySstring = "spMKT_POLO_TASK_INQUIRY_DUKCAPIL_LIST";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Define Query Parameter
                command.Parameters.AddWithValue("@pRegion", model.pRegion);
                command.Parameters.AddWithValue("@pFPName", model.pFPName);
                command.Parameters.AddWithValue("@pBranchName", model.pBranchName);
                command.Parameters.AddWithValue("@pEmpPosition", model.pEmpPosition);
                command.Parameters.AddWithValue("@pTaskID", model.pTaskID);
                command.Parameters.AddWithValue("@pStatProspek", model.pStatProspek);
                command.Parameters.AddWithValue("@pAppID", model.pAppID);
                command.Parameters.AddWithValue("@pPriorityLevel", pPriorityLevel);
                command.Parameters.AddWithValue("@pCustName", model.pCustName);
                command.Parameters.AddWithValue("@pStatDukcapil", model.pStatDukcapil);
                command.Parameters.AddWithValue("@pSdate", model.pSdate);
                command.Parameters.AddWithValue("@pEdate", model.pEdate);
                command.Parameters.AddWithValue("@pSourceData", model.pSourceData);
                command.Parameters.AddWithValue("@pEmpNo", model.pEmpNo);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    ListTaskViewModel data = new ListTaskViewModel();
                    data.sourceData = rd[0].ToString();
                    data.cabang = rd[1].ToString();
                    data.regional = rd[2].ToString();
                    data.taskID = rd[3].ToString();
                    data.jenisTask = rd[4].ToString();
                    data.customerID = rd[5].ToString();
                    data.customerName = rd[6].ToString();
                    try
                    {

                        data.distributedDT = Convert.ToDateTime(rd[7].ToString()).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        data.distributedDT = rd[7].ToString();
                    }
                    try
                    {

                        data.startedDT = Convert.ToDateTime(rd[8].ToString()).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        data.startedDT = rd[8].ToString();
                    }
                    data.slaRemaining = rd[9].ToString();
                    data.fieldPersonName = rd[10].ToString();
                    data.empPosition = rd[11].ToString();
                    data.statusProspek = rd[12].ToString();
                    data.priorityLevel = rd[13].ToString();
                    data.aplikasiAI = rd[14].ToString();
                    data.applicationID = rd[15].ToString();
                    data.statusMSS = rd[16].ToString();
                    data.statusWISE = rd[17].ToString();
                    data.statusDukcapil = rd[18].ToString();
                    data.soa = rd[19].ToString();
                    data.referentorName = rd[20].ToString();
                    data.referentorName2 = rd[21].ToString();
                    data.orderInID = rd[22].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }





        public List<DownloadDukcapilViewModel> ListDownload(string pRegion,
            string pFPName, string pBranchName, string pEmpPosition,
            string pTaskID, string pStatProspek, string pAppID, string[] pPriorityLevelarr,
            string pCustName, string pStatDukcapil, string pSdate, string pEdate,
            string pSource, string pSourceData, string pEmpNo)
        {
            List<DownloadDukcapilViewModel> ListData = new List<DownloadDukcapilViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var pPriorityLevel = string.Join(",", pPriorityLevelarr);
                //Declare COnnection                
                var querySstring = "spMKT_POLO_TASK_INQUIRY_DUKCAPIL_ALL";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@pRegion", pRegion);
                command.Parameters.AddWithValue("@pFPName", pFPName);
                command.Parameters.AddWithValue("@pBranchName", pBranchName);
                command.Parameters.AddWithValue("@pEmpPosition", pEmpPosition);
                command.Parameters.AddWithValue("@pTaskID", pTaskID);
                command.Parameters.AddWithValue("@pStatProspek", pStatProspek);
                command.Parameters.AddWithValue("@pAppID", pAppID);
                command.Parameters.AddWithValue("@pPriorityLevel", pPriorityLevel);
                command.Parameters.AddWithValue("@pCustName", pCustName);
                command.Parameters.AddWithValue("@pStatDukcapil", pStatDukcapil);
                command.Parameters.AddWithValue("@pSdate", pSdate);
                command.Parameters.AddWithValue("@pEdate", pEdate);
                command.Parameters.AddWithValue("@pSourceData", pSourceData);
                command.Parameters.AddWithValue("@pEmpNo", pEmpNo);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DownloadDukcapilViewModel data = new DownloadDukcapilViewModel();
                    data.TaskID = rd[0].ToString();
                    data.NIK = rd[1].ToString();
                    data.CustomerName = rd[2].ToString();
                    data.TempatLahir = rd[3].ToString();
                    data.TglLahir = rd[4].ToString();

                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }

        public List<DownloadDukcapilViewModel> ListDownloadDetail(int pID, string pEmpNo)
        {
            List<DownloadDukcapilViewModel> ListData = new List<DownloadDukcapilViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_TASK_INQUIRY_DUKCAPIL";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@pID", pID);
                command.Parameters.AddWithValue("@pEmpNo", pEmpNo);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DownloadDukcapilViewModel data = new DownloadDukcapilViewModel();
                    data.TaskID = rd[0].ToString();
                    data.NIK = rd[1].ToString();
                    data.CustomerName = rd[2].ToString();
                    data.TempatLahir = rd[3].ToString();
                    data.TglLahir = rd[4].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }

        public List<DropdownListViewModel> ddlRegion()
        {

            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DDL_REGION";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DropdownListViewModel data = new DropdownListViewModel();
                    data.Text = rd[0].ToString();
                    data.Value = rd[1].ToString();
                    data.Filter = rd[2].ToString();
                    data.Filter2 = rd[3].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }


        public List<DropdownListViewModel> ddlBranch()
        {
            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DDL_BRANCH";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DropdownListViewModel data = new DropdownListViewModel();
                    data.Text = rd[0].ToString();
                    data.Value = rd[1].ToString();
                    data.Filter = rd[2].ToString();
                    data.Filter2 = rd[3].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
        public List<DropdownListViewModel> ddlEmpPosition(string type)
        {
            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DDL_EMP_POSITION";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DropdownListViewModel data = new DropdownListViewModel();
                    data.Text = rd[0].ToString();
                    data.Value = rd[1].ToString();
                    data.Filter = rd[2].ToString();
                    data.Filter2 = rd[3].ToString();
                    if (rd[1].ToString() == type)
                    {
                        ListData.Add(data);
                    }
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
        public List<DropdownListViewModel> ddlStsProspek()
        {
            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DDL_STS_PROSPEK";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DropdownListViewModel data = new DropdownListViewModel();
                    data.Text = rd[0].ToString();
                    data.Value = rd[1].ToString();
                    data.Filter = rd[2].ToString();
                    data.Filter2 = rd[3].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
        public List<DropdownListViewModel> ddlPriorityLevel(string source, string emp, string prospec)
        {
            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DDL_PRIORITY_LVL";
                //Define Query Parameter
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pSource", source);
                command.Parameters.AddWithValue("@pEmp", emp);
                command.Parameters.AddWithValue("@pProspec", prospec);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DropdownListViewModel data = new DropdownListViewModel();
                    data.Text = rd[0].ToString();
                    data.Value = rd[1].ToString();
                    data.Filter = rd[2].ToString();
                    data.Filter2 = rd[3].ToString();
                    data.Filter3 = rd[4].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
        public List<DropdownListViewModel> ddlStatusDukcapil()
        {
            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DDL_STS_DUKCAPIL";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DropdownListViewModel data = new DropdownListViewModel();
                    data.Text = rd[0].ToString();
                    data.Value = rd[1].ToString();
                    data.Filter = rd[2].ToString();
                    data.Filter2 = rd[3].ToString();
                    if (rd[0].ToString() == "NOT MATCH")
                    {
                        ListData.Add(data);
                    }
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
        public List<DropdownListViewModel> ddlSourceData()
        {
            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DDL_SOURCE_DATA";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DropdownListViewModel data = new DropdownListViewModel();
                    data.Text = rd[0].ToString();
                    data.Value = rd[1].ToString();
                    data.Filter = rd[2].ToString();
                    data.Filter2 = rd[3].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
        public bool validasiDownload(string empNo)
        {
            bool isSucceed = true;
            var connectionString = context.Database.GetDbConnection().ConnectionString; var userType = validasiUserType(empNo);
            //validasi belum download 
            if (userType == "TELESALES")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Declare COnnection                
                    var querySstring = "spMKT_POLO_CHECK_VALIDASI_DOWNLOAD_UPLOAD";
                    SqlCommand command = new SqlCommand(querySstring, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //Define Query Parameter
                    command.Parameters.AddWithValue("@pEmpNo", empNo);
                    command.Parameters.AddWithValue("@type", "DOWNLOAD");
                    //open Connection
                    command.Connection.Open();

                    //PRoses Sp
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        isSucceed = Convert.ToBoolean(rd[0].ToString());
                    }

                    //Connection Close
                    command.Connection.Close();

                }
            }
            else
            {
                isSucceed = false;
            }

            return isSucceed;

        }

        public List<DownloadDukcapilViewModel> UploadData(DownloadDukcapilViewModel model, string guid)
        {
            List<DownloadDukcapilViewModel> ListData = new List<DownloadDukcapilViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var querySstring = "spMKT_POLO_UPLOAD_STATUS_DUKCAPIL_TASK";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Define Query Parameter
                command.Parameters.AddWithValue("@pTaskID", model.TaskID);
                command.Parameters.AddWithValue("@pCustName", model.CustomerName);
                command.Parameters.AddWithValue("@pIdNo", model.NIK);
                command.Parameters.AddWithValue("@pBirthPlace", model.TempatLahir);
                command.Parameters.AddWithValue("@pBirthDt", model.TglLahir);
                command.Parameters.AddWithValue("@pQueueUid", guid);
                command.Parameters.AddWithValue("@pEmpNo", model.EmpNo);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                }
                //Connection Close
                command.Connection.Close();
            }
            return ListData;

        }

        public async Task SendApiCekDukcapil(string sGUID)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            string url = "";
            SqlCommand command = new SqlCommand();
            command.Connection = new SqlConnection(connectionString);
            command.CommandText = @"SELECT PARAMETER_VALUE FROM M_MKT_POLO_PARAMETER WHERE PARAMETER_ID = 'URL_MKT_POLO_API_DUKCAPIL_FROM POLO'";
            command.CommandType = CommandType.Text;
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                url = dr[0].ToString();
            }

            dr.Close();
            command.Connection.Close();

            string bodyJSON = JsonConvert.SerializeObject(new { dataSource = "UPLOAD", queueUID = sGUID }, Formatting.None);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(new Uri(url), content);
        }


        public async Task SendApiToWiseMSS(string guid)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var querySstring = "spMKT_POLO_SEND_TO_WISE_CHECK";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Define Query Parameter
                command.Parameters.AddWithValue("@pQueueUid", guid);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                SendDataPreparation send = new SendDataPreparation(connectionString);
                while (rd.Read())
                {
                    var TaskID = rd[0].ToString();
                    //Declare COnnection      
                    using (SqlConnection connection2 = new SqlConnection(connectionString))
                    {
                        var querySstring2 = @"
                    INSERT INTO T_MKT_POLO_LOG_DUKCAPIL ([KEY],[VALUE],[DESCRIPTION],[DATE]) VALUES('start','SendDataPreparation','" + TaskID + "','" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "')";
                        SqlCommand command2 = new SqlCommand(querySstring2, connection2);
                        //open Connection
                        command2.Connection.Open();
                        //PRoses Sp
                        SqlDataReader rd2 = command2.ExecuteReader();
                        command2.Connection.Close();
                    }
                    await send.startProcess(TaskID);
                }
                rd.Close();
                //Connection Close
                command.Connection.Close();
            }
        }



        public bool validasiUpload(string empNo)
        {
            bool isSucceed = true;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            var userType = validasiUserType(empNo);
            //validasi belum download 
            if (userType == "TELESALES")
            {
                var validDownload = true;
                var validUpload = true;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Declare COnnection                
                    var querySstring = "spMKT_POLO_CHECK_VALIDASI_DOWNLOAD_UPLOAD";
                    SqlCommand command = new SqlCommand(querySstring, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //Define Query Parameter
                    command.Parameters.AddWithValue("@pEmpNo", empNo);
                    command.Parameters.AddWithValue("@type", "UPLOAD");
                    //open Connection
                    command.Connection.Open();

                    //PRoses Sp
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        isSucceed = Convert.ToBoolean(rd[0].ToString());
                    }

                    //Connection Close
                    command.Connection.Close();

                }
            }
            else
            {
                isSucceed = false;
            }
            return isSucceed;

        }

        public bool validasiUser(string empNo)
        {
            bool isSucceed = true;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"select CASE WHEN COUNT(EMP_NO) > 0 THEN cast(1 as  bit) else cast(0 as  bit) end as validasi from confins.dbo.REF_EMP where EMP_NO='" + empNo + "'";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    isSucceed = Convert.ToBoolean(rd[0].ToString());
                }
                //Connection Close
                command.Connection.Close();

            }

            return isSucceed;

        }

        public string validasiUserType(string empNo)
        {
            string typeUser = "";
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_CHECK_TYPE_USER";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Define Query Parameter
                command.Parameters.AddWithValue("@pEmpNo", empNo);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    typeUser = rd[0].ToString();
                }

                //Connection Close
                command.Connection.Close();

            }

            return typeUser;

        }

        public string getUser(string empNo)
        {
            string empName = string.Empty;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"select EMP_NAME as name from confins.dbo.REF_EMP where EMP_NO='" + empNo + "'";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    empName = rd[0].ToString();
                }
                //Connection Close
                command.Connection.Close();

            }

            return empName;

        }

        public List<ListLog> getLog(string pGuid)
        {
            List<ListLog> listData = new List<ListLog>();
            bool isSucceed = true;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"
select TASK_ID,UPLOAD_MESSAGE from WISE_STAGING.dbo.T_MKT_POLO_UPLOAD where UPLOAD_STS=0 and QUEUE_UID='" + pGuid + "'";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ListLog data = new ListLog();
                    data.TASK_ID = rd[0].ToString();
                    data.UPLOAD_MESSAGE = rd[1].ToString();
                    listData.Add(data);
                }
                //Connection Close
                command.Connection.Close();

            }

            return listData;

        }

        public async Task<string> getLoopDukcapil(string pGuid)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"
                    INSERT INTO T_MKT_POLO_LOG_DUKCAPIL ([KEY],[VALUE],[DESCRIPTION],[DATE]) VALUES('start','getLoopDukcapil','lop','" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "')";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();
                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                command.Connection.Close();
            }
            string result = "not done";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                while (result != "done")
                {
                    //Declare COnnection                
                    var querySstring = @"DECLARE @count INT "+
                                "DECLARE @done INT "+
                                "SELECT @count = COUNT(1) "+
                                "FROM T_MKT_POLO_DUKCAPIL_CHECK_QUEUE "+
                                "where QUEUE_UID='" + pGuid + "' " +
                                "SELECT @done = COUNT(RESPONSE_CODE) " +
                                "FROM T_MKT_POLO_DUKCAPIL_CHECK_QUEUE " +
                                "WHERE RESPONSE_CODE IS NOT NULL " +
                                "AND QUEUE_UID='" + pGuid + "' SELECT CASE WHEN @count = @done THEN 'done' ELSE 'not done' END as cekDukcapil";
                    SqlCommand command = new SqlCommand(querySstring, connection);
                    //open Connection
                    command.Connection.Open();

                    //PRoses Sp
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        result = rd[0].ToString();
                    }
                    //Connection Close
                    command.Connection.Close();
                    await Task.Delay(1500);
                }

            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"
                    INSERT INTO T_MKT_POLO_LOG_DUKCAPIL ([KEY],[VALUE],[DESCRIPTION],[DATE]) VALUES('end','getLoopDukcapil','lop','" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "')";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();
                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                command.Connection.Close();
            }
            return result;
        }

        public List<DownloadDukcapilViewModel> getResultDukcapil(string pGuid)
        {
            List<DownloadDukcapilViewModel> listData = new List<DownloadDukcapilViewModel>();
            bool isSucceed = true;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"
 select TASK_ID,ID_NO,CUST_NAME,BIRTH_PLACE,BIRTH_DT from WISE_STAGING.dbo.T_MKT_POLO_UPLOAD where UPLOAD_STS=1 and QUEUE_UID='" + pGuid + "'";
                SqlCommand command = new SqlCommand(querySstring, connection);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    DownloadDukcapilViewModel data = new DownloadDukcapilViewModel();
                    data.TaskID = rd[0].ToString();
                    data.NIK = rd[1].ToString();
                    data.CustomerName = rd[2].ToString();
                    data.TempatLahir = rd[3].ToString();
                    data.TglLahir = Convert.ToDateTime(rd[4].ToString()).ToString("dd/MM/yyyy");
                    listData.Add(data);
                }
                //Connection Close
                command.Connection.Close();

            }

            return listData;

        }

    }
}

