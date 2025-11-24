using System.Reflection;
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

// Configurando a camada de acesso e manipulação de dados para o banco de dados.
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!; // estudar depois o null not não entendi muito bem!
    public DbSet<Transaction> Transactions { get; set; } = null!;

    // configuração importante para varredura completa para encontrar quais arquivos estão utilizando o
    // IEntityTypeConfiguration<T> para fazer o mapeamento do entityFramework (fluentMapping);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}