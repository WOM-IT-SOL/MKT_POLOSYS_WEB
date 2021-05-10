using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_WEB.Models.MappingProductMtrku
{
    public class ParamListDetail
    {
        public string pMotorku30 { get; set; }
        public string pMotorku120 { get; set; }
        public List<MappingProductMtrkuListViewModel> ListDataMtrku { get; set; }
    }
}
