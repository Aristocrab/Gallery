using Gallery.Domain.Entities.CapturedOnCamera;
using Gallery.Domain.Sharings;

namespace Gallery.Domain.Entities.Albums;

public class PhotoAlbum : Album
{
    public List<Photo> Photos => Album_Photo.Items
        .Where(ap => ap.Album == this)
        .Select(ap => ap.Photo).ToList();
}