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
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsAgentController controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsAgentController>> mockLogger;

        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            mockLogger = new Mock<ILogger<RamMetricsAgentController>>();
            controller = new RamMetricsAgentController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetAvailable(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Verifiable();

            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetAvailable(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
        }
    }
}
