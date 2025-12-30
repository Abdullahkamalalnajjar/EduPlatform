namespace Project.Core.Features.Lectures.Commands.Models
{
    public class ChangeIsFreeLectureMaterialCommand : IRequest<Response<int>>
    {
        public int LectureMaterialId { get; set; }
        public bool IsFree { get; set; }
        public ChangeIsFreeLectureMaterialCommand(int lectureMaterialId, bool isFree)
        {
            LectureMaterialId = lectureMaterialId;
            IsFree = isFree;
        }
    }

}
