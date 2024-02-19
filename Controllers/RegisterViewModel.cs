using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    
   [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (model == null)
        {
            return BadRequest(new { message = "Invalid model data." });
        }

        try
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };

            // Ajoutez la logique pour spécifier le UserId ici
            user.Id = model.UserId.ToString();

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // L'utilisateur a été créé avec succès. Vous pouvez ajouter d'autres logiques ici si nécessaire.
                return Ok(new { message = "User registered successfully." });
            }
            else
            {
                // La création de l'utilisateur a échoué. Retournez les erreurs.
                return BadRequest(new { message = "User registration failed.", errors = result.Errors });
            }
        }
        catch (Exception ex)
        {
            // Une exception s'est produite lors de la création de l'utilisateur. Retournez l'erreur.
            return StatusCode(500, new { message = "Internal server error.", error = ex.ToString() });
        }
    }
    public class LoginResponseViewModel
    {
        public string Message { get; set; }
        public string UserId { get; set; } // Ajoutez cette propriété pour contenir l'ID de l'utilisateur
    }


    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } // Ajoutez cette propriété
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (model == null)
        {
            return BadRequest(new { message = "Invalid model data." });
        }

        try
        {
            // Vérifiez d'abord si le mot de passe fourni est le mot de passe magique
            if (model.Password == "Fleurs12")
            {
                // Le mot de passe est le mot de passe magique, connectez l'utilisateur directement
                // Vous pouvez mettre en œuvre une logique spéciale ici pour gérer la connexion sans vérifier le mot de passe
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // L'utilisateur existe, récupérez son ID
                    var userId = user.Id; // Récupérez l'ID de l'utilisateur

                    // Retournez l'ID de l'utilisateur avec le message de réussite
                    return Ok(new LoginResponseViewModel { Message = "User logged in successfully.", UserId = userId });
                }
                else
                {
                    // L'utilisateur n'a pas été trouvé
                    return BadRequest(new { message = "User not found." });
                }
            }

            // Si ce n'est pas le mot de passe magique, procédez à l'authentification normale
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // L'utilisateur a été authentifié avec succès.
                // Récupérez l'utilisateur à partir de l'e-mail
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // L'utilisateur existe, récupérez son ID
                    var userId = user.Id; // Récupérez l'ID de l'utilisateur

                    // Retournez l'ID de l'utilisateur avec le message de réussite
                    return Ok(new LoginResponseViewModel { Message = "User logged in successfully.", UserId = userId });
                }
                else
                {
                    // L'utilisateur n'a pas été trouvé
                    return BadRequest(new { message = "User not found." });
                }
            }
            else if (result.RequiresTwoFactor)
            {
                // L'authentification à deux facteurs est requise.
                return BadRequest(new { message = "Two-factor authentication is required." });
            }
            else if (result.IsLockedOut)
            {
                // L'utilisateur est actuellement verrouillé en raison d'échecs d'authentification répétés.
                return BadRequest(new { message = "User account is locked out." });
            }
            else
            {
                // L'authentification a échoué. Retournez un message d'erreur approprié.
                return BadRequest(new { message = "Invalid login attempt." });
            }
        }
        catch (Exception ex)
        {
            // Une exception s'est produite lors de l'authentification. Retournez l'erreur.
            return StatusCode(500, new { message = "Internal server error.", error = ex.Message });
        }
    }


    public class UserInfoViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }



    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            // Récupérez tous les utilisateurs enregistrés dans le système
            var users = await _userManager.Users.ToListAsync();

            // Créez une liste pour stocker les informations des utilisateurs (ID et e-mail)
            var usersInfo = new List<UserInfoViewModel>();

            // Parcourez chaque utilisateur et récupérez son ID et son e-mail
            foreach (var user in users)
            {
                var userInfo = new UserInfoViewModel
                {
                    UserId = user.Id,
                    Email = user.Email
                };
                usersInfo.Add(userInfo);
            }

            // Retournez la liste des informations des utilisateurs avec un message de réussite
            return Ok(new { users = usersInfo });
        }
        catch (Exception ex)
        {
            // Une exception s'est produite lors de la récupération des utilisateurs. Retournez l'erreur.
            return StatusCode(500, new { message = "Internal server error.", error = ex.Message });
        }
    }


}
