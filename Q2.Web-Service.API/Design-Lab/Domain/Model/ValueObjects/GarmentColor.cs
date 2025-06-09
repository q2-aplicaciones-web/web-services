namespace Q2.Web_Service.API.Design_Lab.Domain.Model.ValueObjects;

public class GarmentColor
{
    public EGarmentColor Value { get; }

    public GarmentColor(EGarmentColor color)
    {
        Value = color;
    }

    public string ToHexCode()
    {
        return Value switch
        {
            EGarmentColor.BLACK => "#161615",
            EGarmentColor.GRAY => "#403D3B",
            EGarmentColor.LIGHT_GRAY => "#B3B1AF",
            EGarmentColor.WHITE => "#EDEDED",
            EGarmentColor.RED => "#B51B14",
            EGarmentColor.PINK => "#F459B0",
            EGarmentColor.LIGHT_PURPLE => "#D890E4",
            EGarmentColor.PURPLE => "#693FA0",
            EGarmentColor.LIGHT_BLUE => "#00A5BC",
            EGarmentColor.CYAN => "#31B7C9",
            EGarmentColor.SKY_BLUE => "#3F9BDC",
            EGarmentColor.BLUE => "#1B3D92",
            EGarmentColor.GREEN => "#1B8937",
            EGarmentColor.LIGHT_GREEN => "#5BBE65",
            EGarmentColor.YELLOW => "#FECD08",
            EGarmentColor.DARK_YELLOW => "#F2AB00",
            _ => "#000000"
        };
    }
    
}