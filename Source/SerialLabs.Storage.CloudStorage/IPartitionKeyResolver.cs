
namespace SerialLabs.Storage.CloudStorage
{
    public interface IPartitionKeyResolver<T>
    {
        string Resolve(T entityId);
    }
}
