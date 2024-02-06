using System;
using System.Threading.Tasks;
using CCP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CCP.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<CCPUser> _userManager;
        private readonly SignInManager<CCPUser> _signInManager;

        public EmailModel(
            UserManager<CCPUser> userManager,
            SignInManager<CCPUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string NewEmail { get; set; }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var emailChangeResult = await _userManager.SetEmailAsync(user, NewEmail);
            if (!emailChangeResult.Succeeded)
            {
                foreach (var error in emailChangeResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            // Optionally, you can send email confirmation here.

            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = "Email updated successfully.";
            return RedirectToPage();
        }
    }
}
