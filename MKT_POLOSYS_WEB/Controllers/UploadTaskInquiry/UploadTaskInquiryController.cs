using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ExcelDataReader;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_WEB.Models.UploadTaskInquiry;
using MKT_POLOSYS_WEB.Providers;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;

namespace MKT_POLOSYS_WEB.Controllers.TaskInquiry
{
    public class UploadTaskInquiryController : Controller
    {


        // GET: TaskInquiry
        public ActionResult Index(string emp_no)
        {
            try
            {
                UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
                Boolean isSucceed = true;
                var model = new IndexViewModel();
                var base64EncodedBytes = System.Convert.FromBase64String(emp_no);
                var empNo = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                model.empNo = empNo;
                isSucceed = updateTaskInquiryProvider.validasiUser(model.empNo);
                model.empName = updateTaskInquiryProvider.getUser(model.empNo);
                if (isSucceed)
                {
                    ViewData["empNames"] = model.empName;
                    return View(model);
                }
                else
                {

                    return View("~/Views/Shared/ErrorUpload.cshtml");
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorUpload.cshtml");
            }
        }

        public IActionResult ValidasiUpload(string pEmpNo)
        {
            UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
            Boolean isSucceed = true;
            isSucceed = updateTaskInquiryProvider.validasiUpload(pEmpNo);
            return Json(isSucceed);

        }

        public class Proccessresult
        {
            public bool isSucceed { get; set; }
            public string pguid { get; set; }
            public string message { get; set; }
        }

        public static DateTime FromExcelDate(double value)
        {
            return new DateTime(1899, 12, 30).AddDays(value);
        }

        public static DateTime FromExcelDateElse(double value)
        {

            return new DateTime(1899, 30, 12).AddDays(value);
        }

        [HttpPost]
        public async Task<ActionResult> Upload(string empNo)
        {
            Proccessresult result = new Proccessresult();
            string guid = System.Guid.NewGuid().ToString().ToUpper();

            UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
            try
            {
                var files = Request.Form.Files;
                bool isSucceed = true;

                foreach (var uploadFile in files)
                {
                    if (uploadFile != null)
                    {
                        Dictionary<string, string> names = new Dictionary<string, string>();
                        names.Add("labelName1", "No");
                        names.Add("labelName2", "Branch Name");
                        names.Add("labelName3", "Region");
                        names.Add("labelName4", "Task ID");
                        names.Add("labelName5", "Jenis Task");
                        names.Add("labelName6", "Cust ID");
                        names.Add("labelName7", "Customer Name");
                        names.Add("labelName8", "Distributed Date");
                        names.Add("labelName9", "Started Date");
                        names.Add("labelName10", "Emp Position");
                        names.Add("labelName11", "SOA");
                        names.Add("labelName12", "Referentor 1");
                        names.Add("labelName13", "Regional_id");
                        names.Add("labelName14", "Product");
                        names.Add("labelName15", "Cab_Id");
                        names.Add("labelName16", "Nik Ktp");
                        names.Add("labelName17", "Tempat Lahir");
                        names.Add("labelName18", "Tanggal Lahir");
                        names.Add("labelName19", "Rw (Legal)");
                        names.Add("labelName20", "Provinsi (Legal)");
                        names.Add("labelName21", "Kabupaten (Legal)");
                        names.Add("labelName22", "Kecamatan (Legal)");
                        names.Add("labelName23", "Kelurahan (Legal)");
                        /////////////////////////////////////////////////
                        names.Add("labelName24", "Kode Pos (Legal)");
                        names.Add("labelName25", "SubZipcode (Legal)");
                        /////////////////////////////////////////////////
                        names.Add("labelName26", "Alamat (Survey)");
                        names.Add("labelName27", "Rt (Survey)");
                        names.Add("labelName28", "Rw (Survey)");
                        names.Add("labelName29", "Provinsi (Survey)");
                        names.Add("labelName30", "Kabupaten (Survey)");
                        names.Add("labelName31", "Kecamatan (Survey)");
                        names.Add("labelName32", "Kelurahan (Survey)");
                        /////////////////////////////////////////////////
                        names.Add("labelName33", "Kode Pos (Survey)");
                        names.Add("labelName34", "SubZipcode (Survey)");
                        /////////////////////////////////////////////////
                        names.Add("labelName35", "No_Mesin");
                        names.Add("labelName36", "No_Rangka");
                        names.Add("labelName37", "Item_Type");
                        names.Add("labelName38", "Item_Description");
                        names.Add("labelName39", "Mobile1");
                        names.Add("labelName40", "Mobile2");
                        names.Add("labelName41", "Phone1");
                        names.Add("labelName42", "Phone2");
                        names.Add("labelName43", "Office_Phone1");
                        names.Add("labelName44", "Office_Phone2");
                        names.Add("labelName45", "Otr_Price");
                        names.Add("labelName46", "Item_Year");
                        names.Add("labelName47", "Monthly_Income");
                        names.Add("labelName48", "Monthly_Installament");
                        names.Add("labelName49", "Down Payment Pengajuan");
                        names.Add("labelName50", "LTV Pengajuan");
                        names.Add("labelName51", "Tenor Pengajuan");
                        names.Add("labelName52", "Plafond Max");
                        names.Add("labelName53", "Pekerjaan");
                        names.Add("labelName54", "Sisa_Tenor");
                        names.Add("labelName55", "Realease_Date_Bpkb");
                        names.Add("labelName56", "Max_Past_Due_Dt");
                        names.Add("labelName57", "Religion");
                        names.Add("labelName58", "Customer_Rating");
                        names.Add("labelName59", "Tanggal_Jatuh_Tempo");
                        names.Add("labelName60", "Maturity_Dt");
                        names.Add("labelName61", "Status Call");
                        names.Add("labelName62", "Answer Call");
                        names.Add("labelName63", "Status Prospek");
                        names.Add("labelName64", "Reason Not Prospek");
                        names.Add("labelName65", "Notes");

                        string sFileExtension = Path.GetExtension(uploadFile.FileName).ToLower();

                        if ((uploadFile != null) && !string.IsNullOrEmpty(uploadFile.FileName))
                        {
                            string fileName = uploadFile.FileName;
                            string fileExtension = System.IO.Path.GetExtension(fileName);
                            var stream = files[0].OpenReadStream();
                            //if (fileExtension == ".xls")
                            //{
                            IExcelDataReader excelReader;
                            if (fileExtension == ".xls")
                            {
                                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            }
                            else
                            {
                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            }
                            DataSet dtTable = excelReader.AsDataSet();

                            var totalRows = dtTable.Tables[0].Rows.Count;
                            var maxColumn = dtTable.Tables[0].Columns.Count;
                            for (int j = 0; j <= 60; j++)
                            {
                                int i = j + 1;
                                var data1 = names["labelName" + i].ToString();
                                var data2 = dtTable.Tables[0].Rows[0][j].ToString();
                                if (data1 != data2)
                                {
                                    result.isSucceed = false;
                                    result.message = "Template format file yang diupload tidak sesuai ketentuan";
                                    return Json(result);
                                }
                            }
                            for (int i = 1; i < totalRows; i++)
                            {
                                UploadViewModel model = new UploadViewModel();
                                model.Number = dtTable.Tables[0].Rows[i][0].ToString();
                                model.BranchName = dtTable.Tables[0].Rows[i][1].ToString();
                                model.Region = dtTable.Tables[0].Rows[i][2].ToString();
                                model.TaskID = dtTable.Tables[0].Rows[i][3].ToString();
                                model.JenisTask = dtTable.Tables[0].Rows[i][4].ToString();
                                model.CustID = dtTable.Tables[0].Rows[i][5].ToString();
                                model.CustomerName = dtTable.Tables[0].Rows[i][6].ToString();
                                try
                                {
                                    var locDate = dtTable.Tables[0].Rows[i][7].ToString();
                                    model.DistributedDate = Convert.ToDateTime(locDate).ToString("yyyy-MM-dd HH:mm:ss.mmm");
                                }
                                catch
                                {
                                    model.DistributedDate = dtTable.Tables[0].Rows[i][7].ToString();
                                }
                                try
                                {
                                    var locDate = dtTable.Tables[0].Rows[i][8].ToString();
                                    model.StartedDate = Convert.ToDateTime(locDate).ToString("yyyy-MM-dd HH:mm:ss.mmm");
                                }
                                catch
                                {
                                    model.StartedDate = dtTable.Tables[0].Rows[i][8].ToString();
                                }
                                model.EmpPosition = dtTable.Tables[0].Rows[i][9].ToString();
                                model.soa = dtTable.Tables[0].Rows[i][10].ToString();
                                model.Referentor1 = dtTable.Tables[0].Rows[i][11].ToString();
                                model.RegionalId = dtTable.Tables[0].Rows[i][12].ToString();
                                model.Product = dtTable.Tables[0].Rows[i][13].ToString();
                                model.CabId = dtTable.Tables[0].Rows[i][14].ToString();
                                model.NIK = dtTable.Tables[0].Rows[i][15].ToString();
                                model.TempatLahir = dtTable.Tables[0].Rows[i][16].ToString();
                                try
                                {

                                    var locDate = dtTable.Tables[0].Rows[i][17].ToString();
                                    model.TglLahir = Convert.ToDateTime(locDate).ToString("yyyy-MM-dd HH:mm:ss.mmm");
                                }
                                catch
                                {
                                    model.TglLahir = dtTable.Tables[0].Rows[i][17].ToString();
                                }
                                model.RWLeg = dtTable.Tables[0].Rows[i][18].ToString();
                                model.ProvLeg = dtTable.Tables[0].Rows[i][19].ToString();
                                model.KabLeg = dtTable.Tables[0].Rows[i][20].ToString();
                                model.KecLeg = dtTable.Tables[0].Rows[i][21].ToString();
                                model.KelLeg = dtTable.Tables[0].Rows[i][22].ToString();
                                //////////////////////////////////////////////////////////
                                model.KodePosLeg = dtTable.Tables[0].Rows[i][23].ToString();
                                model.SubZipcodeLeg = dtTable.Tables[0].Rows[i][24].ToString();
                                //////////////////////////////////////////////////////////
                                model.AlamatRes = dtTable.Tables[0].Rows[i][25].ToString();
                                model.RTRes = dtTable.Tables[0].Rows[i][26].ToString();
                                model.RWRes = dtTable.Tables[0].Rows[i][27].ToString();
                                model.ProvRes = dtTable.Tables[0].Rows[i][28].ToString();
                                model.KabRes = dtTable.Tables[0].Rows[i][29].ToString();
                                model.KecRes = dtTable.Tables[0].Rows[i][30].ToString();
                                model.KelRes = dtTable.Tables[0].Rows[i][31].ToString();
                                //////////////////////////////////////////////////////////
                                model.KodePosRes = dtTable.Tables[0].Rows[i][32].ToString();
                                model.SubZipcodeRes = dtTable.Tables[0].Rows[i][33].ToString();
                                //////////////////////////////////////////////////////////
                                model.NoMesin = dtTable.Tables[0].Rows[i][34].ToString();
                                model.NoRangka = dtTable.Tables[0].Rows[i][35].ToString();
                                model.ItemType = dtTable.Tables[0].Rows[i][36].ToString();
                                model.ItemDescription = dtTable.Tables[0].Rows[i][37].ToString();
                                model.Mobile1 = dtTable.Tables[0].Rows[i][38].ToString();
                                model.Mobile2 = dtTable.Tables[0].Rows[i][39].ToString();
                                model.Phone1 = dtTable.Tables[0].Rows[i][40].ToString();
                                model.Phone2 = dtTable.Tables[0].Rows[i][41].ToString();
                                model.OfficePhone1 = dtTable.Tables[0].Rows[i][42].ToString();
                                model.OfficePhone2 = dtTable.Tables[0].Rows[i][43].ToString();
                                model.OtrPrice = dtTable.Tables[0].Rows[i][44].ToString();
                                model.ItemYear = dtTable.Tables[0].Rows[i][45].ToString();
                                model.MonthlyIncome = dtTable.Tables[0].Rows[i][46].ToString();
                                model.MonthInstallment = dtTable.Tables[0].Rows[i][47].ToString();
                                model.DP = dtTable.Tables[0].Rows[i][48].ToString();
                                model.LTV = dtTable.Tables[0].Rows[i][49].ToString();
                                model.TenorId = dtTable.Tables[0].Rows[i][50].ToString();
                                model.Plafond = dtTable.Tables[0].Rows[i][51].ToString();
                                model.Pekerjaan = dtTable.Tables[0].Rows[i][52].ToString();
                                model.SisaTenor = dtTable.Tables[0].Rows[i][53].ToString();
                                try
                                {
                                    var locDate = dtTable.Tables[0].Rows[i][54].ToString();
                                    model.ReleaseDateBpkb = Convert.ToDateTime(locDate).ToString("yyyy-MM-dd HH:mm:ss.mmm");
                                }
                                catch
                                {
                                    model.ReleaseDateBpkb = dtTable.Tables[0].Rows[i][54].ToString();
                                }
                                try
                                {
                                    model.MaxPastDueDt = dtTable.Tables[0].Rows[i][55].ToString();
                                }
                                catch
                                {
                                    model.MaxPastDueDt = "";
                                }
                                model.Religion = dtTable.Tables[0].Rows[i][56].ToString();
                                model.CustomerRating = dtTable.Tables[0].Rows[i][57].ToString();
                                try
                                {
                                    var locDate = dtTable.Tables[0].Rows[i][58].ToString();
                                    model.TanggalJatuhTempo = Convert.ToDateTime(locDate).ToString("yyyy-MM-dd HH:mm:ss.mmm");
                                }
                                catch
                                {
                                    model.TanggalJatuhTempo = dtTable.Tables[0].Rows[i][58].ToString();
                                }
                                try
                                {
                                    var locDate = dtTable.Tables[0].Rows[i][59].ToString();
                                    model.MaturityDt = Convert.ToDateTime(locDate).ToString("yyyy-MM-dd HH:mm:ss.mmm");
                                }
                                catch
                                {
                                    model.MaturityDt = dtTable.Tables[0].Rows[i][59].ToString();
                                }
                                model.StatusCall = dtTable.Tables[0].Rows[i][60].ToString();
                                model.AnswerCall = dtTable.Tables[0].Rows[i][61].ToString();
                                model.StatusProspek = dtTable.Tables[0].Rows[i][62].ToString();
                                model.ReasonNotProspek = dtTable.Tables[0].Rows[i][63].ToString();
                                model.Notes = dtTable.Tables[0].Rows[i][64].ToString();
                                model.EmpNo = empNo;
                                var data = updateTaskInquiryProvider.UploadData(model, guid);
                            }

                            //}
                        }

                    }

                }


            }
            catch
            {
                result.isSucceed = true;
                result.pguid = guid;
                result.message = "Upload Error";
                return Json(result);
            }
            try
            {
                await updateTaskInquiryProvider.SendApiCekDukcapil(guid);
                await updateTaskInquiryProvider.SendApiToWiseMSS(guid);
            }
            catch
            {
                result.isSucceed = true;
                result.pguid = guid;
                result.message = "Upload Done";
                return Json(result);
            }
            result.isSucceed = true;
            result.pguid = guid;
            result.message = "Upload Done";
            return Json(result);
        }



        public ActionResult ExportLog(string guid)
        {
            UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            var data = updateTaskInquiryProvider.getLog(guid);

            foreach (var item in data)
            {
                tw.WriteLine("TASK ID : " + item.TASK_ID + ", UPLOAD_MESSAGE : " + item.UPLOAD_MESSAGE);
            }
            tw.Flush();
            tw.Close();

            return File(memoryStream.GetBuffer(), "text/plain", "Log_datagagalupload_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        }



    }
}