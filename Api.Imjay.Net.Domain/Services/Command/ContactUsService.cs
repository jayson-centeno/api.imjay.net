using Api.Imjay.Net.Commands;
using Api.Imjay.Net.Database.Repositories;
using Api.Imjay.Net.Domain.Interface;
using Api.Imjay.Net.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Imjay.Net.Domain.Services.Command
{
    public class ContactUsService: BaseService, IContactUsService
    {
        public ContactUsService(IAppRespository appRespository): base(appRespository)
        {
        }

        public async Task<bool> CreateContactUs(CreateContactUsCommand command)
        {
            int result = 0;

            try
            {
                AppRepo.Add(new ContactUs
                {
                    Approved = true,
                    Email = command.Email,
                    IPAddress = command.IPAddress,
                    Message = command.Message,
                    Name = command.Name,
                    DatePosted = command.DatePosted
                });

                result = await AppRepo.SaveAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            return (result >= 1);
        }
    }
}
