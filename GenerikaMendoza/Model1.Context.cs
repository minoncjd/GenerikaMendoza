﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenerikaMendoza
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GenerikaMendozaEntities1 : DbContext
    {
        public GenerikaMendozaEntities1()
            : base("name=GenerikaMendozaEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<ProductForm> ProductForms { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    
        public virtual ObjectResult<GetDelivery_Result> GetDelivery()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDelivery_Result>("GetDelivery");
        }
    
        public virtual ObjectResult<GetDailyDelivery_Result> GetDailyDelivery()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDailyDelivery_Result>("GetDailyDelivery");
        }
    
        public virtual ObjectResult<GetSales_Result> GetSales()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSales_Result>("GetSales");
        }
    
        public virtual ObjectResult<GetLogs_Result> GetLogs(Nullable<System.DateTime> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLogs_Result>("GetLogs", dateParameter);
        }
    
        public virtual ObjectResult<GetProducts_Result> GetProducts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProducts_Result>("GetProducts");
        }
    }
}
