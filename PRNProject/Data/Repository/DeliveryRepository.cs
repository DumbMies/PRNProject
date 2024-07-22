using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class DeliveryRepository : Repository<Delivery>
    {
        private readonly AppDbContext _context;
        public DeliveryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
