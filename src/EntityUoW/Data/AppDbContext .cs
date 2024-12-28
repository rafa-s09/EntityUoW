﻿using EntityUoW.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityUoW.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
