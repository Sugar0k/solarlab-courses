﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IAdvertImageRepository : IRepository<AdvertImage, string>
    {
        Task<IEnumerable<AdvertImage>> GetByAdvertId(int advertId, CancellationToken cancellationToken);
        Task DeleteByAdvertId(int advertId, CancellationToken cancellationToken);
    }
}