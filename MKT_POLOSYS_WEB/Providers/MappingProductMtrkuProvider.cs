using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_WEB.DataAccess;
using MKT_POLOSYS_WEB.Models.MappingProductMtrku;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_WEB.Providers
{
    public class MappingProductMtrkuProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();

        public List<MappingProductMtrkuListViewModel> Index()
        {
            List<MappingProductMtrkuListViewModel> ListData = new List<MappingProductMtrkuListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //Declare COnnection                
                var querySstring = "spMKT_POLO_Mapping_Mtrku_List_Web";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@pMotorku30", "");
                command.Parameters.AddWithValue("@pMotorku120", "");
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    MappingProductMtrkuListViewModel data = new MappingProductMtrkuListViewModel();
                    data.rowNum = rd[0].ToString();
                    data.id = rd[1].ToString();
                    data.product30Code = rd[2].ToString();
                    data.product30Name = rd[3].ToString();
                    data.product120Code = rd[4].ToString();
                    data.product120Name = rd[5].ToString();


                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }

        public List<MappingProductMtrkuListViewModel> getSearch(string prodMotorkuCode30, string prodMotorkuCode210)
        {
            List<MappingProductMtrkuListViewModel> ListData = new List<MappingProductMtrkuListViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                 
                //Declare COnnection                
                var querySstring = "spMKT_POLO_Mapping_Mtrku_List_Web";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                string pProduct30 = prodMotorkuCode30;
                string pProduct120 = prodMotorkuCode210;
                if (prodMotorkuCode30 == null)  
                {
                    pProduct30 = "";
                }
                if (prodMotorkuCode210 == null)
                {
                    pProduct120 = "";
                }
                command.Parameters.AddWithValue("@pMotorku30", pProduct30);
                command.Parameters.AddWithValue("@pMotorku120", pProduct120); 
                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {

                    MappingProductMtrkuListViewModel data = new MappingProductMtrkuListViewModel();
                    data.rowNum = rd[0].ToString();
                    data.id = rd[1].ToString();
                    data.product30Code = rd[2].ToString();
                    data.product30Name = rd[3].ToString();
                    data.product120Code = rd[4].ToString();
                    data.product120Name = rd[5].ToString();

                    ListData.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }
            return ListData;

        }

        public List<ddlMotorku30ViewModel> ddlMotorku30()
        {
            List<ddlMotorku30ViewModel> DDLMotorku30 = new List<ddlMotorku30ViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                 
                var querySstring = "spMKT_POLO_DDL_MTRKU30_WEB";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ddlMotorku30ViewModel data = new ddlMotorku30ViewModel();
                    data.kode = rd[0].ToString();
                    data.nama = rd[1].ToString();
                    DDLMotorku30.Add(data);
                }
                //Connection Close
                command.Connection.Close();

            }

            return DDLMotorku30;

        }

        public ddlMotorku30ViewModel ddlMotorku30SelectOne(string pKode)
        {
            ddlMotorku30ViewModel DDLMotorku30Select = new  ddlMotorku30ViewModel();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                 
                var querySstring = "spMKT_POLO_DDL_MTRKU30_SelectOne_WEB";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                if (pKode==null)
                {
                    pKode = "";
                }
                command.Parameters.AddWithValue("@pKode", pKode);

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                { 
                    DDLMotorku30Select.kode = rd[0].ToString();
                    DDLMotorku30Select.nama = rd[1].ToString();
                    
                }
                //Connection Close
                command.Connection.Close();

            }

            return DDLMotorku30Select;

        }

        public List<ddlMotorku120ViewModel> ddlMotorku120()
        {
            List<ddlMotorku120ViewModel> DDLMotorku120 = new List<ddlMotorku120ViewModel>();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                 
                var querySstring = "spMKT_POLO_DDL_MTRKU120_WEB";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ddlMotorku120ViewModel data = new ddlMotorku120ViewModel();
                    data.kode = rd[0].ToString();
                    data.nama = rd[1].ToString();
                    DDLMotorku120.Add(data);
                }
                //Connection Close
                command.Connection.Close();

            }

            return DDLMotorku120;

        }

        public ddlMotorku120ViewModel ddlMotorku120SelectOne(string pKode)
        {
            ddlMotorku120ViewModel DDLMotorku120Select = new ddlMotorku120ViewModel();
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                 
                var querySstring = "spMKT_POLO_DDL_MTRKU120_SelectOne_WEB";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                if (pKode==null)
                {
                    pKode = "";
                }
                command.Parameters.AddWithValue("@pKode", pKode);

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    DDLMotorku120Select.kode = rd[0].ToString();
                    DDLMotorku120Select.nama = rd[1].ToString(); 
                }
                //Connection Close
                command.Connection.Close();

            }

            return DDLMotorku120Select;

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

        public List<ProcessResultViewModel> saveData(MappingProductMtrkuCreateUpdateViewModel form, string emp_no)
        {
            string empName = string.Empty;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            List<ProcessResultViewModel> processResult = new List<ProcessResultViewModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_Mapping_Mtrku_save_Web";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter 
                command.Parameters.AddWithValue("@pProdCode30", form.ddlMotorku30);
                command.Parameters.AddWithValue("@pProdName30", form.prod_30_name);
                command.Parameters.AddWithValue("@pProdCode120", form.ddlMotorku120);
                command.Parameters.AddWithValue("@pProdName120", form.prod_120_name);
                command.Parameters.AddWithValue("@pUsrCrt", emp_no);


                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ProcessResultViewModel data = new ProcessResultViewModel();
                    data.statusCode = rd[0].ToString();
                    data.statusMessage = rd[1].ToString();
                    processResult.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return processResult;

        }

        public List<ProcessResultViewModel> updateData(MappingProductMtrkuCreateUpdateViewModel form,string emp_no)
        {
            string empName = string.Empty;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            List<ProcessResultViewModel> processResult = new List<ProcessResultViewModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_Mapping_Mtrku_Update_Web";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter 
                command.Parameters.AddWithValue("@pProdCode30", form.ddlMotorku30Upd);
                command.Parameters.AddWithValue("@pProdCode120Old", form.ddlMotorku120UpdOld);
                command.Parameters.AddWithValue("@pProdCode120", form.ddlMotorku120Upd);
                command.Parameters.AddWithValue("@pProdName120", form.prod_120_name_upd);
                command.Parameters.AddWithValue("@pUsrCrt", emp_no);
                


                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ProcessResultViewModel data = new ProcessResultViewModel();
                    data.statusCode = rd[0].ToString();
                    data.statusMessage = rd[1].ToString();
                    processResult.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return processResult;

        }

        public List<ProcessResultViewModel> deleteData(MappingProductMtrkuCreateUpdateViewModel form,string emp_no)
        {
            string empName = string.Empty;
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            List<ProcessResultViewModel> processResult = new List<ProcessResultViewModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_Mapping_Mtrku_Delete_Web";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter                 
                command.Parameters.AddWithValue("@pId", form.idDel);
                command.Parameters.AddWithValue("@pUsrCrt", emp_no);


                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ProcessResultViewModel data = new ProcessResultViewModel();
                    data.statusCode = rd[0].ToString();
                    data.statusMessage = rd[1].ToString();
                    processResult.Add(data);
                }

                //Connection Close
                command.Connection.Close();

            }

            return processResult;

        }

    }
}
