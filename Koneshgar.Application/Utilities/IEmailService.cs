namespace Koneshgar.Application.Utilities
{
    public interface IEmailService
    {
        Task ConfirmationMailAsync(string link, string email);
        Task ForgetPasswordMailAsync(string link, string email);
    }
}
