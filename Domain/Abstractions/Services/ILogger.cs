namespace Domain.Abstractions.Services;

/// <summary>
/// Логгер.
/// </summary>
public interface ILogger
{
	/// <summary>
	/// Логгировует данные.
	/// </summary>
	/// <param name="message">Сообщение.</param>
	/// <param name="logStatus">Статус лога.</param>
	/// <param name="innerException">Стек ошибок. Заполняется в том случае, если статус лога - Error.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Ассинхронная операция.</returns>
	Task LogInformationAsync(string message, 
		LogStatus logStatus = LogStatus.Information, 
		string? innerException = null,
		CancellationToken? cancellationToken = null);
}