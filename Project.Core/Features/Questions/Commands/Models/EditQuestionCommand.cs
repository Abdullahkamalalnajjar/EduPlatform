using MediatR;
using Project.Core.Bases;
using Microsoft.AspNetCore.Http;

namespace Project.Core.Features.Questions.Commands.Models
{
    public class EditQuestionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string QuestionType { get; set; } = null!;
        // For text questions use Content. For image questions, Content will hold the uploaded file URL and File will be used to upload.
        public string Content { get; set; } = null!;
        public IFormFile? File { get; set; }
        public string AnswerType { get; set; } = null!;
        public int Score { get; set; }
        public int ExamId { get; set; }
    }
}
