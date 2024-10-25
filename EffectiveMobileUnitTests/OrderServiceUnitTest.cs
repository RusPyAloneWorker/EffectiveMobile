using Application;
using Domain;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Moq;
using Range = Moq.Range;

namespace EffectiveMobileUnitTests;

public class OrderServiceUnitTest
{
	private static Mock<IOrderRepository> _orderRepositoryMock;
	
	private static Mock<ILogger> _loggerMock;
	
	private static Mock<IUnitOfWork> _uowMock;

	private static IList<Order> _orders = new List<Order>()
	{
		new (24, DateTime.Parse("2024-09-23 23:22:05"), "Москва"),
		new (44, DateTime.Parse("2024-08-21 08:29:47"), "Москва"),
		new (34, DateTime.Parse("2024-12-03 13:12:55"), "Москва"),
		new (49, DateTime.Parse("2024-10-11 03:45:15"), "Москва"),
	};
	
	[SetUp]
	public void SetUp()
	{
		_orderRepositoryMock = new Mock<IOrderRepository>();
		_orderRepositoryMock.Setup(repo => repo.AddOrderAsync(It.IsAny<Order>()))
			.Returns(Task.CompletedTask);
		_orderRepositoryMock.Setup(repo => repo.GetAllOrders())
			.Returns(Task.FromResult(_orders));
		_orderRepositoryMock.Setup(repo => repo.FindByDistrictAsync(It.IsAny<string>()))
			.Returns(Task.FromResult(_orders));
		_orderRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<Guid>()))
			.Returns(Task.FromResult<Order?>(default));
		_orderRepositoryMock.Setup(repo => repo.FindByDeliveryTimeAsync(It.IsAny<DateTime>()))
			.Returns(Task.FromResult(_orders));

		
		_uowMock = new Mock<IUnitOfWork>();
		_uowMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
			.Returns(Task.CompletedTask);

		_loggerMock = new Mock<ILogger>();
		_loggerMock.Setup(repo => repo.LogInformationAsync(
				It.IsAny<string>(), It.IsAny<LogStatus>(), It.IsAny<string?>(), It.IsAny<CancellationToken?>())
			)
			.Returns(Task.CompletedTask);
	}
	
	[Test]
	public async Task AddOrderAsync_ValidOrder_ReturnsSuccessfulResult()
	{
		// Act
		var order = new Order(25, DateTime.Now, "Московская");
		var orderService = new OrderService(_orderRepositoryMock.Object, _uowMock.Object, _loggerMock.Object);
		
		// Assert
		Assert.DoesNotThrowAsync(() => orderService.AddOrderAsync(order, CancellationToken.None));
		_loggerMock.Verify(mock => mock.LogInformationAsync(
			It.IsAny<string>(), It.IsAny<LogStatus>(), It.IsAny<string?>(), It.IsAny<CancellationToken?>()), Times.AtLeastOnce());
		_orderRepositoryMock.Verify(mock => mock.AddOrderAsync(It.IsAny<Order>()), Times.Once());
		_uowMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
	}
	
	[Test]
	public async Task AddOrderAsync_OrderRepositoryTrowsException_ReturnsError()
	{
		// Arrange
		_orderRepositoryMock.Setup(repo => repo.AddOrderAsync(It.IsAny<Order>()))
			.Throws<ArgumentException>();
		
		// Act
		var order = new Order(25, DateTime.Now, "Московская");
		var orderService = new OrderService(_orderRepositoryMock.Object, _uowMock.Object, _loggerMock.Object);
	
		// Assert
		Assert.ThrowsAsync<ArgumentException>(() => orderService.AddOrderAsync(order, CancellationToken.None));
		_loggerMock.Verify(mock => mock.LogInformationAsync(
			It.IsAny<string>(), It.IsAny<LogStatus>(), It.IsAny<string?>(), It.IsAny<CancellationToken?>()), Times.Between(2, 3, Range.Inclusive));
		_orderRepositoryMock.Verify(mock => mock.AddOrderAsync(It.IsAny<Order>()), Times.Once());
		_uowMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
	}
}