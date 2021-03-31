using System;
using System.Collections.Generic;
using System.Data;
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

        [HttpPost]
        public ActionResult Upload(string empNo)
        {

            Proccessresult result = new Proccessresult();
            string guid = System.Guid.NewGuid().ToString().ToUpper();
            try
            {
                UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
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
                        names.Add("labelName24", "Alamat (Survey)");
                        names.Add("labelName25", "Rt (Survey)");
                        names.Add("labelName26", "Rw (Survey)");
                        names.Add("labelName27", "Provinsi (Survey)");
                        names.Add("labelName28", "Kabupaten (Survey)");
                        names.Add("labelName29", "Kecamatan (Survey)");
                        names.Add("labelName30", "Kelurahan (Survey)");
                        names.Add("labelName31", "No_Mesin");
                        names.Add("labelName32", "No_Rangka");
                        names.Add("labelName33", "Item_Type");
                        names.Add("labelName34", "Item_Description");
                        names.Add("labelName35", "Mobile1");
                        names.Add("labelName36", "Mobile2");
                        names.Add("labelName37", "Phone1");
                        names.Add("labelName38", "Phone2");
                        names.Add("labelName39", "Office_Phone1");
                        names.Add("labelName40", "Office_Phone2");
                        names.Add("labelName41", "Otr_Price");
                        names.Add("labelName42", "Item_Year");
                        names.Add("labelName43", "Monthly_Income");
                        names.Add("labelName44", "Monthly_Installament");
                        names.Add("labelName45", "Down Payment");
                        names.Add("labelName46", "LTV");
                        names.Add("labelName47", "Plafond");
                        names.Add("labelName48", "Pekerjaan");
                        names.Add("labelName49", "Sisa_Tenor");
                        names.Add("labelName50", "Tenor");
                        names.Add("labelName51", "Realease_Date_Bpkb");
                        names.Add("labelName52", "Max_Past_Due_Dt");
                        names.Add("labelName53", "Religion");
                        names.Add("labelName54", "Customer_Rating");
                        names.Add("labelName55", "Tanggal_Jatuh_Tempo");
                        names.Add("labelName56", "Maturity_Dt");
                        names.Add("labelName57", "Status Call");
                        names.Add("labelName58", "Answer Call");
                        names.Add("labelName59", "Status Prospek");
                        names.Add("labelName60", "Reason Not Prospek");
                        names.Add("labelName61", "Notes");

                        string sFileExtension = Path.GetExtension(uploadFile.FileName).ToLower();
                        //if (sFileExtension == ".xls")
                        //{
                        //    if (Request != null)
                        //    {
                        //        if ((uploadFile != null) && !string.IsNullOrEmpty(uploadFile.FileName))
                        //        {
                        //            string fileName = uploadFile.FileName;
                        //            string fileExtension = System.IO.Path.GetExtension(fileName);
                        //            var stream = files[0].OpenReadStream();
                        //            if (fileExtension == ".xls")
                        //            {
                        //                IExcelDataReader excelReader;
                        //                if (fileExtension == ".xls")
                        //                {
                        //                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        //                }
                        //                else {
                        //                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        //                }
                        //                DataSet dtTable = excelReader.AsDataSet();

                        //                var totalRows = dtTable.Tables[0].Rows.Count;
                        //                var maxColumn = dtTable.Tables[0].Columns.Count;
                        //                for (int j = 0; j <= 60; j++)
                        //                {
                        //                    int i = j + 1;
                        //                    var data1 = names["labelName" + i].ToString();
                        //                    var data2 = dtTable.Tables[0].Rows[0][j].ToString();
                        //                    if (data1 != data2)
                        //                    {
                        //                        result.isSucceed = false;
                        //                        result.message = "Template format file yang diupload tidak sesuai ketentuan";
                        //                        return Json(result);
                        //                    }
                        //                }
                        //                for (int i = 1; i < totalRows; i++)
                        //                {

                        //                    UploadViewModel model = new UploadViewModel();
                        //                    model.Number     = dtTable.Tables[0].Rows[i][0].ToString();
                        //                    model.BranchName = dtTable.Tables[0].Rows[i][1].ToString();
                        //                    model.Region     = dtTable.Tables[0].Rows[i][2].ToString();
                        //                    model.TaskID = dtTable.Tables[0].Rows[i][3].ToString();
                        //                    model.JenisTask = dtTable.Tables[0].Rows[i][4].ToString();
                        //                    model.CustID = dtTable.Tables[0].Rows[i][5].ToString();
                        //                    model.CustomerName = dtTable.Tables[0].Rows[i][6].ToString();
                        //                    model.DistributedDate = dtTable.Tables[0].Rows[i][7].ToString();
                        //                    model.StartedDate = dtTable.Tables[0].Rows[i][8].ToString();
                        //                    model.EmpPosition = dtTable.Tables[0].Rows[i][9].ToString();
                        //                    model.soa = dtTable.Tables[0].Rows[i][10].ToString();
                        //                    model.Referentor1 = dtTable.Tables[0].Rows[i][11].ToString();
                        //                    model.RegionalId = dtTable.Tables[0].Rows[i][12].ToString();
                        //                    model.Product = dtTable.Tables[0].Rows[i][13].ToString();
                        //                    model.CabId = dtTable.Tables[0].Rows[i][14].ToString();
                        //                    model.NIK = dtTable.Tables[0].Rows[i][15].ToString();
                        //                    model.TempatLahir = dtTable.Tables[0].Rows[i][16].ToString();
                        //                    model.TglLahir = dtTable.Tables[0].Rows[i][17].ToString();
                        //                    model.RWLeg = dtTable.Tables[0].Rows[i][18].ToString();
                        //                    model.ProvLeg = dtTable.Tables[0].Rows[i][19].ToString();
                        //                    model.KabLeg = dtTable.Tables[0].Rows[i][20].ToString();
                        //                    model.KecLeg = dtTable.Tables[0].Rows[i][21].ToString();
                        //                    model.KelLeg = dtTable.Tables[0].Rows[i][22].ToString();
                        //                    model.AlamatRes = dtTable.Tables[0].Rows[i][23].ToString();
                        //                    model.RTRes = dtTable.Tables[0].Rows[i][24].ToString();
                        //                    model.RWRes = dtTable.Tables[0].Rows[i][25].ToString();
                        //                    model.ProvRes = dtTable.Tables[0].Rows[i][26].ToString();
                        //                    model.KabRes = dtTable.Tables[0].Rows[i][27].ToString();
                        //                    model.KecRes = dtTable.Tables[0].Rows[i][28].ToString();
                        //                    model.KelRes = dtTable.Tables[0].Rows[i][29].ToString();
                        //                    model.NoMesin = dtTable.Tables[0].Rows[i][30].ToString();
                        //                    model.NoRangka = dtTable.Tables[0].Rows[i][31].ToString();
                        //                    model.ItemType = dtTable.Tables[0].Rows[i][32].ToString();
                        //                    model.ItemDescription = dtTable.Tables[0].Rows[i][33].ToString();
                        //                    model.Mobile1 = dtTable.Tables[0].Rows[i][34].ToString();
                        //                    model.Mobile2 = dtTable.Tables[0].Rows[i][35].ToString();
                        //                    model.Phone1 = dtTable.Tables[0].Rows[i][36].ToString();
                        //                    model.Phone2 = dtTable.Tables[0].Rows[i][37].ToString();
                        //                    model.OfficePhone1 = dtTable.Tables[0].Rows[i][38].ToString();
                        //                    model.OfficePhone2 = dtTable.Tables[0].Rows[i][39].ToString();
                        //                    model.OtrPrice = dtTable.Tables[0].Rows[i][40].ToString();
                        //                    model.ItemYear = dtTable.Tables[0].Rows[i][41].ToString();
                        //                    model.MonthlyIncome = dtTable.Tables[0].Rows[i][42].ToString();
                        //                    model.MonthInstallment = dtTable.Tables[0].Rows[i][43].ToString();
                        //                    model.DP = dtTable.Tables[0].Rows[i][44].ToString();
                        //                    model.LTV = dtTable.Tables[0].Rows[i][45].ToString();
                        //                    model.Plafond = dtTable.Tables[0].Rows[i][46].ToString();
                        //                    model.Pekerjaan = dtTable.Tables[0].Rows[i][47].ToString();
                        //                    model.SisaTenor = dtTable.Tables[0].Rows[i][48].ToString();
                        //                    model.TenorId = dtTable.Tables[0].Rows[i][49].ToString();
                        //                    model.ReleaseDateBpkb = dtTable.Tables[0].Rows[i][50].ToString();
                        //                    model.MaxPastDueDt = dtTable.Tables[0].Rows[i][51].ToString();
                        //                    model.Religion = dtTable.Tables[0].Rows[i][52].ToString();
                        //                    model.CustomerRating = dtTable.Tables[0].Rows[i][53].ToString();
                        //                    model.TanggalJatuhTempo = dtTable.Tables[0].Rows[i][54].ToString();
                        //                    model.MaturityDt = dtTable.Tables[0].Rows[i][55].ToString();
                        //                    model.StatusCall = dtTable.Tables[0].Rows[i][56].ToString();
                        //                    model.AnswerCall = dtTable.Tables[0].Rows[i][57].ToString();
                        //                    model.StatusProspek = dtTable.Tables[0].Rows[i][58].ToString();
                        //                    model.ReasonNotProspek = dtTable.Tables[0].Rows[i][59].ToString();
                        //                    model.Notes = dtTable.Tables[0].Rows[i][60].ToString();
                        //                    model.EmpNo = empNo;
                        //                    var data = updateTaskInquiryProvider.UploadData(model, guid);
                        //                }

                        //            }
                        //        }
                        //    }

                        //}
                        //else
                        //{
                        //    var stream = files[0].OpenReadStream();
                        //    using (ExcelPackage package = new ExcelPackage(stream))
                        //    {
                        //        ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                        //        int totalRows = workSheet.Dimension.Rows;
                        //        for (int i = 1; i <= 61; i++)
                        //        {
                        //            var data1 = names["labelName" + i].ToString();
                        //            var data2 = workSheet.Cells[1, i].Value.ToString();
                        //            if (data1 != data2)
                        //            {
                        //                result.isSucceed = false;
                        //                result.message = "Template format file yang diupload tidak sesuai ketentuan";
                        //                return Json(result);
                        //            }
                        //        }
                        //        for (int i = 2; i <= totalRows; i++)
                        //        {
                        //            UploadViewModel model = new UploadViewModel();
                        //            model.Number = workSheet.Cells[i, 1].Value == null ? "" : workSheet.Cells[i, 1].Value.ToString();
                        //            model.BranchName = workSheet.Cells[i, 2].Value == null ? "" : workSheet.Cells[i, 2].Value.ToString();
                        //            model.Region = workSheet.Cells[i, 3].Value == null ? "" : workSheet.Cells[i, 3].Value.ToString();
                        //            model.TaskID = workSheet.Cells[i, 4].Value == null ? "" : workSheet.Cells[i, 4].Value.ToString();
                        //            model.JenisTask = workSheet.Cells[i, 5].Value == null ? "" : workSheet.Cells[i, 5].Value.ToString();
                        //            model.CustID = workSheet.Cells[i, 6].Value == null ? "" : workSheet.Cells[i, 6].Value.ToString();
                        //            model.CustomerName = workSheet.Cells[i, 7].Value == null ? "" : workSheet.Cells[i, 7].Value.ToString();
                        //            workSheet.Cells[i, 8].Style.Numberformat.Format = "dd/mm/yyyy hh:mm:ss";
                        //            model.DistributedDate = workSheet.Cells[i, 8].Value == null ? "" : workSheet.Cells[i, 8].Value.ToString();
                        //            model.StartedDate = workSheet.Cells[i, 9].Value == null ? "" : workSheet.Cells[i, 9].Value.ToString();
                        //            model.EmpPosition = workSheet.Cells[i, 10].Value == null ? "" : workSheet.Cells[i, 10].Value.ToString();
                        //            model.soa = workSheet.Cells[i, 11].Value == null ? "" : workSheet.Cells[i, 11].Value.ToString();
                        //            model.Referentor1 = workSheet.Cells[i, 12].Value == null ? "" : workSheet.Cells[i, 12].Value.ToString();
                        //            model.RegionalId = workSheet.Cells[i, 13].Value == null ? "" : workSheet.Cells[i, 13].Value.ToString();
                        //            model.Product = workSheet.Cells[i, 14].Value == null ? "" : workSheet.Cells[i, 14].Value.ToString();
                        //            model.CabId = workSheet.Cells[i, 15].Value == null ? "" : workSheet.Cells[i, 15].Value.ToString();
                        //            model.NIK = workSheet.Cells[i, 16].Value == null ? "" : workSheet.Cells[i, 16].Value.ToString();
                        //            model.TempatLahir = workSheet.Cells[i, 17].Value == null ? "" : workSheet.Cells[i, 17].Value.ToString();
                        //            model.TglLahir = workSheet.Cells[i, 18].Value == null ? "" : workSheet.Cells[i, 18].Value.ToString();
                        //            model.RWLeg = workSheet.Cells[i, 19].Value == null ? "" : workSheet.Cells[i, 19].Value.ToString();
                        //            model.ProvLeg = workSheet.Cells[i, 20].Value == null ? "" : workSheet.Cells[i, 20].Value.ToString();
                        //            model.KabLeg = workSheet.Cells[i, 21].Value == null ? "" : workSheet.Cells[i, 21].Value.ToString();
                        //            model.KecLeg = workSheet.Cells[i, 22].Value == null ? "" : workSheet.Cells[i, 22].Value.ToString();
                        //            model.KelLeg = workSheet.Cells[i, 23].Value == null ? "" : workSheet.Cells[i, 23].Value.ToString();
                        //            model.AlamatRes = workSheet.Cells[i, 24].Value == null ? "" : workSheet.Cells[i, 24].Value.ToString();
                        //            model.RTRes = workSheet.Cells[i, 25].Value == null ? "" : workSheet.Cells[i, 25].Value.ToString();
                        //            model.RWRes = workSheet.Cells[i, 26].Value == null ? "" : workSheet.Cells[i, 26].Value.ToString();
                        //            model.ProvRes = workSheet.Cells[i, 27].Value == null ? "" : workSheet.Cells[i, 27].Value.ToString();
                        //            model.KabRes = workSheet.Cells[i, 28].Value == null ? "" : workSheet.Cells[i, 28].Value.ToString();
                        //            model.KecRes = workSheet.Cells[i, 29].Value == null ? "" : workSheet.Cells[i, 29].Value.ToString();
                        //            model.KelRes = workSheet.Cells[i, 30].Value == null ? "" : workSheet.Cells[i, 30].Value.ToString();
                        //            model.NoMesin = workSheet.Cells[i, 31].Value == null ? "" : workSheet.Cells[i, 31].Value.ToString();
                        //            model.NoRangka = workSheet.Cells[i, 32].Value == null ? "" : workSheet.Cells[i, 32].Value.ToString();
                        //            model.ItemType = workSheet.Cells[i, 33].Value == null ? "" : workSheet.Cells[i, 33].Value.ToString();
                        //            model.ItemDescription = workSheet.Cells[i, 34].Value == null ? "" : workSheet.Cells[i, 34].Value.ToString();
                        //            model.Mobile1 = workSheet.Cells[i, 35].Value == null ? "" : workSheet.Cells[i, 35].Value.ToString();
                        //            model.Mobile2 = workSheet.Cells[i, 36].Value == null ? "" : workSheet.Cells[i, 36].Value.ToString();
                        //            model.Phone1 = workSheet.Cells[i, 37].Value == null ? "" : workSheet.Cells[i, 37].Value.ToString();
                        //            model.Phone2 = workSheet.Cells[i, 38].Value == null ? "" : workSheet.Cells[i, 38].Value.ToString();
                        //            model.OfficePhone1 = workSheet.Cells[i, 39].Value == null ? "" : workSheet.Cells[i, 39].Value.ToString();
                        //            model.OfficePhone2 = workSheet.Cells[i, 40].Value == null ? "" : workSheet.Cells[i, 40].Value.ToString();
                        //            model.OtrPrice = workSheet.Cells[i, 41].Value == null ? "" : workSheet.Cells[i, 41].Value.ToString();
                        //            model.ItemYear = workSheet.Cells[i, 42].Value == null ? "" : workSheet.Cells[i, 42].Value.ToString();
                        //            model.MonthlyIncome = workSheet.Cells[i, 43].Value == null ? "" : workSheet.Cells[i, 43].Value.ToString();
                        //            model.MonthInstallment = workSheet.Cells[i, 44].Value == null ? "" : workSheet.Cells[i, 44].Value.ToString();
                        //            model.DP = workSheet.Cells[i, 45].Value == null ? "" : workSheet.Cells[i, 45].Value.ToString();
                        //            model.LTV = workSheet.Cells[i, 46].Value == null ? "" : workSheet.Cells[i, 46].Value.ToString();
                        //            model.Plafond = workSheet.Cells[i, 47].Value == null ? "" : workSheet.Cells[i, 47].Value.ToString();
                        //            model.Pekerjaan = workSheet.Cells[i, 48].Value == null ? "" : workSheet.Cells[i, 48].Value.ToString();
                        //            model.SisaTenor = workSheet.Cells[i, 49].Value == null ? "" : workSheet.Cells[i, 49].Value.ToString();
                        //            model.TenorId = workSheet.Cells[i, 50].Value == null ? "" : workSheet.Cells[i, 50].Value.ToString();
                        //            model.ReleaseDateBpkb = workSheet.Cells[i, 51].Value == null ? "" : workSheet.Cells[i, 51].Value.ToString();
                        //            model.MaxPastDueDt = workSheet.Cells[i, 52].Value == null ? "" : workSheet.Cells[i, 52].Value.ToString();
                        //            model.Religion = workSheet.Cells[i, 53].Value == null ? "" : workSheet.Cells[i, 53].Value.ToString();
                        //            model.CustomerRating = workSheet.Cells[i, 54].Value == null ? "" : workSheet.Cells[i, 54].Value.ToString();
                        //            model.TanggalJatuhTempo = workSheet.Cells[i, 55].Value == null ? "" : workSheet.Cells[i, 55].Value.ToString();
                        //            model.MaturityDt = workSheet.Cells[i, 56].Value == null ? "" : workSheet.Cells[i, 56].Value.ToString();
                        //            model.StatusCall = workSheet.Cells[i, 57].Value == null ? "" : workSheet.Cells[i, 57].Value.ToString();
                        //            model.AnswerCall = workSheet.Cells[i, 58].Value == null ? "" : workSheet.Cells[i, 58].Value.ToString();
                        //            model.StatusProspek = workSheet.Cells[i, 59].Value == null ? "" : workSheet.Cells[i, 59].Value.ToString();
                        //            model.ReasonNotProspek = workSheet.Cells[i, 60].Value == null ? "" : workSheet.Cells[i, 60].Value.ToString();
                        //            model.Notes = workSheet.Cells[i, 61].Value == null ? "" : workSheet.Cells[i, 61].Value.ToString();
                        //            model.EmpNo = empNo;
                        //            var data = updateTaskInquiryProvider.UploadData(model, guid);
                        //        }
                        //    }
                        //}

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
                                        model.DistributedDate = dtTable.Tables[0].Rows[i][7].ToString();
                                    }
                                    catch {
                                        model.DistributedDate = "";
                                    }
                                    try
                                    {
                                        model.StartedDate = dtTable.Tables[0].Rows[i][8].ToString();
                                    }
                                    catch
                                    {
                                        model.StartedDate = "";
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
                                        model.TglLahir = FromExcelDate(Convert.ToDouble(dtTable.Tables[0].Rows[i][17].ToString())).ToString();
                                    }
                                    catch {
                                        model.TglLahir = "";
                                    }
                                    model.RWLeg = dtTable.Tables[0].Rows[i][18].ToString();
                                    model.ProvLeg = dtTable.Tables[0].Rows[i][19].ToString();
                                    model.KabLeg = dtTable.Tables[0].Rows[i][20].ToString();
                                    model.KecLeg = dtTable.Tables[0].Rows[i][21].ToString();
                                    model.KelLeg = dtTable.Tables[0].Rows[i][22].ToString();
                                    model.AlamatRes = dtTable.Tables[0].Rows[i][23].ToString();
                                    model.RTRes = dtTable.Tables[0].Rows[i][24].ToString();
                                    model.RWRes = dtTable.Tables[0].Rows[i][25].ToString();
                                    model.ProvRes = dtTable.Tables[0].Rows[i][26].ToString();
                                    model.KabRes = dtTable.Tables[0].Rows[i][27].ToString();
                                    model.KecRes = dtTable.Tables[0].Rows[i][28].ToString();
                                    model.KelRes = dtTable.Tables[0].Rows[i][29].ToString();
                                    model.NoMesin = dtTable.Tables[0].Rows[i][30].ToString();
                                    model.NoRangka = dtTable.Tables[0].Rows[i][31].ToString();
                                    model.ItemType = dtTable.Tables[0].Rows[i][32].ToString();
                                    model.ItemDescription = dtTable.Tables[0].Rows[i][33].ToString();
                                    model.Mobile1 = dtTable.Tables[0].Rows[i][34].ToString();
                                    model.Mobile2 = dtTable.Tables[0].Rows[i][35].ToString();
                                    model.Phone1 = dtTable.Tables[0].Rows[i][36].ToString();
                                    model.Phone2 = dtTable.Tables[0].Rows[i][37].ToString();
                                    model.OfficePhone1 = dtTable.Tables[0].Rows[i][38].ToString();
                                    model.OfficePhone2 = dtTable.Tables[0].Rows[i][39].ToString();
                                    model.OtrPrice = dtTable.Tables[0].Rows[i][40].ToString();
                                    model.ItemYear = dtTable.Tables[0].Rows[i][41].ToString();
                                    model.MonthlyIncome = dtTable.Tables[0].Rows[i][42].ToString();
                                    model.MonthInstallment = dtTable.Tables[0].Rows[i][43].ToString();
                                    model.DP = dtTable.Tables[0].Rows[i][44].ToString();
                                    model.LTV = dtTable.Tables[0].Rows[i][45].ToString();
                                    model.Plafond = dtTable.Tables[0].Rows[i][46].ToString();
                                    model.Pekerjaan = dtTable.Tables[0].Rows[i][47].ToString();
                                    model.SisaTenor = dtTable.Tables[0].Rows[i][48].ToString();
                                    model.TenorId = dtTable.Tables[0].Rows[i][49].ToString();
                                    try
                                    {
                                        model.ReleaseDateBpkb = FromExcelDate(Convert.ToDouble(dtTable.Tables[0].Rows[i][50].ToString())).ToString();
                                    }
                                    catch
                                    {
                                        model.ReleaseDateBpkb = "";
                                    }
                                    try
                                    {
                                        model.MaxPastDueDt = FromExcelDate(Convert.ToDouble(dtTable.Tables[0].Rows[i][51].ToString())).ToString();
                                    }
                                    catch
                                    {
                                        model.MaxPastDueDt = "";
                                    }
                                        model.Religion = dtTable.Tables[0].Rows[i][52].ToString();
                                        model.CustomerRating = dtTable.Tables[0].Rows[i][53].ToString();
                                    try
                                    {
                                        model.TanggalJatuhTempo = FromExcelDate(Convert.ToDouble(dtTable.Tables[0].Rows[i][54].ToString())).ToString();
                                    }
                                    catch
                                    {
                                        model.TanggalJatuhTempo = "";
                                    }
                                    try
                                    {
                                        model.MaturityDt = FromExcelDate(Convert.ToDouble(dtTable.Tables[0].Rows[i][55].ToString())).ToString();
                                    }
                                    catch
                                    {
                                        model.MaturityDt = "";
                                    }
                                    model.StatusCall = dtTable.Tables[0].Rows[i][56].ToString();
                                    model.AnswerCall = dtTable.Tables[0].Rows[i][57].ToString();
                                    model.StatusProspek = dtTable.Tables[0].Rows[i][58].ToString();
                                    model.ReasonNotProspek = dtTable.Tables[0].Rows[i][59].ToString();
                                    model.Notes = dtTable.Tables[0].Rows[i][60].ToString();
                                    model.EmpNo = empNo;
                                    var data = updateTaskInquiryProvider.UploadData(model, guid);
                                }

                            //}
                        }

                    }

                }
                updateTaskInquiryProvider.SendApiCekDukcapil(guid);
                updateTaskInquiryProvider.SendApiToWiseMSS(guid);
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
            var data  = updateTaskInquiryProvider.getLog(guid);

            foreach (var item in data)
            {
                tw.WriteLine("TASK ID : " + item.TASK_ID + ", UPLOAD_MESSAGE : " + item.UPLOAD_MESSAGE);
            }
            tw.Flush();
            tw.Close();

            return File(memoryStream.GetBuffer(), "text/plain", "Log_datagagalupload_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        }

        //[HttpPost]
        //public async Task<IActionResult> ReadExcelFileAsync(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return Content("File Not Selected");

        //    string fileExtension = Path.GetExtension(file.FileName);
        //    if (fileExtension != ".xls" && fileExtension != ".xlsx")
        //        return Content("File Not Selected");

        //    var rootFolder = @"D:\Files";
        //    var fileName = file.FileName;
        //    var filePath = Path.Combine(rootFolder, fileName);
        //    var fileLocation = new FileInfo(filePath);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }


        //    using (ExcelPackage package = new ExcelPackage(fileLocation))
        //    {
        //        ExcelWorksheet workSheet = package.Workbook.Worksheets["TASK INQUIRY"];
        //        //var workSheet = package.Workbook.Worksheets.First();
        //        int totalRows = workSheet.Dimension.Rows;

        //        var DataList = new List<DownloadViewModel>();

        //        for (int i = 2; i <= totalRows; i++)
        //        {
        //            DataList.Add(new DownloadViewModel
        //            {
        //                Number = workSheet.Cells[i, 1].Value.ToString(),
        //            });
        //        }
        //    }
        //    return Ok();
        //}


    }
}