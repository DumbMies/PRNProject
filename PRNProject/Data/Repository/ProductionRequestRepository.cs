using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class ProductionRequestRepository : Repository<ProductionRequest>
    {
        private readonly AppDbContext _context;
        public ProductionRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
