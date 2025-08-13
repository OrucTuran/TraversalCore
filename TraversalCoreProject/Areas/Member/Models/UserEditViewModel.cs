using System.ComponentModel.DataAnnotations;

namespace TraversalCoreProject.Areas.Member.Models
{
    public class UserEditViewModel
    {
        [Display(Name = "Adınız")]
        public string Name { get; set; }

        [Display(Name = "Soyadınız")]
        public string Surname { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şifre (Tekrar)")]
        [DataType(DataType.Password)]
        public string ConfrimPassword { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Posta")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Profil Resmi URL")]
        public string ImageURL { get; set; }
    }
}
