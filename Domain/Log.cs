namespace Domain;

/// <summary>
/// Лог.
/// </summary>
public class Log
{
	/// <summary>
	/// Уникальный идентификатор.
	/// </summary>
	public Guid Id { get; private set; }
	
	/// <summary>
	/// Сообщение.
	/// </summary>
	public string Message { get; private set; }

	/// <summary>
	/// Дата создания лога.
	/// </summary>
	public DateTime DateTime { get; private set; }

	/// <summary>
	/// Тип лога.
	/// </summary>
	public LogStatus Status { get; private set; }

	/// <summary>
	/// Строка стека Exception в случае логирования ошибки.
	/// </summary>
	public string? InnerException { get; private set; } = null;

	public Log(string message, LogStatus logStatus = LogStatus.Information, string? innerException = null)
	{
		if (string.IsNullOrWhiteSpace(message))
		{
			throw new ArgumentException("Message is empty.");
		}

		if (logStatus is LogStatus.Error && string.IsNullOrWhiteSpace(innerException))
		{
			throw new ArgumentException("Log is error type but no innerException was provided.");
		}

		Status = logStatus;
		InnerException = innerException;
		Message = message;
		DateTime = DateTime.Now;
		Id = Guid.NewGuid();
	}

	private Log()
	{ }
};