using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Models;
using Bulky.DataAccess.Data;

namespace Bulky.DataAccess.Repository
{
    internal class ProductREpository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductREpository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Update(product);
        }
    }
}
