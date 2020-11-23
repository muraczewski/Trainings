using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BusinessLayer.Attributes.Validation
{
    public class EmailDomainValidationAttribute : ValidationAttribute
    {
        private readonly string _allowedDomain;

        public EmailDomainValidationAttribute(string allowedDomain)
        {
            _allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            var domain = value.ToString().Split("@").Last();
            return domain == _allowedDomain;
        }
    }
}
