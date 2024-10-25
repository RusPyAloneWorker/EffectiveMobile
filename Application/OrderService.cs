using Domain;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Npgsql;

namespace Application;

/// <summary>
/// Сервис заказов.
/// </summary>
public class OrderService: IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ILogger _logger;

	public OrderService(IOrderRepository orderRepository, 
		IUnitOfWork unitOfWork,
		ILogger logger)
	{
		_orderRepository = orderRepository;
		_unitOfWork = unitOfWork;
		_logger = logger;
	}

	/// <summary>
	/// Возвращает заказ по идентификатору или null-значение.
	/// </summary>
	/// <param name="id">Идентификатор.</param>
	/// <returns>Заказ или null</returns>
	public async Task<Order?> FindByIdAsync(Guid id)
	{
		try
		{
			await _logger.LogInformationAsync($"Getting an order with id = '{id}' from database.");
			return await _orderRepository.FindByIdAsync(id);
		}
		catch (NpgsqlException npgsqlException)
		{
			await _logger.LogInformationAsync(npgsqlException.Message, LogStatus.Error, npgsqlException.InnerException?.ToString());
			throw new NpgsqlException("Couldn't get an order", npgsqlException);
		}
	}

	/// <summary>
	/// Возвращает заказы по адресу.
	/// </summary>
	/// <param name="district">Адрес.</param>
	/// <returns>Список заказов.</returns>
	public async Task<IList<Order>> FindByDistrictAsync(string district)
	{
		try
		{
			await _logger.LogInformationAsync($"Getting orders with district = '{district}' from database.");
			return await _orderRepository.FindByDistrictAsync(district);
		}
		catch (NpgsqlException npgsqlException)
		{
			await _logger.LogInformationAsync(npgsqlException.Message, LogStatus.Error, npgsqlException.InnerException?.ToString());
			throw new NpgsqlException("Couldn't get orders", npgsqlException);
		}
	}

	/// <summary>
	/// Найти заказы по дате прибытия заказа.
	/// </summary>
	/// <param name="dateTime">Дата прибытия заказа.</param>
	/// <returns>Заказы.</returns>
	public async Task<IList<Order>> FindByDeliveryTimeAsync(DateTime dateTime)
	{
		try
		{
			await _logger.LogInformationAsync($"Getting orders with delivery time = '{dateTime}' from database.");
			return await _orderRepository.FindByDeliveryTimeAsync(dateTime);
		}
		catch (NpgsqlException npgsqlException)
		{
			await _logger.LogInformationAsync(npgsqlException.Message, LogStatus.Error, npgsqlException.InnerException?.ToString());
			throw new NpgsqlException("Couldn't get orders", npgsqlException);
		}
	}

	/// <summary>
	/// Добавление нового заказа.
	/// </summary>
	/// <param name="order">Заказ.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Ассинхронная операция.</returns>
	public async Task AddOrderAsync(Order order, CancellationToken cancellationToken)
	{
		try
		{
			await _logger.LogInformationAsync("Adding new order to database.");
			await _orderRepository.AddOrderAsync(order);
			await _unitOfWork.SaveChangesAsync(cancellationToken);
		}
		catch (ArgumentException argumentException)
		{
			await _logger.LogInformationAsync(argumentException.Message, LogStatus.Warning);
			throw new ArgumentException("Couldn't add order", argumentException);
		}
		catch (NpgsqlException npgsqlException)
		{
			await _logger.LogInformationAsync(npgsqlException.Message, LogStatus.Error, npgsqlException.InnerException?.ToString());
			throw new NpgsqlException("Couldn't add order", npgsqlException);
		}
	}
	
	/// <summary>
	/// Возвращает все заказы.
	/// </summary>
	/// <returns>Список всех заказов.</returns>
	public async Task<IList<Order>> GetAllOrders()
	{
		try
		{
			await _logger.LogInformationAsync("Getting all orders from database.");
			return await _orderRepository.GetAllOrders();
		}
		catch (NpgsqlException npgsqlException)
		{
			await _logger.LogInformationAsync(npgsqlException.Message, LogStatus.Error, npgsqlException.InnerException?.ToString());
			throw new NpgsqlException("Couldn't get orders", npgsqlException);
		}
	}
}