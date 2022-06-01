using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Sharings;

namespace Gallery.Domain.Entities.CapturedOnCamera;

public class Video : Base<Video>, ICapturedOnCamera
{
    public static List<Video> GetVideosWithSound()
    {
        return Items.Where(video => video.HasSound).ToList();
    }
    
    private Guid _cameraSettingId;
    public CameraSetting CameraSetting
    {
        get => CameraSetting.Items.First(cs => cs.Id == _cameraSettingId);
        set => _cameraSettingId = value.Id;
    }
    
    public List<Album> Albums => Album_Video.Items
        .Where(av => av.Video == this)
        .Select(av => av.Album).ToList();

    public DateTime TimeCreated { get; set; }
    public string? Description { get; set; }
    public int FileId { get; set; }
    public string? Place { get; set; }
    public int Length { get; set; }
    public bool HasSound { get; set; }

    public TimeSpan GetTimeFromCreation()
    {
        return DateTime.Now - TimeCreated;
    }

    public string GetLengthInMinutes()
    {
        return $"{Length / 60}" + ':' + (Length % 60 >= 10 ? Length % 60 : "0" + Length % 60);
    }

    public int GetAlbumsCount()
    {
        return Albums.Count;
    }
}