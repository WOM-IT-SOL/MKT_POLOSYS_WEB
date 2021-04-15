using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_WEB.Models;
using MKT_POLOSYS_WEB.Providers;

namespace MKT_POLOSYS_WEB.Controllers.ChangeDukcapil
{
    public class ChangeDukcapilController : Controller
    {
        // GET: TaskInquiry
        public ActionResult Index(string emp_no)
        {
            try
            {
                ChangeDukcapilProvider changeDukcapilProvider = new ChangeDukcapilProvider();
                Boolean isSucceed = true;
                var model = new IndexViewModel();
                model.ddlRegion = changeDukcapilProvider.ddlRegion().ToList();
                model.ddlBranch = changeDukcapilProvider.ddlBranch().ToList();
                model.ddlEmpPosition = changeDukcapilProvider.ddlEmpPosition().ToList();
                model.ddlStsProspek = changeDukcapilProvider.ddlStsProspek().ToList();
                model.ddlPriorityLevel = changeDukcapilProvider.ddlPriorityLevel("All", "All", "All").ToList();
                model.ddlStatusDukcapil = changeDukcapilProvider.ddlStatusDukcapil().ToList();
                model.ddlSourceData = changeDukcapilProvider.ddlSourceData().ToList();
                var base64EncodedBytes = System.Convert.FromBase64String(emp_no);
                var empNo = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                model.empNo = empNo;
                isSucceed = changeDukcapilProvider.validasiUser(model.empNo);
                model.empName = changeDukcapilProvider.getUser(model.empNo);
                if (isSucceed)
                {
                    ViewData["empNames"] = model.empName;
                    return View(model);
                }
                else
                {

                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            catch(Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

        }


        public ActionResult ListDetail(ParamListDetail model)
        {
            ChangeDukcapilProvider changeDukcapilProvider = new ChangeDukcapilProvider();
            var result = changeDukcapilProvider.get(model);
            return Json(result);
        }

        public ActionResult DdlPriorityLvl(string source,string empPost,string prospect)
        {
            ChangeDukcapilProvider changeDukcapilProvider = new ChangeDukcapilProvider();
            var result = changeDukcapilProvider.ddlPriorityLevel(source, empPost,prospect);
            return Json(result);
        }

        public IActionResult Excel(string pRegion,
            string pFPName, string pBranchName, string pEmpPosition,
            string pTaskID, string pStatProspek, string pAppID, string[] pPriorityLevel,
            string pCustName, string pStatDukcapil, string pSdate, string pEdate,
            string pSource, string pSourceData, string pEmpNo)
        {
            using (var workbook = new XLWorkbook())
            {
                ChangeDukcapilProvider changeDukcapilProvider = new ChangeDukcapilProvider();
                var worksheet = workbook.Worksheets.Add("DUKCAPIL");
                var currentRow = 1;
                worksheet.Cell(currentRow, 2).Value = "Task ID";
                worksheet.Cell(currentRow, 2).Value = "Nik Ktp";
                worksheet.Cell(currentRow, 3).Value = "Customer Name";
                worksheet.Cell(currentRow, 5).Value = "Tempat Lahir";
                worksheet.Cell(currentRow, 6).Value = "Tanggal Lahir";
                var result = changeDukcapilProvider.ListDownload(pRegion,
                    pFPName, pBranchName, pEmpPosition,
             pTaskID, pStatProspek, pAppID, pPriorityLevel,
             pCustName, pStatDukcapil, pSdate, pEdate,
             pSource, pSourceData, pEmpNo);
                foreach (var item in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.TaskID;
                    worksheet.Cell(currentRow, 2).Value = "'" + item.NIK; 
                    worksheet.Cell(currentRow, 3).Value = item.CustomerName;
                    worksheet.Cell(currentRow, 4).Value = item.TempatLahir;
                    try
                    {
                        worksheet.Cell(currentRow, 5).Value = "'" + Convert.ToDateTime(item.TglLahir).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 5).Value = "'" + item.TglLahir;
                    }

                }

                var type = changeDukcapilProvider.validasiUserType(pEmpNo);
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       pEmpNo + "_"+ type + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
                }
            }
        }

        public IActionResult ExcelDetail(int pID, string pEmpNo)
        {
            using (var workbook = new XLWorkbook())
            {
                ChangeDukcapilProvider changeDukcapilProvider = new ChangeDukcapilProvider();
                var worksheet = workbook.Worksheets.Add("DUKCAPIL");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Task ID";
                worksheet.Cell(currentRow, 2).Value = "Nik Ktp";
                worksheet.Cell(currentRow, 3).Value = "Customer Name";
                worksheet.Cell(currentRow, 4).Value = "Tempat Lahir";
                worksheet.Cell(currentRow, 5).Value = "Tanggal Lahir";
                var result = changeDukcapilProvider.ListDownloadDetail(pID, pEmpNo);
                foreach (var item in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.TaskID;
                    worksheet.Cell(currentRow, 2).Value = "'" + item.NIK;
                    worksheet.Cell(currentRow, 3).Value = item.CustomerName;
                    worksheet.Cell(currentRow, 4).Value = item.TempatLahir;
                    try
                    {
                        worksheet.Cell(currentRow, 5).Value = "'" + Convert.ToDateTime(item.TglLahir).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 5).Value = "'" + item.TglLahir;
                    }

                }
                var type = changeDukcapilProvider.validasiUserType(pEmpNo);
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       pEmpNo + "_"+ type + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
                }
            }
        }

        public IActionResult DecriptUser(string Id, string userName)
        {
            ChangeDukcapilProvider changeDukcapilProvider = new ChangeDukcapilProvider();
            Boolean isSucceed = true;
            var idDecript = Id + "|" + userName;
            var idDecriptEncrypt = System.Text.Encoding.UTF8.GetBytes(idDecript);
            var idDecriptEncrypt64 = Convert.ToBase64String(idDecriptEncrypt);
            var result = idDecriptEncrypt64;
            return Json(result);

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

            ChangeDukcapilProvider changeDukcapilProvider = new ChangeDukcapilProvider();
            try
            {
                var files = Request.Form.Files;
                bool isSucceed = true;

                foreach (var uploadFile in files)
                {
                    if (uploadFile != null)
                    {
                        Dictionary<string, string> names = new Dictionary<string, string>();
                        names.Add("labelName1", "Task ID");
                        names.Add("labelName2", "Nik Ktp");
                        names.Add("labelName3", "Customer Name");
                        names.Add("labelName4", "Tempat Lahir");
                        names.Add("labelName5", "Tanggal Lahir");
                        string sFileExtension = Path.GetExtension(uploadFile.FileName).ToLower();

                        if ((uploadFile != null) && !string.IsNullOrEmpty(uploadFile.FileName))
                        {
                            string fileName = uploadFile.FileName;
                            string fileExtension = System.IO.Path.GetExtension(fileName);
                            var stream = files[0].OpenReadStream();
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
                            for (int j = 0; j <= 4; j++)
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
                                DownloadDukcapilViewModel model = new DownloadDukcapilViewModel();
                                model.TaskID = dtTable.Tables[0].Rows[i][0].ToString();
                                model.NIK = dtTable.Tables[0].Rows[i][1].ToString();
                                model.CustomerName = dtTable.Tables[0].Rows[i][2].ToString();
                                model.TempatLahir = dtTable.Tables[0].Rows[i][3].ToString();
                                try
                                {

                                    var locDate = dtTable.Tables[0].Rows[i][4].ToString();
                                    model.TglLahir = Convert.ToDateTime(locDate).ToString("yyyy-MM-dd HH:mm:ss.mmm");
                                }
                                catch
                                {
                                    model.TglLahir = dtTable.Tables[0].Rows[i][4].ToString();
                                }
                                model.EmpNo = empNo;
                                var data = changeDukcapilProvider.UploadData(model, guid);
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
                Task.Run(async () => await changeDukcapilProvider.SendApiCekDukcapil(guid));
                await changeDukcapilProvider.SendApiToWiseMSS(guid);
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