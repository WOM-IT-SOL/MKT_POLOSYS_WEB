using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_WEB.DataAccess;
using MKT_POLOSYS_WEB.Models.UploadTaskInquiry;
using MKT_POLOSYS_WEB.Models;
using POLO_EXTENSION;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Data;

namespace MKT_POLOSYS_WEB.Providers
{
    public class UpdateTaskInquiryProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();
        
        public List<ListTaskViewModel> UploadData(UploadViewModel model,string guid)
        {
            List<ListTaskViewModel> ListData = new List<ListTaskViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {         
                var querySstring = "spMKT_POLO_UPLOAD_STATUS_TASK";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Define Query Parameter
                command.Parameters.AddWithValue("@pOfficeCode", model.CabId);
                command.Parameters.AddWithValue("@pOfficeName", model.BranchName);
                command.Parameters.AddWithValue("@pOfficeRegionCode", model.RegionalId);
                command.Parameters.AddWithValue("@pOfficeRegionName", model.Region);
                command.Parameters.AddWithValue("@pTaskID", model.TaskID);
                command.Parameters.AddWithValue("@pJenisTask", model.JenisTask);
                command.Parameters.AddWithValue("@pCustNo", model.CustID);
                command.Parameters.AddWithValue("@pCustName", model.CustomerName);
                command.Parameters.AddWithValue("@pDistributedDt", model.DistributedDate);
                command.Parameters.AddWithValue("@pStartedDt", model.StartedDate);
                command.Parameters.AddWithValue("@pEmpPosition", model.EmpPosition);
                command.Parameters.AddWithValue("@pSoa", model.soa);
                command.Parameters.AddWithValue("@pReferentor1", model.Referentor1);
                command.Parameters.AddWithValue("@pProduct", model.Product);
                command.Parameters.AddWithValue("@pIdNo", model.NIK);
                command.Parameters.AddWithValue("@pBirthPlace", model.TempatLahir);
                command.Parameters.AddWithValue("@pBirthDt", model.TglLahir);
                command.Parameters.AddWithValue("@pRwLeg", model.RWLeg);
                command.Parameters.AddWithValue("@pProvinsiLeg", model.ProvLeg);
                command.Parameters.AddWithValue("@pKabupatenLeg", model.KabLeg);
                command.Parameters.AddWithValue("@pKecamatanLeg", model.KecLeg);
                command.Parameters.AddWithValue("@pKelurahanLeg", model.KelLeg);
                command.Parameters.AddWithValue("@pAlamatRes", model.AlamatRes);
                command.Parameters.AddWithValue("@pRtRes", model.RTRes);
                command.Parameters.AddWithValue("@pRwRes", model.RWRes);
                command.Parameters.AddWithValue("@pProvinsiRes", model.ProvRes);
                command.Parameters.AddWithValue("@pKabupatenRes", model.KabRes);
                command.Parameters.AddWithValue("@pKecamatanRes", model.KecRes);
                command.Parameters.AddWithValue("@pKelurahanRes", model.KelRes);
                command.Parameters.AddWithValue("@pMachineNo", model.NoMesin);
                command.Parameters.AddWithValue("@pChassisNo", model.NoRangka);
                command.Parameters.AddWithValue("@pItemType", model.ItemType);
                command.Parameters.AddWithValue("@pItemDescription", model.ItemDescription);
                command.Parameters.AddWithValue("@pMobile1", model.Mobile1);
                command.Parameters.AddWithValue("@pMobile2", model.Mobile2);
                command.Parameters.AddWithValue("@pPhone1", model.Phone1);
                command.Parameters.AddWithValue("@pPhone2", model.Phone2);
                command.Parameters.AddWithValue("@pOfficePhone1", model.OfficePhone1);
                command.Parameters.AddWithValue("@pOfficePhone2", model.OfficePhone2);
                command.Parameters.AddWithValue("@pOtrPrice", model.OtrPrice);
                command.Parameters.AddWithValue("@pItemYear", model.ItemYear);
                command.Parameters.AddWithValue("@pMonthIncome", model.MonthlyIncome);
                command.Parameters.AddWithValue("@pMonthlyInstallment", model.MonthInstallment);
                command.Parameters.AddWithValue("@pDownPayment", model.DP);
                command.Parameters.AddWithValue("@pLtv", model.LTV);
                command.Parameters.AddWithValue("@pPlafond", model.Plafond);
                command.Parameters.AddWithValue("@pProfession", model.Pekerjaan);
                command.Parameters.AddWithValue("@pOsTenor", model.SisaTenor);
                command.Parameters.AddWithValue("@pTenorId", model.TenorId);
                command.Parameters.AddWithValue("@pReleaseDateBpkb", model.ReleaseDateBpkb);
                command.Parameters.AddWithValue("@pMaxPastDueDt", model.MaxPastDueDt);
                command.Parameters.AddWithValue("@pReligion", model.Religion);
                command.Parameters.AddWithValue("@pMaturityDt", model.MaturityDt);
                command.Parameters.AddWithValue("@pTglJatuhTempo", model.TanggalJatuhTempo);
                command.Parameters.AddWithValue("@pCallStat", model.StatusCall);
                command.Parameters.AddWithValue("@pAnswerCall", model.AnswerCall);
                command.Parameters.AddWithValue("@pCustRating", model.CustomerRating);
                command.Parameters.AddWithValue("@pProspectStat", model.StatusProspek);
                command.Parameters.AddWithValue("@pReasonNotProspek", model.ReasonNotProspek);
                command.Parameters.AddWithValue("@pNotes", model.Notes);
                command.Parameters.AddWithValue("@pQueueUid", guid);
                command.Parameters.AddWithValue("@pEmpNo", model.EmpNo);
                command.Parameters.AddWithValue("@pZipcodeLeg", model.KodePosLeg);
                command.Parameters.AddWithValue("@pSubZipcodeLeg", model.SubZipcodeLeg);
                command.Parameters.AddWithValue("@pZipcodeRes", model.KodePosRes);
                command.Parameters.AddWithValue("@pSubZipcodeRes", model.SubZipcodeRes);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    ListTaskViewModel data = new ListTaskViewModel();
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

            SqlCommand command = new SqlCommand();
            command.Connection = new SqlConnection(connectionString);
            command.CommandText = @"SELECT TASK_ID FROM T_MKT_POLO_UPLOAD WHERE UPLOAD_STS = 1 AND QUEUE_UID = '" + guid + "'";
            command.CommandType = CommandType.Text;
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            SendDataPreparation send = new SendDataPreparation(connectionString);
            while (dr.Read())
            {
                await send.startProcess(dr[0].ToString());
            }

            dr.Close();
            command.Connection.Close();
        }

        public bool  validasiUpload(string empNo)
        {
            bool isSucceed = true;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            var userType = validasiUserType(empNo);
            //validasi belum download 
            if (userType == "TELESALES")
            {
                var validDownload =true;
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
    }
}

