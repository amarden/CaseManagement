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
    public class ExcelToClientVisit
    {
        public ClientVisit client = new ClientVisit();
        private Dictionary<string, int> dict;
        public ExcelToClientVisit(int row, ExcelWorksheet data)
        {
            AgencyDictionary ad = AgencyDictionary.getInstance();
            this.dict = ad.dict;
            using (var db = new SQLDatabase())
            {
                var clientId = Convert.ToInt16(data.Cells[row, 1].Value);
                //bool checkId = checkForClient(clientId);
                var client = db.Clients.Where(x => x.waysideId == clientId).Single();
                this.client.clientId = client.clientId;
                TranslateBasics(row, data);
                TranslateLookUps(row, data);
                TranslateReferrals(row, data);
                this.client.dateCreated = DateTime.Now;
                this.client.dateEdited = DateTime.Now;
            }
        }

        private bool checkForClient(object clientId)
        {
            int id;
            if (Int32.TryParse(clientId.ToString(), out id))
            {
                using (var db = new SQLDatabase())
                {
                    var client = db.Clients.Where(x=>x.waysideId == id).Single();
                    if(client != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void TranslateBasics (int row, ExcelWorksheet data)
        {
            this.client.dateVisit = Convert.ToDateTime(data.Cells[row, 2].Value);
            this.client.timeSpentMintues = Convert.ToInt32(data.Cells[row, 7].Value);
            this.client.notes = (string)data.Cells[row, 13].Value;
            this.client.housingChange = data.Cells[row, 11].Value == null ? false :
                  (Double)data.Cells[row, 11].Value == 1 ? true : false;
            this.client.mentalHealthChange = data.Cells[row, 12].Value == null ? false :
                  (Double)data.Cells[row, 12].Value == 1 ? true : false;
        }

        private void TranslateLookUps(int row, ExcelWorksheet data)
        {
            this.client.responderId = Convert.ToInt32(data.Cells[row, 3].Value);
            this.client.responderId = this.client.responderId == 0 ? 1 : this.client.responderId;
            this.client.visitResultId = Convert.ToInt32(data.Cells[row, 8].Value) + 1;
            this.client.contactTypeId = Convert.ToInt32(data.Cells[row, 4].Value) + 1;
        }

        private int resolveVisit(object visitResult)
        {
            int id;
            if(Int32.TryParse(visitResult.ToString(), out id))
            {
                return id + 1;
            }
            else
            {
                return 99;
            }
        }

        private void TranslateReferrals(int row, ExcelWorksheet data)
        {
            var referralColStart = new List<int> { 14, 18, 22 };
            List<ClientVisitReferral> referrals = new List<ClientVisitReferral>();
            foreach (int refCol in referralColStart)
            {
                var typeId = data.Cells[row, refCol].Value;
                var agencyName = data.Cells[row, refCol + 1].Value;
                var contact = data.Cells[row, refCol + 2].Value;

                if (typeId != null && agencyName != null)
                {
                    ClientVisitReferral referral = new ClientVisitReferral();
                    referral.referralTypeId = Convert.ToInt32(typeId) + 1;
                    referral.agencyId = this.dict[agencyName as string];
                    referral.contactName = contact as string;
                    referrals.Add(referral);
                }
            }
            this.client.ClientVisitReferrals = referrals;
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
