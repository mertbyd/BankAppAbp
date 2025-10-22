using System;
using Volo.Abp.Application.Dtos;

namespace BankApp.Customers.Dtos
{
    [Serializable]
    public class CustomerDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        public string FullName { get; set; }
        public string TcNumber { get; set; }
        public Guid UserId { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal RiskLimit { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}