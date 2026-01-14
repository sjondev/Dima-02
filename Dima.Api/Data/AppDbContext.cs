using System.Reflection;
using Dima.Api.Models;
using Dima.Core.Models;
using Dima.Core.Models.Reports;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

// Configurando a camada de acesso e manipulação de dados para o banco de dados.
public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<
        User,
        IdentityRole<long>,
        long,
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>
    >(options)
{
    
    /*  Entrada e Saida do dinheiro */
    public DbSet<Category> Categories { get; set; } = null!; // estudar depois o null not não entendi muito bem!
    public DbSet<Transaction> Transactions { get; set; } = null!;

    
    /* REPORTS - Relatorios */
    public DbSet<IncomesAndExpenses> IncomesAndExpenses { get; set; } = null!;
    public DbSet<IncomesByCategory> IncomesByCategory { get; set; } = null!;
    public DbSet<ExpensesByCategory> ExpensesByCategory { get; set; } = null!;
    
    

    // configuração importante para varredura completa para encontrar quais arquivos estão utilizando o
    // IEntityTypeConfiguration<T> para fazer o mapeamento do entityFramework (fluentMapping);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<IncomesAndExpenses>().HasNoKey().ToView("vwGetIncomesAndExpenses");
        modelBuilder.Entity<IncomesByCategory>().HasNoKey().ToView("vwGetIncomesByCategory");
        modelBuilder.Entity<ExpensesByCategory>().HasNoKey().ToView("vwGetExpensesByCategory");
    }
}