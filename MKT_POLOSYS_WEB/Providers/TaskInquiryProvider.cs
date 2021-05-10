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
    public class TaskInquiryProvider
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
                var querySstring = "spMKT_POLO_TASK_INQUIRY_LIST";
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
                    //penambahanArif
                    data.referentorCode = rd[23].ToString();
                    data.referentorCode2 = rd[24].ToString();
                    data.negativeCust = rd[25].ToString();
                    data.orderNo = rd[26].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }


        public ActionResult<CreateEditViewModel> getView(int id)
        {

            CreateEditViewModel data = new CreateEditViewModel();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_DETAIL_DATA_TASK";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@pID", id);

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    data.TaskID = rd[0].ToString();
                    data.JenisTask = rd[1].ToString();
                    data.CustID = rd[2].ToString();
                    data.CustomerName = rd[3].ToString();
                    try
                    {

                        data.DistributedDate = Convert.ToDateTime(rd[4].ToString()).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        data.DistributedDate = rd[4].ToString();
                    }
                    try
                    {

                        data.StartedDate = Convert.ToDateTime(rd[5].ToString()).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        data.StartedDate = rd[5].ToString();
                    }
                    data.StatusDukcapil = rd[6].ToString();
                    data.FieldPersonName = rd[7].ToString();
                    data.EmpPosition = rd[8].ToString();
                    data.StatusProspek = rd[9].ToString();
                    data.PriorityLevel = rd[10].ToString();
                    data.AplikasiIA = rd[11].ToString();
                    data.AplicationID = rd[12].ToString();
                    data.SourceData = rd[13].ToString();
                    data.NIK = rd[14].ToString();

                    //---
                    data.TempatLahir = rd[15].ToString();
                    try
                    {

                        data.TglLahir = Convert.ToDateTime(rd[16].ToString()).ToString("dd MMM yyyy");
                    }
                    catch
                    {
                        data.TglLahir = rd[16].ToString();
                    }
                    data.AlamatLeg = rd[17].ToString();
                    data.ProvLeg = rd[18].ToString();
                    data.KabLeg = rd[19].ToString();
                    data.KecLeg = rd[20].ToString();
                    data.KelLeg = rd[21].ToString();
                    data.RTLeg = rd[22].ToString();
                    data.RWLeg = rd[23].ToString();
                    data.AlamatRes = rd[24].ToString();
                    data.ProvRes = rd[25].ToString();
                    data.KabRes = rd[26].ToString();
                    data.KecRes = rd[27].ToString();
                    data.KelRes = rd[28].ToString();
                    data.RTRes = rd[29].ToString();
                    data.RWRes = rd[30].ToString();
                    data.KodePosRes = rd[31].ToString();
                    data.SubZipcodeRes = rd[32].ToString();
                    data.Product = rd[33].ToString();
                    data.ItemType = rd[34].ToString();
                    data.ItemYear = rd[35].ToString();
                    data.OtrPrice =  Convert.ToDecimal(rd[36].ToString()).ToString("#,##0.00");
                    data.DP =  rd[37].ToString() == "" ? "-" : Convert.ToDecimal(rd[37].ToString()).ToString("#,##0.00");
                    data.LTV = rd[38].ToString();
                    data.Tenor = rd[39].ToString();
                    data.Plafond = rd[40].ToString() == "" ? "-" : Convert.ToDecimal(rd[40].ToString()).ToString("#,##0.00");
                    data.MonthInstallment = rd[41].ToString() == "" ? "-" :  Convert.ToDecimal(rd[41].ToString()).ToString("#,##0.00");
                    data.Notes = rd[42].ToString();
                    data.KodePosLeg = rd[43].ToString();
                    data.SubZipcodeLeg = rd[44].ToString();
                }

                //Connection Close
                command.Connection.Close();

            }
            return data;

        }


        public List<ListTaskDetailViewModel> getDetail(string taskID)
        { 
            List<ListTaskDetailViewModel> ListData = new List<ListTaskDetailViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_HISTORY_PENANGANAN";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@pTASK_ID", taskID);

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ListTaskDetailViewModel data = new ListTaskDetailViewModel();
                    data.sourceProspek = rd[0].ToString();
                    data.fieldPersonName = rd[1].ToString();
                    try
                    {

                        data.startedDate = Convert.ToDateTime(rd[2].ToString()).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        data.startedDate = rd[2].ToString();
                    }
                    try
                    {

                        data.submittedDate = Convert.ToDateTime(rd[3].ToString()).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        data.submittedDate = rd[3].ToString();
                    }
                    data.priorityLevel = rd[4].ToString();
                    ListData.Add(data);
                }
                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }

        public List<DownloadViewModel> ListDownload(string pRegion,
            string pFPName, string pBranchName, string pEmpPosition,
            string pTaskID, string pStatProspek, string pAppID, string[] pPriorityLevelarr,
            string pCustName, string pStatDukcapil, string pSdate, string pEdate,
            string pSource, string pSourceData, string pEmpNo)
        {
            List<DownloadViewModel> ListData = new List<DownloadViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var pPriorityLevel = string.Join(",", pPriorityLevelarr);
                //Declare COnnection                
                var querySstring = "spMKT_POLO_TASK_INQUIRY_ALL";
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
                command.Parameters.AddWithValue("@pCustName",pCustName);
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

                    DownloadViewModel data = new DownloadViewModel();
                    data.Number = rd[0].ToString();
                    data.BranchName = rd[1].ToString();
                    data.Region = rd[2].ToString();
                    data.TaskID = rd[3].ToString();
                    data.JenisTask = rd[4].ToString();
                    data.CustID = rd[5].ToString();
                    data.CustomerName = rd[6].ToString();
                    data.DistributedDate = rd[7].ToString();
                    data.StartedDate = rd[8].ToString();
                    data.EmpPosition = rd[9].ToString();
                    data.soa = rd[10].ToString();
                    data.Referentor1 = rd[11].ToString();
                    data.RegionalId = rd[12].ToString();
                    data.Product = rd[13].ToString();
                    data.CabId = rd[14].ToString();
                    data.NIK = rd[15].ToString();
                    data.TempatLahir = rd[16].ToString();
                    data.TglLahir = rd[17].ToString();
                    data.RWLeg = rd[18].ToString();
                    data.ProvLeg = rd[19].ToString();
                    data.KabLeg = rd[20].ToString();
                    data.KecLeg = rd[21].ToString();
                    data.KelLeg = rd[22].ToString();
                    data.KodeposLeg = rd[23].ToString();
                    data.SubZipcodeLeg = rd[24].ToString();
                    data.AlamatRes = rd[25].ToString();
                    data.RTRes = rd[26].ToString();
                    data.RWRes = rd[27].ToString();
                    data.ProvRes = rd[28].ToString();
                    data.KabRes = rd[29].ToString();
                    data.KecRes = rd[30].ToString();
                    data.KelRes = rd[31].ToString();
                    data.KodeposRes = rd[32].ToString();
                    data.SubZipcodeRes = rd[33].ToString();
                    data.NoMesin = rd[34].ToString();
                    data.NoRangka = rd[35].ToString();
                    data.ItemType = rd[36].ToString();
                    data.ItemDescription = rd[37].ToString();
                    data.Mobile1 = rd[38].ToString();
                    data.Mobile2 = rd[39].ToString();
                    data.Phone1 = rd[40].ToString();
                    data.Phone2 = rd[41].ToString();
                    data.OfficePhone1 = rd[42].ToString();
                    data.OfficePhone2 = rd[43].ToString();
                    data.OtrPrice = rd[44].ToString();
                    data.ItemYear = rd[45].ToString();
                    data.MonthlyIncome = rd[46].ToString();
                    data.MonthInstallment = rd[47].ToString();
                    data.DP = rd[48].ToString();
                    data.LTV = rd[49].ToString();
                    data.TenorId = rd[50].ToString();
                    data.Plafond = rd[51].ToString();
                    data.Pekerjaan = rd[52].ToString();
                    data.SisaTenor = rd[53].ToString();
                    data.ReleaseDateBpkb = rd[54].ToString();
                    data.MaxPastDueDt = rd[55].ToString();
                    data.Religion = rd[56].ToString();
                    data.CustomerRating = rd[57].ToString();
                    data.TanggalJatuhTempo = rd[58].ToString();
                    data.MaturityDt = rd[59].ToString();
                    data.StatusCall = rd[60].ToString();
                    data.AnswerCall = rd[61].ToString();
                    data.StatusProspek = rd[62].ToString();
                    data.ReasonNotProspek = rd[63].ToString();
                    data.Notes = rd[64].ToString();
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }

        public List<DownloadViewModel> ListDownloadDetail (int pID,string pEmpNo)
        {
            List<DownloadViewModel> ListData = new List<DownloadViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_TASK_INQUIRY";
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

                    DownloadViewModel data = new DownloadViewModel();
                    data.Number = rd[0].ToString();
                    data.BranchName = rd[1].ToString();
                    data.Region = rd[2].ToString();
                    data.TaskID = rd[3].ToString();
                    data.JenisTask = rd[4].ToString();
                    data.CustID = rd[5].ToString();
                    data.CustomerName = rd[6].ToString();
                    data.DistributedDate = rd[7].ToString();
                    data.StartedDate = rd[8].ToString();
                    data.EmpPosition = rd[9].ToString();
                    data.soa = rd[10].ToString();
                    data.Referentor1 = rd[11].ToString();
                    data.RegionalId = rd[12].ToString();
                    data.Product = rd[13].ToString();
                    data.CabId = rd[14].ToString();
                    data.NIK = rd[15].ToString();
                    data.TempatLahir = rd[16].ToString();
                    data.TglLahir = rd[17].ToString();
                    data.RWLeg = rd[18].ToString();
                    data.ProvLeg = rd[19].ToString();
                    data.KabLeg = rd[20].ToString();
                    data.KecLeg = rd[21].ToString();
                    data.KelLeg = rd[22].ToString();
                    data.KodeposLeg = rd[23].ToString();
                    data.SubZipcodeLeg = rd[24].ToString();
                    data.AlamatRes = rd[25].ToString();
                    data.RTRes = rd[26].ToString();
                    data.RWRes = rd[27].ToString();
                    data.ProvRes = rd[28].ToString();
                    data.KabRes = rd[29].ToString();
                    data.KecRes = rd[30].ToString();
                    data.KelRes = rd[31].ToString();
                    data.KodeposRes = rd[32].ToString();
                    data.SubZipcodeRes = rd[33].ToString();
                    data.NoMesin = rd[34].ToString();
                    data.NoRangka = rd[35].ToString();
                    data.ItemType = rd[36].ToString();
                    data.ItemDescription = rd[37].ToString();
                    data.Mobile1 = rd[38].ToString();
                    data.Mobile2 = rd[39].ToString();
                    data.Phone1 = rd[40].ToString();
                    data.Phone2 = rd[41].ToString();
                    data.OfficePhone1 = rd[42].ToString();
                    data.OfficePhone2 = rd[43].ToString();
                    data.OtrPrice = rd[44].ToString();
                    data.ItemYear = rd[45].ToString();
                    data.MonthlyIncome = rd[46].ToString();
                    data.MonthInstallment = rd[47].ToString();
                    data.DP = rd[48].ToString();
                    data.LTV = rd[49].ToString();
                    data.TenorId = rd[50].ToString();
                    data.Plafond = rd[51].ToString();
                    data.Pekerjaan = rd[52].ToString();
                    data.SisaTenor = rd[53].ToString();
                    data.ReleaseDateBpkb = rd[54].ToString();
                    data.MaxPastDueDt = rd[55].ToString();
                    data.Religion = rd[56].ToString();
                    data.CustomerRating = rd[57].ToString();
                    data.TanggalJatuhTempo = rd[58].ToString();
                    data.MaturityDt = rd[59].ToString();
                    data.StatusCall = rd[60].ToString();
                    data.AnswerCall = rd[61].ToString();
                    data.StatusProspek = rd[62].ToString();
                    data.ReasonNotProspek = rd[63].ToString();
                    data.Notes = rd[64].ToString();
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

