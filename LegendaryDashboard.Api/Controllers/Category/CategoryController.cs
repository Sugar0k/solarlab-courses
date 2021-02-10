using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LegendaryDashboard.Api.DbContext;
using Microsoft.Extensions.Configuration;

namespace LegendaryDashboard.Api.Controllers.Category
{
    [ApiController]
    [Route("api/category/")]
    public partial class CategoryController : ControllerBase
    {
        private readonly DashboardContext _db;
        
        public CategoryController(DashboardContext context)
        {
            _db = context;
        }
    }
}
