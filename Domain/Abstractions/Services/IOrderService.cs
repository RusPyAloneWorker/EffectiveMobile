namespace Domain.Abstractions.Services;

public interface IOrderService
{
	/// <summary>
	/// Найти заказ по уникальному идентификатору.
	/// </summary>
	/// <param name="id">Уникальный идентификатор.</param>
	/// <returns>Заказ.</returns>
	Task<Order?> FindByIdAsync(Guid id);
	
	/// <summary>
	/// Найти заказы по месту получения.
	/// </summary>
	/// <param name="district">Адрес.</param>
	/// <returns>Заказы.</returns>
	Task<IList<Order>> FindByDistrictAsync(string district);
	
	/// <summary>
	/// Найти заказы по дате прибытия заказа.
	/// </summary>
	/// <param name="dateTime">Дата прибытия заказа.</param>
	/// <returns>Заказы.</returns>
	Task<IList<Order>> FindByDeliveryTimeAsync(DateTime dateTime);

	/// <summary>
	/// Добавление нового заказа.
	/// </summary>
	/// <param name="order">Заказ.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Ассинхронная операция.</returns>
	Task AddOrderAsync(Order order, CancellationToken cancellationToken);
	
	/// <summary>
	/// Возвращает все заказы.
	/// </summary>
	/// <returns>Список заказов.</returns>
	Task<IList<Order>> GetAllOrders();
}