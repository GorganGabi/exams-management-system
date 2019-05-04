using System;

namespace EMS.Business
{
    public class ProfessorDetailsModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }
    }
}
