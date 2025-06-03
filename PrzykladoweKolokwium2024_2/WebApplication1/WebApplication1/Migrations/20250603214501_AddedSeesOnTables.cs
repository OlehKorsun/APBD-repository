using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeesOnTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "CJohn", "Doe" },
                    { 2, "CJane", "Doe" },
                    { 3, "CJohn", "Doe" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "EJohn", "Doe" },
                    { 2, "EJane", "Doe" },
                    { 3, "EJohn", "Doe" }
                });

            migrationBuilder.InsertData(
                table: "Pastry",
                columns: new[] { "ID", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Pastry A", 0.99m, "TypeA" },
                    { 2, "Pastry B", 1.59m, "TypeB" },
                    { 3, "Pastry C", 6.91m, "TypeC" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "ID", "AcceptedAt", "ClientID", "Comments", "EmployeeID", "FulfilledAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "CommentA", 2, new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "CommentB", 1, new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "CommentC", 2, new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "CommentD", 3, new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Order_Pastry",
                columns: new[] { "OrderID", "PastryID", "Amount", "Comments" },
                values: new object[,]
                {
                    { 1, 1, 4, "Comment A" },
                    { 1, 3, 2, "Comment B" },
                    { 2, 2, 5, null },
                    { 3, 1, 1, null },
                    { 4, 1, 5, null },
                    { 4, 2, 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Order_Pastry",
                keyColumns: new[] { "OrderID", "PastryID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Order_Pastry",
                keyColumns: new[] { "OrderID", "PastryID" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Order_Pastry",
                keyColumns: new[] { "OrderID", "PastryID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Order_Pastry",
                keyColumns: new[] { "OrderID", "PastryID" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Order_Pastry",
                keyColumns: new[] { "OrderID", "PastryID" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "Order_Pastry",
                keyColumns: new[] { "OrderID", "PastryID" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pastry",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pastry",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pastry",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
