using EmisTracking.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string Email { get; set; }

        [Display(Name = LangResources.Fields.FirstName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string FirstName { get; set; }
        public string FirstNameInitial => (FirstName != null && FirstName.Length != 0) ? (FirstName[0] + ".") : string.Empty;


        [Display(Name = LangResources.Fields.MiddleName)]
        public string MiddleName { get; set; }
        public string MiddleNameInitial => (MiddleName != null && MiddleName.Length != 0) ? (MiddleName[0] + ".") : string.Empty;
        public string FullMiddleName => (string.IsNullOrEmpty(MiddleName) ? string.Empty : MiddleName + " ");

        [Display(Name = LangResources.Fields.LastName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string LastName { get; set; }

        [Display(Name = LangResources.Fields.Info)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Info { get; set; }


        [Display(Name = LangResources.Fields.Role)]
        public string RoleName { get; set; }

        public string InitialsName => $"{FirstNameInitial}{MiddleNameInitial}{LastName}";
        public string FullName => $"{FirstName} {FullMiddleName}{LastName}";

        [Display(Name = LangResources.Fields.Password)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string InitialPassword { get; set; }
    }
}
