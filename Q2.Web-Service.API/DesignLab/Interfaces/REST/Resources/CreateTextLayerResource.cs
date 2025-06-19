namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateTextLayerResource(
    string ProjectId,
    string Text,
    string FontColor,
    string FontFamily,
    int FontSize,
    bool IsBold,
    bool IsItalic,
    bool IsUnderline
    );