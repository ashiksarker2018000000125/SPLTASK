﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPL_HOME_TASK.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SPLHOMETASKEntities : DbContext
    {
        public SPLHOMETASKEntities()
            : base("name=SPLHOMETASKEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DocumentCategoryInfo> DocumentCategoryInfoes { get; set; }
        public virtual DbSet<DocumentInformation> DocumentInformations { get; set; }
        public virtual DbSet<FileInformation> FileInformations { get; set; }
        public virtual DbSet<MetaDataInformation> MetaDataInformations { get; set; }
    }
}
