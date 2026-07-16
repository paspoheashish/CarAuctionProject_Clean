using AuctionService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        // Configuring the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your actual SQL Server connection string
            string connectionString = "Data Source=YAKSH;Initial Catalog=AuctionDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}
