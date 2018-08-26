using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneRosterProviderDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicSessions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    SchoolYear = table.Column<string>(nullable: false),
                    ParentAcademicSessionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSessions_AcademicSessions_ParentAcademicSessionId",
                        column: x => x.ParentAcademicSessionId,
                        principalTable: "AcademicSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Demographics",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    AmericanIndianOrAlaskaNative = table.Column<bool>(nullable: false),
                    Asian = table.Column<bool>(nullable: false),
                    BlackOrAfricanAmerican = table.Column<bool>(nullable: false),
                    NativeHawaiianOrOtherPacificIslander = table.Column<bool>(nullable: false),
                    White = table.Column<bool>(nullable: false),
                    DemographicRaceTwoOrMoreRaces = table.Column<bool>(nullable: false),
                    HispanicOrLatinoEthnicity = table.Column<bool>(nullable: false),
                    CountryOfBirthCode = table.Column<string>(nullable: true),
                    StateOfBirthAbbreviation = table.Column<string>(nullable: true),
                    CityOfBirth = table.Column<string>(nullable: true),
                    PublicSchoolResidenceStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demographics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineItemCategories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItemCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OauthNonces",
                columns: table => new
                {
                    Value = table.Column<string>(nullable: false),
                    UsedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OauthNonces", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "OauthTokens",
                columns: table => new
                {
                    Value = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OauthTokens", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Orgs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    ParentOrgId = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orgs_Orgs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Orgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    _roles = table.Column<string>(nullable: true),
                    Importance = table.Column<int>(nullable: false),
                    VendorResourceId = table.Column<string>(nullable: false),
                    VendorId = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    _userIds = table.Column<string>(nullable: true),
                    EnabledUser = table.Column<bool>(nullable: false),
                    GivenName = table.Column<string>(nullable: false),
                    FamilyName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SMS = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    _grades = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    SchoolYearAcademicSessionId = table.Column<string>(nullable: true),
                    CourseCode = table.Column<string>(nullable: true),
                    OrgId = table.Column<string>(nullable: false),
                    _resources = table.Column<string>(nullable: true),
                    _grades = table.Column<string>(nullable: true),
                    _subjectCodes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Orgs_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Orgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_AcademicSessions_SchoolYearAcademicSessionId",
                        column: x => x.SchoolYearAcademicSessionId,
                        principalTable: "AcademicSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAgents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    SubjectUserId = table.Column<string>(nullable: true),
                    AgentUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAgents_Users_AgentUserId",
                        column: x => x.AgentUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAgents_Users_SubjectUserId",
                        column: x => x.SubjectUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserOrgs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    OrgId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOrgs_Orgs_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Orgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserOrgs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Klasses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    ClassCode = table.Column<string>(nullable: true),
                    ClassType = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    CourseId = table.Column<string>(nullable: true),
                    SchoolOrgId = table.Column<string>(nullable: true),
                    _grades = table.Column<string>(nullable: true),
                    _subjectCodes = table.Column<string>(nullable: true),
                    _periods = table.Column<string>(nullable: true),
                    _resources = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klasses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Klasses_Orgs_SchoolOrgId",
                        column: x => x.SchoolOrgId,
                        principalTable: "Orgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Primary = table.Column<bool>(nullable: true),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    KlassId = table.Column<string>(nullable: false),
                    SchoolOrgId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Klasses_KlassId",
                        column: x => x.KlassId,
                        principalTable: "Klasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Orgs_SchoolOrgId",
                        column: x => x.SchoolOrgId,
                        principalTable: "Orgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlassAcademicSessions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    KlassId = table.Column<string>(nullable: true),
                    AcademicSessionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlassAcademicSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KlassAcademicSessions_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalTable: "AcademicSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KlassAcademicSessions_Klasses_KlassId",
                        column: x => x.KlassId,
                        principalTable: "Klasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AssignDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    KlassId = table.Column<string>(nullable: false),
                    LineItemCategoryId = table.Column<string>(nullable: false),
                    AcademicSessionId = table.Column<string>(nullable: false),
                    ResultValueMin = table.Column<float>(nullable: false),
                    ResultValueMax = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItems_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalTable: "AcademicSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_Klasses_KlassId",
                        column: x => x.KlassId,
                        principalTable: "Klasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_LineItemCategories_LineItemCategoryId",
                        column: x => x.LineItemCategoryId,
                        principalTable: "LineItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Metadata = table.Column<string>(nullable: true),
                    LineItemId = table.Column<string>(nullable: false),
                    StudentUserId = table.Column<string>(nullable: false),
                    ScoreStatus = table.Column<int>(nullable: false),
                    Score = table.Column<float>(nullable: false),
                    ScoreDate = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_LineItems_LineItemId",
                        column: x => x.LineItemId,
                        principalTable: "LineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_ParentAcademicSessionId",
                table: "AcademicSessions",
                column: "ParentAcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OrgId",
                table: "Courses",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SchoolYearAcademicSessionId",
                table: "Courses",
                column: "SchoolYearAcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_KlassId",
                table: "Enrollments",
                column: "KlassId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SchoolOrgId",
                table: "Enrollments",
                column: "SchoolOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KlassAcademicSessions_AcademicSessionId",
                table: "KlassAcademicSessions",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_KlassAcademicSessions_KlassId",
                table: "KlassAcademicSessions",
                column: "KlassId");

            migrationBuilder.CreateIndex(
                name: "IX_Klasses_CourseId",
                table: "Klasses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Klasses_SchoolOrgId",
                table: "Klasses",
                column: "SchoolOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_AcademicSessionId",
                table: "LineItems",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_KlassId",
                table: "LineItems",
                column: "KlassId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_LineItemCategoryId",
                table: "LineItems",
                column: "LineItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orgs_ParentId",
                table: "Orgs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_LineItemId",
                table: "Results",
                column: "LineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_StudentUserId",
                table: "Results",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgents_AgentUserId",
                table: "UserAgents",
                column: "AgentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgents_SubjectUserId",
                table: "UserAgents",
                column: "SubjectUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrgs_OrgId",
                table: "UserOrgs",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrgs_UserId",
                table: "UserOrgs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Demographics");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "KlassAcademicSessions");

            migrationBuilder.DropTable(
                name: "OauthNonces");

            migrationBuilder.DropTable(
                name: "OauthTokens");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "UserAgents");

            migrationBuilder.DropTable(
                name: "UserOrgs");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Klasses");

            migrationBuilder.DropTable(
                name: "LineItemCategories");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Orgs");

            migrationBuilder.DropTable(
                name: "AcademicSessions");
        }
    }
}
