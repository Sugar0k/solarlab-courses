using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.FileService.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Application.Services.FileService.Implementation
{
    public class FileService : IFileService
    {
        private const string FilePath = "Files";

        public async Task<string> Create(IFormFile file, string path, CancellationToken cancellationToken)
        {
            var currentPath = Path.Combine(FilePath, path);
            
            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }
            
            if (file.Length <= 0) throw new FileLoadException("Файл осутствует");
            
            var guid = Guid.NewGuid() + Path.GetExtension(file.FileName);
            await using Stream fileStream = new FileStream(Path.Combine(currentPath, guid), FileMode.Create);
            await file.CopyToAsync(fileStream, cancellationToken);
            return guid;
        }

        public async Task Delete(string id, string path, CancellationToken cancellationToken)
        {
            var currentPath = Path.Combine(FilePath, path, id);

            if (!File.Exists(currentPath))
            {
                await File.AppendAllTextAsync("log.txt", "Попытка удалить отсутствующий файл! " + currentPath + "\n",
                    cancellationToken);
                return;
            }

            File.Delete(currentPath);
        }

        public async Task<byte[]> Get(string id, string path, CancellationToken cancellationToken)
        {
            var currentPath = Path.Combine(FilePath, path, id);
            if (!File.Exists(currentPath)) 
                throw new FileNotFoundException($"Файл по пути {currentPath} отсутствует");

            return await File.ReadAllBytesAsync(currentPath, cancellationToken);
        }
    }
}