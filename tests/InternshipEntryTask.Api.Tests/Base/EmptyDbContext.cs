using InternshipEntryTask.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InternshipEntryTask.Api.IntegrationTests.Base;

public class EmptyDbContext : ApplicationDbContext
{
    public EmptyDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
