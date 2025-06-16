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
       EGarmentColor.Black => "#161615",
            EGarmentColor.Gray => "#403D3B",
            EGarmentColor.LightGray => "#B3B1AF",
            EGarmentColor.White => "#EDEDED",
            EGarmentColor.Red => "#B51B14",
            EGarmentColor.Pink => "#F459B0",
            EGarmentColor.LightPurple => "#D890E4",
            EGarmentColor.Purple => "#693FA0",
            EGarmentColor.LightBlue => "#00A5BC",
            EGarmentColor.Cyan => "#31B7C9",
            EGarmentColor.SkyBlue => "#3F9BDC",
            EGarmentColor.Blue => "#1B3D92",
            EGarmentColor.Green => "#1B8937",
            EGarmentColor.LightGreen => "#5BBE65",
            EGarmentColor.Yellow => "#FECD08",
            EGarmentColor.DarkYellow => "#F2AB00",
            _ => "#000000"
        };
    }
    
}