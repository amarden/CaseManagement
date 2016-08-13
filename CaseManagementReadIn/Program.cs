using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
using CaseManagementTests;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaseManagementReadIn
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //CreateMocks cm = new CreateMocks();
            //cm.createClients();
            //var import = new ImportExcel(@"C:\Secure_Data\CaseManagement\Materials\Client.xlsx");
            //import.getClients();
            //List<Client> clients = import.clients;
            //writeClients(clients);

            var import = new ImportExcel(@"C:\Secure_Data\CaseManagement\Materials\ClientVisit.xlsx");
            import.getClientVisits();
            List<ClientVisit> cv = import.clientVisits;
            writeClientVisits(cv);
        }

        private static void writeClientVisits(List<ClientVisit> clientVisit)
        {
            using (var db = new SQLDatabase())
            {
                db.ClientVisits.AddRange(clientVisit);
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        private static void writeAgencies(List<Agency> agencies)
        {
            using (var db = new SQLDatabase())
            {
                db.Agencies.AddRange(agencies);
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        private static void writeClients(List<Client> clients)
        {
            using(var db = new SQLDatabase())
            {
                var other = clients.Where(x => x.dob == null);
                var test = clients.Where(x => x.dob != null);
                var dob = test.Select(x => x.dob);

                db.Clients.AddRange(clients);
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }
    }
}
