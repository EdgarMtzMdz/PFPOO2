using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal;

public class UserController : Controller
{
    
    private readonly ApplicationDBContext _context;
     private readonly ILogger<UserController> _logger;
    private readonly UserManager<IdentityUser>_userManager;
    private readonly SignInManager<IdentityUser>_signInManager;

     public UserController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            ApplicationDBContext context)
        {
            _context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registry(RegistryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser() 
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, password: model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

        }

        [AllowAnonymous]
        public IActionResult Login (string mensaje = null)
        {
            if ( mensaje is not null)
            {
                ViewData["mensaje"] = mensaje;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            

            

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password
            , model.Remember, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                this._logger.LogWarning("NOMBRE DE USUARIO O PASSWORD INCORRECTO");
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrecta.");
                return  View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }


        

        public async Task<IActionResult> UserList(string confirmed = null, string remove = null)
        {
            var userList =  await this._context.Users.ToListAsync();
            var userRoleList = await this._context.UserRoles.ToListAsync();

            var userDtoList = userList.GroupJoin(userRoleList, u => u.Id, ur => ur.UserId, 
                (u, ur) => new UserViewModel  {
                    User = u.UserName,
                    Email = u.Email,
                    Confirmed = u.EmailConfirmed,
                    IsAdmin = ur.Any(ur => ur.UserId == u.Id)
                })
                .OrderBy(u => u.User)
                .ToList();


            var modelo = new UserListModel();
            modelo.UserList = userDtoList;
            modelo.MessageConfirmed = confirmed;
            modelo.MessageRemoved = remove;

            return View(modelo);
        }

        [HttpPost]
        // [Authorize(Roles = Constantes.RolAdmin)]
        public  async Task<IActionResult> HacerAdmin(string email)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario is null)
            {
                return NotFound();
            }

            await _userManager.AddToRoleAsync(usuario, MyConstants.RolAdmin);

            return RedirectToAction("List",
                routeValues: new { confirmed = "Rol asignado correctamente a " + email, remove = ""  });
        }

        [HttpPost]
        // [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> RemoverAdmin(string email)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario is null)
            {
                return NotFound();
            }

            await _userManager.RemoveFromRoleAsync(usuario, MyConstants.RolAdmin);

            return RedirectToAction("List",
                routeValues: new { confirmed = "", remove = "Rol removido correctamente a " + email });
        }
}