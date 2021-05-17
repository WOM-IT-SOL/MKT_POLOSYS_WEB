using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKT_POLOSYS_WEB;

namespace MKT_POLOSYS_WEB.Models
{
    public class ListTaskViewModel
    {
        public string sourceData { get; set; }

        public string cabang { get; set; }

        public string regional { get; set; }

        public string taskID { get; set; }
        public string jenisTask { get; set; }
        public string customerID { get; set; }
        public string customerName { get; set; }
        public string distributedDT { get; set; }
        public string startedDT { get; set; }
        public string slaRemaining { get; set; }
        public string fieldPersonName { get; set; }
        public string empPosition { get; set; }
        public string statusProspek { get; set; }
        public string priorityLevel { get; set; }
        public string aplikasiAI { get; set; }
        public string applicationID { get; set; }
        public string statusMSS { get; set; }
        public string statusWISE { get; set; }
        public string statusDukcapil { get; set; }
        public string soa { get; set; }
        public string referentorName { get; set; }
        public string referentorName2 { get; set; }
        public string orderInID { get; set; }

        //penambahan Arif
        public string referentorCode { get; set; }
        public string referentorCode2 { get; set; }
        public string negativeCust { get; set; }
        public string orderNo { get; set; }


    }
}
