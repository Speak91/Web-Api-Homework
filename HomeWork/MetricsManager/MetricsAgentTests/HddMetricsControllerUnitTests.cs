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
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsAgentController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsAgentController>> mockLogger;

        public HddMetricsControllerUnitTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            mockLogger = new Mock<ILogger<HddMetricsAgentController>>();
            controller = new HddMetricsAgentController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void GetFreeHDDSpace_ReturnsOk()
        {
            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetFreeHDDSpace(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Verifiable();

            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetFreeHDDSpace(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
        }
    }
}
