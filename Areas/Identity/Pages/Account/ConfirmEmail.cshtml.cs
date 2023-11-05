// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CCP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using CCP.Services;

namespace CCP.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<CCPUser> _userManager;
        private readonly EmailService _emailService;
        private readonly ILogger<ConfirmEmailModel> _logger;

        public ConfirmEmailModel(UserManager<CCPUser> userManager, EmailService emailService, ILogger<ConfirmEmailModel> logger)
        {
            _userManager = userManager;
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            _logger.LogInformation("User ID: {UserId}, Confirmation Code: {Code}", userId, code);
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            // Confirm the email using Identity
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // Email confirmation was successful
                StatusMessage = "Thank you for confirming your email.";
            }
            else
            {
                // Email confirmation failed
                StatusMessage = "Error confirming your email.";
            }

            // Generate the confirmation link
            string confirmationLink = Url.Page("/Account/ConfirmEmail",pageHandler: null,
                values: new { userId = user.Id, code }, protocol: Request.Scheme);


            // Send the confirmation email
            await _emailService.SendConfirmationEmail(user.Email, confirmationLink);
            return Page();
        }
    }
}
