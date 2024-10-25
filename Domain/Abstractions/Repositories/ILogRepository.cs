namespace Domain.Abstractions.Repositories;

/// <summary>
/// Репозиторий логов.
/// </summary>
public interface ILogRepository
{
	/// <summary>
	/// Сохранить лог.
	/// </summary>
	/// <param name="log">Лог.</param>
	/// <returns>Ассинхронная операция.</returns>
	Task SaveLogAsync(Log log);
	
	/// <summary>
	/// Получить весь список логов.
	/// </summary>
	/// <returns>Список логов.</returns>
	Task<IList<Log>> GetAllLogsAsync();
	
	/// <summary>
	/// Найти лог по идентификатору.
	/// </summary>
	/// <param name="id">Идентификатор лога.</param>
	/// <returns>Лог.</returns>
	Task<Log?> GetByIdAsync(Guid id);
}