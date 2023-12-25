using Application.UserCQRS.Commands;
using Azure.Core;
using Domain.Entities;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;

namespace WebAPI.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Headers.TryGetValue("Authorization", out var jwtToken))
            {
                //Console.WriteLine($"Authorization Header (JWT): {jwt}");
                string jwt = jwtToken.ToString();

                var actualJWT = jwt.Split(" ").Last();

                if (!string.IsNullOrEmpty(actualJWT))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.ReadToken(actualJWT) as JwtSecurityToken;

                    if (token != null)
                    {
                        // Extract the role claim from the decoded JWT
                        var roleClaim = token.Claims.FirstOrDefault(c => c.Type == "role");

                        if (roleClaim.Value == "Seller")
                        {
                            await _next(context); // go to controller

                        }

                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsync("Only Sellers can access this api! From Custom Middleware");
                            return;
                        }
                    }
                }

            }

            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization Header not found from Custom Middleware");
                return; // The return statement ensures that the pipeline doesn't continue

            }



        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
