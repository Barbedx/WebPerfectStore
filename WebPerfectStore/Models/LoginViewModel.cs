using System.ComponentModel.DataAnnotations;

namespace WebPerfectStore.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        [MinLength(length:8,ErrorMessage ="Не меньше 8 символов")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Запомнить браузер?")]
        public bool RememberMe { get; set; }

    }
}