using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

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

    // Constructor protegido requerido por EF Core
    protected TextLayer() : base()
    {
        Text = null!; // Will be set by EF Core
        FontColor = null!; // Will be set by EF Core
        FontFamily = null!; // Will be set by EF Core
    }

    public TextLayer(CreateTextLayerCommand command) : base(command.ProjectId, ELayerType.Text)
    {
        Text= command.Text;
        FontSize = command.FontSize;
        FontColor = command.FontColor;
        FontFamily = command.FontFamily;
        IsBold = command.IsBold;
        IsItalic = command.IsItalic;
        IsUnderline = command.IsUnderline;
    }

    public void UpdateDetails(UpdateTextLayerCommand command)
    {
        Text = command.Text;
        FontSize = command.FontSize;
        FontColor = command.FontColor;
        FontFamily = command.FontFamily;
        IsBold = command.IsBold;
        IsItalic = command.IsItalic;
        IsUnderline = command.IsUnderlined;
        // UpdatedAt will be handled by the base Layer class if we add it
    }
}