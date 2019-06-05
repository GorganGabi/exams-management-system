using System.ComponentModel.DataAnnotations;

namespace EMS.Business
{
    public class UpdateStudentModel
    {
        [MaxLength(2)]
        public string FatherInitial { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}