﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    internal class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
