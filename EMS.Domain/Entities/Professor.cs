using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EMS.Domain.Entities
{
    public class Professor : Entity, IUpdatable<Professor>
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public string Title { get; private set; }

        public List<Course> Course { get; private set; }

        public static Professor Create(Guid userId) => new Professor
        {
            UserId = userId,
        };

        public void Update(Professor updatedProfessor)
        {
            Title = updatedProfessor.Title == null  ? Title : updatedProfessor.Title;
            Name = updatedProfessor.Name == null  ? Name : updatedProfessor.Name;
        }
    }
}
