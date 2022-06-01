using Gallery.Domain.Entities;
using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Entities.CapturedOnCamera;

namespace Gallery.Domain.Sharings;

public class Album_Video : Base<Album_Video>
{
    private Guid _albumId;
    public Album Album
    {
        get => Album.Items.First(a => a.Id == _albumId);
        set => _albumId = value.Id;
    }
    
    private Guid _videoId;
    public Video Video
    {
        get => Video.Items.First(v => v.Id == _videoId);
        set => _videoId = value.Id;
    }
    
    public Album_Video(Album album, Video video)
    {
        Album = album;
        Video = video;
    }
}