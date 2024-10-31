using Application.Trainings.DTOs.Nutrition;
using FluentValidation;

namespace Application.Trainings.Commands.SaveMealCommand
{
    public class SaveMealCommandValidator : AbstractValidator<SaveMealCommand>
    {
        public SaveMealCommandValidator()
        {
            RuleFor(x => x.Products).NotNull().NotEmpty().WithMessage("There are no products provided.");
            RuleFor(x => x.MealTime).NotNull().WithMessage("No information was provided for meal time");

            RuleForEach(x => x.Products).SetValidator(new ConsumedProductDtoValidator());
        }


        public class ConsumedProductDtoValidator : AbstractValidator<ConsumedProductDto>
        {
            public ConsumedProductDtoValidator()
            {
                RuleFor(x => x.Product).NotEmpty().WithMessage("Product Id must be greater than 0.");
                RuleFor(x => x.Quantity).InclusiveBetween(1, 100).WithMessage("Quantity number must be between 1 - 100");
                RuleFor(x => x.WeightGrams).InclusiveBetween(1, 5000).WithMessage("Weight (grams) number must be between 1 - 5kg");
            }
        }
    }
}
