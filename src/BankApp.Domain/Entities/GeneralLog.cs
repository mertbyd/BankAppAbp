using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BankApp.Entities;

public class GeneralLog : FullAuditedEntity<Guid>
{
    public string OperationType { get; private set; } // Private set
    public string Description { get; private set; } // Private set
    public Guid? CustomerId { get; private set; } // Private set
    public Guid? AccountId { get; private set; } // Private set
    public Guid? CardId { get; private set; } // Private set
    public Guid? TransactionId { get; private set; } // Private set
    public decimal? Amount { get; private set; } // Private set
    public string? OldValue { get; private set; } // Private set
    public string? NewValue { get; private set; } // Private set
    public bool Success { get; private set; } = true; // Private set

    /*
     * protected Kullanımı: EF Core'un veritabanından veri çekerken
     * bu boş yapıcı metodunu kullanmasına izin verir
     */
    protected GeneralLog()
    {
    }

    /*
     * ABP Pattern: Repo, services gibi yerlerde nesne oluşturulacağında
     * id'siz nesne oluşturulmasın diye
     */
    public GeneralLog(Guid id) : base(id)
    {
    }

    /*
     * Business Constructor: Log oluştururken minimum gerekli bilgiler
     */
    public GeneralLog(
        Guid id,
        string operationType,
        string description,
        bool success = true) : base(id)
    {
        if (string.IsNullOrWhiteSpace(operationType))
            throw new ArgumentException("Operation type cannot be empty");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty");

        OperationType = operationType.Trim();
        Description = description.Trim();
        Success = success;
    }

    /*
     * Full Constructor: Tüm parametrelerle log oluştur
     */
    public GeneralLog(
        Guid id,
        string operationType,
        string description,
        bool success = true,
        Guid? customerId = null,
        Guid? accountId = null,
        Guid? cardId = null,
        Guid? transactionId = null,
        decimal? amount = null,
        string? oldValue = null,
        string? newValue = null) : base(id)
    {
        if (string.IsNullOrWhiteSpace(operationType))
            throw new ArgumentException("Operation type cannot be empty");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty");

        OperationType = operationType.Trim();
        Description = description.Trim();
        Success = success;
        CustomerId = customerId;
        AccountId = accountId;
        CardId = cardId;
        TransactionId = transactionId;
        Amount = amount;
        OldValue = oldValue?.Trim();
        NewValue = newValue?.Trim();
    }
}