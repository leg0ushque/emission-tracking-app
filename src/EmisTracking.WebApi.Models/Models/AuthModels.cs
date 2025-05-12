using EmisTracking.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.Models
{
    public class RegisterModel
    {
        [Display(Name = LangResources.Fields.Email)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Email { get; set; }

        [Display(Name = LangResources.Fields.FirstName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string FirstName { get; set; }


        [Display(Name = LangResources.Fields.MiddleName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string MiddleName { get; set; }


        [Display(Name = LangResources.Fields.LastName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string LastName { get; set; }


        [Display(Name = LangResources.Fields.Password)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Password { get; set; }

        [Display(Name = LangResources.Fields.ConfirmPassword)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string ConfirmPassword { get; set; }

        [Display(Name = LangResources.Fields.Info)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Info { get; set; }
    }

    public class ChangePasswordModel
    {
        [Display(Name = LangResources.Fields.OldPassword)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string OldPassword { get; set; }

        [Display(Name = LangResources.Fields.NewPassword)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string NewPassword { get; set; }

        [Display(Name = LangResources.Fields.ConfirmPassword)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Display(Name = LangResources.Fields.Email)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Email { get; set; }

        [Display(Name = LangResources.Fields.Password)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Password { get; set; }
    }
}
