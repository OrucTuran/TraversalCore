using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class GuideValidator : AbstractValidator<Guide>
    {
        public GuideValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Rehber adı soyadı boş bırakılamaz.")
                .MinimumLength(5).WithMessage("Rehber adı en az 5 karakter olmalı.")
                .MaximumLength(50).WithMessage("Rehber adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Rehber açıklaması boş bırakılamaz.")
                .MinimumLength(20).WithMessage("Rehber açıklaması en az 20 karakter olmalı.")
                .MaximumLength(500).WithMessage("Rehber açıklaması en fazla 500 karakter olabilir.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Fotoğraf URL boş olamaz.")
                .Must(link => string.IsNullOrEmpty(link) || link.StartsWith("http"))
                .WithMessage("Fotoğraf linki http veya https ile başlamalı.");

            RuleFor(x => x.SocialMediaA)
                .Must(link => string.IsNullOrEmpty(link) || link.StartsWith("http"))
                .WithMessage("Facebook linki http veya https ile başlamalıdır.");

            RuleFor(x => x.SocialMediaB)
                .Must(link => string.IsNullOrEmpty(link) || link.StartsWith("http"))
                .WithMessage("Instagram linki http veya https ile başlamalıdır.");

            RuleFor(x => x.Status)
                .NotNull().WithMessage("Durum seçilmelidir.");
        }
    }
}
