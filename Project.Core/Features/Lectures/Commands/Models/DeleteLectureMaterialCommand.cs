namespace Project.Core.Features.Lectures.Commands.Models
{
    public class DeleteLectureMaterialCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}