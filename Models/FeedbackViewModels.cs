namespace KinoPortal.Models
{
    public record FeedbackItem(int Id, System.Guid FilmKey, int Rating, string? Comment, string? UserName, System.DateTime CreatedUtc);
    public record FeedbackListVm(System.Guid FilmKey, double Average, int Count, System.Collections.Generic.List<FeedbackItem> Items);
}
