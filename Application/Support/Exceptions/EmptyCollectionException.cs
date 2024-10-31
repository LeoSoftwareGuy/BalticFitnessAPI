namespace Application.Support.Exceptions
{
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException(string name, object key)
            : base($"Collection of \"{name}\" ({key}) was not found.")
        {
        }
    }
}
