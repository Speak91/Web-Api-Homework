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
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsAgentController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsAgentController>> mockLogger;

        public NetworkMetricsControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            mockLogger = new Mock<ILogger<NetworkMetricsAgentController>>();
            controller = new NetworkMetricsAgentController(mockLogger.Object, mock.Object);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_VerifyRequestToRepository()
        {
            mock.Setup(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Verifiable();

            var fromTime = new DateTime(2021, 5, 11);
            var toTime = new DateTime(2021, 5, 20);

            var result = controller.GetMetrics(fromTime, toTime);

            mock.Verify(r => r.GetByTimePeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
        }
    }
}
