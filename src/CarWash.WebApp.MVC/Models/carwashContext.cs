using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CarWash.WebApp.MVC.Models
{
    public partial class carwashContext : DbContext
    {
        public carwashContext()
        {
        }

        public carwashContext(DbContextOptions<carwashContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<Lavagen> Lavagens { get; set; }
        public virtual DbSet<TipoLavagem> TipoLavagems { get; set; }
        public virtual DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=carwash;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(e => e.Cpf)
                    .HasName("PK__Funciona__C1F89730308B0C3A");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CPF");

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("date")
                    .HasColumnName("DATA_NASCIMENTO");

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ENDERECO");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOME");
            });

            modelBuilder.Entity<Lavagen>(entity =>
            {
                entity.HasKey(e => new { e.Cpf, e.Placa, e.DataLavagem })
                    .HasName("PK_teste_mult");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CPF");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("PLACA");

                entity.Property(e => e.DataLavagem)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_LAVAGEM");

                entity.Property(e => e.CodTipoLavagem).HasColumnName("COD_TIPO_LAVAGEM");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("VALOR");

                entity.HasOne(d => d.CodTipoLavagemNavigation)
                    .WithMany(p => p.Lavagens)
                    .HasForeignKey(d => d.CodTipoLavagem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COD_TIPO_LAVAGEM");

                entity.HasOne(d => d.CpfNavigation)
                    .WithMany(p => p.Lavagens)
                    .HasForeignKey(d => d.Cpf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CPF");

                entity.HasOne(d => d.PlacaNavigation)
                    .WithMany(p => p.Lavagens)
                    .HasForeignKey(d => d.Placa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PLACA");
            });

            modelBuilder.Entity<TipoLavagem>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__TipoLava__CC87E12737F9F671");

                entity.ToTable("TipoLavagem");

                entity.Property(e => e.Codigo)
                    .ValueGeneratedNever()
                    .HasColumnName("CODIGO");

                entity.Property(e => e.NomeLavagem)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOME_LAVAGEM");
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(e => e.Placa)
                    .HasName("PK__Veiculos__E441AE01C23A481B");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("PLACA");

                entity.Property(e => e.Ano).HasColumnName("ANO");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MARCA");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MODELO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
