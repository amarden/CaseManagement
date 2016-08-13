using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementReadIn
{
    public class ImportExcel
    {
        public List<Client> clients = new List<Client>();
        public List<Agency> agencies = new List<Agency>();
        public List<ClientVisit> clientVisits = new List<ClientVisit>();
        private string excelFile;
        public ImportExcel(string file)
        {
            this.excelFile = file;
        }

        public void getClients()
        {
            int startingRow = 3;
            using (var ep = new ExcelPackage(new FileInfo(this.excelFile)))
            {
                var wb = ep.Workbook;
                var ws = wb.Worksheets.First();
                var cell = ws.Cells[startingRow, 1].Value;
                while (cell != null)
                {
                    var toClient = new ExcelToClient(startingRow, ws);
                    this.clients.Add(toClient.client);
                    startingRow++;
                    cell = ws.Cells[startingRow, 1].Value;
                }
            }
        }

        public void getClientVisits()
        {
            int startingRow = 2;
            using (var ep = new ExcelPackage(new FileInfo(this.excelFile)))
            {
                var wb = ep.Workbook;
                var ws = wb.Worksheets.First();
                var cell = ws.Cells[startingRow, 1].Value;
                while (cell != null)
                {
                    if(cell.ToString() != "0")
                    {
                        var toClient = new ExcelToClientVisit(startingRow, ws);
                        if (toClient.client.clientId != 0)
                        {
                            this.clientVisits.Add(toClient.client);
                        }
                    }
                    startingRow++;
                    cell = ws.Cells[startingRow, 1].Value;
                }
            }
        }

        public void getAgencies()
        {
            using (var ep = new ExcelPackage(new FileInfo(this.excelFile)))
            {
                var wb = ep.Workbook;
                var ws = wb.Worksheets.First();
                var ea = new ExcelToAgency(ws);
                this.agencies = ea.agencies;
            }
        }
    }
}
