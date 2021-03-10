using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.Image;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.DbContext;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LegendaryDashboard.Application.Services.Repositories
{
    public class ImageRepository : Repository<Image, string>, IImageRepository
    {
        //private const string FilePath = "Files";

        public ImageRepository(DashboardContext context) : base(context)
        {
        }

        // public async Task<string> SaveFile(IFormFile file, CancellationToken cancellationToken)
        // {
        //     if (!Directory.Exists(FilePath))
        //     {
        //         Directory.CreateDirectory(FilePath);
        //     }
        //     
        //     if (file.Length <= 0) throw new FileLoadException("Файл осутствует");
        //     
        //     
        //     var guid = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //     await using Stream fileStream = new FileStream(Path.Combine(FilePath, guid), FileMode.Create);
        //     await file.CopyToAsync(fileStream, cancellationToken);
        //     await Save(new Image
        //     {
        //         Id = guid,
        //         FileName = file.FileName,
        //         FilePath = FilePath
        //     }, cancellationToken);
        //     return guid;
        //
        // }

        // public async Task<Image> FindFileById(string id, CancellationToken cancellationToken)
        // {
            // var image = await DbSet.FindAsync(id, cancellationToken);
            // if (image == null) 
            //     throw new FileNotFoundException($"Файл с id {id} не найден");
            // return new ImageDto
            // {
            //     Data = await File.ReadAllBytesAsync(Path.Combine(image.FilePath, image.Id), cancellationToken),
            //     FileType = "image/jpeg",
            //     FileName = image.FileName
            // };
        //     return await DbSet.FindAsync(id, cancellationToken);
        // }

        // public new async Task Delete(string id, CancellationToken cancellationToken)
        // {
        //     var image = await DbSet.FindAsync(id, cancellationToken);
        //     
        //     if (image == null) 
        //         throw new FileNotFoundException($"Файл с id {id} не найден");
        //     
        //     File.Delete(Path.Combine(image.FilePath, id));
        //     
        //     DbSet.Remove(image);
        //     await Context.SaveChangesAsync(cancellationToken);
        // }
    }
}