using System;
// using BankApp.Domain.Enums; // Eğer Enums'unuz BankApp projesindeyse isim alanını buna göre düzeltin.
using BankApp.Enums;
using Volo.Abp.Domain.Entities.Auditing;
namespace BankApp.Entities;

public class Transaction: FullAuditedEntity<Guid>
{
    // YENİ ALAN: Hangi ABP Kullanıcısına ait olduğu
    public Guid UserId { get; set; }
    
    public int  TransactionTypesId { get; set; }  /// İşlem tipi 
    public Guid CustomerId { get; set; }                     /// Hesap sahibi müşteri ID (Foreign Key)                                                        
    public Guid? CardId { get; set; }                        /// Kullanılan kart ID (kartlı işlemler için)
    public string? Description { get; set; }                /// İşlem açıklaması
    public decimal Amount { get; set; }                     /// İşlem tutarı (pozitif olmalı)
    public DateTime TransactionDate { get; set; }           /// İşlem tarihi
    public Guid? TargetAccountId { get; set; }               /// Hedef hesap 
    public Guid? SourceAccountId { get; set; }               /// Kaynak hesap 

    protected Transaction()
    {
        
    }
    
    public Transaction(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }

    public void SetId(Guid id)
    {
        this.Id = id;
    }
}