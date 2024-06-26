﻿using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Models;
using System.Data;

namespace RecommendationEngineServer.DAL
{
    public class RecommendationEngineDBContext : DbContext
    {
        public RecommendationEngineDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<DailyMenu> DailyMenus { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }  
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ITT-AYUSH-SRIV\\SQLEXPRESS;Database=RecommendationEngineDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

    }
}
