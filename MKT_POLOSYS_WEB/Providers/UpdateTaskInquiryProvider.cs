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


namespace MKT_POLOSYS_WEB.Providers
{
    public class UpdateTaskInquiryProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();
        string guid = System.Guid.NewGuid().ToString();
        public List<ListTaskViewModel> UploadData(UploadViewModel model)
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
                command.Parameters.AddWithValue("@pMonthlyInstallment", model.MonthInstallment);
                command.Parameters.AddWithValue("@pDownPayment", model.DP);
                command.Parameters.AddWithValue("@pLtv", model.LTV);
                command.Parameters.AddWithValue("@pPlafond", model.Plafond);
                command.Parameters.AddWithValue("@pProfession", model.Pekerjaan);
                command.Parameters.AddWithValue("@pOsTenor", model.SisaTenor);
                command.Parameters.AddWithValue("@pTenorId", model.TenorId);
                command.Parameters.AddWithValue("@pReleaseDateBpkb", model.ReleaseDateBpkb);
                command.Parameters.AddWithValue("@pMaxPastDueDt", model.TanggalJatuhTempo);
                command.Parameters.AddWithValue("@pReligion", model.Religion);
                command.Parameters.AddWithValue("@pCallStat", model.StatusCall);
                command.Parameters.AddWithValue("@pAnswerCall", model.AnswerCall);
                command.Parameters.AddWithValue("@pCustRating", model.CustomerRating);
                command.Parameters.AddWithValue("@pProspectStat", model.StatusProspek);
                command.Parameters.AddWithValue("@pReasonNotProspek", model.ReasonNotProspek);
                command.Parameters.AddWithValue("@pNotes", model.Notes);
                command.Parameters.AddWithValue("@pQueueUid", guid);
                command.Parameters.AddWithValue("@pEmpNo", model.EmpNo);
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    ListTaskViewModel data = new ListTaskViewModel();
                    //data.sourceData = rd[0].ToString();
                    //data.cabang = rd[1].ToString();
                    //data.regional = rd[2].ToString();
                    //data.taskID = rd[3].ToString();
                    //data.jenisTask = rd[4].ToString();
                    //data.customerID = rd[5].ToString();
                    //data.customerName = rd[6].ToString();
                    //data.distributedDT = rd[7].ToString();
                    //data.startedDT = rd[8].ToString();
                    //data.slaRemaining = rd[9].ToString();
                    //data.fieldPersonName = rd[10].ToString();
                    //data.empPosition = rd[11].ToString();
                    //data.statusProspek = rd[12].ToString();
                    //data.priorityLevel = rd[13].ToString();
                    //data.aplikasiAI = rd[14].ToString();
                    //data.applicationID = rd[15].ToString();
                    //data.statusMSS = rd[16].ToString();
                    //data.statusWISE = rd[17].ToString();
                    //data.statusDukcapil = rd[18].ToString();
                    //data.soa = rd[19].ToString();
                    //data.referentorName = rd[20].ToString();
                    //data.referentorName2 = rd[21].ToString();
                    //data.orderInID = rd[22].ToString();
                    //ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }


        

        public bool  validasiDownload(string empNo)
        {
            bool isSucceed = true;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"select CASE WHEN COUNT(EMP_NO) > 0 THEN cast(1 as  bit) else cast(0 as  bit) end as validasi from WISE_STAGING.DBO.T_MKT_POLO_LOG_USER_DOWNLOAD where EMP_NO='" + empNo + "'AND FLAG_DOWNLOAD='T'";
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
    }
}

