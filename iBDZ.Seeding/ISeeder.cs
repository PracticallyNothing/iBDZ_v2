using System;
using System.Threading.Tasks;

namespace iBDZ.Seeding
{
    public interface ISeeder
    {
		Task SeedAsync(IServiceProvider serviceProvider);
    }
}
