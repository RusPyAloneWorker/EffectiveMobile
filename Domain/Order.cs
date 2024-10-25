using Domain.Base;

namespace Domain;

/// <summary>
/// Заказ.
/// </summary>
public class Order : Entity
{
	/// <summary>
	/// Макисмальный вес для заказа.
	/// </summary>
	private const int MaxWeight = 50;
	
	/// <summary>
	/// Вес заказа в килограммах.
	/// </summary>
	public decimal Weight { get; private set; }

	/// <summary>
	/// Время прибытия заказа в формате yyyy-MM-dd HH:mm:ss.
	/// </summary>
	public DateTime DeliveryTime { get; private set; }
	
	/// <summary>
	/// Адрес для отправки.
	/// </summary>
	public string District { get; private set; }

	public Order(decimal weight, DateTime deliveryTime, string district)
	{
		if (weight is not (< MaxWeight and > 0))
		{
			throw new ArgumentException("Weight is outside of bound.");
		}

		if (string.IsNullOrWhiteSpace(district))
		{
			throw new ArgumentException("District string for delivery is empty.");
		}
		
		District = district;
		Weight = weight;
		DeliveryTime = deliveryTime;
	}
}