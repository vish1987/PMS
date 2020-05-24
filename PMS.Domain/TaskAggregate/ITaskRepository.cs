namespace PMS.Domain.TaskAggregate
{
    public interface ITaskRepository
    {
        void Add(Task task);
        void Update(Task task);
        void AddSubTask(Task task);
    }
}
