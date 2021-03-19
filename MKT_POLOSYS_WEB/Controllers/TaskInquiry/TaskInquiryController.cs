using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_WEB.Models;
using MKT_POLOSYS_WEB.Providers;

namespace MKT_POLOSYS_WEB.Controllers.TaskInquiry
{
    public class TaskInquiryController : Controller
    {

        //public TaskInquiryController(TaskInquiryProvider taskInquiryProvider)
        //{
        //    this.taskInquiryProvider = taskInquiryProvider;


        //}
        // GET: TaskInquiry
        public ActionResult Index()
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            var model = new IndexViewModel();
            model.ddlRegion = taskInquiryProvider.ddlRegion().ToList();
            model.ddlBranch = taskInquiryProvider.ddlBranch().ToList();
            model.ddlEmpPosition = taskInquiryProvider.ddlEmpPosition().ToList();
            model.ddlStsProspek = taskInquiryProvider.ddlStsProspek().ToList();
            model.ddlPriotityLevel = taskInquiryProvider.ddlPriotityLevel().ToList();
            model.ddlStatusDukcapil = taskInquiryProvider.ddlStatusDukcapil().ToList();
            model.ddlSourceData = taskInquiryProvider.ddlSourceData().ToList();
            return View(model);
        }


        // GET: TaskInquiry/Details/5
        public ActionResult Views(int id)
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            CreateEditViewModel model = new CreateEditViewModel();
            var data = taskInquiryProvider.getView(id);
            model.TaskID = data.Value.TaskID;
            model.JenisTask = data.Value.JenisTask;
            model.CustID = data.Value.CustID;
            model.CustomerName = data.Value.CustomerName;
            model.DistributedDate = data.Value.DistributedDate;
            model.StartedDate = data.Value.StartedDate;
            model.StatusDukcapil = data.Value.StatusDukcapil;
            model.FieldPersonName = data.Value.FieldPersonName;
            model.EmpPosition = data.Value.EmpPosition;
            model.StatusProspek = data.Value.StatusProspek;
            model.PriorityLevel = data.Value.PriorityLevel;
            model.AplikasiIA = data.Value.AplikasiIA;
            model.AplicationID = data.Value.AplicationID;
            model.SourceData = data.Value.SourceData;

            model.SourceData = data.Value.SourceData;
            model.NIK = data.Value.NIK;

            //---
            model.TempatLahir = data.Value.TempatLahir;
            model.TglLahir = data.Value.TglLahir;
            model.AlamatLeg = data.Value.AlamatLeg;
            model.ProvLeg = data.Value.ProvLeg;
            model.KabLeg = data.Value.KabLeg;
            model.KelLeg = data.Value.KelLeg;
            model.RTLeg = data.Value.RTLeg;
            model.RWLeg = data.Value.RWLeg;
            model.AlamatRes = data.Value.AlamatRes;
            model.ProvRes = data.Value.ProvRes;
            model.KabRes = data.Value.KabRes;
            model.KelRes = data.Value.KelRes;
            model.RTRes = data.Value.RTRes;
            model.RWRes = data.Value.RWRes;
            model.KodePosRes = data.Value.KodePosRes;
            model.SubZipcodeRes = data.Value.SubZipcodeRes;
            model.Product = data.Value.Product;
            model.ItemType = data.Value.ItemType;
            model.ItemYear = data.Value.ItemYear;
            model.OtrPrice = data.Value.OtrPrice;
            model.DP = data.Value.DP;
            model.LTV = data.Value.LTV;

            model.Tenor = data.Value.Tenor;
            model.Plafond = data.Value.Plafond;
            model.MonthInstallment = data.Value.MonthInstallment;
            model.Notes = data.Value.Notes;
            return View("CreateEdit", model);
        }
        public ActionResult ListDetail()
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            var result = taskInquiryProvider.get();
            return Json(result);
        }

        public ActionResult ListDetailTask()
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            var result = taskInquiryProvider.getDetail();
            return Json(result);
        }

        public IActionResult Excel()
        {
            using (var workbook = new XLWorkbook())
            {
                TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Branch Name";
                worksheet.Cell(currentRow, 3).Value = "Region";
                worksheet.Cell(currentRow, 4).Value = "Task ID";
                worksheet.Cell(currentRow, 5).Value = "Jenis Task";
                worksheet.Cell(currentRow, 6).Value = "Cust ID";


                worksheet.Cell(currentRow, 7).Value = "Customer Name";
                worksheet.Cell(currentRow, 8).Value = "Distributed Date";
                worksheet.Cell(currentRow, 9).Value = "Started Date";
                worksheet.Cell(currentRow, 10).Value = "Emp Position";
                worksheet.Cell(currentRow, 11).Value = "SOA";
                worksheet.Cell(currentRow, 12).Value = "Referentor 1";
                worksheet.Cell(currentRow, 13).Value = "Regional_id";
                worksheet.Cell(currentRow, 14).Value = "Product";
                worksheet.Cell(currentRow, 15).Value = "Cab_Id";
                worksheet.Cell(currentRow, 16).Value = "Nik Ktp"; 
                worksheet.Cell(currentRow, 17).Value = "Tempat Lahir";
                worksheet.Cell(currentRow, 18).Value = "Tanggal Lahir";
                worksheet.Cell(currentRow, 19).Value = "Rw (Legal)";
                worksheet.Cell(currentRow, 20).Value = "Provinsi (Legal)";
                worksheet.Cell(currentRow, 21).Value = "Kabupaten (Legal)";
                worksheet.Cell(currentRow, 22).Value = "Kecamatan (Legal)";
                worksheet.Cell(currentRow, 23).Value = "Kelurahan (Legal)";
                worksheet.Cell(currentRow, 24).Value = "Alamat (Survey)";
                worksheet.Cell(currentRow, 25).Value = "Rt (Survey)";
                worksheet.Cell(currentRow, 26).Value = "Rw (Survey)";
                worksheet.Cell(currentRow, 27).Value = "Provinsi (Survey)";
                worksheet.Cell(currentRow, 28).Value = "Kabupaten (Survey)";
                worksheet.Cell(currentRow, 29).Value = "Kecamatan (Survey)";
                worksheet.Cell(currentRow, 30).Value = "Kelurahan (Survey)";
                worksheet.Cell(currentRow, 31).Value = "No_Mesin";
                worksheet.Cell(currentRow, 32).Value = "No_Rangka";
                worksheet.Cell(currentRow, 33).Value = "Item_Type";
                worksheet.Cell(currentRow, 34).Value = "Item_Description";
                worksheet.Cell(currentRow, 35).Value = "Mobile1";
                worksheet.Cell(currentRow, 36).Value = "Mobile2";
                worksheet.Cell(currentRow, 37).Value = "Phone1";
                worksheet.Cell(currentRow, 38).Value = "Phone2";
                worksheet.Cell(currentRow, 39).Value = "Office_Phone1";
                worksheet.Cell(currentRow, 40).Value = "Office_Phone2";
                worksheet.Cell(currentRow, 41).Value = "Otr_Price";
                worksheet.Cell(currentRow, 42).Value = "Item_Year";
                worksheet.Cell(currentRow, 43).Value = "Monthly_Income";
                worksheet.Cell(currentRow, 44).Value = "Monthly_Installament";
                worksheet.Cell(currentRow, 45).Value = "Down Payment";
                worksheet.Cell(currentRow, 46).Value = "LTV";
                worksheet.Cell(currentRow, 47).Value = "Plafond";
                worksheet.Cell(currentRow, 48).Value = "Pekerjaan";
                worksheet.Cell(currentRow, 49).Value = "Sisa_Tenor";
                worksheet.Cell(currentRow, 50).Value = "Tenor";
                worksheet.Cell(currentRow, 51).Value = "Realease_Date_Bpkb";
                worksheet.Cell(currentRow, 52).Value = "Max_Past_Due_Dt";
                worksheet.Cell(currentRow, 53).Value = "Religion";
                worksheet.Cell(currentRow, 54).Value = "Customer_Rating";
                worksheet.Cell(currentRow, 55).Value = "Tanggal_Jatuh_Tempo";
                worksheet.Cell(currentRow, 56).Value = "Maturity_Dt";
                worksheet.Cell(currentRow, 57).Value = "Status Call";
                worksheet.Cell(currentRow, 58).Value = "Answer Call";
                worksheet.Cell(currentRow, 59).Value = "Status Prospek";
                worksheet.Cell(currentRow, 60).Value = "Reason Not Prospek";
                worksheet.Cell(currentRow, 61).Value = "Notes";
                var result = taskInquiryProvider.ListDownload();
                foreach (var item in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.sourceData;
                    worksheet.Cell(currentRow, 2).Value = item.cabang;
                    worksheet.Cell(currentRow, 3).Value = item.regional;
                    worksheet.Cell(currentRow, 4).Value = item.taskID;
                    worksheet.Cell(currentRow, 5).Value = item.jenisTask;
                    worksheet.Cell(currentRow, 6).Value = item.customerID;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Task_Inquiry.xlsx");
                }
            }
        }
    }
}