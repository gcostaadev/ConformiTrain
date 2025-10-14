using ConformiTrain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConformiTrain.Persistence
{
    public class ConformiTrainDbContext: DbContext
    {
        public ConformiTrainDbContext(DbContextOptions<ConformiTrainDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Treinamento> Treinamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<HistoricoTreinamento> HistoricoTreinamentos { get; set; }
        public DbSet<ComentariosDoTreinamento> Comentarios { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Treinamento>(e =>
                {
                    e.HasKey(s => s.Id);
                    e.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
                    e.Property(t => t.Descricao).HasMaxLength(1000);
                    e.Property(t => t.CargaHoraria).IsRequired();
                    e.Property(t => t.DataCriacao).IsRequired();
                    e.Property(t => t.Status).IsRequired();
                    
                    e.HasMany(t => t.Comentarios)
                    .WithOne(c => c.Treinamento)
                    .HasForeignKey(c => c.IdTreinamento)
                    .OnDelete(DeleteBehavior.Cascade);
                  
                    e.HasMany(t => t.HistoricoTreinamentos)
                     .WithOne(h => h.Treinamento)
                     .HasForeignKey(h => h.TreinamentoId)
                     .OnDelete(DeleteBehavior.Cascade);
                });

            builder.Entity<Funcionario>(e =>
            {
                e.HasKey(f => f.Id);

                e.Property(f => f.NomeCompleto).IsRequired().HasMaxLength(150);
                e.Property(f => f.Email).IsRequired().HasMaxLength(150);
                e.Property(f => f.Departamento).HasMaxLength(100);
                e.Property(f => f.Cargo).HasMaxLength(100);
                e.Property(f => f.DataContratacao).IsRequired();
                e.Property(f => f.Ativo).IsRequired();

                // Relacionamento 1:N - Funcionario -> Comentários
                e.HasMany(f => f.Comentarios)
                 .WithOne(c => c.Funcionario)
                 .HasForeignKey(c => c.IdFuncionario)
                 .OnDelete(DeleteBehavior.Restrict);

                // Relacionamento 1:N - Funcionario -> Histórico
                e.HasMany(f => f.HistoricoTreinamentos)
                 .WithOne(h => h.Funcionario)
                 .HasForeignKey(h => h.FuncionarioId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ComentariosDoTreinamento>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Conteudo).IsRequired().HasMaxLength(1000);
                e.Property(c => c.IdTreinamento).IsRequired();
                e.Property(c => c.IdFuncionario).IsRequired();
            });

            builder.Entity<HistoricoTreinamento>(e =>
            {
                e.HasKey(h => h.Id);

                e.Property(h => h.DataParticipacao).IsRequired();
                e.Property(h => h.Concluido).IsRequired();
                e.Property(h => h.DataInicioTreinamento).IsRequired(false);
                e.Property(h => h.DataFimTreinamento).IsRequired(false);

                e.HasOne(h => h.Treinamento)
                 .WithMany(t => t.HistoricoTreinamentos)
                 .HasForeignKey(h => h.TreinamentoId);

                e.HasOne(h => h.Funcionario)
                 .WithMany(f => f.HistoricoTreinamentos)
                 .HasForeignKey(h => h.FuncionarioId);
            });




            base.OnModelCreating(builder);
        }
    }
}
