
namespace SerialLabs
{
    public interface IMapper<T, V>
    {
        T Map(V obj);
        V Map(T obj);
    }
}
