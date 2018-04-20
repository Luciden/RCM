﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RCM.Domain.Models.ProdutoModels;
using RCM.Domain.Repositories;
using RCM.Infra.Data.Context;

namespace RCM.Infra.Data.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(RCMDbContext dbContext) : base(dbContext)
        {
        }

        public override Produto GetById(Guid id)
        {
            return _dbSet
                .Include(ap => ap.Aplicacoes)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}