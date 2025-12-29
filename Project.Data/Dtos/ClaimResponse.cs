namespace Project.Data.Dtos
{
    public record ClaimResponse
    (string Type, IList<string> Permissions);
}
