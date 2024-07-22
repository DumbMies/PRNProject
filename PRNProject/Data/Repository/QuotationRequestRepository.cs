using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class QuotationRequestRepository : Repository<QuotationRequest>
    {
        private readonly AppDbContext _context;
        public QuotationRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
