namespace Gallery.Domain.Entities.CapturedOnCamera;

public interface ICapturedOnCamera
{
    public DateTime TimeCreated { get; set; }
    public string? Description { get; set; }
    public int FileId { get; set; }
    public string? Place { get; set; }
}