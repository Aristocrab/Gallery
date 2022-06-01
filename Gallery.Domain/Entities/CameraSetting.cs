using Gallery.Domain.Entities.CapturedOnCamera;

namespace Gallery.Domain.Entities;

public class CameraSetting : Base<CameraSetting>
{
    public List<Photo> Photos => Photo.Items.Where(p => p.CameraSetting == this).ToList();
    public List<Video> Videos => Video.Items.Where(v => v.CameraSetting == this).ToList();
    
    public int ISO { get; set; }
    public int ExposureTime { get; set; }
    public int ResolutionWidth { get; set; }
    public int ResolutionHeight { get; set; }

    public List<ICapturedOnCamera> GetAllItems()
    {
        var all = Photos.Cast<ICapturedOnCamera>().ToList();
        all.AddRange(Videos);
        
        return all;
    }

    public ResolutionFormat GetResolutionType()
    {
        var megaPixel = ResolutionWidth * ResolutionHeight / 1000000.0;
        
        return megaPixel switch
        {
            >= 8.29 => ResolutionFormat.UltraHD,
            >= 2.07 => ResolutionFormat.FullHD,
            >= 0.92 => ResolutionFormat.HD,
            _ => ResolutionFormat.SD
        };
    }

    public string GetIsoType()
    {
        return ISO switch
        {
            >= 1600 => "Темно",
            >= 800 => "Похмуро",
            >= 400 => "Хмарно",
            < 400 => "Сонячно",
        };
    }
}

public enum ResolutionFormat  
{
    SD,
    HD,
    FullHD,
    UltraHD
}