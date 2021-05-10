using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_WEB.Models;
using MKT_POLOSYS_WEB.Models.MappingProductMtrku;
using MKT_POLOSYS_WEB.Providers;
using MKT_POLOSYS_WEB.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_WEB.Controllers.MappingProductMtrku
{
    public class MappingProductMtrkuController : Controller
    {
        MappingProductMtrkuProvider mappingProductMtrku = new MappingProductMtrkuProvider();
        // GET: MappingProductMtrkuController
        [HttpGet]
        public ActionResult Index(string emp_no, string id)
        {
            //jika menu navbar perlu dinamis, id bisa sertakan pada header
            if (id == null)
            {
                id = "MTRKUMP";
            }
            if (emp_no != null && id != null)
                {
                var base64EncodedBytes = System.Convert.FromBase64String(emp_no);
                var emp_nox = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                var emp_name =  mappingProductMtrku.getUser(emp_nox);
                var sessionLogin = new SessionLogin() { empName = emp_name, empNo= emp_nox, idMenu=id };
                HttpContext.Session.SetString("SessionLogin", JsonConvert.SerializeObject(sessionLogin));
                HttpContext.Session.SetString("getusername", sessionLogin.empName);
                HttpContext.Session.SetString("getuserid", sessionLogin.empNo);
                HttpContext.Session.SetString("getmenuid", sessionLogin.idMenu);
            }
            if (HttpContext.Session.GetString("getusername") != null)
            {
                var sessionLogin = JsonConvert.DeserializeObject<SessionLogin>(HttpContext.Session.GetString("SessionLogin"));
                ViewBag.session = sessionLogin;
            }
            else
            {
                return View("~/Views/Shared/Redir.cshtml");
            }

            if (HttpContext.Session.GetString("getusername") == null || HttpContext.Session.GetString("getmenuid")==null)
            {
                return View("~/Views/Shared/Redir.cshtml"); 
            }

            try
            { 
                
                Boolean isSucceed = true;
                var model = new MappingProductMtrkuIndexViewModel();
                model.empNo = HttpContext.Session.GetString("getuserid");
                isSucceed = mappingProductMtrku.validasiUser(model.empNo);
                model.empName = HttpContext.Session.GetString("getusername");
                model.ListDetail = mappingProductMtrku.Index().ToList();
                model.ddlMotorku30 = mappingProductMtrku.ddlMotorku30().ToList();
                model.ddlMotorku120 = mappingProductMtrku.ddlMotorku120().ToList();

                if (isSucceed)
                {
                    ViewData["empNames"] = model.empName;
                    return View(model);
                }
                else
                {

                    return View("~/Views/Shared/ErrorMappingProduct.cshtml");
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorMappingProduct.cshtml");
            }


        }

        [HttpPost]
        public ActionResult Index([Bind] MappingProductMtrkuCreateUpdateViewModel param)
        {
       
            try
            {
                if (param.action == "search")
                {                    
                    MappingProductMtrkuIndexViewModel dataModel = new MappingProductMtrkuIndexViewModel();
                    dataModel.empNo = param.empNo;
                    dataModel.empName = param.empName;
                    dataModel.ListDetail = mappingProductMtrku.getSearch(param.product30,param.product120);
                    dataModel.ddlMotorku30 = mappingProductMtrku.ddlMotorku30().ToList();
                    dataModel.ddlMotorku120 = mappingProductMtrku.ddlMotorku120().ToList();
                    //dataModel.message = "00";
                    return View(dataModel);
                }else if (param.action == "saveadd")
                {
                    
                    MappingProductMtrkuIndexViewModel dataModel = new MappingProductMtrkuIndexViewModel();
                    string emp_no = HttpContext.Session.GetString("getuserid");
                    dataModel.message = mappingProductMtrku.saveData(param, emp_no);
                    dataModel.empNo = param.empNo;
                    dataModel.empName = param.empName;
                    dataModel.ListDetail = mappingProductMtrku.getSearch(param.product30, param.product120);
                    dataModel.ddlMotorku30 = mappingProductMtrku.ddlMotorku30().ToList();
                    dataModel.ddlMotorku120 = mappingProductMtrku.ddlMotorku120().ToList();
                    dataModel.messages= dataModel.message[0].statusMessage;
                    dataModel.messagesCode = dataModel.message[0].statusCode;

                    return View(dataModel);
                }
                else if (param.action == "update")
                {
                    string emp_no = HttpContext.Session.GetString("getuserid"); 
                    MappingProductMtrkuIndexViewModel dataModel = new MappingProductMtrkuIndexViewModel();
                    dataModel.empNo = param.empNo;
                    dataModel.empName = param.empName;
                    dataModel.message = mappingProductMtrku.updateData(param, emp_no);
                    dataModel.ListDetail = mappingProductMtrku.getSearch(param.product30, param.product120);
                    dataModel.ddlMotorku30 = mappingProductMtrku.ddlMotorku30().ToList();
                    dataModel.ddlMotorku120 = mappingProductMtrku.ddlMotorku120().ToList();
                    dataModel.messages = dataModel.message[0].statusMessage;
                    dataModel.messagesCode = dataModel.message[0].statusCode;

                    return View(dataModel);
                }
                else if (param.action == "delete")
                {
                    if (param.idDel == null)
                    {
                        string emp_no = HttpContext.Session.GetString("getuserid");
                        MappingProductMtrkuIndexViewModel dataModel = new MappingProductMtrkuIndexViewModel();
                        dataModel.empNo = param.empNo;
                        dataModel.empName = param.empName; 
                        dataModel.ListDetail = mappingProductMtrku.getSearch(param.product30, param.product120);
                        dataModel.ddlMotorku30 = mappingProductMtrku.ddlMotorku30().ToList();
                        dataModel.ddlMotorku120 = mappingProductMtrku.ddlMotorku120().ToList();
                        dataModel.messages = "Proses Hapus Dibatalkan";
                        dataModel.messagesCode = "01";
                        return View(dataModel);
                    }
                    else
                    {
                        string emp_no = HttpContext.Session.GetString("getuserid");
                        MappingProductMtrkuIndexViewModel dataModel = new MappingProductMtrkuIndexViewModel();
                        dataModel.empNo = param.empNo;
                        dataModel.empName = param.empName;
                        dataModel.message = mappingProductMtrku.deleteData(param, emp_no);
                        dataModel.ListDetail = mappingProductMtrku.getSearch(param.product30, param.product120);
                        dataModel.ddlMotorku30 = mappingProductMtrku.ddlMotorku30().ToList();
                        dataModel.ddlMotorku120 = mappingProductMtrku.ddlMotorku120().ToList();
                        dataModel.messages = dataModel.message[0].statusMessage;
                        dataModel.messagesCode = dataModel.message[0].statusCode; 
                        return View(dataModel);
                    }
                    
                }

                return View();

            }
            catch (Exception ex)
            { 
                return View("~/Views/Shared/ErrorMappingProduct.cshtml");
            }

        }

        // GET: MappingProductMtrkuController/Details/5
        public ActionResult ddlMotorku30SelectOne(string pKode)
        {
        
            var data = mappingProductMtrku.ddlMotorku30SelectOne(pKode);
            //var data = "data";
            var msg = "00";
            return Json(new { message=msg, namaProd=data});
        }

        public ActionResult ddlMotorku120SelectOne(string pKode)
        {
             
            var data = mappingProductMtrku.ddlMotorku120SelectOne(pKode);
            //var data = "data";
            var msg = "00";
            return Json(new { message120 = msg, namaProd120 = data });
        }

        public ActionResult getOneMapping(string refCode30, string refCode120)
        {         
            var data = mappingProductMtrku.getSearch(refCode30, refCode120);
            //var data = "data";
            var msg = "00";
            return Json(new { messageUpd = msg, data = data });
        }

    

        // GET: MappingProductMtrkuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MappingProductMtrkuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MappingProductMtrkuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MappingProductMtrkuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MappingProductMtrkuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MappingProductMtrkuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
