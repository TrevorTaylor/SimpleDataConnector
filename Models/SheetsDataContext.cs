using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Xml.Linq;


namespace SimpleDataConnector.Models
{

    // Create a class for Database Table named Sheets$
    // This class represents a table in the database.
    [Table("Sheets$")]
    public class Sheet
    {
        [Key]
        [Column("Survey ID")]
        public double SurveyID { get; set; }

        // The column decorator is used to specify the column name in the database.
        // It is not required if the column name is the same as the property name.
        // However, in this case the table name includes a space. (Don't use spaces in your DB schemas!)
        [Column("Sheet Names")]
        public string SheetNames { get; set; }

        [Column("Sheet Number")]
        public string SheetNumber { get; set; }
     
        public string Scale { get; set; }

        [Column("Discipline Name")]
        public string DisciplineName { get; set; }

        [Column("Appears In Sheet List")]
        public double? AppearsInSheetList { get; set; } // Nullable float to allow nulls

        [Column("Sheet Issue Date")]
        public DateTime? SheetIssueDate { get; set; }  // Nullable DateTime to allow nulls

        [Column("Revisions on Sheet")]
        public string RevisionsOnSheet { get; set; }
    }
}

namespace SimpleDataConnector.Data
{
    // Create a class for the Database Context
    // You'll use this class to query and save data to the database.
    public class SheetsDataContext : DbContext
    {
        public DbSet<Models.Sheet> Sheets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Hard-coded connection string:
                //if (!optionsBuilder.IsConfigured)
                //{
                //    optionsBuilder.UseSqlServer("Data Source=PDX-SQL-1-DTECH;Initial Catalog=ZGF_PHD;Integrated Security=True;Persist Security Info=True;TrustServerCertificate=True;");
                //}

                string directory = Directory.GetCurrentDirectory();
                // Use appsettings.json for connection string                
                // Create a file in your project and name it "appsettings.json"
                // In Properties, set "Copy to Output Directory" to "Copy if newer"
                // You can store all of the connection strings for your project in this json file.
                var configuration = new ConfigurationBuilder() // Requires Microsoft.Extensions.Configuration
                    .SetBasePath(Directory.GetCurrentDirectory()) // Requires System.IO & Microsoft.Extensions.Configuration.Json
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SheetsDbConnectionString"));
            }
        }
    }
}
