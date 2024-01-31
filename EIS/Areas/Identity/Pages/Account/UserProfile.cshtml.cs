using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EIS.Models;

namespace EIS.Areas.Identity.Pages.Account
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public UserProfileModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public void OnGet()
        {
        }
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            // all lines of code below are working just with the first creation of the cookie with the first login. but if rerun the app again, they all return null if redirect here directly without logIn.

            string? userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            Claim v = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            ApplicationUser _user = await _userManager.GetUserAsync(HttpContext.User);

            string? cookieValueFromReq = Request.Cookies["RememberMeWebTIP"];

            // this is for normal login without remember me functionality
            //AppUser user = await userManager.GetUserAsync(User);
            return Page();// View(/*user*/);
        }
    }
}
