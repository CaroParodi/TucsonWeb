using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Models.Entities;

namespace Tucson.Data.Interfaces
{
    public interface ICategoryRepository
    {
        public Category GetCategoryById(int categoryId);
    }
}
