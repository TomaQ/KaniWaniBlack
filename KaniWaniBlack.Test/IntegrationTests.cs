using KaniWaniBlack.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Xunit;

namespace KaniWaniBlack.Test
{
    public class IntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private KaniWaniBlackContext _context;

        public IntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

            var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlServer()
                    .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<KaniWaniBlackContext>();

            //TODO: remove string
            builder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=KaniWaniBlack; user id=KaniWaniUser;password=waniisakani1337;Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider);

            _context = new KaniWaniBlackContext(builder.Options);
        }

        [Fact]
        public void Test()
        {
            User u = new User
            {
                Username = "TestUsertest",
                PasswordSalt = "s",
                PasswordHash = "h",
                LastLoginDate = DateTime.UtcNow,
                LastApplicationUsed = "this",
                LoginAttempts = 0,
                LastLoginAttempt = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow
            };

            _context.User.Add(u);
            _context.SaveChanges();

            string username = _context.User.FirstAsync().Result.Username;
            Assert.True(username == "TestUse");

            _context.User.Remove(u);
            _context.SaveChanges();
        }
    }
}