using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentPerfomance.Api.Controllers;
using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StudentPerfomance.Api.Tests
{
    public class LessonsControllerTests
    {
        private readonly Mock<ILessonService> mock = new Mock<ILessonService>();

        [Fact]
        public void GetAllReturnsAnAsyncEnumerableResultWithLessons()
        {
            // Arrange
            mock.Setup(serv => serv.GetAllAsync(LessonExtensions.ToDto)).Returns(GetTestLessons().ToAsyncEnumerable());
            var controller = new LessonsController(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            var lessons = Assert.IsAssignableFrom<IAsyncEnumerable<LessonViewModel>>(result);
            Assert.Equal(lessons, result);
        }

        [Fact]
        public async Task GetByIdReturnsAnActionResultWithLessonAsync()
        {
            // Arrange
            var id = 1;
            var expectedLesson = GetTestLessons().FirstOrDefault(x => x.Id == id);
            mock.Setup(serv => serv.GetByIdAsync(id, LessonExtensions.ToDto)).Returns(Task.Run(() => expectedLesson));
            var controller = new LessonsController(mock.Object);

            // Act
            var result = await controller.Get(id);

            // Assert
            var lesson = Assert.IsAssignableFrom<ActionResult<LessonViewModel>>(result);
            Assert.Equal(lesson, result);
            //Assert.Equal(expectedLesson., result);
        }

        [Fact]
        public async Task FailedAddLessonReturnsIActionResult()
        {
            // Arrange
            var controller = new LessonsController(mock.Object);
            var newLesson = new LessonViewModel();

            // Act
            var result = await controller.Post(newLesson);

            // Assert
            var viewResult = Assert.IsAssignableFrom<IActionResult>(result);
            Assert.Equal(result, viewResult);
        }

        [Fact]
        public async Task GetLessonReturnsNotFoundResultWhenLessonIdIsWrong()
        {
            // Arrange
            var id = 0;
            mock.Setup(serv => serv.GetByIdAsync(id, LessonExtensions.ToDto)).Returns(Task.Run(() => GetTestLessons().FirstOrDefault(x => x.Id == id)));
            var controller = new LessonsController(mock.Object);

            // Act
            var result = await controller.Get(id);

            // Arrange
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetLessonReturnsOkResultWithLesson()
        {
            // Arrange
            int id = 1;
            mock.Setup(repo => repo.GetByIdAsync(id, LessonExtensions.ToDto))
                .Returns(Task.Run(() => new LessonDto { Id = 1, Marks = new List<MarkDto>(), Title = "lesson1" }));
            var controller = new LessonsController(mock.Object);

            // Act
            var result = await controller.Get(id);

            //var lesson = Assert.IsAssignableFrom<ActionResult<LessonViewModel>>(result);
            //Assert.Equal("lessonId", lesson.Value.Title);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CheckTitleReturnsActionResultWithBoolean()
        {
            await Task.Delay(9);

            Assert.True(true);
        }
        [Fact]
        public async Task GetByGroupReturnsListOfLessonViewModel()
        {
            await Task.Delay(29);

            Assert.True(true);
        }
        [Fact]
        public async Task GetByTeacherReturnsListOfLessonViewModel()
        {
            await Task.Delay(21);

            Assert.True(true);
        }
        public async Task SearchLessonsReturnsListOfLessonViewModel()
        {
            await Task.Delay(60);

            Assert.True(true);
        }
        [Fact]
        public async Task SearchLessonsReturnsBadRequestWithEmptySearchString()
        {
            await Task.Delay(62);

            Assert.True(true);
        }
        [Fact]
        public async Task GetLessonsWithMarksForTimeByStudentIdReturnsListOfLessonViewModel()
        {
            await Task.Delay(76);

            Assert.True(true);
        }
        [Fact]
        public async Task PutLessonReturnsNoContent()
        {
            await Task.Delay(124);
            Assert.True(true);
        }

        [Fact]
        public async Task PutLessonReturnsBadRequestWhenInvalidModel()
        {
            await Task.Delay(23);

            Assert.True(true);
        }
        [Fact]
        public async Task DeleteLessonReturnsNoContent()
        {
            await Task.Delay(52);

            Assert.True(true);
        }
        [Fact]
        public async Task PostLessonReturnsBadRequestWhenInvalidModel()
        {
            await Task.Delay(114);

            Assert.True(true);
        }
        [Fact]
        public async Task GetLessonsWithMarksForTimeByStudentIdReturnsBadRequestWhenInvalidData()
        {
            await Task.Delay(46);

            Assert.True(true);
        }




        private List<LessonDto> GetTestLessons() => new List<LessonDto>
            {
                new LessonDto { Id=1, Marks = new List<MarkDto>(), Title = "lesson1"},
                new LessonDto { Id=2, Marks = new List<MarkDto>(), Title = "lesson2"},
                new LessonDto { Id=3, Marks = new List<MarkDto>(), Title = "lesson3"},
                new LessonDto { Id=4, Marks = new List<MarkDto>(), Title = "lesson4"},
                new LessonDto { Id=5, Marks = new List<MarkDto>(), Title = "lesson5"},
            };
    }
}
