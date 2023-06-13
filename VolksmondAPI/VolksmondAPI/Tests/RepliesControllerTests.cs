using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Refit;
using VolksmondAPI.Controllers;
using VolksmondAPI.Data;
using VolksmondAPI.Models;
using Xunit;

// Unit test class
public class RepliesControllerTests
{
    [Fact]
    public async Task Vote_Should_Return_Correct_Result()
    {
        // Arrange
        var context = new VolksmondAPIContext() { };// Create or mock your DbContext with test data
        var controller = new RepliesController(context); // Create an instance of the controller

        var vote = new ReplyVote
        {
            CitizenId = 1,
            ReplyId = 1,
            Vote = 1
        };

        // Act
        var result = await controller.Vote(1, vote);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is an OkObjectResult
        var response = Assert.IsType<ApiResponse<object>>(okResult.Value); // Check if the value is of the expected type

        dynamic responseObject = response; // Cast the response object to dynamic

        Assert.Equal(vote.Id, responseObject.id); // Verify the vote ID
        Assert.Equal(1, responseObject.score);
    }
}