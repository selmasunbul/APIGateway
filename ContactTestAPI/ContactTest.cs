using Business.Abstract;
using Business.Concrete;
using Core.Helpers;
using DataAccess;
using DataAccess.Entity;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RehberApp.Controllers;
using System.Linq.Expressions;

namespace ContactTest
{
    public class ContactTest
    {
        [Fact]
        public async Task GetAllPerson_ShouldReturnListOfPersons()
        {
            var serviceMock = new Mock<IPersonService>();
            var controller = new PersonController(serviceMock.Object);

            var persons = new List<Person>
            {
                new Person { Name = "Selma", Surname = "Sünbül", Firm = "ABC Corp" },
                new Person { Name = "Selma", Surname = "Sünbül", Firm = "XYZ Ltd." },
                new Person { Name = "Selma", Surname = "Sünbül", Firm = "123 Industries" }
            };

            serviceMock.Setup(x => x.GetList()).ReturnsAsync(ServiceOutput<List<Person>>.Generate(200, true, "Listelendi", 1, persons.Count, data: persons));
            var actionResult = await controller.GetList();

            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(200, objectResult.StatusCode);

            var resultData = objectResult.Value;
            Console.WriteLine($"Result Data: {resultData}");
        }
        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResultWithCreatedPerson()
        {
            var serviceMock = new Mock<IPersonService>();
            var controller = new PersonController(serviceMock.Object);

            var personModel = new PersonModel
            {
                Name = "Selma",
                Surname = "Sünbül",
                Firm = "ABC Corp"
            };

            var createdPerson = new Person
            {
                Name = "Selma",
                Surname = "Sünbül",
                Firm = "ABC Corp"
            };

            serviceMock.Setup(x => x.CreateAsync(It.IsAny<PersonModel>()))
                .ReturnsAsync(ServiceOutput<Person>.Generate(200, true, "Baþarýlý", data: createdPerson));

            var actionResult = await controller.Create(personModel);

            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(200, objectResult.StatusCode);

            var resultData = objectResult.Value as ServiceOutput<Person>;
            Assert.NotNull(resultData);
            Assert.True(resultData.Status);
            Assert.Equal("Baþarýlý", resultData.Message);

            var person = Assert.IsType<Person>(resultData.Data);
            Assert.Equal("Selma", person.Name);
            Assert.Equal("Sünbül", person.Surname);
            Assert.Equal("ABC Corp", person.Firm);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnSuccessResultWhenItemIsSoftDeletedPerson()
        {
            var serviceMock = new Mock<IPersonService>();
            var controller = new PersonController(serviceMock.Object);

            var idToRemove = new Guid("41a47a0f-564d-4679-b132-341e21549b27");

            serviceMock.Setup(x => x.RemoveAsync(idToRemove))
                .ReturnsAsync(ServiceOutput<List<Person>>.Generate(200, true, "Silindi", data: new List<Person>()));

            var result = await controller.Delete(idToRemove);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var resultData = Assert.IsType<ServiceOutput<List<Person>>>(objectResult.Value);

            Assert.Equal(200, objectResult.StatusCode);
            if (resultData != null)
            {
                Assert.Equal("Silindi", resultData.Message);
            }

            serviceMock.Verify(x => x.RemoveAsync(idToRemove), Times.Once);

        }

        [Fact]
        public async Task GetById_ShouldReturnSuccessResultWhenPersonExists()
        {
            // Arrange
            var serviceMock = new Mock<IPersonService>();
            var controller = new PersonController(serviceMock.Object);

            var personId = Guid.NewGuid();
            var newKisi = new Person();
            serviceMock.Setup(x => x.GetById(personId))
                .ReturnsAsync(ServiceOutput<List<Person>>.Generate(200, true, "Baþarýlý", data: newKisi));

            // Act
            var result = await controller.GetById(personId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);

            var serviceOutput = Assert.IsType<ServiceOutput<Person>>(objectResult.Value);
            Assert.True(serviceOutput.Status);
            Assert.Equal("Baþarýlý", serviceOutput.Message);

        }

        [Fact]
        public async Task GetList_ShouldReturnListOfInfoType()
        {
            var serviceMock = new Mock<IInfoTypeService>();
            var controller = new InfoTypeController(serviceMock.Object);

            var infoTypes = new List<InfoType>
                {
                    new InfoType { Name = "Type1" },
                    new InfoType { Name = "Type2" },
                    new InfoType { Name = "Type3" }
                };

            serviceMock.Setup(x => x.GetList()).ReturnsAsync(ServiceOutput<List<InfoType>>.Generate(200, true, "Listelendi", 1, infoTypes.Count, data: infoTypes));
            var actionResult = await controller.GetList();

            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(200, objectResult.StatusCode);

            var resultData = Assert.IsType<ServiceOutput<List<InfoType>>>(objectResult.Value);

            Assert.True(resultData.Status);
            Assert.Equal("Listelendi", resultData.Message);
            Assert.NotNull(resultData.Data);

            var resultList = Assert.IsType<List<InfoType>>(resultData.Data);
            Assert.Equal(infoTypes.Count, resultList.Count);

        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResultWithCreatedInfoType()
        {
            var serviceMock = new Mock<IInfoTypeService>();
            var controller = new InfoTypeController(serviceMock.Object);


            var name = "telefon";

            var createdInfoType = new InfoType
            {
                Name = "telefon"

            };

            serviceMock.Setup(x => x.CreateAsync(name))
                .ReturnsAsync(ServiceOutput<InfoType>.Generate(200, true, "Baþarýlý", data: createdInfoType));

            var actionResult = await controller.Create(name);

            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(200, objectResult.StatusCode);

            var resultData = objectResult.Value as ServiceOutput<InfoType>;
            Assert.NotNull(resultData);
            Assert.True(resultData.Status);
            Assert.Equal("Baþarýlý", resultData.Message);

            var InfoType = Assert.IsType<InfoType>(resultData.Data);
            Assert.Equal("telefon", InfoType.Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResultWithCreatedComminication()
        {
            var serviceMock = new Mock<IComminicationService>();
            var controller = new ComminicationController(serviceMock.Object);


            var comminication = new ComminicationModel
            {
                InfoTypeId = Guid.Parse("41a47a0f-564d-4679-b132-341e21549b27"),
                PersonId = Guid.Parse("41a47a0f-564d-4679-b132-341e21549b27"),
                Content = "kayseri"
            };

            var createdcomminication = new Comminication
            {
                InfoTypeId = Guid.Parse("41a47a0f-564d-4679-b132-341e21549b27"),
                PersonId = Guid.Parse("41a47a0f-564d-4679-b132-341e21549b27"),
                Content = "kayseri"
            };

            serviceMock.Setup(x => x.CreateAsync(comminication))
                .ReturnsAsync(ServiceOutput<Comminication>.Generate(200, true, "Baþarýlý", data: createdcomminication));

            var actionResult = await controller.Create(comminication);

            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(200, objectResult.StatusCode);

            var resultData = objectResult.Value as ServiceOutput<Comminication>;
            Assert.NotNull(resultData);
            Assert.True(resultData.Status);
            Assert.Equal("Baþarýlý", resultData.Message);

            var Comminication = Assert.IsType<Comminication>(resultData.Data);
            Assert.Equal("kayseri", Comminication.Content);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnSuccessResultWhenItemIsSoftDeletedComminication()
        {
            var serviceMock = new Mock<IComminicationService>();
            var controller = new ComminicationController(serviceMock.Object);

            var idToRemove = new Guid("41a47a0f-564d-4679-b132-341e21549b27");

            serviceMock.Setup(x => x.RemoveAsync(idToRemove))
                .ReturnsAsync(ServiceOutput<List<Comminication>>.Generate(200, true, "Silindi", data: new List<Comminication>()));

            var result = await controller.Delete(idToRemove);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var resultData = Assert.IsType<ServiceOutput<List<Comminication>>>(objectResult.Value);

            Assert.Equal(200, objectResult.StatusCode);
            if (resultData != null)
            {
                Assert.Equal("Silindi", resultData.Message);
            }

            serviceMock.Verify(x => x.RemoveAsync(idToRemove), Times.Once);

        }

        [Fact]
        public async Task GetRequestRapor_ShouldReturnSuccessResultWhenGetRequestRapor()
        {
            var serviceMock = new Mock<IRaportService>();
            var controller = new RaportController(serviceMock.Object);

            var infoTypeId = Guid.NewGuid();
            var icerik = "kayseri";
            var newRapor = new RaportModel
            {

                RaportStatus = "tamamlandý",
                PersonCount = 5,
                PhoneNoCount = 10,
                Location = icerik,

            };

            serviceMock.Setup(x => x.GetRequestRapor(infoTypeId, icerik))
                .ReturnsAsync(ServiceOutput<List<RaportModel>>.Generate(200, true, "Baþarýlý", data: newRapor));

            var result = await controller.GetRequest(infoTypeId, icerik);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);

            var serviceOutput = Assert.IsType<ServiceOutput<RaportModel>>(objectResult.Value);
            Assert.True(serviceOutput.Status);
            Assert.Equal("Baþarýlý", serviceOutput.Message);

        }

        [Fact]
        public async Task GetList_ShouldReturnListOfRaportList()
        {
            var serviceMock = new Mock<IRaportService>();
            var controller = new RaportController(serviceMock.Object);

            var raportList = new List<Raport>
                {
                    new Raport { RaportStatus = "Status1", Location = "Location1", PersonCount = 10, PhoneNoCount = 5 },
                    new Raport { RaportStatus = "Status2", Location = "Location2", PersonCount = 15, PhoneNoCount = 8 },
                    new Raport { RaportStatus = "Status3", Location = "Location3", PersonCount = 20, PhoneNoCount = 12 }
                };


            serviceMock.Setup(x => x.GetList()).ReturnsAsync(ServiceOutput<List<Raport>>.Generate(200, true, "Listelendi", 1, raportList.Count, data: raportList));
            var actionResult = await controller.GetListRapor();

            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(200, objectResult.StatusCode);

            var resultData = Assert.IsType<ServiceOutput<List<Raport>>>(objectResult.Value);

            Assert.True(resultData.Status);
            Assert.Equal("Listelendi", resultData.Message);
            Assert.NotNull(resultData.Data);

            var resultList = Assert.IsType<List<Raport>>(resultData.Data);
            Assert.Equal(raportList.Count, resultList.Count);

        }

        [Fact]
        public async Task GetById_ShouldReturnSuccessResultWhenRaport()
        {
            var serviceMock = new Mock<IRaportService>();
            var controller = new RaportController(serviceMock.Object);

            var id = Guid.NewGuid();
            var newRaport = new Raport();
            serviceMock.Setup(x => x.GetById(id))
                .ReturnsAsync(ServiceOutput<List<Raport>>.Generate(200, true, "Baþarýlý", data: newRaport));

            var result = await controller.GetById(id);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);

            var serviceOutput = Assert.IsType<ServiceOutput<Raport>>(objectResult.Value);
            Assert.True(serviceOutput.Status);
            Assert.Equal("Baþarýlý", serviceOutput.Message);

        }
    }
}
