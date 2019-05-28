using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get Customer object
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            // Birthday is Nullable type, so it could be null.
            if (!customer.Birthday.HasValue)
                return new ValidationResult("Birthday is required to go on a membership.");

            // Calculate the age
            var today = DateTime.Today;
            var birthday = customer.Birthday.Value;
            var age = today.Year - birthday.Year;
            if (birthday.AddYears(age) > today)
                age--;

            // Validate the age
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer must be at least 18 years old to go on a membership.");
        }
    }
}