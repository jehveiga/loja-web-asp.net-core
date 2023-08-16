using LojaGeek.App.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LojaGeek.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copia os dados do RegisterViewModel para o IdentityUser
                var user = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };

                // Armazena os dados do usuário na tabela AspNetUsers
                var result = await _userManager.CreateAsync(user, model.Password);

                //Se o usuário foi criado com sucess, faz o login do usuário
                // usando o serviço SignInManager e redireciona para o Método Action Index
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), "home");
                }

                // Se houver erros então inclui no ModelState
                // que será exibido pega tag helper summary na validação
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("login-user")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login-user")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index), "home");

                ModelState.AddModelError(string.Empty, "Login Inválido");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "home");
        }

        [HttpGet]
        [Route("Identity/Account/Login")]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
