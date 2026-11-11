
using MediCare.Data.Entities;
using MediCare.Services.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace MediCare.Services.Implementations;

public class AccountService : IAccountService
{

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterAsync(string fullName, string email, string password, string role)
    {
        ApplicationUser applicationUser = new()
        {
            UserName = email,
            Email = email,
            FullName = fullName
        };

        if (role != "Patient" && role != "Doctor")
        {
            return IdentityResult.Failed(new IdentityError { Description = "Invalid role" });
        }
        var result = await _userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(applicationUser, role);
            await _signInManager.SignInAsync(applicationUser, isPersistent: false);
        }

        return result;
    }

}