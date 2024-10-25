using Application;
using Domain;
using Domain.Abstractions.Repositories;
using Moq;

namespace EffectiveMobileUnitTests;

public class DatabaseLoggerUnitTest
{
	private static Mock<ILogRepository> _logRepositoryMock;
	private static Mock<IUnitOfWork> _uowMock;
	
	[SetUp]
	public void SetUp()
	{
		_logRepositoryMock = new Mock<ILogRepository>();
		_logRepositoryMock.Setup(repo => repo.SaveLogAsync(It.IsAny<Log>()))
			.Returns(Task.CompletedTask);
		
		_uowMock = new Mock<IUnitOfWork>();
		_uowMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
			.Returns(Task.CompletedTask);
	}
	
	[Test]
	public async Task LogInformationAsync_ValidLog_ReturnsSuccessfulResult()
	{
		// Act
		var databaseLogger = new DatabaseLogger(_logRepositoryMock.Object, _uowMock.Object);
		await databaseLogger.LogInformationAsync("Message");
		
		// Assert
		_logRepositoryMock.Verify(mock => mock.SaveLogAsync(It.IsAny<Log>()), Times.Once);
		_uowMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
	}
	
	[Test]
	public void LogInformationAsync_ErrorLogWithoutInnerException_ReturnsError()
	{
		// Act
		var databaseLogger = new DatabaseLogger(_logRepositoryMock.Object, _uowMock.Object);
		
		// Assert
		Assert.ThrowsAsync<ArgumentException>(
			() => databaseLogger.LogInformationAsync("Message", LogStatus.Error, null));
		_uowMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
	}
}