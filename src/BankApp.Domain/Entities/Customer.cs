using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
namespace BankApp.Entities;

public class Customer:FullAuditedEntity<Guid>
{
    public Guid UserId { get; set; }

    public string FullName { get; set; }
    public string TcNumber { get; set; }
    public string BirthPlace { get; set; }
    public DateTime? BirthDate { get; set; }
    public decimal RiskLimit { get; set; } = 10000.00m;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    
    public void SetId(Guid id)
    {
        this.Id = id;
    }
    protected Customer()
    {
    }
    public Customer(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }
}