using MediatR;
using Project.Core.Features.Lectures.Queries.Results;

namespace Project.Core.Features.Lectures.Queries.Models
{
    public class GetLectureByIdQuery : IRequest<Response<LectureResponse>>
    {
        public int Id { get; set; }
    }
}