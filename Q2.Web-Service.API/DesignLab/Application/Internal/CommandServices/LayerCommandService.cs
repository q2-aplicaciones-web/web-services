using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.Web_Service.API.DesignLab.Application.Internal.CommandServices;

public class LayerCommandService(ILayerRepository layerRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork) : ILayerCommandService
{
    public async Task<LayerId?> Handle(CreateTextLayerCommand command)
    {
        var layer = new TextLayer(command);
        await layerRepository.AddAsync(layer);
        await unitOfWork.CompleteAsync();
        
        // Here should be executed all events
        
        return layer.Id;
    }
    
    public async Task<LayerId?> Handle(CreateImageLayerCommand command)
    {
        var layer = new ImageLayer(command);
        await layerRepository.AddAsync(layer);
        await unitOfWork.CompleteAsync();
        
        // Here should be executed all events
        
        return layer.Id;
    }
    
    public async Task<LayerId?> Handle(DeleteProjectLayerCommand command)
    {
        var project = await projectRepository.GetProjectByIdAsync(Guid.Parse(command.ProjectId));
        if (project == null)
        {
            throw new ArgumentException("Project not found", nameof(command.ProjectId));
        }

        var layer = project.Layers.FirstOrDefault(l => l.Id.ToString() == command.LayerId);
        if (layer == null)
        {
            throw new ArgumentException("Layer not found in the project", nameof(command.LayerId));
        }

        project.RemoveLayer(layer);
        await unitOfWork.CompleteAsync();

        return layer.Id;
    }

    public async Task<LayerId?> Handle(UpdateTextLayerCommand command)
    {
        var layer = await layerRepository.GetByIdAsync(command.LayerId);
        if (layer == null)
        {
            throw new ArgumentException("Layer not found", nameof(command.LayerId));
        }

        if (layer is not TextLayer textLayer)
        {
            throw new ArgumentException("Layer is not a text layer", nameof(command.LayerId));
        }

        textLayer.UpdateDetails(command);
        await layerRepository.UpdateAsync(textLayer);
        await unitOfWork.CompleteAsync();

        return textLayer.Id;
    }

    public async Task<LayerId?> Handle(UpdateImageLayerCommand command)
    {
        var layer = await layerRepository.GetByIdAsync(command.LayerId);
        if (layer == null)
        {
            throw new ArgumentException("Layer not found", nameof(command.LayerId));
        }

        if (layer is not ImageLayer imageLayer)
        {
            throw new ArgumentException("Layer is not an image layer", nameof(command.LayerId));
        }

        imageLayer.UpdateDetails(command);
        await layerRepository.UpdateAsync(imageLayer);
        await unitOfWork.CompleteAsync();

        return imageLayer.Id;
    }

}