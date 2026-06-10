using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features._Ylesanded
{
    // 15.11.2025
    // Valideerimise klass SaveYlesanneCommand käsu jaoks
    // Võetakse programmi poolt külge automaatselt
    public class SaveYlesanneCommandValidator : AbstractValidator<SaveYlesanneCommand>
    {
        public SaveYlesanneCommandValidator(ApplicationDbContext context)
        {
            RuleFor(x => x.Pealkiri)
                .NotEmpty().WithMessage("Pealkiri on vaja")
                .MaximumLength(100).WithMessage("Pealkiri ei saa olla pikem kui 100 tähemärki")
                // Oma loogikaga valideerimise reegel
                // Siin võib kasutada DbContexti klassi
                .Custom((s, context) =>
                {
                    // Command või query, mida valideerime
                    var command = context.InstanceToValidate;

                    // Oma valideerimise loogika
                    // koos vea lisamisega
                    //var failure = new ValidationFailure();
                    //failure.AttemptedValue = command.ProjectId;
                    //failure.ErrorMessage = "Cannot find project with Id " + command.ProjectId;
                    //failure.PropertyName = nameof(command.ProjectId);

                    //context.AddFailure(failure);
                });
        }
    }
}
