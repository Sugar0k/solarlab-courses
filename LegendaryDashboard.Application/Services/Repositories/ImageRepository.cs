using System;
using System.Collections.Generic;
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

        public ImageRepository(DashboardContext context) : base(context)
        {
        }
        
        public async Task DeleteMany(List<Image> images, CancellationToken cancellationToken)
        {
            DbSet.RemoveRange(images);
        }
    }
}