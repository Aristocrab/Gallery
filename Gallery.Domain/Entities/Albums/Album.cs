using Gallery.Domain.Entities.CapturedOnCamera;

namespace Gallery.Domain.Entities.Albums;

public abstract class Album : Base<Album>
{
    public static List<Album> GetPhotoAlbums()
    {
        return Items.Where(a => a is PhotoAlbum).ToList();
    }
    
    public static List<Album> GetVideoAlbums()
    {
        return Items.Where(a => a is VideoAlbum).ToList();
    }
    
    public static List<Album> GetMixedAlbums()
    {
        return Items.Where(a => a is MixedAlbum).ToList();
    }
    
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime TimeCreated { get; set; }

    public List<ICapturedOnCamera> GetAllItems()
    {
        if (this is PhotoAlbum photoAlbum)
        {
            return photoAlbum.Photos.Cast<ICapturedOnCamera>().ToList();
        }

        if (this is VideoAlbum videoAlbum)
        {
            return videoAlbum.Videos.Cast<ICapturedOnCamera>().ToList();
        }

        var mixedAlbum = (this as MixedAlbum)!;
        return mixedAlbum.Photos.Cast<ICapturedOnCamera>().Union(mixedAlbum.Videos)
            .ToList();
    }
    
    public string GetAlbumType()
    {
        if (this is PhotoAlbum) return "Фотоальбом";
        if (this is VideoAlbum) return "Відеоальбом";
        return "Змішаний альбом";
    }

    public TimeSpan GetTimeFromCreation()
    {
        return DateTime.Now - TimeCreated;
    }
}