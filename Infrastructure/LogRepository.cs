using Domain;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure;

/// <summary>
/// Репозиторий логов.
/// </summary>
public class LogRepository: ILogRepository
{
	/// <summary>
	/// База данных.
	/// </summary>
	private readonly ApplicationDbContext _applicationDbContext;

	public LogRepository(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}
	
	/// <summary>
	/// Сохранить лог.
	/// </summary>
	/// <param name="log">Лог.</param>
	/// <returns>Ассинхронная операция.</returns>
	public async Task SaveLogAsync(Log log)
	{
		await _applicationDbContext.Logs.AddAsync(log);
	}
	
	/// <summary>
	/// Получить весь список логов.
	/// </summary>
	/// <returns>Список логов.</returns>
	public async Task<IList<Log>> GetAllLogsAsync()
	{
		return await _applicationDbContext.Logs.ToListAsync();
	}

	/// <summary>
	/// Найти лог по идентификатору.
	/// </summary>
	/// <param name="id">Идентификатор лога.</param>
	/// <returns>Лог.</returns>
	public async Task<Log?> GetByIdAsync(Guid id)
	{
		return await _applicationDbContext.Logs.FindAsync(id);
	}
}