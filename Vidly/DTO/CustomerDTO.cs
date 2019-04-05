using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;
using Vidly.Resources;

namespace Vidly.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = nameof(CustomerResources.NameErrorMessage), ErrorMessageResourceType = typeof(CustomerResources)), StringLength(255)]
        public string Name { get; set; }
        public bool IsSuscribedToNewsletter { get; set; }
        [Required(ErrorMessageResourceName = nameof(CustomerResources.MembershipTypeIdErrorMessage), ErrorMessageResourceType = typeof(CustomerResources))]
        public byte MembershipTypeId { get; set; }
        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
    }
}