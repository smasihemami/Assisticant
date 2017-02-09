﻿using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Assisticant.Validation
{
    public static class StringValidationExtensions
    {
        public static ValidationRules For(this ValidationRules validator, Expression<Func<string>> property,
            Func<StringPropertyValidationRule, StringPropertyValidationRule> rule)
        {
            var propertyValidator = validator.AddPropertyValidator(property);
            rule(new StringPropertyValidationRule(propertyValidator));
            return validator;
        }
    }

    public class StringPropertyValidationRule
    {
        private PropertyValidator _propertyValidator;

        public StringPropertyValidationRule(PropertyValidator propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public StringPropertyValidationRule Required()
        {
            _propertyValidator.AddRule(v => string.IsNullOrEmpty((string)v)
                ? $"{_propertyValidator.PropertyName} is required"
                : null);
            return this;
        }

        public StringPropertyValidationRule MaxLength(int length)
        {
            _propertyValidator.AddRule(v => v != null && ((string)v).Length > length
                ? $"{_propertyValidator.PropertyName} must be no more than {length} characters"
                : null);
            return this;
        }

        public StringPropertyValidationRule Matches(string pattern)
        {
            var regex = new Regex(pattern);
            _propertyValidator.AddRule(v => v != null && regex.IsMatch((string)v)
                ? $"{_propertyValidator.PropertyName} must match the pattern {pattern} (whatever that means)"
                : null);
            return this;
        }
    }
}
