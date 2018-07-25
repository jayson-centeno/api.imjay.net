using Api.Imjay.Net.Commands;
using System;
using System.Threading.Tasks;

namespace Api.Imjay.Net.Domain.Interface
{
    public interface IContactUsService
    {
        Task<bool> CreateContactUs(CreateContactUsCommand command);
    }
}
