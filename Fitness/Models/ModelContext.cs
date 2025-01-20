using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Infofitness> Infofitnesses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<Typeperson> Typepeople { get; set; }

    public virtual DbSet<Workoutplan> Workoutplans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("DATA SOURCE=192.168.1.126;USER ID=C##Fitness;PASSWORD=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##FITNESS")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Infofitness>(entity =>
        {
            entity.HasKey(e => e.Idif).HasName("SYS_C008447");

            entity.ToTable("INFOFITNESS");

            entity.Property(e => e.Idif)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("IDIF");
            entity.Property(e => e.Aboutus)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ABOUTUS");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Facebook)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("FACEBOOK");
            entity.Property(e => e.Inprofileid)
                .HasColumnType("NUMBER")
                .HasColumnName("INPROFILEID");
            entity.Property(e => e.Linkedin)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("LINKEDIN");
            entity.Property(e => e.Location)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Phone)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Photoaboutus)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("PHOTOABOUTUS");

            entity.HasOne(d => d.Inprofile).WithMany(p => p.Infofitnesses)
                .HasForeignKey(d => d.Inprofileid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_INFO");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("SYS_C008462");

            entity.ToTable("PAYMENT");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedOnAdd()  // تحديد أن القيمة ستُولَّد تلقائيًا عند الإضافة
                .HasColumnType("NUMBER")  // تحديد نوع البيانات في قاعدة البيانات
                .HasColumnName("PAYMENTID");

            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(18,2)")  // تحديد النوع في قاعدة البيانات
                .HasColumnName("AMOUNT");

            entity.Property(e => e.Cardholdername)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARDHOLDERNAME");

            entity.Property(e => e.Cardnumber)
                .HasPrecision(16)  // تحديد عدد الأرقام في رقم البطاقة
                .HasColumnName("CARDNUMBER");

            entity.Property(e => e.Expirydate)
                .HasColumnType("DATE")  // تحديد النوع في قاعدة البيانات
                .HasColumnName("EXPIRYDATE");

            entity.Property(e => e.Paymentdate)
                .HasDefaultValueSql("SYSDATE")  // تحديد القيمة الافتراضية في قاعدة البيانات
                .HasColumnType("DATE")  // تحديد النوع في قاعدة البيانات
                .HasColumnName("PAYMENTDATE");

            entity.Property(e => e.Profileid)
                .HasColumnType("NUMBER(38)")  // تحديد النوع في قاعدة البيانات
                .HasColumnName("PROFILEID");

            entity.HasOne(d => d.Profile)  // الربط مع جدول Profile
                .WithMany(p => p.Payments)  // العلاقة بين Profile و Payment
                .HasForeignKey(d => d.Profileid)  // تحديد المفتاح الأجنبي
                .HasConstraintName("FK_PROFILE_PAYMENT");  // تحديد اسم القيد
        });


     

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Profileid).HasName("SYS_C008427");

            entity.ToTable("PROFILE");

            entity.Property(e => e.Profileid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PROFILEID");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("DATE")
                .HasColumnName("DATE_OF_BIRTH");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Photo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PHOTO");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERPASSWORD");

            entity.HasOne(d => d.Role).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK_ROLE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C008436");

            entity.ToTable("ROLE");

            entity.Property(e => e.Roleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RNAME");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Subscrid).HasName("SYS_C008441");

            entity.ToTable("SUBSCRIPTIONS");

            entity.Property(e => e.Subscrid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SUBSCRID");
            entity.Property(e => e.Countweeks)
                .HasColumnType("NUMBER")
                .HasColumnName("COUNTWEEKS");
            entity.Property(e => e.Nameplan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAMEPLAN");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER")
                .HasColumnName("PRICE");
            entity.Property(e => e.Sidwop)
                .HasColumnType("NUMBER")
                .HasColumnName("SIDWOP");

            entity.HasOne(d => d.SidwopNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.Sidwop)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SUBSC");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Testimoid).HasName("SYS_C008429");

            entity.ToTable("TESTIMONIAL");

            entity.Property(e => e.Testimoid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TESTIMOID");
            entity.Property(e => e.Feedback)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("FEEDBACK");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.Tprofileid)
                .HasColumnType("NUMBER")
                .HasColumnName("TPROFILEID");

            entity.HasOne(d => d.Tprofile).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.Tprofileid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PROFILE");
        });

        modelBuilder.Entity<Typeperson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008450");

            entity.ToTable("TYPEPERSON");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Enddate)
                .HasColumnType("DATE")
                .HasColumnName("ENDDATE");
            entity.Property(e => e.Startdate)
                .HasColumnType("DATE")
                .HasColumnName("STARTDATE");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.Tprofileid)
                .HasColumnType("NUMBER")
                .HasColumnName("TPROFILEID");
            entity.Property(e => e.Tsubscrid)
                .HasColumnType("NUMBER")
                .HasColumnName("TSUBSCRID");

            entity.HasOne(d => d.Tprofile).WithMany(p => p.Typepeople)
                .HasForeignKey(d => d.Tprofileid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FKT_PROFILE");

            entity.HasOne(d => d.Tsubscr).WithMany(p => p.Typepeople)
                .HasForeignKey(d => d.Tsubscrid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FKT_SUBSCR");
        });

        modelBuilder.Entity<Workoutplan>(entity =>
        {
            entity.HasKey(e => e.Idwop).HasName("SYS_C008439");

            entity.ToTable("WORKOUTPLANS");

            entity.Property(e => e.Idwop)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("IDWOP");
            entity.Property(e => e.Day1)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DAY1");
            entity.Property(e => e.Day2)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DAY2");
            entity.Property(e => e.Day3)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DAY3");
            entity.Property(e => e.Day4)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DAY4");
            entity.Property(e => e.Day5)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DAY5");
            entity.Property(e => e.Day6)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DAY6");
            entity.Property(e => e.Day7)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DAY7");
            entity.Property(e => e.Goals)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("GOALS");
            entity.Property(e => e.Numberofweek)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMBEROFWEEK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
