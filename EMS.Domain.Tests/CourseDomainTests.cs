using Xunit;

namespace EMS.Domain.Tests
{
    public class CoursesDomainTests
    {
        [Fact]
        public void Given_Create_When_ModelIsValid_Then_Create()
        {
            var course = Course.Create("BD", "2017-2018", 2, 1);
            Assert.Equal(course.Title, "BD");
            Assert.Equal(course.UniversityYear, "2017-2018");
            Assert.Equal(course.StudentYear, 2);
            Assert.Equal(course.Semester, 1);
        }

        [Fact]
        public void Given_Update_When_ModelIsValid_Then_Update()
        {
            var toBeUpdated = Course.Create("Mate", "2014-2015", 1, 3);
            var forUpdate = Course.Create("BD", "2017-2018", 2, 4);
            toBeUpdated.Update(forUpdate);
            Assert.Equal(toBeUpdated.Title, forUpdate.Title);
            Assert.Equal(toBeUpdated.UniversityYear, forUpdate.UniversityYear);
            Assert.Equal(toBeUpdated.StudentYear, forUpdate.StudentYear);
            Assert.Equal(toBeUpdated.Semester, forUpdate.Semester);
        }

    }
}
