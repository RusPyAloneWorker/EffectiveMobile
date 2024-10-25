namespace Domain.Base;

/// <summary>
/// Сущность бизнес-логики.
/// </summary>
public abstract class Entity
{
	/// <summary>
	/// Уникальный идентификатор.
	/// </summary>
	public Guid Id { get; private set; }

	protected Entity()
	{
		Id = Guid.NewGuid();
	}

	protected Entity(Guid id)
	{
		if (id == Guid.Empty)
		{
			throw new ArgumentException("Id is empty");
		}
		
		Id = id;
	}
}