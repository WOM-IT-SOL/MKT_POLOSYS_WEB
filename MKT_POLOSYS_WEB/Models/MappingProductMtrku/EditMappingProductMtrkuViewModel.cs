using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_WEB.Models.MappingProductMtrku
{
    public class EditMappingProductMtrkuViewModel
    {
        public List<MappingProductMtrkuListViewModel> ListDetail { get; set; }
        public IEnumerable<ddlMotorku30ViewModel> ddlMotorku30 { get; set; }
        public IEnumerable<ddlMotorku120ViewModel> ddlMotorku120 { get; set; }
        [DisplayName("Product Motorku 30")]
        public string product30 { get; set; }
        [DisplayName("Product Motorku 120")]
        public string product120 { get; set; }
        public string empNo { get; set; }
        public string empName { get; set; }
        public List<ProcessResultViewModel> message { get; set; }
        public string messagesCode { get; set; }
        public string messages { get; set; }
        public string action { get; set; }

        
    }
 
}
