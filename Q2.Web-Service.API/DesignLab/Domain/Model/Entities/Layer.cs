﻿using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public class Layer
{
    public LayerId Id { get; private set; }
    public ProjectId ProjectId { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }
    public float Opacity { get; private set; }
    public bool IsVisible { get; private set; }
    
    public ELayerType LayerType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Constructor protegido requerido por EF Core
    protected Layer() { }

    public Layer(CreateTextLayerCommand command)
    {
        Id = new LayerId(Guid.NewGuid());
        ProjectId = command.ProjectId;
        X = 0; // Default value, can be changed later
        Y = 0; // Default value, can be changed later
        Z = 0; // Default value, can be changed later
        Opacity = 1.0f; // Default opacity
        IsVisible = true; // Default visibility
        LayerType = ELayerType.Text; // Set layer type based on command
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Layer(CreateImageLayerCommand command)
    {
        Id = new LayerId(Guid.NewGuid());
        ProjectId = command.ProjectId;
        X = 0; // Default value, can be changed later
        Y = 0; // Default value, can be changed later
        Z = 0; // Default value, can be changed later
        Opacity = 1.0f; // Default opacity
        IsVisible = true; // Default visibility
        LayerType = ELayerType.Image; // Set layer type based on command
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}