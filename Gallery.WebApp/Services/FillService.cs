using Gallery.Domain.Entities;
using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Entities.CapturedOnCamera;
using Gallery.Domain.Sharings;

namespace Gallery.WebApp.Services;

public static class FillService
{
    public static void Fill()
    {
        var album1 = new PhotoAlbum
        {
            Name = "Гарні фотографії",
            Description = "Збірка гарних фотографій",
            TimeCreated = DateTime.Now - TimeSpan.FromMinutes(10)
        };
        var album2 = new VideoAlbum
        {
            Name = "Кліпи",
            Description = "Альбом з короткими відео",
            TimeCreated = DateTime.Now.AddDays(-1)
        };
        var album3 = new MixedAlbum
        {
            Name = "Інше",
            Description = "Альбом для всього іншого",
            TimeCreated = DateTime.Now.AddHours(-2)
        };

        var cs1 = new CameraSetting
        {
            ResolutionWidth = 1280,
            ResolutionHeight = 720,
            ExposureTime = 5,
            ISO = 600
        };
        var cs2 = new CameraSetting
        {
            ResolutionWidth = 1920,
            ResolutionHeight = 1080,
            ExposureTime = 100,
            ISO = 1600
        };

        var photo1 = new Photo
        {
            CameraSetting = cs1,
            Description = "Гарне фото",
            FileId = 1008,
            Place = "Київ",
            TimeCreated = DateTime.Now
        };
        
        new Album_Photo(album1, photo1);
        new Album_Photo(album3, photo1);

        var video1 = new Video
        {
            CameraSetting = cs2,
            Description = "Опис відео",
            FileId = 1006,
            Length = 121,
            Place = "Житомир",
            TimeCreated = DateTime.Now
        };

        new Album_Video(album2, video1);
        new Album_Video(album3, video1);
    }
}