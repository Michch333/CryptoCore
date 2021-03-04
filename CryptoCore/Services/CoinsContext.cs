using CryptoCore.Models.DALModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Services
{
    public class CoinsContext : DbContext
    {
        public CoinsContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CoinDAL> Coins { get; set; }
    }
}
