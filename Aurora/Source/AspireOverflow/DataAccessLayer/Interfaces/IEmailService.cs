
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces{
    public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
}