using Active_Blog_Service.Context;
using Active_Blog_Service.Models;
using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.ValidationAttributes
{
    public class UniquePhoneNumber : ValidationAttribute
    {
        private readonly AppDbContext _context;

        public string ErorrName { get; set; }

        public UniquePhoneNumber(AppDbContext context)
        {
            _context = context;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not null)
            {
                var empBeingValidation = (Employee)validationContext.ObjectInstance;

                if (empBeingValidation != null)
                {
                   

                }
            }

            return base.IsValid(value, validationContext);
        }
    }
}
