using MediatR;
using Project.Core.Features.Lectures.Queries.Results;

namespace Project.Core.Features.Lectures.Queries.Models
{
    public class GetAllLecturesQuery : IRequest<Response<IEnumerable<LectureResponse>>>
    {
    }
}