using Gallery.Domain.Entities;
using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Entities.CapturedOnCamera;
using Gallery.Domain.Sharings;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.WebApp.Controllers;

[Route("/videos")]
public class VideosController : Controller
{
    public IActionResult All(string withSoundOnly = "")
    {
        if (withSoundOnly == "on")
        {
            var allVideos = Video.GetVideosWithSound().OrderByDescending(v => v.TimeCreated).ToList();
            return View(allVideos);
        }
        else
        {
            var allVideos = Video.Items.OrderByDescending(v => v.TimeCreated).ToList();
            return View(allVideos);
        }
    }
    
    [Route("{id:guid}")]
    public IActionResult GetVideo(Guid id)
    {
        var video = Video.Items.FirstOrDefault(p => p.Id == id);
        if (video is not null)
        {
            return View(video);
        }
        
        return Redirect("/videos");
    }
    
    [HttpPost("new")]
    public ActionResult<Guid> CreateVideo(List<Guid> albumIds, Guid cameraSettingId,
        int fileId, string hasSound, int length, string? place = null, string? description = null)
    {
        var albums = Album.Items.Where(album => albumIds.Contains(album.Id));
        var cameraSetting = CameraSetting.Items.FirstOrDefault(cs => cs.Id == cameraSettingId);

        if(cameraSetting is not null)
        {
            var video = new Video
            {
                CameraSetting = cameraSetting,
                FileId = fileId,
                Place = place,
                Description = description,
                HasSound = hasSound == "on",
                Length = length,
                TimeCreated = DateTime.Now
            };

            foreach (var album in albums)
            {
                if (album is VideoAlbum or MixedAlbum)
                {
                    new Album_Video(album, video);
                }
            }
            
            return Redirect("/videos/" + video.Id);
        }
        
        return Redirect("/videos/");
    }
    
    [HttpPost("update/{id}")]
    public ActionResult<Guid> UpdateVideo(Guid id, List<Guid> albumIds, Guid cameraSettingId,
        int fileId, string hasSound, int length, string? place = null, string? description = null)
    {
        var albums = Album.Items.Where(album => albumIds.Contains(album.Id));
        var updatedVideo = Video.Items.FirstOrDefault(u => u.Id == id);
        var cameraSetting = CameraSetting.Items.FirstOrDefault(cs => cs.Id == cameraSettingId);

        if(updatedVideo is not null && cameraSetting is not null)
        {
            updatedVideo.CameraSetting = cameraSetting;
            updatedVideo.FileId = fileId;
            updatedVideo.Place = place;
            updatedVideo.Description = description;
            updatedVideo.HasSound = hasSound == "on";
            updatedVideo.Length = length;

            Album_Video.Items.RemoveAll(ap => ap.Video == updatedVideo);
            
            foreach (var album in albums)
            {
                if (album is VideoAlbum or MixedAlbum)
                {
                    new Album_Video(album, updatedVideo);
                }
            }
        }
        
        return Redirect("/videos/" + id);
    }
    
    [HttpGet("delete/{id}")]
    public ActionResult<Guid> DeleteVideo(Guid id)
    {
        var deletedVideo = Video.Items.FirstOrDefault(u => u.Id == id);
        
        if (deletedVideo is not null)
        {
            Album_Video.Items.RemoveAll(av => av.Video == deletedVideo);
            Video.Items.Remove(deletedVideo);
        }
        
        return Redirect("/videos");
    }
}