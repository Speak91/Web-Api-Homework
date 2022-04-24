using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsAgentController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private Mock<ILogger<DotNetMetricsAgentController>> mockLogger;

        public DotNetMetricsControllerUnitTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsAgentController>>();
            controller = new DotNetMetricsAgentController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void GetErrorsCount_ReturnsOk()
        {
            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetErrorsCount(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Verifiable();

            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetErrorsCount(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
        }
    }
}
