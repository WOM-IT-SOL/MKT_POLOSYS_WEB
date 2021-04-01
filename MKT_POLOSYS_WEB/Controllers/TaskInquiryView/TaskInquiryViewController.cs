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

namespace MKT_POLOSYS_WEB.Controllers.TaskInquiryView
{
    public class TaskInquiryViewController : Controller
    {
        // GET: TaskInquiry
        public ActionResult Index(string emp_no)
        {
            try
            {
                TaskInquiryViewProvider taskInquiryProvider = new TaskInquiryViewProvider();
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

        public ActionResult ListDetail(ParamListDetail model)
        {
            TaskInquiryViewProvider taskInquiryProvider = new TaskInquiryViewProvider();
            var result = taskInquiryProvider.get(model);
            return Json(result);
        }

        public ActionResult DdlPriorityLvl(string source,string empPost,string prospect)
        {
            TaskInquiryViewProvider taskInquiryProvider = new TaskInquiryViewProvider();
            var result = taskInquiryProvider.ddlPriorityLevel(source, empPost,prospect);
            return Json(result);
        }

        public IActionResult DecriptUser(string Id, string userName)
        {
            TaskInquiryViewProvider taskInquiryProvider = new TaskInquiryViewProvider();
            Boolean isSucceed = true;
            var idDecript = Id + "|" + userName;
            var idDecriptEncrypt = System.Text.Encoding.UTF8.GetBytes(idDecript);
            var idDecriptEncrypt64 = Convert.ToBase64String(idDecriptEncrypt);
            var result = idDecriptEncrypt64;
            return Json(result);

        }

    }
}