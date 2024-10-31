using AutoMapper;

namespace Application.Support.Mappings
{
    /// <summary>
    /// useful way to standardize mapping configurations when using AutoMapper in your application.
    /// It allows any class that implements this interface to easily define how it should be mapped from a source type to a destination type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
