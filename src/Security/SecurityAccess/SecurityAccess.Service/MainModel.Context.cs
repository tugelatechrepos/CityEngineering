﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecurityAccess.Service
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MainDevEnvEntities : DbContext
    {
        public MainDevEnvEntities()
            : base("name=MainDevEnvEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public virtual DbSet<CompanyPromotion> CompanyPromotions { get; set; }
        public virtual DbSet<CompanySubscribedCategory> CompanySubscribedCategories { get; set; }
        public virtual DbSet<CompanyUserType> CompanyUserTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAreaCode> UserAreaCodes { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<CompanyBranchDetail> CompanyBranchDetails { get; set; }
        public virtual DbSet<CompanyUserDetail> CompanyUserDetails { get; set; }
        public virtual DbSet<UserGeometry> UserGeometries { get; set; }
        public virtual DbSet<BranchGeometry> BranchGeometries { get; set; }
    }
}
