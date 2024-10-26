using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace FlextStarTubeApi.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Criador> Criadores { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<ItemPlaylist> ItemPlaylists { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Criador>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Criador>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Conteudo>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Conteudo>()
                .HasOne(c => c.Criador)
                .WithMany()
                .HasForeignKey(c => c.CriadorId);

            modelBuilder.Entity<Playlist>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<ItemPlaylist>()
                .HasKey(ip => ip.Id);
            modelBuilder.Entity<ItemPlaylist>()
                .HasOne(ip => ip.Playlist)
                .WithMany()
                .HasForeignKey(ip => ip.PlaylistId);
            modelBuilder.Entity<ItemPlaylist>()
                .HasOne(ip => ip.Conteudo)
                .WithMany()
                .HasForeignKey(ip => ip.ConteudoId);
        }
    }

}
