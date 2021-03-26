using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_WEB.Models.UploadTaskInquiry;
using MKT_POLOSYS_WEB.Providers;
using OfficeOpenXml;

namespace MKT_POLOSYS_WEB.Controllers.TaskInquiry
{
    public class UploadTaskInquiryController : Controller
    {

        //public TaskInquiryController(TaskInquiryProvider taskInquiryProvider)
        //{
        //    this.taskInquiryProvider = taskInquiryProvider;


        //}
        // GET: TaskInquiry
        public ActionResult Index(string emp_no)
        {
            UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
            var model = new IndexViewModel();
            model.empNo = emp_no;
            return View(model);
        }

        public IActionResult ValidasiUpload(string pEmpNo)
        {
            UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
            Boolean isSucceed = true;
            isSucceed = updateTaskInquiryProvider.validasiDownload(pEmpNo);
            return Json(isSucceed);

        }

        [HttpPost]
        public ActionResult Upload(string empNo)
        {
            UpdateTaskInquiryProvider updateTaskInquiryProvider = new UpdateTaskInquiryProvider();
            var files = Request.Form.Files;
            foreach (var uploadFile in files)
            {
                if (uploadFile != null)
                {
                    var stream = files[0].OpenReadStream();
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["TASK INQUIRY"];
                        //var workSheet = package.Workbook.Worksheets.First();
                        int totalRows = workSheet.Dimension.Rows;

                        var DataList = new List<UploadViewModel>();

                        for (int i = 2; i <= totalRows; i++)
                        {
                            UploadViewModel model = new UploadViewModel();
                            model.Number = workSheet.Cells[i, 1].Value == null ? "" : workSheet.Cells[i, 1].Value.ToString();
                            model.BranchName = workSheet.Cells[i, 2].Value == null ? "" : workSheet.Cells[i, 2].Value.ToString();
                            model.Region = workSheet.Cells[i, 3].Value == null ? "" : workSheet.Cells[i, 3].Value.ToString();
                            model.TaskID = workSheet.Cells[i, 4].Value == null ? "" : workSheet.Cells[i, 4].Value.ToString();
                            model.JenisTask = workSheet.Cells[i, 5].Value == null ? "" : workSheet.Cells[i, 5].Value.ToString();
                            model.CustID = workSheet.Cells[i, 6].Value == null ? "" : workSheet.Cells[i, 6].Value.ToString();
                            model.CustomerName = workSheet.Cells[i, 7].Value == null ? "" : workSheet.Cells[i, 7].Value.ToString();
                            model.DistributedDate = workSheet.Cells[i, 8].Value == null ? "" : workSheet.Cells[i, 8].Value.ToString();
                            model.StartedDate = workSheet.Cells[i, 9].Value == null ? "" : workSheet.Cells[i, 9].Value.ToString();
                            model.EmpPosition = workSheet.Cells[i, 10].Value == null ? "" : workSheet.Cells[i, 10].Value.ToString();
                            model.soa = workSheet.Cells[i, 11].Value == null ? "" : workSheet.Cells[i, 11].Value.ToString();
                            model.Referentor1 = workSheet.Cells[i, 12].Value == null ? "" : workSheet.Cells[i, 12].Value.ToString();
                            model.RegionalId = workSheet.Cells[i, 13].Value == null ? "" : workSheet.Cells[i, 13].Value.ToString();
                            model.Product = workSheet.Cells[i, 14].Value == null ? "" : workSheet.Cells[i, 14].Value.ToString();
                            model.CabId = workSheet.Cells[i, 15].Value == null ? "" : workSheet.Cells[i, 15].Value.ToString();
                            model.NIK = workSheet.Cells[i, 16].Value == null ? "" : workSheet.Cells[i, 16].Value.ToString();
                            model.TempatLahir = workSheet.Cells[i, 17].Value == null ? "" : workSheet.Cells[i, 17].Value.ToString();
                            model.TglLahir = workSheet.Cells[i, 18].Value == null ? "" : workSheet.Cells[i, 18].Value.ToString();
                            model.RWLeg = workSheet.Cells[i, 19].Value == null ? "" : workSheet.Cells[i, 19].Value.ToString();
                            model.ProvLeg = workSheet.Cells[i, 20].Value == null ? "" : workSheet.Cells[i, 20].Value.ToString();
                            model.KabLeg = workSheet.Cells[i, 21].Value == null ? "" : workSheet.Cells[i, 21].Value.ToString();
                            model.KecLeg = workSheet.Cells[i, 22].Value == null ? "" : workSheet.Cells[i, 22].Value.ToString();
                            model.KelLeg = workSheet.Cells[i, 23].Value == null ? "" : workSheet.Cells[i, 23].Value.ToString();
                            model.AlamatRes = workSheet.Cells[i, 24].Value == null ? "" : workSheet.Cells[i, 24].Value.ToString();
                            model.RTRes = workSheet.Cells[i, 25].Value == null ? "" : workSheet.Cells[i, 25].Value.ToString();
                            model.RWRes = workSheet.Cells[i, 26].Value == null ? "" : workSheet.Cells[i, 26].Value.ToString();
                            model.ProvRes = workSheet.Cells[i, 27].Value == null ? "" : workSheet.Cells[i, 27].Value.ToString();
                            model.KabRes = workSheet.Cells[i, 28].Value == null ? "" : workSheet.Cells[i, 28].Value.ToString();
                            model.KecRes = workSheet.Cells[i, 29].Value == null ? "" : workSheet.Cells[i, 29].Value.ToString();
                            model.KelRes = workSheet.Cells[i, 30].Value == null ? "" : workSheet.Cells[i, 30].Value.ToString();
                            model.NoMesin = workSheet.Cells[i, 31].Value == null ? "" : workSheet.Cells[i, 31].Value.ToString();
                            model.NoRangka = workSheet.Cells[i, 32].Value == null ? "" : workSheet.Cells[i, 32].Value.ToString();
                            model.ItemType = workSheet.Cells[i, 33].Value == null ? "" : workSheet.Cells[i, 33].Value.ToString();
                            model.ItemDescription = workSheet.Cells[i, 34].Value == null ? "" : workSheet.Cells[i, 34].Value.ToString();
                            model.Mobile1 = workSheet.Cells[i, 35].Value == null ? "" : workSheet.Cells[i, 35].Value.ToString();
                            model.Mobile2 = workSheet.Cells[i, 36].Value == null ? "" : workSheet.Cells[i, 36].Value.ToString();
                            model.Phone1 = workSheet.Cells[i, 37].Value == null ? "" : workSheet.Cells[i, 37].Value.ToString();
                            model.Phone2 = workSheet.Cells[i, 38].Value == null ? "" : workSheet.Cells[i, 38].Value.ToString();
                            model.OfficePhone1 = workSheet.Cells[i, 39].Value == null ? "" : workSheet.Cells[i, 39].Value.ToString();
                            model.OfficePhone2 = workSheet.Cells[i, 40].Value == null ? "" : workSheet.Cells[i, 40].Value.ToString();
                            model.OtrPrice = workSheet.Cells[i, 41].Value == null ? "" : workSheet.Cells[i, 41].Value.ToString();
                            model.ItemYear = workSheet.Cells[i, 42].Value == null ? "" : workSheet.Cells[i, 42].Value.ToString();
                            model.MonthlyIncome = workSheet.Cells[i, 43].Value == null ? "" : workSheet.Cells[i, 43].Value.ToString();
                            model.MonthInstallment = workSheet.Cells[i, 44].Value == null ? "" : workSheet.Cells[i, 44].Value.ToString();
                            model.DP = workSheet.Cells[i, 45].Value == null ? "" : workSheet.Cells[i, 45].Value.ToString();
                            model.LTV = workSheet.Cells[i, 46].Value == null ? "" : workSheet.Cells[i, 46].Value.ToString();
                            model.Plafond = workSheet.Cells[i, 47].Value == null ? "" : workSheet.Cells[i, 47].Value.ToString();
                            model.Pekerjaan = workSheet.Cells[i, 48].Value == null ? "" : workSheet.Cells[i, 48].Value.ToString();
                            model.SisaTenor = workSheet.Cells[i, 49].Value == null ? "" : workSheet.Cells[i, 49].Value.ToString();
                            model.TenorId = workSheet.Cells[i, 50].Value == null ? "" : workSheet.Cells[i, 50].Value.ToString();
                            model.ReleaseDateBpkb = workSheet.Cells[i, 51].Value == null ? "" : workSheet.Cells[i, 51].Value.ToString();
                            model.MaxPastDueDt = workSheet.Cells[i, 52].Value == null ? "" : workSheet.Cells[i, 52].Value.ToString();
                            model.Religion = workSheet.Cells[i, 53].Value == null ? "" : workSheet.Cells[i, 53].Value.ToString();
                            model.CustomerRating = workSheet.Cells[i, 54].Value == null ? "" : workSheet.Cells[i, 54].Value.ToString();
                            model.TanggalJatuhTempo = workSheet.Cells[i, 55].Value == null ? "" : workSheet.Cells[i, 55].Value.ToString();
                            model.MaturityDt = workSheet.Cells[i, 56].Value == null ? "" : workSheet.Cells[i, 56].Value.ToString();
                            model.StatusCall = workSheet.Cells[i, 57].Value == null ? "" : workSheet.Cells[i, 57].Value.ToString();
                            model.AnswerCall = workSheet.Cells[i, 58].Value == null ? "" : workSheet.Cells[i, 58].Value.ToString();
                            model.StatusProspek = workSheet.Cells[i, 59].Value == null ? "" : workSheet.Cells[i, 59].Value.ToString();
                            model.ReasonNotProspek = workSheet.Cells[i, 60].Value == null ? "" : workSheet.Cells[i, 60].Value.ToString();
                            model.Notes = workSheet.Cells[i, 61].Value == null ? "" : workSheet.Cells[i, 61].Value.ToString();
                            model.EmpNo = empNo;
                            var data = updateTaskInquiryProvider.UploadData(model);
                            DataList.Add(model);
                        }

                        foreach (var item in DataList)
                        {
                            var custID = item.CustID;
                        }

                    }

                }

            }


          

            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            Boolean isSucceed = true;
            isSucceed = taskInquiryProvider.validasiDownload("a");
            return Json(isSucceed);

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