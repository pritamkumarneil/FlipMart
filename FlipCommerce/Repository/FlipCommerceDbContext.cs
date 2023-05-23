using Microsoft.EntityFrameworkCore;
using FlipCommerce.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FlipCommerce.Enums;

namespace FlipCommerce.Repository
{
    public class FlipCommerceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        public FlipCommerceDbContext(DbContextOptions<FlipCommerceDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        // property definition of all the entity 
            // Seller Properties
            modelBuilder.Entity<Seller>()
                .Property(s => s.MobNo)
                .HasMaxLength(10)
                .IsRequired();
            modelBuilder.Entity<Seller>()
                .HasIndex(s => s.MobNo)
                .IsUnique();
            modelBuilder.Entity<Seller>()
                .HasIndex(s => s.EmailId)
                .IsUnique();
            modelBuilder.Entity<Seller>()
                .Property(s => s.gender)
                .HasConversion(new EnumToStringConverter<Gender>());
            // Customer Properties
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.MobNo)
                .IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(c=>c.EmailId)
                .IsUnique();
            modelBuilder.Entity<Customer>()
                .Property(c => c.MobNo)
                .HasMaxLength(10)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(c => c.gender)
                .HasConversion(new EnumToStringConverter<Gender>());
            //Card Properties
            modelBuilder.Entity<Card>()
                .HasIndex(c => c.CardNo)
                .IsUnique();
            modelBuilder.Entity<Card>()
                .Property(c => c.cardType)
                .HasConversion(new EnumToStringConverter<CardType>());
            // Order properties
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNo)
                .IsUnique();
            // Product properties
            modelBuilder.Entity<Product>()
                .Property(p => p.category)
                .HasConversion(new EnumToStringConverter<Category>());
            modelBuilder.Entity<Product>()
                .Property(p => p.productStatus)
                .HasConversion(new EnumToStringConverter<ProductStatus>());

            // defining the relation between Product and seller
            modelBuilder.Entity<Product>()
                .HasOne<Seller>(p => p.seller)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId);
            // defining the relation between item and product
            modelBuilder.Entity<Item>()
                .HasOne<Product>(i => i.product)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.ProductId);
            //defining the relation between item and Order
            modelBuilder.Entity<Item>()
                .HasOne<Order>(i => i.order)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OrderId);
            //defining the relation between item to cart
            modelBuilder.Entity<Item>()
                .HasOne<Cart>(i => i.cart)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CartId);
            //defining relation between Order and Customer
            modelBuilder.Entity<Order>()
                .HasOne<Customer>(o => o.customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
            //defining realtion between Cart and Customer
            modelBuilder.Entity<Cart>()
                .HasOne<Customer>(c => c.customer)
                .WithOne(c => c.cart)
            // here we had to define the <Cart> so that it can  understand that cart is child
                .HasForeignKey<Cart>(c =>c.CustomerId);
            //defining Relation between Card and Customer
            modelBuilder.Entity<Card>()
                .HasOne<Customer>(c => c.custmer)
                .WithMany(c => c.Cards)
                .HasForeignKey(c => c.CustomerId);
        }

    }
}
