using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.ProjectAggregate
{
    public interface IProjectRepository
    {
        Project Add(Project project);
        Project Update(Project project);
    }
}
