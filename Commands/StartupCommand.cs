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
            //var viewModel = new SimpleDataConnectorViewModel();
            //var view = new SimpleDataConnectorView(viewModel);
            //view.ShowDialog();



            // CREATE
            using (var context = new SheetsDataContext())
            {
                var sheet = new Sheet
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
            }

            // READ
            using (var context = new SheetsDataContext())
            {
                var sheet = context.Sheets.FirstOrDefault(s => s.SheetNumber == "A2.01");
                if (sheet != null)
                {
                    Debug.WriteLine($"Sheet found: {sheet.SheetNames}, {sheet.SheetNumber}");
                }
            }

            // UPDATE
            using (var context = new SheetsDataContext())
            {
                var sheet = context.Sheets.FirstOrDefault(s => s.SheetNumber == "A2.01");
                if (sheet != null)
                {
                    sheet.AppearsInSheetList = 1;
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