using Gallery.Domain.Entities;
using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Entities.CapturedOnCamera;

namespace Gallery.Domain.Sharings;

public class Album_Photo : Base<Album_Photo>
{
    private Guid _albumId;
    public Album Album
    {
        get => Album.Items.First(a => a.Id == _albumId);
        set => _albumId = value.Id;
    }
    
    private Guid _photoId;
    public Photo Photo
    {
        get => Photo.Items.First(p => p.Id == _photoId);
        set => _photoId = value.Id;
    }
    
    public Album_Photo(Album album, Photo photo)
    {
        Album = album;
        Photo = photo;
    }
}