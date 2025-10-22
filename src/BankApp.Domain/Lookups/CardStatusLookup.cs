using System;
using BankApp.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace BankApp.Domain.Lookups;

public class CardStatusLookup : FullAuditedAggregateRoot<int>
{
    public CardStatuses Status { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    protected CardStatusLookup() { }
    
    public CardStatusLookup(CardStatuses status, string name, string description)
    {
        Id = (int)status;
        Status = status;
        Name = name;
        Description = description;
    }
}