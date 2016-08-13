using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CaseManagementTests
{
    public class CreateMocks
    {
        string directory = Directory.GetCurrentDirectory();
        public CreateMocks()
        {
            this.directory = this.directory.Replace("\\CaseManagementReadIn\\bin\\Debug", "\\CaseManagementTests\\MockData\\");
        }

        public void createClients()
        {
            List<Client> clients;
            using (var db = new SQLDatabase())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                clients = db.Clients.Include("Gender").ToList();
            }
            var fileName = "clients.xml";
            XmlSerializer s = new XmlSerializer(typeof(List<Client>));
            TextWriter writer = new StreamWriter(this.directory + fileName);
            s.Serialize(writer, clients);
            writer.Close();
        }
    }

   
}
