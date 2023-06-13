using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolksmondAPI.Controllers;
using VolksmondAPI.Data;
using VolksmondAPI.Models;
using Xunit;

namespace VolksmondAPI.Tests.Repository
{
    public class RepliesControllerTests
    {
        private async Task<VolksmondAPIContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<VolksmondAPIContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new VolksmondAPIContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Reply.CountAsync() <= 0 )
            {
                for (int i = 1; i < 10; i++)
                {
                    databaseContext.Reply.Add(
                        new Reply()
                        {
                            Id = i,
                            CitizenId = 1,
                            SolutionId = 1,
                        });
                }
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        public async Task RepliesController_Vote_ReturnsScore()
        {
            //Arrange
            var context = await GetDatabaseContext();
            var repliesController = new RepliesController(context);
            var vote = new ReplyVote()
            {
                CitizenId = 1,
                ReplyId = 1,
                Vote = 1
            };

            //Act
            var result = repliesController.Vote(1, vote);
            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }
    }
}
