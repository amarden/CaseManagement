using CaseManagement.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.ClientObjects
{
    public class ViewClientProvider
    {
        public int clientProviderId { get; set; }

        public int clientId { get; set; }

        public int agencyId { get; set; }

        public int needId { get; set; }

        public string contactPerson { get; set; }

        public string phoneNumber { get; set; }

        public DateTime dateCreated { get; set; }

        public virtual Agency Agency { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }

        public virtual Need Need { get; set; }
        public string agencyName { get; set; }
    }
}
