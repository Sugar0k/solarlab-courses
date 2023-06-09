﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Application.Services.FileService.Interfaces
{
    public interface IFileService
    {
        Task<string> Create(IFormFile file, string path, CancellationToken cancellationToken);
        Task Delete(string id, string path, CancellationToken cancellationToken);
        Task<byte[]> Get(string id, string path, CancellationToken cancellationToken);
    }
}