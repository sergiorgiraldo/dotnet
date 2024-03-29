using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore 
{
    public class User
    {    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var password = context.Request.Query["password"];

                if (string.IsNullOrWhiteSpace(password))
                    password = "123456789";

                var usr = new User();
                var hasher = new PasswordHasher<User>();
                var hashedPassword = hasher.HashPassword(usr, password);

                var isPasswordMatch = hasher.VerifyHashedPassword(usr, hashedPassword, password);

                return context.Response.WriteAsync($"Append ?password at url to test your own password hashing.\nPassword : {password} => Hashed : {hashedPassword} \nPassword Matched : {isPasswordMatch}");
            });
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
