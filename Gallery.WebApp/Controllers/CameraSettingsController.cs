using Gallery.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.WebApp.Controllers;

[Route("/camerasettings")]
public class CameraSettingsController : Controller
{
    public IActionResult All()
    {
        var allCameraSettings = CameraSetting.Items;
        
        return View(allCameraSettings);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetCameraSetting(Guid id)
    {
        var cs = CameraSetting.Items.FirstOrDefault(cs => cs.Id == id);
        
        return View(cs);
    }
    
    [HttpPost("new")]
    public ActionResult<Guid> CreateCameraSetting(int width, int height, int exposureTime, int iso)
    {
        var cs = new CameraSetting
        {
            ResolutionWidth = width,
            ResolutionHeight = height,
            ExposureTime = exposureTime,
            ISO = iso
        };
        
        return Redirect("/camerasettings/" + cs.Id);
    }
    
    [HttpPost("update/{id}")]
    public ActionResult<Guid> UpdateCameraSetting(Guid id, int width, int height, int exposureTime, int iso)
    {
        var updatedCameraSetting = CameraSetting.Items.FirstOrDefault(u => u.Id == id);
        
        if (updatedCameraSetting is not null)
        {
            updatedCameraSetting.ResolutionWidth = width;
            updatedCameraSetting.ResolutionHeight = height;
            updatedCameraSetting.ExposureTime = exposureTime;
            updatedCameraSetting.ISO = iso;
            
            return Redirect("/camerasettings/" + id);
        }
        
        return Redirect("/camerasettings");
    }
    
    [HttpGet("delete/{id}")]
    public ActionResult<Guid> DeleteCameraSetting(Guid id)
    {
        var deletedCameraSetting = CameraSetting.Items.FirstOrDefault(u => u.Id == id);
        
        if (deletedCameraSetting is not null && !deletedCameraSetting.Photos.Any() && !deletedCameraSetting.Videos.Any())
        {
            CameraSetting.Items.Remove(deletedCameraSetting);
            return Redirect("/camerasettings");
        }
        
        return Redirect("/camerasettings/" + id);
    }
}