using PMS.Domain;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;
using System.Collections.Generic;
using Xunit;

namespace PMS.UnitTests.Domain
{
    public class ProjectAggregateTests
    {


        [Fact]
        public void CalculateStatus_should_return_status_InProgress()
        {
            List<Task> tasks = CreateTasks(StateType.InProgress, StateType.Completed, StateType.Planned);

            var project = new Project();

            var state = project.CalcualteState(tasks);

            Assert.Equal(StateType.InProgress, state);
        }

        [Fact]
        public void CalculateStatus_should_return_status_Completed()
        {
            List<Task> tasks = CreateTasks(StateType.Completed, StateType.Completed, StateType.Completed);

            var project = new Project();

            var state = project.CalcualteState(tasks);

            Assert.Equal(StateType.Completed, state);
        }

        [Fact]
        public void CalculateStatus_should_return_status_Planned()
        {
            List<Task> tasks = CreateTasks(StateType.Planned, StateType.Planned, StateType.Planned);

            var project = new Project();

            var state = project.CalcualteState(tasks);

            Assert.Equal(StateType.Planned, state);
        }

        private static List<Task> CreateTasks(StateType task1Sate, StateType task2Sate, StateType task3Sate)
        {
            var tasks = new List<Task>();

            var task1 = new Task
            {
                Id = 1,
                State = task1Sate
            };

            tasks.Add(task1);

            var task2 = new Task
            {
                Id = 2,
                State = task2Sate
            };

            tasks.Add(task2);


            var task3 = new Task
            {
                Id = 3,
                State = task3Sate
            };

            tasks.Add(task3);
            return tasks;
        }
    }
}
