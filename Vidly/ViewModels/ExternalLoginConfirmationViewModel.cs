using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "駕照號碼")]
        public string DrivingLicense { get; set; }

        [Required]
        [Display(Name = "電話號碼")]
        public string Phone { get; set; }
    }
}
