﻿using Application.Trainings.DTOs.Trainings;
using FluentValidation;

namespace Application.Trainings.Commands.SaveTrainingCommand
{
    public class SaveTrainingCommandValidator : AbstractValidator<SaveTrainingCommand>
    {
        public SaveTrainingCommandValidator()
        {
            RuleFor(x => x.Trained).NotNull();
            RuleFor(x => x.ExerciseSets).NotNull().NotEmpty();

            RuleForEach(x => x.ExerciseSets).SetValidator(new ExerciseSetDtoValidator());
        }

        public class ExerciseSetDtoValidator : AbstractValidator<ExerciseSetDto>
        {
            public ExerciseSetDtoValidator()
            {
                RuleFor(x => x.Exercise).NotNull().NotEmpty().WithMessage("ExerciseId must be greater than 0.");
                RuleFor(x => x.Reps).InclusiveBetween(1, 100).WithMessage("Reps number must be between 1 - 100");
                RuleFor(x => x.Weight).InclusiveBetween(1, 400).WithMessage("Weight number must be between 1 - 400.");

                RuleFor(x => x.Pre).InclusiveBetween(1, 10).WithMessage("Pre value must be be between 1-10");
            }
        }
    }
}
