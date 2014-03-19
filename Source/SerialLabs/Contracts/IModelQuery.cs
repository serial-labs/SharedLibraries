
namespace SerialLabs
{
    /// <summary>
    /// Encapsulates a query logic
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface IModelQuery<out TResult, in TModel>
    {
        TResult Execute(TModel model);
    }
}
