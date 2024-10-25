using Domain;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure;

/// <summary>
/// Репозиторий заказов.
/// </summary>
public class OrderRepository: IOrderRepository
{
	private readonly ApplicationDbContext _applicationDbContext;

	public OrderRepository(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}
	
	/// <summary>
	/// Возвращает заказ по идентификатору или null-значение.
	/// </summary>
	/// <param name="id">Идентификатор.</param>
	/// <returns>Заказ или null.</returns>
	public async Task<Order?> FindByIdAsync(Guid id)
	{
		return await _applicationDbContext.Orders.FindAsync(id);
	}

	/// <summary>
	/// Возвращает заказы по адресу.
	/// </summary>
	/// <param name="district">Адрес.</param>
	/// <returns>Список заказов.</returns>
	public async Task<IList<Order>> FindByDistrictAsync(string district)
	{
		return await _applicationDbContext.Orders.Where(x => x.District == district).ToListAsync();
	}

	/// <summary>
	/// Найти заказы по дате прибытия заказа.
	/// </summary>
	/// <param name="dateTime">Дата прибытия заказа.</param>
	/// <returns>Заказы.</returns>
	public async Task<IList<Order>> FindByDeliveryTimeAsync(DateTime dateTime)
	{
		return await _applicationDbContext.Orders.Where(x => x.DeliveryTime == dateTime).ToListAsync();
	}

	/// <summary>
	/// Добавление нового заказа.
	/// </summary>
	/// <param name="order">Заказ.</param>
	/// <returns>Ассинхронная операция.</returns>
	public async Task AddOrderAsync(Order order)
	{
		await _applicationDbContext.AddAsync(order);
	}

	/// <summary>
	/// Возвращает все заказы.
	/// </summary>
	/// <returns>Список заказов.</returns>
	public async Task<IList<Order>> GetAllOrders()
	{
		return await _applicationDbContext.Orders.ToListAsync();
	}
}