using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Data.Config;
using Tucson.Data.Interfaces;
using Tucson.Models.Entities;

namespace Tucson.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TucsonDbContext dbContext;
        public CategoryRepository(TucsonDbContext db)
        {
            dbContext = db;
        }


        public Category GetCategoryById(int categoryId)
        {
            return dbContext.Set<Category>().Find(categoryId);
        }
    }
}
