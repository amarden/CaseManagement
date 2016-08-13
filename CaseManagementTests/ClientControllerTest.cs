using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Moq;
using System.Linq;
using System.Collections.Generic;
using CaseManagement.Controllers;
using System.Web.Http;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Web.Http.Results;
using CaseManagement.Models.SQL;
using CaseManagement.DataObjects;
using CaseManagement.Models.View;

namespace CaseManagementTests
{
    [TestClass]
    public class ClientControllerTest
    {
        private Mock<DbSet<Client>> mockSet;
        private Mock<SQLDatabase> mockContext;
        private List<Client> clients;
        private ClientsController controller;

        public ClientControllerTest()
        {
            var fileName = "clients.xml";
            string directory = Directory.GetCurrentDirectory();
            directory = directory.Replace("\\CaseManagementTests\\bin\\Debug", "\\CaseManagementTests\\MockData\\");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Client>));
            StreamReader reader = new StreamReader(directory + fileName);
            this.clients = (List<Client>)serializer.Deserialize(reader);
            var queryData = this.clients.AsQueryable();
            this.mockSet = new Mock<DbSet<Client>>();

            mockSet.As<IQueryable<Client>>().Setup(m => m.Provider).Returns(queryData.Provider);
            mockSet.As<IQueryable<Client>>().Setup(m => m.Expression).Returns(queryData.Expression);
            mockSet.As<IQueryable<Client>>().Setup(m => m.ElementType).Returns(queryData.ElementType);
            mockSet.As<IQueryable<Client>>().Setup(m => m.GetEnumerator()).Returns(queryData.GetEnumerator());

            this.mockContext = new Mock<SQLDatabase>();
            this.mockContext.Setup(m => m.Clients).Returns(mockSet.Object);

            this.controller = new ClientsController(mockContext.Object);

            reader.Close();
        }

        [TestMethod]
        public async Task CreateClient()
        {
            var client = this.clients.First();

            this.controller.Configuration = new HttpConfiguration();
            this.controller.Validate(client);

            IHttpActionResult result = await this.controller.PostClient(client);

            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task AttemptCreateBadClient()
        {
            var client = this.clients.First();
            client.firstName = null;
            client.genderId = 0;

            this.controller.Configuration = new HttpConfiguration();
            this.controller.Validate(client);
            var modelState = controller.ModelState;

            IHttpActionResult result = await this.controller.PostClient(client);

            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            Assert.AreEqual(modelState.IsValid, false);
        }

        [TestMethod]
        public void GetAllClients()
        {
            List<ViewListClient> result = this.controller.GetClients().ToList();
            Assert.AreEqual(result.Count, 206);
        }

        [TestMethod]
        public async Task GetAClient()
        {
            var id = 2;
            mockContext.Setup(x => x.Clients.FindAsync(id))
            .ReturnsAsync(this.clients.Where(x => x.clientId == id).Single());

            var response = this.controller.GetClient(2);
            var result = await response as OkNegotiatedContentResult<Client>;
            Assert.AreEqual(result.Content.clientId, 2);
        }
    }
}
