using Volo.Abp.Domain.Entities.Auditing;
using System;
// using BankApp.Domain.Enums; // Eğer Enums'unuz BankApp projesindeyse isim alanını buna göre düzeltin.
// Varsayılan olarak CardType ve CardStatuses'un BankApp.Domain.Enums'da olduğunu varsayıyorum.
using BankApp.Enums;
namespace BankApp.Entities;

public class Card :FullAuditedEntity<Guid>
{
    public Guid UserId { get; set; }
    public CardType  CardType { get; set; }                 
    public string CardNumber { get; set; }               
    public int ExpiryMonth { get; set; }                   
    public int ExpiryYear { get; set; }                    
    public string CvvCode { get; set; }                   
    public CardStatuses  Status { get; set; }               
    public Guid CustomerId { get; set; }                   
    public Guid? AccountId { get; set; }                   
    public decimal? CreditLimit { get; set; }              
    public decimal? AvailableLimit { get; set; }           
    public decimal UsedLimit { get; set; } = 0;

    protected Card() // EF Core için boş constructor
    {
    }
    // Constructor'ı Guid id ve Guid userId alacak şekilde güncelledik (isteğe bağlı ama önerilir)
    public Card(Guid id, Guid userId) : base(id) 
    {
        UserId = userId;
    }
    public void SetId(Guid id)
    {
        this.Id = id;
    }
}