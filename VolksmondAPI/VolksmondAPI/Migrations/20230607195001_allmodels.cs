using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolksmondAPI.Migrations
{
    /// <inheritdoc />
    public partial class allmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Referendum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    VotingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VotingEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referendum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referendum_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solution_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferendumVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferendumId = table.Column<int>(type: "int", nullable: false),
                    SolutionId = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferendumVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferendumVote_Referendum_ReferendumId",
                        column: x => x.ReferendumId,
                        principalTable: "Referendum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    SolutionId = table.Column<int>(type: "int", nullable: false),
                    ReplyId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reply_Reply_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Reply",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reply_Solution_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "Solution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolutionVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolutionId = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolutionVote_Solution_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "Solution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplyVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyId = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    Reply = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyVote_Reply_Reply",
                        column: x => x.Reply,
                        principalTable: "Reply",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Referendum_ProblemId",
                table: "Referendum",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferendumVote_ReferendumId",
                table: "ReferendumVote",
                column: "ReferendumId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_ReplyId",
                table: "Reply",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_SolutionId",
                table: "Reply",
                column: "SolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyVote_Reply",
                table: "ReplyVote",
                column: "Reply");

            migrationBuilder.CreateIndex(
                name: "IX_Solution_ProblemId",
                table: "Solution",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_SolutionVote_SolutionId",
                table: "SolutionVote",
                column: "SolutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citizen");

            migrationBuilder.DropTable(
                name: "ReferendumVote");

            migrationBuilder.DropTable(
                name: "ReplyVote");

            migrationBuilder.DropTable(
                name: "SolutionVote");

            migrationBuilder.DropTable(
                name: "Referendum");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "Solution");

            migrationBuilder.DropTable(
                name: "Problem");
        }
    }
}
