using Gallery.Domain.Entities.CapturedOnCamera;
using Gallery.Domain.Sharings;

namespace Gallery.Domain.Entities.Albums;

public class MixedAlbum : Album
{
    public List<Photo> Photos => Album_Photo.Items
        .Where(ap => ap.Album == this)
        .Select(ap => ap.Photo).ToList();
    public List<Video> Videos => Album_Video.Items
        .Where(av => av.Album == this)
        .Select(av => av.Video).ToList();
}