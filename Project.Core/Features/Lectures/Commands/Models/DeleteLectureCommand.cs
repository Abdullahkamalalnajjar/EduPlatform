namespace Project.Core.Features.Lectures.Commands.Models
{
    public class DeleteLectureCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}