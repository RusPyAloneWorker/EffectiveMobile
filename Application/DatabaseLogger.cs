using Domain;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;

namespace Application;

/// <summary>
/// Логгер, сохраняющий данные в базу данных.
/// </summary>
public class DatabaseLogger: ILogger
{
	/// <summary>
	/// Репозиторий логов.
	/// </summary>
	private readonly ILogRepository _logRepository;

	private readonly IUnitOfWork _unitOfWork;

	public DatabaseLogger(ILogRepository logRepository, IUnitOfWork unitOfWork)
	{
		_logRepository = logRepository;
		_unitOfWork = unitOfWork;
	}
	
	/// <summary>
	/// Логгировует данные в базу данных.
	/// </summary>
	/// <param name="message">Сообщение.</param>
	/// <param name="logStatus">Статус лога.</param>
	/// <param name="innerException">Стек ошибок. Заполняется в том случае, если статус лога - Error.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Ассинхронная операция.</returns>
	public async Task LogInformationAsync(string message, 
		LogStatus logStatus = LogStatus.Information, 
		string? innerException = null,
		CancellationToken? cancellationToken = null)
	{
		await _logRepository.SaveLogAsync(new Log(message, logStatus, innerException));
		await _unitOfWork.SaveChangesAsync(cancellationToken ?? new CancellationToken());
	}
}