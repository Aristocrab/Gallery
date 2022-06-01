using Gallery.Domain.Entities.Albums;
using Gallery.Domain.Sharings;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.WebApp.Controllers;

[Route("/albums")]
public class AlbumsController : Controller
{
    [HttpGet("/")]
    public IActionResult All()
    {
        var allAlbums = Album.Items.OrderByDescending(a => a.TimeCreated).ToList();
        
        return View(allAlbums);
    }
    
    [HttpGet("filter")]
    public IActionResult All(string photoAlbums, string videoAlbums, string mixedAlbums)
    {
        var allAlbums = new List<Album>();
        if (photoAlbums == "on")
        {
            allAlbums.AddRange(Album.Items.Where(a => a is PhotoAlbum));
        }
        if (videoAlbums == "on")
        {
            allAlbums.AddRange(Album.Items.Where(a => a is VideoAlbum));
        } 
        if (mixedAlbums == "on")
        {
            allAlbums.AddRange(Album.Items.Where(a => a is MixedAlbum));
        }
        
        return View(allAlbums.OrderByDescending(a => a.TimeCreated).ToList());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetAlbum(Guid id)
    {
        var album = Album.Items.FirstOrDefault(a => a.Id == id);
        
        return View(album);
    }
    
    [HttpGet("new")]
    public ActionResult<Guid> CreateAlbum(string name, string? description, string type)
    {
        switch (type)
        {
            case "photo":
                new PhotoAlbum
                {
                    Name = name,
                    Description = description,
                    TimeCreated = DateTime.Now
                };
                break;
            case "video":
                new VideoAlbum
                {
                    Name = name,
                    Description = description,
                    TimeCreated = DateTime.Now
                };
                break;
            case "mixed":
                new MixedAlbum
                {
                    Name = name,
                    Description = description,
                    TimeCreated = DateTime.Now
                };
                break;
        }
        
        
        return Redirect("/");
    }
    
    [HttpGet("update/{id}")]
    public ActionResult<Guid> UpdateAlbum(Guid id, string name, string? description)
    {
        var updatedAlbum = Album.Items.FirstOrDefault(u => u.Id == id);
        
        if (updatedAlbum is not null)
        {
            updatedAlbum.Name = name;
            updatedAlbum.Description = description;
        }
        
        return Redirect("/albums/" + id);
    }
    
    [HttpGet("delete/{id}")]
    public ActionResult<Guid> DeleteAlbum(Guid id)
    {
        var deletedAlbum = Album.Items.FirstOrDefault(u => u.Id == id);
        
        if (deletedAlbum is not null)
        {
            Album_Photo.Items.RemoveAll(ap => ap.Album == deletedAlbum);
            Album_Video.Items.RemoveAll(av => av.Album == deletedAlbum);
            Album.Items.Remove(deletedAlbum);
        }
        
        return Redirect("/");
    }
}