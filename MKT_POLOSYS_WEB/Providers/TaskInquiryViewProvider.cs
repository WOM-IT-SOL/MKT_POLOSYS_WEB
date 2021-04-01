using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_WEB.DataAccess;
using MKT_POLOSYS_WEB.Models;
using MKT_POLOSYS_WEB.Models;
using POLO_EXTENSION;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace MKT_POLOSYS_WEB.Providers
{
    public class TaskInquiryViewProvider
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
                var querySstring = "spMKT_POLO_TASK_INQUIRY_LIST_VIEW";
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
                    data.distributedDT = rd[7].ToString();
                    data.startedDT = rd[8].ToString();
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
        public List<DropdownListViewModel> ddlEmpPosition()
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
                    ListData.Add(data);
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
        public List<DropdownListViewModel> ddlPriorityLevel(string source,string emp, string prospec)
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
                    ListData.Add(data);
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
        public bool  validasiDownload(string empNo)
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
    }
}

