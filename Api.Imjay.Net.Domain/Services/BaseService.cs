using Api.Imjay.Net.Database.Repositories;

namespace Api.Imjay.Net.Domain.Services
{
    public class BaseService
    {
        public IAppRespository AppRepo { get; }

        public BaseService(IAppRespository appRespository)
        {
            AppRepo = appRespository;
        }
    }
}