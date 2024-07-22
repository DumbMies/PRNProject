using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class MaterialSetRepository : Repository<MaterialSet>
    {
        private readonly AppDbContext _context;
        public MaterialSetRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Jewelry GetJewelryByJewelryID(int jewelryID)
        {
            return _context.Jewelry.FirstOrDefault(j => j.JewelryID == jewelryID);
        }
    }
}
