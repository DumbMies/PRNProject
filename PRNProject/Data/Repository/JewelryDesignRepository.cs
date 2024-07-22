using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class JewelryDesignRepository : Repository<JewelryDesign>
    {
        private readonly AppDbContext _context;
        public JewelryDesignRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
