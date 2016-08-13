using CaseManagement.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementReadIn
{
    public class AgencyDictionary
    {
        public Dictionary<string, int> dict = new Dictionary<string, int>();
        private static AgencyDictionary ad = null;

        private AgencyDictionary()
        {
            using (var db = new SQLDatabase())
            {
                this.dict = db.Agencies.ToDictionary(x => x.name, x => x.agencyId);
            }
        }
        public static AgencyDictionary getInstance()
        {
            if(ad == null)
            {
                ad = new AgencyDictionary();
            }
            return ad;
        }
    }
}
