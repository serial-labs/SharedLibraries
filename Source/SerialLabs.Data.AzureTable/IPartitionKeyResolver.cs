
namespace SerialLabs.Data.AzureTable
{
    public interface IPartitionKeyResolver<T>
    {
        string Resolve(T entityId);
    }
}
