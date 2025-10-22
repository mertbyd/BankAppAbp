using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace BankApp.Customers.Dtos
{
    [Serializable]
    public class CreateCustomerDto : ExtensibleObject
    {
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public string TcNumber { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}