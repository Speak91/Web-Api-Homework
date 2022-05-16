using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsAgentController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuMetricsAgentController>> mockLogger;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            mockLogger = new Mock<ILogger<CpuMetricsAgentController>>();
            controller = new CpuMetricsAgentController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void GetByTimePeriod_ReturnsOk()
        {
            var fromTime = new DateTime(2021, 5, 20);
            var toTime = new DateTime(2021, 5, 20);

            IActionResult result = controller.GetByTimePeriod(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Verifiable();

            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetByTimePeriod(fromTime, toTime);

            mock.Verify(repostitory => repostitory.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
        }

    }
}
