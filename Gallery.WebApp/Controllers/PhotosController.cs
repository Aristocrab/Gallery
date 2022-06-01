using Gallery.Domain.Entities;
using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Entities.CapturedOnCamera;
using Gallery.Domain.Sharings;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.WebApp.Controllers;

[Route("/photos")]
public class PhotosController : Controller
{
    public IActionResult All(string? date = null)
    {
        if (date is null)
        {
            var allPhotos = Photo.Items.OrderByDescending(p => p.TimeCreated).ToList();
        
            return View(allPhotos);
        }

        var dateOnly = DateOnly.Parse(date);
        var photosByDate = Photo.GetPhotosByDate(dateOnly).OrderByDescending(p => p.TimeCreated).ToList();
        
        return View(photosByDate);
    }
    
    [Route("{id}")]
    public IActionResult GetPhoto(Guid id)
    {
        var photo = Photo.Items.FirstOrDefault(p => p.Id == id);
        if (photo is not null)
        {
            return View(photo);
        }
        
        return Redirect("/photos");
    }
    
    [HttpPost("new")]
    public ActionResult<Guid> CreatePhoto(List<Guid> albumIds, Guid cameraSettingId,
        int fileId, string? place = null, string? description = null)
    {
        var albums = Album.Items.Where(album => albumIds.Contains(album.Id));
        var cameraSetting = CameraSetting.Items.FirstOrDefault(cs => cs.Id == cameraSettingId);

        if(cameraSetting is not null)
        {
            var photo = new Photo
            {
                CameraSetting = cameraSetting,
                FileId = fileId,
                Place = place,
                Description = description,
                TimeCreated = DateTime.Now
            };

            foreach (var album in albums)
            {
                if (album is PhotoAlbum or MixedAlbum)
                {
                    new Album_Photo(album, photo);
                }
            }
            
            return Redirect("/photos/" + photo.Id);
        }
        
        return Redirect("/photos/");
    }
    
    [HttpPost("update/{id}")]
    public ActionResult<Guid> UpdatePhoto(Guid id, List<Guid> albumIds, Guid cameraSettingId,
        int fileId, string? place = null, string? description = null)
    {
        var albums = Album.Items.Where(album => albumIds.Contains(album.Id));
        var updatedPhoto = Photo.Items.FirstOrDefault(u => u.Id == id);
        var cameraSetting = CameraSetting.Items.FirstOrDefault(cs => cs.Id == cameraSettingId);

        if(updatedPhoto is not null && cameraSetting is not null)
        {
            updatedPhoto.CameraSetting = cameraSetting;
            updatedPhoto.FileId = fileId;
            updatedPhoto.Place = place;
            updatedPhoto.Description = description;

            Album_Photo.Items.RemoveAll(ap => ap.Photo == updatedPhoto);
            
            foreach (var album in albums)
            {
                if (album is PhotoAlbum or MixedAlbum)
                {
                    new Album_Photo(album, updatedPhoto);
                }
            }
        }
        
        return Redirect("/photos/" + id);
    }
    
    [HttpGet("delete/{id}")]
    public ActionResult<Guid> DeletePhoto(Guid id)
    {
        var deletedPhoto = Photo.Items.FirstOrDefault(u => u.Id == id);
        
        if (deletedPhoto is not null)
        {
            Album_Photo.Items.RemoveAll(ap => ap.Photo == deletedPhoto);
            Photo.Items.Remove(deletedPhoto);
        }
        
        return Redirect("/photos");
    }
}