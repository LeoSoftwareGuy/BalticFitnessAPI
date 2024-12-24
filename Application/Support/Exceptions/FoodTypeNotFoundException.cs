using BuildingBlocks.Exceptions;

namespace Application.Support.Exceptions
{
    public class FoodTypeNotFoundException : NotFoundException
    {
        public FoodTypeNotFoundException(string name, object key)
          : base($"Food Type: \"{name}\" ({key}) was not found.")
        {
        }
    }
}
