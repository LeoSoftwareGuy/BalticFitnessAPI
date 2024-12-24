using BuildingBlocks.Exceptions;

namespace Application.Support.Exceptions
{
    public class MuscleGroupNotFoundException : NotFoundException
    {
        public MuscleGroupNotFoundException(string name, object key)
          : base($"Muscle Group: \"{name}\" ({key}) was not found.")
        {
        }
    }
}
