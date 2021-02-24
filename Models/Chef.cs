using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ChefAndDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get;set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [FutureDate]
        [MinimumAge]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Dish> CreatedDishes { get; set; }

        public int getAge()
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(DOB.ToString("yyyyMMdd"));
            return (now - dob) / 10000;
        }

        public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value >= DateTime.Now)
                return new ValidationResult("Date of birth must be from the past!");
            return ValidationResult.Success;
        }
    }
        public class MinimumAgeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {   
            DateTime dt = DateTime.Parse(value.ToString());
            DateTime dt_now = DateTime.Now;

            DateTime dt_18 = dt.AddYears(18);

            if (dt_18.Date >= dt_now.Date)
            {
                return new ValidationResult("Chefs must be 18 years old or older!");
            }
            return ValidationResult.Success;
        }
    }
    }
}