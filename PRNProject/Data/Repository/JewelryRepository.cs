using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class JewelryRepository : Repository<Jewelry>
    {
        private readonly AppDbContext _context;
        public JewelryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Jewelry> GetByProductionRequestID(int productionRequestId)
        {
            return _context.Jewelry.Where(j => j.ProductionRequestID == productionRequestId).ToList();
        }
        public IEnumerable<MaterialSet> GetMaterialSetsByJewelryID(int jewelryId)
        {
            return _context.MaterialSet.Where(ms => ms.JewelryID == jewelryId).ToList();
        }

        public ProductionRequest GetProductionRequestByJewelryID(int jewelryId)
        {
            var jewelry = _context.Jewelry.Find(jewelryId);
            return _context.ProductionRequest.FirstOrDefault(pr => pr.ProductionRequestID == jewelry.ProductionRequestID);
        }
    }
}
