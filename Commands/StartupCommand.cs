using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using SimpleDataConnector.ViewModels;
using SimpleDataConnector.Views;
using SimpleDataConnector.Models;
using SimpleDataConnector.Data;
using System.Diagnostics;

namespace SimpleDataConnector.Commands
{
    /// <summary>
    ///     External command entry point invoked from the Revit interface
    /// </summary>
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class StartupCommand : ExternalCommand
    {

        public override void Execute()
        {
            //Basic_CRUD();

            bool isRemoteDatabaseConnected = IsRemoteDatabaseConnected();

            Console.ReadLine();

            

            Console.ReadLine();

        }

        private bool IsRemoteDatabaseConnected()
        {
            using (var context = new SheetsDataContext())
            {
                try
                {
                    context.Database.CanConnect();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }


        public void Basic_CRUD()
        {
            using (var context = new SheetsDataContext())
            {
                // READ:
                var sheet = context.Sheets.FirstOrDefault(s => s.SheetNumber == "S3.01");
                if (sheet != null)
                {
                    Debug.WriteLine("Sheet added " + sheet.SheetNumber + " exists.");
                }
                else
                {
                    // CREATE:
                    sheet = new Sheet
                    {
                        //SurveyID = 1,
                        SheetNames = "Sheet1",
                        SheetNumber = "S3.01",
                        Scale = "1:100",
                        DisciplineName = "Structure",
                        AppearsInSheetList = 1,
                        SheetIssueDate = DateTime.Now,
                        RevisionsOnSheet = "Rev 1"
                    };

                    context.Sheets.Add(sheet);
                    context.SaveChanges();
                    Debug.WriteLine("Sheet added.");
                };
            }     

            // UPDATE
            using (var context = new SheetsDataContext())
            {
                var sheet = context.Sheets.FirstOrDefault(s => s.SheetNumber == "S3.01");
                if (sheet != null)
                {
                    sheet.RevisionsOnSheet = "Rev 2";
                    context.SaveChanges();
                    Debug.WriteLine("Sheet updated.");
                }
            }

            // DELETE
            using (var context = new SheetsDataContext())
            {
                var sheet = context.Sheets.FirstOrDefault(s => s.SheetNumber == "S3.01");
                if (sheet != null)
                {
                    context.Sheets.Remove(sheet);
                    context.SaveChanges();
                    Debug.WriteLine("Sheet deleted.");
                }
            }
        }



    }
}