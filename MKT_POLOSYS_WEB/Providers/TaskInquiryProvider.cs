using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_WEB.DataAccess;
using MKT_POLOSYS_WEB.Models;
using MKT_POLOSYS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace MKT_POLOSYS_WEB.Providers
{
    public class TaskInquiryProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();

        public List<ListTaskViewModel> get()
        {
            List<ListTaskViewModel> ListData = new List<ListTaskViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_TASK_INQUIRY_LIST";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@pTASK_ID", "TESS");

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
                    data.DistributedDate = null;
                    data.StartedDate = null;
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
                    data.TglLahir = rd[16].ToString();
                    data.AlamatLeg = rd[17].ToString();
                    data.ProvLeg = rd[18].ToString();
                    data.KecLeg = rd[20].ToString();
                    data.KabLeg = rd[21].ToString();
                    data.KelLeg = rd[22].ToString();
                    data.RTLeg = rd[23].ToString();
                    data.RWLeg = rd[24].ToString();
                    data.AlamatRes = rd[25].ToString();
                    data.ProvRes = rd[26].ToString();
                    data.KabRes = rd[27].ToString();
                    data.KelRes = rd[28].ToString();
                    data.RTRes = rd[29].ToString();
                    data.RWRes = rd[30].ToString();
                    data.KodePosRes = rd[31].ToString();
                    data.SubZipcodeRes = rd[32].ToString();
                    data.Product = rd[33].ToString();
                    data.ItemType = rd[34].ToString();
                    data.ItemYear = rd[35].ToString();
                    data.OtrPrice = rd[36].ToString();
                    data.DP = rd[37].ToString();
                    data.LTV = rd[38].ToString();

                    data.Tenor = rd[39].ToString();
                    data.Plafond = rd[40].ToString();
                    data.MonthInstallment = rd[41].ToString();
                    data.Notes = rd[42].ToString();
                }

                //Connection Close
                command.Connection.Close();

            }
            return data;

        }


        public List<ListTaskDetailViewModel> getDetail()
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
                command.Parameters.AddWithValue("@pTASK_ID", "TESS");

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    ListTaskDetailViewModel data = new ListTaskDetailViewModel();
                    data.sourceProspek = rd[0].ToString();
                    data.fieldPersonName = rd[1].ToString();
                    data.startedDate = rd[2].ToString();
                    data.submittedDate = rd[3].ToString();
                    data.priorityLevel = rd[4].ToString();
                    ListData.Add(data);
                }
                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }

        public List<DownloadViewModel> ListDownload()
        {
            List<DownloadViewModel> ListData = new List<DownloadViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_TASK_INQUIRY_LIST";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@pTASK_ID", "TESS");

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    DownloadViewModel data = new DownloadViewModel();
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
                var querySstring = @"select ROA.AREA_NAME AS TEXT ,OFFICE_REGION_MBR_X_ID AS VALUE
from CONFINS.dbo.REF_OFFICE_AREA ROA
JOIN CONFINS.dbo.OFFICE_REGION_MBR_X ORMX ON ROA.REF_OFFICE_AREA_ID = ORMX.REF_OFFICE_AREA_ID";
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
                var querySstring = @"select RO.OFFICE_NAME AS TEXT ,RO.OFFICE_NAME AS VALUE
from CONFINS.dbo.REF_OFFICE_AREA ROA
JOIN CONFINS.dbo.OFFICE_REGION_MBR_X ORMX ON ROA.REF_OFFICE_AREA_ID=ORMX.REF_OFFICE_AREA_ID
JOIN CONFINS.dbo.REF_OFFICE RO ON ROA.REF_OFFICE_AREA_ID = RO.REF_OFFICE_AREA_ID
WHERE RO.ORG_MDL_ID in ('6','7')";
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
                var querySstring = @"
SELECT PARAMETER_VALUE AS TEXT,PARAMETER_VALUE AS VALUE FROM WISE_STAGING.DBO.M_MKT_POLO_PARAMETER WHERE PARAMETER_TYPE='EMP_POSITION'";
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
                var querySstring = @"
SELECT PARAMETER_VALUE AS TEXT,PARAMETER_VALUE AS VALUE FROM WISE_STAGING.DBO.M_MKT_POLO_PARAMETER WHERE PARAMETER_TYPE='STATUS_PROSPEK'";
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
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
        public List<DropdownListViewModel> ddlPriotityLevel()
        {
            List<DropdownListViewModel> ListData = new List<DropdownListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = @"
SELECT PARAMETER_VALUE AS TEXT,PARAMETER_VALUE AS VALUE FROM WISE_STAGING.DBO.M_MKT_POLO_PARAMETER WHERE PARAMETER_TYPE='PRIORITY_LEVEL'";
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
                var querySstring = @"
SELECT PARAMETER_VALUE AS TEXT,PARAMETER_VALUE AS VALUE FROM WISE_STAGING.DBO.M_MKT_POLO_PARAMETER WHERE PARAMETER_TYPE='PRIORITY_LEVEL'";
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
                var querySstring = @"
SELECT PARAMETER_VALUE AS TEXT,PARAMETER_VALUE AS VALUE FROM WISE_STAGING.DBO.M_MKT_POLO_PARAMETER WHERE PARAMETER_TYPE='PRIORITY_LEVEL'";
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
                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return ListData;

        }
    }
}

