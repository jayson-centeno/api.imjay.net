using Api.Imjay.Net.Database.Context;
using Api.Imjay.Net.Database.Repositories;
using Api.Imjay.Net.Domain.Interface;
using Api.Imjay.Net.Domain.Services.Command;
using Microsoft.Extensions.Configuration;
using StructureMap;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace Api.Imjay.Net.Domain.Registration
{
    public class DIServiceRegistry : Registry
    {
        public IConfiguration Configuration { get; }

        public DIServiceRegistry(IConfiguration configuration)
        {
            Configuration = configuration;

            For<DbConnection>().Use(x => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));
            For<AppDbContext>().Use(x => new AppDbContext(x.GetInstance<DbConnection>()));

            For<IAppRespository>().Use(x => new AppRepository(
                x.GetInstance<AppDbContext>()
            ));

            For<IContactUsService>()
                .Use<ContactUsService>();
        }
    }
}
