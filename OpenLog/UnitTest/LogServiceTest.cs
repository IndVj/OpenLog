using Core.Interfaces.Repo;
using Core.Models;
using Core.Services;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTest
{
    public class LogServiceTest
    {
        [Fact]
        public async void AddLogAsync_ReturnsEntryId_WhenValidRequestIsSent()
        {
            
            var logRepoMoq = new Mock<ILogRepository>();
            var logService = new LogService(logRepoMoq.Object);
            var expectedEntryId = "mock-123";
            
            var newLogEntry = new LogEntry
            {
                Source = "API",
                Level = "Warning",
                Message = "UnitTest"
            };

            logRepoMoq.Setup(r => r.AddLogAsync(newLogEntry)).Returns(Task.FromResult(expectedEntryId));

            
            var entryId = await logService.AddLogAsync(newLogEntry);


            Assert.Equal(expectedEntryId, entryId);
            logRepoMoq.Verify(t => t.AddLogAsync(newLogEntry), Times.Once());

        }


        [Fact]
        public async void GetAllLogsAsync_ReturnsLogEntryList_WhenValidRequestIsSent()
        {

            var logRepoMoq = new Mock<ILogRepository>();
            var logService = new LogService(logRepoMoq.Object);


            var newLogEntry1 = new LogEntry
            {
                TimeStamp = DateTime.UtcNow,
                Source = "API",
                Level = "Warning",
                Message = "UnitTest",
                Id = "mock-123"
            };

            var newLogEntry2 = new LogEntry
            {
                TimeStamp = DateTime.UtcNow,
                Source = "API",
                Level = "Warning",
                Message = "UnitTest2",
                Id = "mock-1234"
            };

            var expectedLogEntryList =  new List<LogEntry> { newLogEntry1, newLogEntry2 };
            logRepoMoq.Setup(r => r.GetAllLogsAsync("API", "Warning"))
                .Returns(Task.FromResult(expectedLogEntryList));

            var actualLogEntryList = await logService.GetAllLogsAsync("API", "Warning");

            Assert.True(actualLogEntryList.Any());
            Assert.Equal(expectedLogEntryList.Count, actualLogEntryList.Count);
            logRepoMoq.Verify(r => r.GetAllLogsAsync("API", "Warning"), Times.Once);
        }


        [Fact]
        public async void AddLogAsync_ThrowsException_WhenInValidRequestIsSent()
        {

            var logRepoMoq = new Mock<ILogRepository>();
            var logService = new LogService(logRepoMoq.Object);

            var newLogEntry = new LogEntry
            {
                Source = "API",
                Level = "Warning",
                Message = "UnitTest"
            };

            logRepoMoq.Setup(r => r.AddLogAsync(newLogEntry)).ThrowsAsync(new InvalidOperationException("DB Error"));

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => logService.AddLogAsync(newLogEntry));


            Assert.Equal("DB Error", ex.Message);
            logRepoMoq.Verify(t => t.AddLogAsync(newLogEntry), Times.Once());
        }


    }
}