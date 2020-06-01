using PMS.Domain;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;
using System.Collections.Generic;
using Xunit;

namespace PMS.UnitTests.Domain
{
    public class ProjectAggregateTests
    {
        [Theory]
        [InlineData(StateType.Completed, StateType.Completed, StateType.Completed, StateType.Completed)]
        [InlineData(StateType.Planned, StateType.InProgress, StateType.Completed, StateType.InProgress)]
        [InlineData(StateType.Planned, StateType.Planned, StateType.Planned, StateType.Planned)]
        public void CalculateState_should_return_correct_state(StateType task1Sate, StateType task2Sate, StateType task3Sate, StateType expectedState)
        {
            List<Task> tasks = CreateTasks(task1Sate, task2Sate, task3Sate);

            var project = new Project();

            var state = project.CalcualteState(tasks);

            Assert.Equal(expectedState, state);
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
