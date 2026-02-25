using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCoursePeriodsNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursePeriods_Courses_CourseId",
                table: "CoursePeriods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Teachers",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Subcategories",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subcategories",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Students",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CourseSessions",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseSessions",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CourseSessionAttendances",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseSessionAttendances",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Courses",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriods",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriods",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriodReviews",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriodReviews",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriodResources",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriodResources",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriodEnrollments",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriodEnrollments",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Classrooms",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Classrooms",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Categories",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Campuses",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campuses",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Name",
                table: "Classrooms",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePeriods_Courses_CourseId",
                table: "CoursePeriods",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursePeriods_Courses_CourseId",
                table: "CoursePeriods");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_Name",
                table: "Classrooms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Teachers",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Subcategories",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subcategories",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Students",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CourseSessions",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseSessions",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CourseSessionAttendances",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseSessionAttendances",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Courses",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriods",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriods",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriodReviews",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriodReviews",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriodResources",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriodResources",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "CoursePeriodEnrollments",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePeriodEnrollments",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Classrooms",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Classrooms",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Categories",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Campuses",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campuses",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePeriods_Courses_CourseId",
                table: "CoursePeriods",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
