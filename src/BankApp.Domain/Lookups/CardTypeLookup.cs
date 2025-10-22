using System;
using BankApp.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace BankApp.Domain.Lookups;

public class CardTypeLookup : FullAuditedAggregateRoot<int>
{
    public CardType Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    protected CardTypeLookup() { }
    
    public CardTypeLookup(CardType type, string name, string description)
    {
        Id = (int)type;
        Type = type;
        Name = name;
        Description = description;
    }
}