﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Entities.ValueObjects;
using Sales.Repositories.Abstracts;
using Sales.Repositories.Interfaces;

namespace Sales.Repositories;

public class SaleRepository: AbstractRepository<Sale, SaleDbModel>
{

    public SaleRepository(Context context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<IEnumerable<Sale>> GetAll(int limit, int offset)
    {
        var salesDbModels = await Context.Sales
            .Skip(offset)
            .Take(limit)
            .Include(s => s.SaleData)
            .ToListAsync();
        return Mapper.Map<List<Sale>>(salesDbModels);
    }

    public override async Task<Sale?> GetById(long id)
    {
        var saleDbModel = await Context.Sales
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Include(s => s.SaleData)
            .FirstOrDefaultAsync();
        if (saleDbModel == null) return null;
        var sale = Mapper.Map<Sale>(saleDbModel);
        return sale;
    }
}