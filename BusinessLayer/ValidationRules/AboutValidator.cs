using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.TitleA)
                .NotEmpty().WithMessage("Küçük başlık alanı boş geçilemez.")
                .MinimumLength(3).WithMessage("Lütfen en az 3 karakterli küçük başlık giriniz.")
                .MaximumLength(50).WithMessage("Lütfen en fazla 50 karakterli küçük başlık giriniz.");

            RuleFor(x => x.TitleB)
                .NotEmpty().WithMessage("Büyük başlık alanı boş geçilemez.")
                .MinimumLength(5).WithMessage("Lütfen en az 5 karakterli büyük başlık giriniz.")
                .MaximumLength(100).WithMessage("Lütfen en fazla 100 karakterli büyük başlık giriniz.");

            RuleFor(x => x.DescriptionA)
                .NotEmpty().WithMessage("Başlık kısmı boş geçilemez.")
                .MinimumLength(5).WithMessage("Lütfen en az 5 karakterli başlık bilgisi giriniz.")
                .MaximumLength(50).WithMessage("Lütfen en fazla 50 karakterli başlık bilgisi giriniz.");

            RuleFor(x => x.DescriptionB)
                .NotEmpty().WithMessage("Açıklama kısmı boş geçilemez.")
                .MinimumLength(50).WithMessage("Lütfen en az 50 karakterli açıklama bilgisi giriniz.")
                .MaximumLength(500).WithMessage("Lütfen en fazla 500 karakterli açıklama bilgisi giriniz.");

            RuleFor(x => x.ImageA)
                .NotEmpty().WithMessage("Lütfen görsel seçiniz.");

            RuleFor(x => x.ImageB)
                .NotEmpty().WithMessage("Lütfen görsel seçiniz.");
        }

    }
}
