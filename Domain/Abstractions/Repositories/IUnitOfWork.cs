namespace Domain.Abstractions.Repositories;

/// <summary>
/// Транзакция sql-запроса.
/// </summary>
public interface IUnitOfWork
{
	/// <summary>
	/// Сохранить изменения в базу данных.
	/// </summary>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Ассинхронная операция.</returns>
	Task SaveChangesAsync(CancellationToken cancellationToken);
}