using Microsoft.EntityFrameworkCore;

namespace Api.Imjay.Net.Database.Repositories
{
    public class AppRepository : GenericRepository
    {
        public AppRepository(DbContext context) : base(context)
        {
        }
    }
}