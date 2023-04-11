using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarService.Models.Entities;

public partial class CarServiceContext : DbContext
{
    public CarServiceContext()
    {
    }

    public CarServiceContext(DbContextOptions<CarServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblEstimateItem> TblEstimateItems { get; set; }

    public virtual DbSet<TblEstimateMaster> TblEstimateMasters { get; set; }

    public virtual DbSet<TblJobMaster> TblJobMasters { get; set; }

    public virtual DbSet<TblJobRemark> TblJobRemarks { get; set; }

    public virtual DbSet<TblJobRemarkMaster> TblJobRemarkMasters { get; set; }

    public virtual DbSet<TblOrganizationMaster> TblOrganizationMasters { get; set; }

    public virtual DbSet<TblPayment> TblPayments { get; set; }

    public virtual DbSet<TblServiceItemMaster> TblServiceItemMasters { get; set; }

    public virtual DbSet<TblSystemUser> TblSystemUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:CarService");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblEstimateItem>(entity =>
        {
            entity.HasKey(e => e.FldEstimateItemId);

            entity.ToTable("tbl_EstimateItems");

            entity.Property(e => e.FldEstimateItemId).HasColumnName("fld_EstimateItemId");
            entity.Property(e => e.FldCancelReason)
                .HasMaxLength(200)
                .HasColumnName("fld_CancelReason");
            entity.Property(e => e.FldDiscountAmount)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_DiscountAmount");
            entity.Property(e => e.FldEstimateId).HasColumnName("fld_EstimateId");
            entity.Property(e => e.FldIsCancelled).HasColumnName("fld_IsCancelled");
            entity.Property(e => e.FldItemTitle)
                .HasMaxLength(100)
                .HasColumnName("fld_ItemTitle");
            entity.Property(e => e.FldItemTotal)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_ItemTotal");
            entity.Property(e => e.FldItemType)
                .HasMaxLength(50)
                .HasColumnName("fld_ItemType");
            entity.Property(e => e.FldQuantity)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_Quantity");
            entity.Property(e => e.FldQuantityUnit)
                .HasMaxLength(20)
                .HasColumnName("fld_QuantityUnit");
            entity.Property(e => e.FldUnitAmount)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_UnitAmount");
        });

        modelBuilder.Entity<TblEstimateMaster>(entity =>
        {
            entity.HasKey(e => e.FldEstimateId);

            entity.ToTable("tbl_EstimateMaster");

            entity.Property(e => e.FldEstimateId).HasColumnName("fld_EstimateId");
            entity.Property(e => e.FldCreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("fld_CreatedOn");
            entity.Property(e => e.FldEstimateNumber)
                .HasMaxLength(50)
                .HasColumnName("fld_EstimateNumber");
            entity.Property(e => e.FldInvoiceCreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("fld_InvoiceCreatedOn");
            entity.Property(e => e.FldInvoiceNumber)
                .HasMaxLength(50)
                .HasColumnName("fld_InvoiceNumber");
            entity.Property(e => e.FldInvoiceType)
                .HasMaxLength(50)
                .HasColumnName("fld_InvoiceType");
            entity.Property(e => e.FldIsInvoiceGenerated).HasColumnName("fld_IsInvoiceGenerated");
            entity.Property(e => e.FldJobId).HasColumnName("fld_JobId");
            entity.Property(e => e.FldTotalAmount)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_TotalAmount");
        });

        modelBuilder.Entity<TblJobMaster>(entity =>
        {
            entity.HasKey(e => e.FldJobId);

            entity.ToTable("tbl_JobMaster");

            entity.Property(e => e.FldJobId).HasColumnName("fld_JobId");
            entity.Property(e => e.FldChessisNumber)
                .HasMaxLength(50)
                .HasColumnName("fld_ChessisNumber");
            entity.Property(e => e.FldCustomerContact1)
                .HasMaxLength(15)
                .HasColumnName("fld_CustomerContact1");
            entity.Property(e => e.FldCustomerContact2)
                .HasMaxLength(15)
                .HasColumnName("fld_CustomerContact2");
            entity.Property(e => e.FldCustomerName)
                .HasMaxLength(100)
                .HasColumnName("fld_CustomerName");
            entity.Property(e => e.FldEngineNumber)
                .HasMaxLength(50)
                .HasColumnName("fld_EngineNumber");
            entity.Property(e => e.FldHandedOverOn)
                .HasColumnType("datetime")
                .HasColumnName("fld_HandedOverOn");
            entity.Property(e => e.FldJobNo)
                .HasMaxLength(100)
                .HasColumnName("fld_JobNo");
            entity.Property(e => e.FldKmReadingOnRegistration)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_KmReadingOnRegistration");
            entity.Property(e => e.FldModelNameNumber)
                .HasMaxLength(50)
                .HasColumnName("fld_ModelNameNumber");
            entity.Property(e => e.FldOrgId).HasColumnName("fld_OrgId");
            entity.Property(e => e.FldRegisteredOn)
                .HasColumnType("datetime")
                .HasColumnName("fld_RegisteredOn");
            entity.Property(e => e.FldServiceType)
                .HasMaxLength(15)
                .HasColumnName("fld_ServiceType");
            entity.Property(e => e.FldVehicleRegisteredNumber)
                .HasMaxLength(15)
                .HasColumnName("fld_VehicleRegisteredNumber");
        });

        modelBuilder.Entity<TblJobRemark>(entity =>
        {
            entity.HasKey(e => new { e.FldJobId, e.FldRemarkTitle });

            entity.ToTable("tbl_JobRemarks");

            entity.Property(e => e.FldJobId).HasColumnName("fld_JobId");
            entity.Property(e => e.FldRemarkTitle)
                .HasMaxLength(100)
                .HasColumnName("fld_RemarkTitle");
        });

        modelBuilder.Entity<TblJobRemarkMaster>(entity =>
        {
            entity.HasKey(e => e.FldRemarkId);

            entity.ToTable("tbl_JobRemarkMaster");

            entity.Property(e => e.FldRemarkId).HasColumnName("fld_RemarkId");
            entity.Property(e => e.FldRemarkTitle)
                .HasMaxLength(200)
                .HasColumnName("fld_RemarkTitle");
        });

        modelBuilder.Entity<TblOrganizationMaster>(entity =>
        {
            entity.HasKey(e => e.FldOrgId);

            entity.ToTable("tbl_OrganizationMaster");

            entity.Property(e => e.FldOrgId).HasColumnName("fld_OrgId");
            entity.Property(e => e.FldActiveUntil)
                .HasColumnType("date")
                .HasColumnName("fld_ActiveUntil");
            entity.Property(e => e.FldAddress)
                .HasMaxLength(300)
                .HasColumnName("fld_Address");
            entity.Property(e => e.FldContactNumber1)
                .HasMaxLength(15)
                .HasColumnName("fld_ContactNumber1");
            entity.Property(e => e.FldContactNumber2)
                .HasMaxLength(15)
                .HasColumnName("fld_ContactNumber2");
            entity.Property(e => e.FldContactPerson1)
                .HasMaxLength(100)
                .HasColumnName("fld_ContactPerson1");
            entity.Property(e => e.FldContactPerson2)
                .HasMaxLength(100)
                .HasColumnName("fld_ContactPerson2");
            entity.Property(e => e.FldIsFullSubscription).HasColumnName("fld_IsFullSubscription");
            entity.Property(e => e.FldLicenseNumber)
                .HasMaxLength(50)
                .HasColumnName("fld_LicenseNumber");
            entity.Property(e => e.FldLogo)
                .HasMaxLength(100)
                .HasColumnName("fld_Logo");
            entity.Property(e => e.FldOrgEmail)
                .HasMaxLength(100)
                .HasColumnName("fld_OrgEmail");
            entity.Property(e => e.FldOrgName)
                .HasMaxLength(200)
                .HasColumnName("fld_OrgName");
        });

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity.HasKey(e => e.FldPaymentId);

            entity.ToTable("tbl_Payments");

            entity.Property(e => e.FldPaymentId).HasColumnName("fld_PaymentId");
            entity.Property(e => e.FldJobId).HasColumnName("fld_JobId");
            entity.Property(e => e.FldPaidAmount)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_PaidAmount");
            entity.Property(e => e.FldPaymentDatetime)
                .HasColumnType("datetime")
                .HasColumnName("fld_PaymentDatetime");
            entity.Property(e => e.FldPaymentMode)
                .HasMaxLength(50)
                .HasColumnName("fld_PaymentMode");
        });

        modelBuilder.Entity<TblServiceItemMaster>(entity =>
        {
            entity.HasKey(e => e.FldServiceItemId);

            entity.ToTable("tbl_ServiceItemMaster");

            entity.Property(e => e.FldServiceItemId).HasColumnName("fld_ServiceItemId");
            entity.Property(e => e.FldIdealAmount)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("fld_IdealAmount");
            entity.Property(e => e.FldItemName)
                .HasMaxLength(100)
                .HasColumnName("fld_ItemName");
            entity.Property(e => e.FldItemType)
                .HasMaxLength(20)
                .HasColumnName("fld_ItemType");
            entity.Property(e => e.FldQuanitityUnit)
                .HasMaxLength(20)
                .HasColumnName("fld_QuanitityUnit");
        });

        modelBuilder.Entity<TblSystemUser>(entity =>
        {
            entity.HasKey(e => e.FldUserId);

            entity.ToTable("tbl_SystemUsers");

            entity.Property(e => e.FldUserId).HasColumnName("fld_UserId");
            entity.Property(e => e.FldEmailId)
                .HasMaxLength(100)
                .HasColumnName("fld_EmailId");
            entity.Property(e => e.FldFullName)
                .HasMaxLength(100)
                .HasColumnName("fld_FullName");
            entity.Property(e => e.FldIsActive).HasColumnName("fld_IsActive");
            entity.Property(e => e.FldMobileNumber)
                .HasMaxLength(15)
                .HasColumnName("fld_MobileNumber");
            entity.Property(e => e.FldOrgId).HasColumnName("fld_OrgId");
            entity.Property(e => e.FldPassword)
                .HasMaxLength(200)
                .HasColumnName("fld_Password");
            entity.Property(e => e.FldRole)
                .HasMaxLength(15)
                .HasColumnName("fld_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
