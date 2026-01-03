using Project.Core.Features.Exams.Queries.Models;
using Project.Core.Features.Exams.Queries.Results;
using Project.Core.Features.QuestionOptions.Queries.Results;
using Project.Core.Features.Questions.Queries.Results;

namespace Project.Core.Features.Exams.Queries.Handlers
{
    public class ExamQueryHandler : ResponseHandler,
        IRequestHandler<GetAllExamsQuery, Response<IEnumerable<ExamResponse>>>,
        IRequestHandler<GetExamByIdQuery, Response<ExamResponse>>,
        IRequestHandler<GetExamByLectureIdQuery, Response<ExamResponse>>
    {
        private readonly IExamService _service;
        private readonly IMapper _mapper;

        public ExamQueryHandler(IExamService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ExamResponse>>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            var result = items
                .Select(e => new ExamResponse
                {
                    Id = e.Id,
                    Title = e.Title,
                    LectureId = e.LectureId,
                    LectureName = e.Lecture.Title,
                    IsFinished = e.IsFinashed,
                    Deadline = e.Deadline,
                    DurationInMinutes = e.DurationInMinutes,
                    Questions = e.Questions.Select(q => new QuestionResponse
                    {
                        Id = q.Id,
                        QuestionType = q.QuestionType,
                        Content = q.Content,
                        AnswerType = q.AnswerType,
                        Score = q.Score,
                        ExamId = q.ExamId,
                        Options = q.Options.Select(o => new QuestionOptionResponse
                        {
                            Id = o.Id,
                            Content = o.Content,
                            IsCorrect = o.IsCorrect,
                            QuestionId = o.QuestionId
                        }).ToList()
                    })
                }
                ).ToList();
            return Success<IEnumerable<ExamResponse>>(result);
        }

        public async Task<Response<ExamResponse>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<ExamResponse>("Exam not found");
            var resp = new ExamResponse
            {
                Id = item.Id,
                Title = item.Title,
                LectureId = item.LectureId,
                LectureName = item.Lecture.Title,
                IsFinished = item.IsFinashed,
                Deadline = item.Deadline,
                DurationInMinutes = item.DurationInMinutes,
                Questions = item.Questions.Select(q => new QuestionResponse
                {

                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    Content = q.Content,
                    AnswerType = q.AnswerType,
                    Score = q.Score,
                    ExamId = q.ExamId,
                    Options = q.Options.Select(o => new QuestionOptionResponse
                    {
                        Id = o.Id,
                        Content = o.Content,
                        IsCorrect = o.IsCorrect,
                        QuestionId = o.QuestionId
                    }).ToList()
                }
                 )
            };
            return Success(resp);
        }

        public async Task<Response<ExamResponse>> Handle(GetExamByLectureIdQuery request, CancellationToken cancellationToken)
        {
            var item = (await _service.GetAllAsync(cancellationToken)).FirstOrDefault(e => e.LectureId == request.LectureId);
            if (item is null) return NotFound<ExamResponse>("Exam not found for lecture");
            var resp = new ExamResponse
            {
                Id = item.Id,
                Title = item.Title,
                LectureId = item.LectureId,
                LectureName = item.Lecture.Title,
                IsFinished = item.IsFinashed,
                Deadline = item.Deadline,
                DurationInMinutes = item.DurationInMinutes,
                Questions = item.Questions.Select(q => new QuestionResponse
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    Content = q.Content,
                    AnswerType = q.AnswerType,
                    Score = q.Score,
                    ExamId = q.ExamId,
                    Options = q.Options.Select(o => new QuestionOptionResponse
                    {
                        Id = o.Id,
                        Content = o.Content,
                        IsCorrect = o.IsCorrect,
                        QuestionId = o.QuestionId
                    }).ToList()
                })
            };
            return Success(resp);
        }
    }
}
