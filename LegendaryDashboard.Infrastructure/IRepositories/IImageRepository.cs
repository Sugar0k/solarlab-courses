using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.Image;
using LegendaryDashboard.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IImageRepository: IRepository<Image, string>
    {
        Task DeleteMany(List<Image> images, CancellationToken cancellationToken);
        // Task<string> SaveFile(IFormFile file, CancellationToken cancellationToken);
        // Task<Image> FindFileById(string id, CancellationToken cancellationToken);
    }
}