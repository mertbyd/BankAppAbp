using System;
using BankApp.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace BankApp.Domain.Lookups;

public class TransactionTypeLookup : FullAuditedAggregateRoot<int>
{
    public TransactionTypes Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    protected TransactionTypeLookup() { }
    public TransactionTypeLookup(TransactionTypes type, string name, string description)
    {
        Id = (int)type;
        Type = type;
        Name = name;
        Description = description;
    }
}
