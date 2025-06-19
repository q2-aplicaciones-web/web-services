

using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

public record CreateTextLayerCommand(
    ProjectId ProjectId,
    string Text,
    string FontColor,
    string FontFamily,
    int FontSize,
    bool IsBold,
    bool IsItalic,
    bool IsUnderline
);