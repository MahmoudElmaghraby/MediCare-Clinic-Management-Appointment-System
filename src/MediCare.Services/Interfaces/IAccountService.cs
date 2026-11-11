using Microsoft.AspNetCore.Identity;

namespace MediCare.Services.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(string fullName, string email, string password, string role);
}