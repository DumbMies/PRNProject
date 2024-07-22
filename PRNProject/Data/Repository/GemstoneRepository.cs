﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public class GemstoneRepository : Repository<Gemstone>
    {
        private readonly AppDbContext _context;
        public GemstoneRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
