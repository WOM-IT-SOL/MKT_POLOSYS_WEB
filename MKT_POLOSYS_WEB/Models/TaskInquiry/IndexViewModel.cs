using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKT_POLOSYS_WEB;

namespace MKT_POLOSYS_WEB.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ListTaskViewModel> ListData { get; set; }
        public IEnumerable<CreateEditViewModel> detailData { get; set; }
        public IEnumerable<DropdownListViewModel> ddlRegion { get; set; }
        public IEnumerable<DropdownListViewModel> ddlBranch { get; set; }
        public IEnumerable<DropdownListViewModel> ddlEmpPosition { get; set; }
        public IEnumerable<DropdownListViewModel> ddlStsProspek { get; set; }
        public IEnumerable<DropdownListViewModel> ddlPriotityLevel { get; set; }
        public IEnumerable<DropdownListViewModel> ddlStatusDukcapil { get; set; }
        public IEnumerable<DropdownListViewModel> ddlSourceData { get; set; }


    }
}
