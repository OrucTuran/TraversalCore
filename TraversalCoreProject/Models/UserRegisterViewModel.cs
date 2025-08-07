using System.ComponentModel.DataAnnotations;

namespace TraversalCoreProject.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen Adınızı Giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen Email Adresinizi Giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Giriniz.")]
        [Compare("Password",ErrorMessage ="Şifreler Uyuşmuyor.")]
        public string ConfrimPassword { get; set; }
    }
}
