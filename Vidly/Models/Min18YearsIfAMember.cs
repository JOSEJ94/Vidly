using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Resources;

namespace Vidly.Models
{
    public class Min18YearsIfAMember :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId==MembershipType.Unknown ||customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;
            if (customer.Birthday == null)
                return new ValidationResult(CustomerResources.NoBirthdayValidation);
            bool isAdult = customer.Birthday.Value.AddYears(18) < DateTime.Now;
            return isAdult ? ValidationResult.Success : new ValidationResult(CustomerResources.NoOver18AgeBirthdayValidation);
        }
    }
}