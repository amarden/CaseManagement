using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using CaseManagement.Models.SQL;
using Faker;
using CaseManagement.DataObjects;

namespace CaseManagementReadIn
{
    public class ExcelToClient
    {
        public Client client = new Client();
        public ExcelToClient(int row, ExcelWorksheet data)
        {
            TranslateBasics(row, data);
            TranslateLookUps(row, data);
            TranslateNeeds(row, data);
            this.client.ClientFamilies = TranslateFamily(row, data);
            this.client.dateCreated = DateTime.Now;
            this.client.dateEdited = DateTime.Now;
        }

        private void TranslateBasics (int row, ExcelWorksheet data)
        {
            this.client.waysideId = Convert.ToInt16(data.Cells[row, 1].Value);
            this.client.intakeDate = DateTime.Now;
            this.client.dateCreated = DateTime.Now;
            this.client.dateEdited = DateTime.Now;
            this.client.firstName = NameFaker.FirstName();
            this.client.lastName = NameFaker.LastName();
            this.client.dob = (DateTime?)data.Cells[row, 15].Value ?? null;
            this.client.veteran = data.Cells[row, 14].Value == null ? false :
                  (Double)data.Cells[row, 14].Value == 1 ? true : false;
            this.client.phone = data.Cells[row, 22].Value != null ? data.Cells[row, 22].Value.ToString().Trim(' ') : "";
            this.client.email = (string)data.Cells[row, 23].Value;
            this.client.address = (string)data.Cells[row, 19].Value;
            this.client.canContact = data.Cells[row, 37].Value == null || 
                data.Cells[row, 37].Value.ToString() == "?" 
                ? false :
                  (Double)data.Cells[row, 37].Value == 1 ? true : false;
            this.client.houseLossRisk = data.Cells[row, 21].Value == null ? false :
                  (Double)data.Cells[row, 21].Value == 1 ? true : false;
        }

        private void TranslateLookUps(int row, ExcelWorksheet data)
        {
            this.client.languageId = checkNull(data.Cells[row, 11].Value, 1);
            this.client.raceId = checkNull(data.Cells[row, 17].Value, 1);
            this.client.learnAboutId = checkNull(data.Cells[row, 36].Value,01);
            this.client.ethnicityId = ethnicityCheck(data.Cells[row, 18].Value);
            this.client.genderId = checkNull(data.Cells[row, 10].Value, 1);
            this.client.housingStatusId = checkNull(data.Cells[row, 20].Value, 1);
            this.client.translationId = checkNull(data.Cells[row, 13].Value, 1);
        }

        private List<ClientFamily> TranslateFamily(int row, ExcelWorksheet data)
        {
            List<ClientFamily> cf = new List<ClientFamily>();
            List<int> famCols = new List<int> { 25, 28, 31, 34 };

            foreach (int col in famCols)
            {
                var name = data.Cells[row, col].Value;
                var age = data.Cells[row, col + 1].Value;
                var liveWith = data.Cells[row, col + 2].Value;
                if(name != null)
                {
                    ClientFamily fam = new ClientFamily();
                    fam.name = (string)name;
                    fam.liveWith = liveWith == null || liveWith.ToString() == "0" ? false : true;
                    fam.age = age == null ? "" : age.ToString();
                    fam.dateCreated = DateTime.Now;
                    cf.Add(fam);
                }
            }

            return cf;
        }

        private void TranslateNeeds(int row, ExcelWorksheet data)
        {
            int colFrom = 39;
            int colTo = 45;
            List<ClientNeed> needs = new List<ClientNeed>();
            for (int i = colFrom; i <= colTo; i++)
            {
                var value = data.Cells[row, i].Value;
                if(value != null)
                {
                    ClientNeed need = new ClientNeed();
                    need.dateCreated = DateTime.Now;
                    need.met = true;
                    need.needId = Convert.ToInt32(value) + 1;
                    needs.Add(need);
                }
            }
            this.client.ClientNeeds = needs;
        }

        private int checkNull(object value, int increment)
        {
            return value == null ? 99 : Convert.ToInt16(value) + increment;
        }

        private int ethnicityCheck(object value)
        {
            var numVal = value == null ? 99 : Convert.ToInt16(value);
            if (numVal == 99)
                return numVal;
            else if (numVal < 15)
                return numVal + 1;
            else if (numVal == 15)
                return 10;
            else
                return numVal;

        }
    }
}
