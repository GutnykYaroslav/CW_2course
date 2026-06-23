using CW_2course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CW_2course.Services
{
    public class PdfExportService
    {
        public void Export(IEnumerable<TaskModel> tasksToExport, string filePath)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument doc = new FlowDocument
                {
                    PagePadding = new Thickness(50),
                    FontFamily = new FontFamily("Segoe UI")
                };

                doc.Blocks.Add(new Paragraph(new Run("Звіт: Чек-ліст завдань")) { FontSize = 24, FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 20) });

                foreach (var task in tasksToExport)
                {
                    string status = task.IsCompleted ? "☑ ВИКОНАНО" : "☐ НЕ ВИКОНАНО";
                    string text = $"{status} | {task.Title}\nДедлайн: {task.Deadline:dd.MM.yyyy} | Пріоритет: {task.Priority} | Складність: {task.Complexity}";

                    var p = new Paragraph(new Run(text)) { Margin = new Thickness(0, 0, 0, 15) };
                    if (task.IsOverdue) p.Foreground = Brushes.Red;

                    doc.Blocks.Add(p);
                }

                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "Експорт Чек-ліста");
            }
        }
    }
}
