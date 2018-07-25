using Microsoft.EntityFrameworkCore;

namespace Api.Imjay.Net.Database.Repositories
{
    public class AppRepository : GenericRepository, IAppRespository
    {
        public AppRepository(DbContext context) : base(context)
        {
        }
    }
}