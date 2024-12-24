using BuildingBlocks.Exceptions;

namespace Application.Support.Exceptions
{
    public class ProductsNotFoundException : NotFoundException
    {
        public ProductsNotFoundException(string name, object key)
            : base($"Collection of \"{name}\" ({key}) was not found.")
        {
        }
    }
}
