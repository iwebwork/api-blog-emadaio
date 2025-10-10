using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Contexts;
using Api.Models;
using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers.Usuarios;

[Route("api/usuarios")]
public class HomeController(IResponseControler responseControler,
    IConfiguration configuration,
    UserManager<Usuario> userManager,
    AppIdentityDbContext appIdentityDbContext,
    SignInManager<Usuario> signInManager) : BaseAutenticateController(responseControler)
{

    private readonly AppIdentityDbContext _appIdentityDbContext = appIdentityDbContext;

    [HttpPost("cadastro")]
    public async Task CadastroAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        var usuario = await userManager.FindByEmailAsync(requestViewModel.Email)
            ?? await userManager.FindByNameAsync(requestViewModel.UserName);

        if (usuario != null)
        {
            responseControler.AddMessageErro($"Já existe um usuario com o mesmo Nome: {requestViewModel.UserName} e/ou Email: {requestViewModel.Email}");
            return;
        }

        try
        {
            var user = new Usuario
            {
                UserName = requestViewModel.UserName,
                Email = requestViewModel.Email,
                PasswordHash = requestViewModel.Password
            };

            var result = await userManager.CreateAsync(user, requestViewModel.Password);

            if (result.Succeeded)
            {
                responseControler.AddMessageSuccesso("Usuario criado com sucesso!");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    responseControler.AddMessageErro(item.Description);
                }
            }

        }
        catch (Exception e)
        {
            responseControler.AddMessageErro(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task LoginAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        try
        {
            if (requestViewModel.Email == null || requestViewModel.Password == null)
            {
                responseControler.AddMessageErro("Email ou senha não foram informados!");
                return;
            }

            var usuario = await userManager.FindByEmailAsync(requestViewModel.Email);
            var result = await signInManager.PasswordSignInAsync(usuario.UserName, requestViewModel.Password,
                 isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration["JWT:Expiration"]));
                var claimsPricipal = await signInManager.CreateUserPrincipalAsync(usuario);

                var token = BuildToken(configuration, expiration: expiration, claimsPrincipal: claimsPricipal);

                var userToken = await _appIdentityDbContext.UsuarioToken
                    .Where(w => w.UsuarioId == usuario.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (userToken == null)
                {
                    userToken = new(token: token,
                        expiration: expiration,
                        usuarioId: usuario.Id);

                    await _appIdentityDbContext.AddAsync(userToken, cancellationToken);
                }
                else
                {
                    userToken.Update(token: token, expiration: expiration);

                    var update = _appIdentityDbContext.Update(userToken);

                }

                await _appIdentityDbContext.SaveChangesAsync(cancellationToken);

                responseControler.IsAuthenticated = true;

                responseControler.SetResponseData(new
                {
                    Usuario = new
                    {
                        Nome = usuario.UserName,
                        AccessToken = userToken.Token
                    }
                });

                responseControler.AddMessageSuccesso("Usuario logado com sucesso!");
            }
            else
            {
                responseControler.AddMessageErro("Usuario ou senha invalidos!");
            }

        }
        catch (Exception ex)
        {
            responseControler.AddMessageErro(ex.Message);
        }
    }

    private string BuildToken(IConfiguration configuration, DateTime expiration, ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = claimsPrincipal.Claims.ToList();

            // tempo de expiração do token
            JwtSecurityToken token = new(
               issuer: configuration["JWT:Issuer"],
               audience: configuration["JWT:Audience"],
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            responseControler.AddMessageErro(ex.Message);
            return "";
        }

    }

}
