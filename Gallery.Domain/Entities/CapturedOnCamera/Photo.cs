using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Sharings;

namespace Gallery.Domain.Entities.CapturedOnCamera;

public class Photo : Base<Photo>, ICapturedOnCamera
{
    public static List<Photo> GetPhotosByDate(DateOnly date)
    {
        return Items.Where(photo => DateOnly.FromDateTime(photo.TimeCreated) == date).ToList();
    }
    
    private Guid _cameraSettingId;
    public CameraSetting CameraSetting
    {
        get => CameraSetting.Items.First(cs => cs.Id == _cameraSettingId);
        set => _cameraSettingId = value.Id;
    }
    
    public List<Album> Albums => Album_Photo.Items
        .Where(ap => ap.Photo == this)
        .Select(ap => ap.Album).ToList();

    public DateTime TimeCreated { get; set; }
    public string? Description { get; set; }
    public int FileId { get; set; }
    public string? Place { get; set; }
    
    public TimeSpan GetTimeFromCreation()
    {
        return DateTime.Now - TimeCreated;
    }

    public int GetAlbumsCount()
    {
        return Albums.Count;
    }
}