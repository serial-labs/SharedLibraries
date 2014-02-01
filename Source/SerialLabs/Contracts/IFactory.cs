
namespace SerialLabs
{
    /// <summary>
    /// Factory Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<T>
    {
        /// <summary>
        /// Creates an instance of <see cref="{T}"/>
        /// </summary>
        /// <returns></returns>
        T Create();
    }
}
