namespace PMS.Domain.TaskAggregate
{
    public interface ITaskRepository
    {
        Task Add(Task task);
        Task Update(Task task);
    }
}
