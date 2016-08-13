using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementReadIn
{
    public class ExcelToAgency
    {
        public List<Agency> agencies = new List<Agency>();
        private List<int> agencyCols = new List<int> { 15, 19, 23 };

        public ExcelToAgency(ExcelWorksheet data)
        {
            List<string> names = new List<string>();
            int n = 2;
            var cell = data.Cells[n, 1].Value;
            while(cell != null)
            {
                foreach (var col in agencyCols)
                {
                    var agencyVal = data.Cells[n, col].Value;
                    if(agencyVal != null)
                    {
                       names.Add(agencyVal.ToString());
                    }
                }
                n++;
                cell = data.Cells[n, 1].Value;
            }
            var distinct = names.Distinct();
            foreach(var a in distinct)
            {
                Agency agency = new Agency();
                agency.name = a;
                agencies.Add(agency);
            }
        }
    }
}
