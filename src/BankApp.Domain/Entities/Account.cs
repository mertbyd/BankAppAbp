using System;
using Volo.Abp.Domain.Entities.Auditing;
namespace BankApp.Entities;

public class Account : FullAuditedEntity<Guid>
{
    public Guid UserId { get; set; }
    /// Hesap sahibi müşteri ID (Foreign Key)
    public string AccountName { get; set; } = string.Empty;
    /// Hesap adı (VADESİZ ANADOLU vs.a)
    public string AccountNumber { get; set; } = string.Empty;
    /// Hesap numarası (unique, min 8 karakter)
    public string IBAN { get; set; } = string.Empty;
    /// IBAN (TR ile başlayan 26 karakter)
    public decimal Balance { get; set; } = 0;
    /// Güncel bakiye (negatif olamaz)
    public DateTime OpenedAt { get; set; }
    /// Hesap açılış tarihi
    public DateTime? ClosedAt { get; set; }
    public Guid CustomerId { get; set; }
    protected Account()
    {
       
    }

    public Account(Guid id) : base(id)
    {
       
    }

    public void setid(Guid id)
    {
        this.Id = id;
    }
}