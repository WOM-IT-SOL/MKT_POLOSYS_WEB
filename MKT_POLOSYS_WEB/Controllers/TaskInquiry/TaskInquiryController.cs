﻿using System;
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
        // GET: TaskInquiry
        public ActionResult Index(string emp_no)
        {
            try
            {
                TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
                Boolean isSucceed = true;
                var model = new IndexViewModel();
                model.ddlRegion = taskInquiryProvider.ddlRegion().ToList();
                model.ddlBranch = taskInquiryProvider.ddlBranch().ToList();
                model.ddlEmpPosition = taskInquiryProvider.ddlEmpPosition().ToList();
                model.ddlStsProspek = taskInquiryProvider.ddlStsProspek().ToList();
                model.ddlPriorityLevel = taskInquiryProvider.ddlPriorityLevel("All", "All", "All").ToList();
                model.ddlStatusDukcapil = taskInquiryProvider.ddlStatusDukcapil().ToList();
                model.ddlSourceData = taskInquiryProvider.ddlSourceData().ToList();
                var base64EncodedBytes = System.Convert.FromBase64String(emp_no);
                var empNo = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                model.empNo = empNo;
                isSucceed = taskInquiryProvider.validasiUser(model.empNo);
                model.empName = taskInquiryProvider.getUser(model.empNo);
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


        // GET: TaskInquiry/Details/5
        public ActionResult Views(string Id)
        {

            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(Id);
                var idDecrt = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                string[] paramList = idDecrt.Split("|");
                int id = Convert.ToInt32(paramList[0].ToString());
                ViewData["empNames"] = paramList[1].ToString();
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
                model.KecLeg = data.Value.KecLeg;
                model.KelLeg = data.Value.KelLeg;
                model.RTLeg = data.Value.RTLeg;
                model.RWLeg = data.Value.RWLeg;
                model.AlamatRes = data.Value.AlamatRes;
                model.ProvRes = data.Value.ProvRes;
                model.KabRes = data.Value.KabRes;
                model.KecRes = data.Value.KecRes;
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
                model.KodePosLeg = data.Value.KodePosLeg;
                model.SubZipcodeLeg = data.Value.SubZipcodeLeg;
                return View("CreateEdit", model);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult ViewsTask(string Id)
        {

            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(Id);
                var idDecrt = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                string[] paramList = idDecrt.Split("|");
                int id = Convert.ToInt32(paramList[0].ToString());
                ViewData["empNames"] = paramList[1].ToString();
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
                model.KecLeg = data.Value.KecLeg;
                model.KelLeg = data.Value.KelLeg;
                model.RTLeg = data.Value.RTLeg;
                model.RWLeg = data.Value.RWLeg;
                model.AlamatRes = data.Value.AlamatRes;
                model.ProvRes = data.Value.ProvRes;
                model.KabRes = data.Value.KabRes;
                model.KecRes = data.Value.KecRes;
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
                model.KodePosLeg = data.Value.KodePosLeg;
                model.SubZipcodeLeg = data.Value.SubZipcodeLeg;
                return View("~/Views/TaskInquiryView/CreateEdit.cshtml", model);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult ListDetail(ParamListDetail model)
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            var result = taskInquiryProvider.get(model);
            return Json(result);
        }

        public ActionResult ListDetailTask(string idTask)
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            var result = taskInquiryProvider.getDetail(idTask);
            return Json(result);
        }

        public ActionResult DdlPriorityLvl(string source,string empPost,string prospect)
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            var result = taskInquiryProvider.ddlPriorityLevel(source, empPost,prospect);
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
                TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
                var worksheet = workbook.Worksheets.Add("TASK INQUIRY");
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
                worksheet.Cell(currentRow, 12).Value = "Referantor 1";
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
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 24).Value = "Kode Pos (Legal)";
                worksheet.Cell(currentRow, 25).Value = "SubZipcode (Legal)";
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 26).Value = "Alamat (Survey)";
                worksheet.Cell(currentRow, 27).Value = "Rt (Survey)";
                worksheet.Cell(currentRow, 28).Value = "Rw (Survey)";
                worksheet.Cell(currentRow, 29).Value = "Provinsi (Survey)";
                worksheet.Cell(currentRow, 30).Value = "Kabupaten (Survey)";
                worksheet.Cell(currentRow, 31).Value = "Kecamatan (Survey)";
                worksheet.Cell(currentRow, 32).Value = "Kelurahan (Survey)";
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 33).Value = "Kode Pos (Survey)";
                worksheet.Cell(currentRow, 34).Value = "SubZipcode (Survey)";
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 35).Value = "No_Mesin";
                worksheet.Cell(currentRow, 36).Value = "No_Rangka";
                worksheet.Cell(currentRow, 37).Value = "Item_Type";
                worksheet.Cell(currentRow, 38).Value = "Item_Description";
                worksheet.Cell(currentRow, 39).Value = "Mobile1";
                worksheet.Cell(currentRow, 40).Value = "Mobile2";
                worksheet.Cell(currentRow, 41).Value = "Phone1";
                worksheet.Cell(currentRow, 42).Value = "Phone2";
                worksheet.Cell(currentRow, 43).Value = "Office_Phone1";
                worksheet.Cell(currentRow, 44).Value = "Office_Phone2";
                worksheet.Cell(currentRow, 45).Value = "Otr_Price";
                worksheet.Cell(currentRow, 46).Value = "Item_Year";
                worksheet.Cell(currentRow, 47).Value = "Monthly_Income";
                worksheet.Cell(currentRow, 48).Value = "Monthly_Installament";
                worksheet.Cell(currentRow, 49).Value = "Down Payment Pengajuan";
                worksheet.Cell(currentRow, 50).Value = "LTV Pengajuan";
                ///////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 51).Value = "Tenor Pengajuan";
                ///////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 52).Value = "Plafond Max";
                worksheet.Cell(currentRow, 53).Value = "Pekerjaan";
                worksheet.Cell(currentRow, 54).Value = "Sisa_Tenor";
                worksheet.Cell(currentRow, 55).Value = "Realease_Date_Bpkb";
                worksheet.Cell(currentRow, 56).Value = "Max_Past_Due_Dt";
                worksheet.Cell(currentRow, 57).Value = "Religion";
                worksheet.Cell(currentRow, 58).Value = "Customer_Rating";
                worksheet.Cell(currentRow, 59).Value = "Tanggal_Jatuh_Tempo";
                worksheet.Cell(currentRow, 60).Value = "Maturity_Dt";
                worksheet.Cell(currentRow, 61).Value = "Status Call";
                worksheet.Cell(currentRow, 62).Value = "Answer Call";
                worksheet.Cell(currentRow, 63).Value = "Status Prospek";
                worksheet.Cell(currentRow, 64).Value = "Reason Not Prospek";
                worksheet.Cell(currentRow, 65).Value = "Notes";
                var result = taskInquiryProvider.ListDownload(pRegion,
                    pFPName, pBranchName, pEmpPosition,
             pTaskID, pStatProspek, pAppID, pPriorityLevel,
             pCustName, pStatDukcapil, pSdate, pEdate,
             pSource, pSourceData, pEmpNo);
                foreach (var item in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Number;
                    worksheet.Cell(currentRow, 2).Value = item.BranchName;
                    worksheet.Cell(currentRow, 3).Value = "'" + item.Region;
                    worksheet.Cell(currentRow, 4).Value = item.TaskID;
                    worksheet.Cell(currentRow, 5).Value = item.JenisTask;
                    worksheet.Cell(currentRow, 6).Value = "'" + item.CustID;
                    worksheet.Cell(currentRow, 7).Value = item.CustomerName;
                    try
                    {
                        worksheet.Cell(currentRow, 8).Value = "'" + Convert.ToDateTime(item.DistributedDate).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 8).Value = "'" + item.DistributedDate;
                    }
                    try
                    {
                        worksheet.Cell(currentRow, 9).Value = "'" + Convert.ToDateTime(item.StartedDate).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 9).Value = "'" + item.StartedDate;
                    }
                    worksheet.Cell(currentRow, 10).Value = item.EmpPosition;
                    worksheet.Cell(currentRow, 11).Value = item.soa;
                    worksheet.Cell(currentRow, 12).Value = "'" + item.Referentor1;
                    worksheet.Cell(currentRow, 13).Value = "'" + item.RegionalId;
                    worksheet.Cell(currentRow, 14).Value = item.Product;
                    worksheet.Cell(currentRow, 15).Value = "'" + item.CabId;
                    worksheet.Cell(currentRow, 16).Value = "'" + item.NIK;
                    worksheet.Cell(currentRow, 17).Value = item.TempatLahir;
                    try
                    {
                        worksheet.Cell(currentRow, 18).Value = "'" + Convert.ToDateTime(item.TglLahir).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 18).Value = "'" + item.TglLahir;
                    }
                    worksheet.Cell(currentRow, 19).Value = "'" + item.RWLeg;
                    worksheet.Cell(currentRow, 20).Value = item.ProvLeg;
                    worksheet.Cell(currentRow, 21).Value = item.KabLeg;
                    worksheet.Cell(currentRow, 22).Value = item.KecLeg;
                    worksheet.Cell(currentRow, 23).Value = item.KelLeg;
                    worksheet.Cell(currentRow, 24).Value = item.KodeposLeg;
                    worksheet.Cell(currentRow, 25).Value = item.SubZipcodeLeg;
                    worksheet.Cell(currentRow, 26).Value = item.AlamatRes;
                    worksheet.Cell(currentRow, 27).Value = "'" + item.RTRes;
                    worksheet.Cell(currentRow, 28).Value = "'" + item.RWRes;
                    worksheet.Cell(currentRow, 29).Value = item.ProvRes;
                    worksheet.Cell(currentRow, 30).Value = item.KabRes;
                    worksheet.Cell(currentRow, 31).Value = item.KecRes;
                    worksheet.Cell(currentRow, 32).Value = item.KelRes;
                    worksheet.Cell(currentRow, 33).Value = item.KodeposRes;
                    worksheet.Cell(currentRow, 34).Value = item.SubZipcodeRes;
                    worksheet.Cell(currentRow, 35).Value = item.NoMesin;
                    worksheet.Cell(currentRow, 36).Value = item.NoRangka;
                    worksheet.Cell(currentRow, 37).Value = item.ItemType;
                    worksheet.Cell(currentRow, 38).Value = item.ItemDescription;
                    worksheet.Cell(currentRow, 39).Value = "'" + item.Mobile1;
                    worksheet.Cell(currentRow, 40).Value = "'" + item.Mobile2;
                    worksheet.Cell(currentRow, 41).Value = "'" + item.Phone1;
                    worksheet.Cell(currentRow, 42).Value = "'" + item.Phone2;
                    worksheet.Cell(currentRow, 43).Value = "'" + item.OfficePhone1;
                    worksheet.Cell(currentRow, 44).Value = "'" + item.OfficePhone2;
                    worksheet.Cell(currentRow, 45).Value = item.OtrPrice;
                    worksheet.Cell(currentRow, 46).Value = item.ItemYear;
                    worksheet.Cell(currentRow, 47).Value = item.MonthlyIncome;
                    worksheet.Cell(currentRow, 48).Value = item.MonthInstallment;
                    worksheet.Cell(currentRow, 49).Value = item.DP;
                    worksheet.Cell(currentRow, 50).Value = item.LTV;
                    worksheet.Cell(currentRow, 51).Value = "'" + item.TenorId;
                    worksheet.Cell(currentRow, 52).Value = item.Plafond;
                    worksheet.Cell(currentRow, 53).Value = item.Pekerjaan;
                    worksheet.Cell(currentRow, 54).Value = item.SisaTenor;
                    try
                    {
                        worksheet.Cell(currentRow, 55).Value = "'" + Convert.ToDateTime(item.ReleaseDateBpkb).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 55).Value = "'" + item.ReleaseDateBpkb;
                    }
                    worksheet.Cell(currentRow, 56).Value = "'" + item.MaxPastDueDt;
                    worksheet.Cell(currentRow, 57).Value = item.Religion;
                    worksheet.Cell(currentRow, 58).Value = item.CustomerRating;
                    try
                    {
                        worksheet.Cell(currentRow, 59).Value = "'" + Convert.ToDateTime(item.TanggalJatuhTempo).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 59).Value = "'" + item.TanggalJatuhTempo;
                    }
                    try
                    {
                        worksheet.Cell(currentRow, 60).Value = "'" + Convert.ToDateTime(item.MaturityDt).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 60).Value = "'" + item.MaturityDt;
                    }
                    worksheet.Cell(currentRow, 61).Value = item.StatusCall;
                    worksheet.Cell(currentRow, 62).Value = item.AnswerCall;
                    worksheet.Cell(currentRow, 63).Value = item.StatusProspek;
                    worksheet.Cell(currentRow, 64).Value = item.ReasonNotProspek;
                    worksheet.Cell(currentRow, 65).Value = item.Notes;
                }

                var type = taskInquiryProvider.validasiUserType(pEmpNo);
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
                TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
                var worksheet = workbook.Worksheets.Add("TASK INQUIRY");
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
                worksheet.Cell(currentRow, 12).Value = "Referantor 1";
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
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 24).Value = "Kode Pos (Legal)";
                worksheet.Cell(currentRow, 25).Value = "SubZipcode (Legal)";
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 26).Value = "Alamat (Survey)";
                worksheet.Cell(currentRow, 27).Value = "Rt (Survey)";
                worksheet.Cell(currentRow, 28).Value = "Rw (Survey)";
                worksheet.Cell(currentRow, 29).Value = "Provinsi (Survey)";
                worksheet.Cell(currentRow, 30).Value = "Kabupaten (Survey)";
                worksheet.Cell(currentRow, 31).Value = "Kecamatan (Survey)";
                worksheet.Cell(currentRow, 32).Value = "Kelurahan (Survey)";
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 33).Value = "Kode Pos (Survey)";
                worksheet.Cell(currentRow, 34).Value = "SubZipcode (Survey)";
                /////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 35).Value = "No_Mesin";
                worksheet.Cell(currentRow, 36).Value = "No_Rangka";
                worksheet.Cell(currentRow, 37).Value = "Item_Type";
                worksheet.Cell(currentRow, 38).Value = "Item_Description";
                worksheet.Cell(currentRow, 39).Value = "Mobile1";
                worksheet.Cell(currentRow, 40).Value = "Mobile2";
                worksheet.Cell(currentRow, 41).Value = "Phone1";
                worksheet.Cell(currentRow, 42).Value = "Phone2";
                worksheet.Cell(currentRow, 43).Value = "Office_Phone1";
                worksheet.Cell(currentRow, 44).Value = "Office_Phone2";
                worksheet.Cell(currentRow, 45).Value = "Otr_Price";
                worksheet.Cell(currentRow, 46).Value = "Item_Year";
                worksheet.Cell(currentRow, 47).Value = "Monthly_Income";
                worksheet.Cell(currentRow, 48).Value = "Monthly_Installament";
                worksheet.Cell(currentRow, 49).Value = "Down Payment Pengajuan";
                worksheet.Cell(currentRow, 50).Value = "LTV Pengajuan";
                ///////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 51).Value = "Tenor Pengajuan";
                ///////////////////////////////////////////////////////
                worksheet.Cell(currentRow, 52).Value = "Plafond Max";
                worksheet.Cell(currentRow, 53).Value = "Pekerjaan";
                worksheet.Cell(currentRow, 54).Value = "Sisa_Tenor";
                worksheet.Cell(currentRow, 55).Value = "Realease_Date_Bpkb";
                worksheet.Cell(currentRow, 56).Value = "Max_Past_Due_Dt";
                worksheet.Cell(currentRow, 57).Value = "Religion";
                worksheet.Cell(currentRow, 58).Value = "Customer_Rating";
                worksheet.Cell(currentRow, 59).Value = "Tanggal_Jatuh_Tempo";
                worksheet.Cell(currentRow, 60).Value = "Maturity_Dt";
                worksheet.Cell(currentRow, 61).Value = "Status Call";
                worksheet.Cell(currentRow, 62).Value = "Answer Call";
                worksheet.Cell(currentRow, 63).Value = "Status Prospek";
                worksheet.Cell(currentRow, 64).Value = "Reason Not Prospek";
                worksheet.Cell(currentRow, 65).Value = "Notes";
                var result = taskInquiryProvider.ListDownloadDetail(pID, pEmpNo);
                foreach (var item in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Number;
                    worksheet.Cell(currentRow, 2).Value = item.BranchName;
                    worksheet.Cell(currentRow, 3).Value = "'" + item.Region;
                    worksheet.Cell(currentRow, 4).Value = item.TaskID;
                    worksheet.Cell(currentRow, 5).Value = item.JenisTask;
                    worksheet.Cell(currentRow, 6).Value = "'" + item.CustID;
                    worksheet.Cell(currentRow, 7).Value = item.CustomerName;
                    try
                    {
                        worksheet.Cell(currentRow, 8).Value = "'" + Convert.ToDateTime(item.DistributedDate).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 8).Value = "'" + item.DistributedDate;
                    }
                    try
                    {
                        worksheet.Cell(currentRow, 9).Value = "'" + Convert.ToDateTime(item.StartedDate).ToString("dd/MM/yyyy HH:mm:ss.mmm");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 9).Value = "'" + item.StartedDate;
                    }
                    worksheet.Cell(currentRow, 10).Value = item.EmpPosition;
                    worksheet.Cell(currentRow, 11).Value = item.soa;
                    worksheet.Cell(currentRow, 12).Value = "'" + item.Referentor1;
                    worksheet.Cell(currentRow, 13).Value = "'" + item.RegionalId;
                    worksheet.Cell(currentRow, 14).Value = item.Product;
                    worksheet.Cell(currentRow, 15).Value = "'" + item.CabId;
                    worksheet.Cell(currentRow, 16).Value = "'" + item.NIK;
                    worksheet.Cell(currentRow, 17).Value = item.TempatLahir;
                    try
                    {
                        worksheet.Cell(currentRow, 18).Value = "'" + Convert.ToDateTime(item.TglLahir).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 18).Value = "'" + item.TglLahir;
                    }
                    worksheet.Cell(currentRow, 19).Value = "'" + item.RWLeg;
                    worksheet.Cell(currentRow, 20).Value = item.ProvLeg;
                    worksheet.Cell(currentRow, 21).Value = item.KabLeg;
                    worksheet.Cell(currentRow, 22).Value = item.KecLeg;
                    worksheet.Cell(currentRow, 23).Value = item.KelLeg;
                    worksheet.Cell(currentRow, 24).Value = "'" + item.KodeposLeg;
                    worksheet.Cell(currentRow, 25).Value = "'" + item.SubZipcodeLeg;
                    worksheet.Cell(currentRow, 26).Value = item.AlamatRes;
                    worksheet.Cell(currentRow, 27).Value = "'" + item.RTRes;
                    worksheet.Cell(currentRow, 28).Value = "'" + item.RWRes;
                    worksheet.Cell(currentRow, 29).Value = item.ProvRes;
                    worksheet.Cell(currentRow, 30).Value = item.KabRes;
                    worksheet.Cell(currentRow, 31).Value = item.KecRes;
                    worksheet.Cell(currentRow, 32).Value = item.KelRes;
                    worksheet.Cell(currentRow, 33).Value = "'" + item.KodeposRes;
                    worksheet.Cell(currentRow, 34).Value = "'" + item.SubZipcodeRes;
                    worksheet.Cell(currentRow, 35).Value = item.NoMesin;
                    worksheet.Cell(currentRow, 36).Value = item.NoRangka;
                    worksheet.Cell(currentRow, 37).Value = item.ItemType;
                    worksheet.Cell(currentRow, 38).Value = item.ItemDescription;
                    worksheet.Cell(currentRow, 39).Value = "'" + item.Mobile1;
                    worksheet.Cell(currentRow, 40).Value = "'" + item.Mobile2;
                    worksheet.Cell(currentRow, 41).Value = "'" + item.Phone1;
                    worksheet.Cell(currentRow, 42).Value = "'" + item.Phone2;
                    worksheet.Cell(currentRow, 43).Value = "'" + item.OfficePhone1;
                    worksheet.Cell(currentRow, 44).Value = "'" + item.OfficePhone2;
                    worksheet.Cell(currentRow, 45).Value = item.OtrPrice;
                    worksheet.Cell(currentRow, 46).Value = item.ItemYear;
                    worksheet.Cell(currentRow, 47).Value = item.MonthlyIncome;
                    worksheet.Cell(currentRow, 48).Value = item.MonthInstallment;
                    worksheet.Cell(currentRow, 49).Value = item.DP;
                    worksheet.Cell(currentRow, 50).Value = item.LTV;
                    worksheet.Cell(currentRow, 51).Value = "'" + item.TenorId;
                    worksheet.Cell(currentRow, 52).Value = item.Plafond;
                    worksheet.Cell(currentRow, 53).Value = item.Pekerjaan;
                    worksheet.Cell(currentRow, 54).Value = item.SisaTenor;
                    try
                    {
                        worksheet.Cell(currentRow, 55).Value = "'" + Convert.ToDateTime(item.ReleaseDateBpkb).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 55).Value = "'" + item.ReleaseDateBpkb;
                    }
                    worksheet.Cell(currentRow, 56).Value = "'" + item.MaxPastDueDt;
                    worksheet.Cell(currentRow, 57).Value = item.Religion;
                    worksheet.Cell(currentRow, 58).Value = item.CustomerRating;
                    try
                    {
                        worksheet.Cell(currentRow, 59).Value = "'" + Convert.ToDateTime(item.TanggalJatuhTempo).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 59).Value = "'" + item.TanggalJatuhTempo;
                    }
                    try
                    {
                        worksheet.Cell(currentRow, 60).Value = "'" + Convert.ToDateTime(item.MaturityDt).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        worksheet.Cell(currentRow, 60).Value = "'" + item.MaturityDt;
                    }
                    worksheet.Cell(currentRow, 61).Value = item.StatusCall;
                    worksheet.Cell(currentRow, 62).Value = item.AnswerCall;
                    worksheet.Cell(currentRow, 63).Value = item.StatusProspek;
                    worksheet.Cell(currentRow, 64).Value = item.ReasonNotProspek;
                    worksheet.Cell(currentRow, 65).Value = item.Notes;
                }
                var type = taskInquiryProvider.validasiUserType(pEmpNo);
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

        public IActionResult ValidasiDownload(string pEmpNo)
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            Boolean isSucceed = true;
            isSucceed = taskInquiryProvider.validasiDownload(pEmpNo);
            return Json(isSucceed);

        }

        public IActionResult DecriptUser(string Id, string userName)
        {
            TaskInquiryProvider taskInquiryProvider = new TaskInquiryProvider();
            Boolean isSucceed = true;
            var idDecript = Id + "|" + userName;
            var idDecriptEncrypt = System.Text.Encoding.UTF8.GetBytes(idDecript);
            var idDecriptEncrypt64 = Convert.ToBase64String(idDecriptEncrypt);
            var result = idDecriptEncrypt64;
            return Json(result);

        }

    }
}