using Gallery.Domain.Entities.CapturedOnCamera;
using Gallery.Domain.Sharings;

namespace Gallery.Domain.Entities.Albums;

public class VideoAlbum : Album
{
    public List<Video> Videos => Album_Video.Items
        .Where(av => av.Album == this)
        .Select(av => av.Video).ToList();
}