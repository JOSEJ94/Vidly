using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Resources;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = nameof(CustomerResources.NameErrorMessage), ErrorMessageResourceType =typeof(CustomerResources)), StringLength(255)]
        public string Name { get; set; }
        [Display(Name = nameof(CustomerResources.IsSubscribedToNewsletter), ResourceType = typeof(CustomerResources))]
        public bool IsSuscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        [Required(ErrorMessageResourceName = nameof(CustomerResources.MembershipTypeIdErrorMessage), ErrorMessageResourceType = typeof(CustomerResources))]
        [Display(Name = nameof(CustomerResources.MembershipTypeId), ResourceType = typeof(CustomerResources))]
        public byte MembershipTypeId { get; set; }
        [Display(Name = nameof(CustomerResources.Birthday), ResourceType = typeof(CustomerResources))]
        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; } 
    }
}