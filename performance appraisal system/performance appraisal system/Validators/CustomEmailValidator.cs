using Microsoft.CodeAnalysis;
using performance_appraisal_system.Data;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace performance_appraisal_system.Validators
{
    public class CustomEmailValidator : ValidationAttribute
    {

        //employee table...
        private readonly EmployeeContext? _employeeContext;

      


        //cheking the email is unique or not...
        protected override  ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value != null)
            {
                //cheking email correct  or not
                
                string userEmail = (string)value;
               
                if (userEmail.Contains(".com"))
                {
                    return ValidationResult.Success;
                   

                }
                else
                {
              
                    return new ValidationResult(ErrorMessage ?? "Incorrect Email Formate");
           
                }

             



                

            }

            return new ValidationResult( "Something went wrong in Validation With Email..");
        }
    }
}
