using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public class TextLayer : Layer
{
    public string Text { get; set; }
    public int FontSize { get; set; }
    public string FontColor { get; set; }
    public string FontFamily { get; set; }
    public bool IsBold { get; set; }
    public bool IsItalic { get; set; }
    public bool IsUnderline { get; set; }

    public TextLayer(CreateTextLayerCommand command) : base(command)
    {
        Text= command.Text;
        FontSize = command.FontSize;
        FontColor = command.FontColor;
        FontFamily = command.FontFamily;
        IsBold = command.IsBold;
        IsItalic = command.IsItalic;
        IsUnderline = command.IsUnderline;
    }
}