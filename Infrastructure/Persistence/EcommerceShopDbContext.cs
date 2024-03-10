﻿using EcommerceShop.Domain.Entities.Cart;
using EcommerceShop.Domain.Entities.Product;
using EcommerceShop.Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class EcommerceShopDbContext(DbContextOptions<EcommerceShopDbContext> options)
    : IdentityDbContext<User>(options)
    {      

        public DbSet<EcommerceShop.Domain.Entities.Product.Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<EcommerceShop.Domain.Entities.Product.Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EcommerceShop.Domain.Entities.Product.Product>()
                .HasMany(p => p.Items)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<EcommerceShop.Domain.Entities.Product.Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<ProductItem>()
                .HasKey(pi => pi.SKU);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Color)
                .WithMany(c => c.Items)
                .HasForeignKey(pi => pi.ColorId);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Size)
                .WithMany(s => s.Items)
                .HasForeignKey(pi => pi.SizeId);

            modelBuilder.Entity<Color>()
                .HasMany(c => c.Items)
                .WithOne(pi => pi.Color)
                .HasForeignKey(pi => pi.ColorId);

            modelBuilder.Entity<Size>()
                .HasMany(s => s.Items)
                .WithOne(pi => pi.Size)
                .HasForeignKey(pi => pi.SizeId);

            modelBuilder.Entity<EcommerceShop.Domain.Entities.Product.Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.Owner)
                .HasForeignKey<Cart>(c => c.UserId);
        }
    }
}
