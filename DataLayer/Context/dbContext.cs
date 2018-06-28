using DomainClasses.Entity;
using DomainClasses.EntityConfig;
using Microsoft.EntityFrameworkCore;
using Sobhan.DomainClasses;
using System;

namespace DataLayer.Context
{
    public class dbContext : DbContext, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             //optionsBuilder.UseSqlServer("Data Source=188.40.161.246;Initial Catalog=dbSobhan;User ID=baran;Password=Sobhan+1347;");
              optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbSobhan;Integrated Security = true");
        }

        public dbContext(DbContextOptions<dbContext> options)
       : base(options)
        { }

        public dbContext()
        {
        }

        #region Dataset

        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<CountryCodes> CountryCodes { get; set; }
        public virtual DbSet<UserImg> UserImg { get; set; }

        #endregion Dataset

        #region IUnitOfWork

        DbSet<TEntity> IUnitOfWork.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            try
            {
                return base.SaveChanges(true);
             
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void MarkAsAdd<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        #endregion IUnitOfWork

        //!!!!!! The Code Must Entired to Config
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContactsConfig());
            builder.ApplyConfiguration(new ChatConfig());
            builder.ApplyConfiguration(new UserImgConfig());
            builder.ApplyConfiguration(new UserConfig());
            //UserConfig

            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            // Custom application mappings
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).HasMaxLength(450);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.SerialNumber).HasMaxLength(450);
            });

            builder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
                entity.HasOne(d => d.UserIdentity).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
            });

            builder.Entity<UserToken>(entity =>
            {
                entity.HasOne(ut => ut.UserIdentity)
                      .WithMany(u => u.UserTokens)
                      .HasForeignKey(ut => ut.UserId);

                entity.Property(ut => ut.RefreshTokenIdHash).HasMaxLength(450).IsRequired();
                entity.Property(ut => ut.RefreshTokenIdHashSource).HasMaxLength(450);
            });
        }
    }
}