using AutoMapper;
using CrossCutting.AutoMapper;
using Data.Repositories;
using Domain.DTO.Requests;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Services
{

    [TestClass]
    public class ContasAPagarServiceTest
    {
        protected static IMapper Mapper { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperResponsesProfile());
                mc.AddProfile(new AutoMapperRequestProfile());
            }).CreateMapper();
        }

        [TestMethod]
        public async Task ContasAPagarServiceTest_GetAll()
        {

            var mockRepository = new Mock<Repository>();

            List<ContaAPagar> contas = new List<ContaAPagar> {
                new ContaAPagar
                {
                    Id = Guid.NewGuid(),
                    Nome = "Pagamento 1",
                    ValorOriginal = 10,
                    ValorCorrigido = 10,
                    QuantidadeDiasEmAtraso = 0,
                    DataVencimento = DateTime.Parse("2020-09-25T03:45:22.196Z"),
                    DataPagamento = DateTime.Parse("2020-09-25T03:45:22.196Z"),
                },
                new ContaAPagar
                {
                    Id = Guid.NewGuid(),
                    Nome = "Pagamento 2",
                    ValorOriginal = 20,
                    ValorCorrigido = 20,
                    QuantidadeDiasEmAtraso = 0,
                    DataVencimento = DateTime.Parse("2020-09-25T03:45:22.196Z"),
                    DataPagamento = DateTime.Parse("2020-09-25T03:45:22.196Z"),
                },
            };

            mockRepository.Setup(x => x.GetAll<ContaAPagar>()).Returns(Helpers.GetQueryableMockDbSet(contas));
            var multaService = new MultaService(mockRepository.Object, Mapper);
            var service = new ContasAPagarService(mockRepository.Object, Mapper, multaService);

            var result = await service.GetAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public async Task ContasAPagarServiceTest_Create()
        {
            const int diasEmAtraso = 5;
            const decimal valorCorrigido = 1270M;

            var mockRepository = new Mock<Repository>();

            mockRepository.Setup(x => x.Add<ContaAPagar>(It.IsAny<ContaAPagar>()))
                .Callback<ContaAPagar>(e => e.Id = Guid.NewGuid());

            List<Multa> multas = new List<Multa> {
                new Multa
                {
                    Id = Guid.NewGuid(),
                    PeriodoMaximoDeDiasEmAtraso = 10,
                    PorcentagemAplicadaDiaAtraso = 5.0 / 100.0,
                    PorcentagemAplicadaMulta = 2.0 / 100.0,
                }
            };

            mockRepository.Setup(x => x.GetAll<Multa>()).Returns(Helpers.GetQueryableMockDbSet(multas));


            var multaService = new MultaService(mockRepository.Object, Mapper);
            var service = new ContasAPagarService(mockRepository.Object, Mapper, multaService);


            var request = new CreateContaAPagarRequest
            {
                Nome = "Conta teste",
                ValorOriginal = 1000,
                DataVencimento = DateTime.Parse("2020-09-25T03:45:22.196Z"),
                DataPagamento = DateTime.Parse("2020-09-30T03:45:22.196Z"),
            };

            var result = await service.Create(request);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(result.QuantidadeDiasEmAtraso, diasEmAtraso);
            Assert.AreEqual(result.ValorCorrigido, valorCorrigido);
        }
    }
}