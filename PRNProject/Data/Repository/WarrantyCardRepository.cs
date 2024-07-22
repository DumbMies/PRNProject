using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class WarrantyCardRepository : Repository<WarrantyCard>
    {
        private readonly AppDbContext _context;
        public WarrantyCardRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
