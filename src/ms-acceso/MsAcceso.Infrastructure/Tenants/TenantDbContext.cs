using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Auditorias;
using MsAcceso.Domain.Root.Categorias;
using MsAcceso.Domain.Root.Ciudadanos;
using MsAcceso.Domain.Root.DetalleProductos;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Pasaportes;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Domain.Root.ProductoProductoCategorias;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Root.Resenias;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Infrastructure.Tenants
{


    public class TenantDbContext : DbContext, IUnitOfWorkTenant
    {
        // This context is for looking up the tenant when a request comes in.
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<MenuOpcion> MenuOpcions { get; set; }
        public DbSet<Opcion> Opcions { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<RolPermisoOpcion> RolPermisoOpcions { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<PersonaNatural> PersonasNaturales { get; set; }
        public DbSet<PersonaJuridica> PersonasJuridicas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleProducto> DetalleProducto { get; set; }
        public DbSet<Resenia> Resenia { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<ProductoCategoria> ProductoCategoria { get; set; }
        public DbSet<Ciudadano> Ciudadanos { get; set; }
        public DbSet<Pasaporte> Paraportes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(TenantDbContext).Assembly);
            base.OnModelCreating(builder);

        }

        public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
        )
        {
            try
            {

                var result = await base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("La excepcion por concurrencia se disparo", ex);
            }
        }
    }
}
