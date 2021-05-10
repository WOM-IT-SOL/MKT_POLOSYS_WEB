using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_WEB.Models.MappingProductMtrku
{
    public class MappingProductMtrkuIndexViewModel
    {
        public List<MappingProductMtrkuListViewModel> ListDetail { get; set; } 
        public IEnumerable<ddlMotorku30ViewModel> ddlMotorku30 { get; set; }
        public IEnumerable<ddlMotorku120ViewModel> ddlMotorku120 { get; set; }
        [DisplayName("Product Motorku 30 : ")] 
        public string product30 { get; set; }
        [DisplayName("Product Motorku 120 : ")]
        public string product120 { get; set; }
        public string empNo { get; set; }
        public string empName { get; set; }
        public List<ProcessResultViewModel> message { get; set; }
        public string messagesCode { get; set; }
        public string messages { get; set; }
        public string action { get; set; }
        
    }

    public class ddlMotorku30ViewModel
    {
        public string kode { get; set; }
        public string nama { get; set; }
    }
 

    public class ddlMotorku120ViewModel
    {
        public string kode { get; set; }
        public string nama { get; set; }
    }

    public class MappingProductMtrkuCreateUpdateViewModel
    {
        public string idDel { get; set; }
        public string ddlMotorku30 { get; set; }
        public string ddlMotorku120 { get; set; }
        public string product30 { get; set; }
        public string prod_30_name { get; set; }
        public string product120 { get; set; }
        public string prod_120_name { get; set; } 
        
        public string usrCrt { get; set; }
        public string empNo { get; set; }
        public string empName { get; set; }
        
        public string action { get; set; }
        
        //untuk update
        public string ddlMotorku30Upd { get; set; }
        public string product30Name { get; set; }
        public string ddlMotorku120Upd { get; set; }
        public string prod_120_name_upd { get; set; }
        public string ddlMotorku120UpdOld { get; set; }
    }

    public class ProcessResultViewModel
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }

}
