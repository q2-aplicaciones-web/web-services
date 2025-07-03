using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Q2.Web_Service.API.IAM.Domain.Model.Aggregates;


public partial class User : IEntityWithCreatedUpdatedDate
{
    [Column(name:"CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column(name:"UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}