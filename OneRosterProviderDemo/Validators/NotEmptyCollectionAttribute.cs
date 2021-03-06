﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace OneRosterProviderDemo.Validators
{
    public class NotEmptyCollectionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ICollection collection = value as ICollection;

            if (collection.Count == 0)
            {
                return new ValidationResult($"At least one {validationContext.MemberName} required");
            }
            return ValidationResult.Success;
        }
    }
}
