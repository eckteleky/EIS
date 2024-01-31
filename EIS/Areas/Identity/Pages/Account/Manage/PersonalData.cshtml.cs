using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using EIS.Models;
using Microsoft.AspNetCore.Http;

namespace EIS.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<ApplicationUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public override bool Equals(object obj)
        {
            return obj is PersonalDataModel model &&
                   EqualityComparer<ILogger<PersonalDataModel>>.Default.Equals(_logger, model._logger);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_logger);
        }

        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.SetInt32("HeaderGroup", 0); //Hide header
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}