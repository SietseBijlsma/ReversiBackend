using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReversiMvcApp.Controllers;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {

        private readonly PlayerController _playerController;
        private readonly GoogleAuthenticator _googleAuthenticator;
        private readonly UserManager<IdentityUser> _userManager;

        public ForgotPasswordConfirmation(
            PlayerController playerController,
            GoogleAuthenticator googleAuthenticator,
            UserManager<IdentityUser> userManager
        )
        {
            _playerController = playerController;
            _googleAuthenticator = googleAuthenticator;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string Key { get; set; }
        }

        public IActionResult OnGet(string email)
        {
            Input = new InputModel()
            {
                Email = email
            };
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(string email)
        {
            Input.Email = email;
            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = Input.Key.Replace(" ", string.Empty).Replace("-", string.Empty);

            var player = _playerController.GetPlayer(user.Id);
            var isValid = _googleAuthenticator.ValidateAuthenticatorCode(player, authenticatorCode);

            if (!isValid)
            {
                ModelState.AddModelError("TwoFactorCode", "Invalid authenticator code.");
                return Page();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            return RedirectToPage("./ResetPassword", new { code = code, email = Input.Email });
        }
    }
}
