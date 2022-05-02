
namespace SerialLabs
{
    /// <summary>
    /// Transience interface
    /// <see cref="http://en.wikipedia.org/wiki/Transient_(computer_programming)"/>
    /// </summary>
    /// 
    public interface ITransient
    {
        /// <summary>
        /// Returns true if the current object state is transient        
        /// </summary>
        bool IsTransient { get; }
    }
}
