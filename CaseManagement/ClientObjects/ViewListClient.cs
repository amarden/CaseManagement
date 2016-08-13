using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseManagement.Models.View
{
    public class ViewListClient
    {
        public int clientId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? dob { get; set; }
        public string gender { get; set; }

    }
}