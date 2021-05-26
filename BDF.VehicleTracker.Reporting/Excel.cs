using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ClosedXML.Excel;
using System.IO;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;

namespace BDF.Utilities.Reporting
{
    public static class Excel
    {
        public static void Export(string filename, string[,] data)
        {
            try
            {
                IXLWorkbook xlWB = new XLWorkbook();
                IXLWorksheet xlWS = xlWB.AddWorksheet("Vehicles");
                int rows = data.GetLength(0);
                int cols = data.GetLength(1);

                // Must have write permissions to the path folder
                PdfWriter writer = new PdfWriter(filename.Substring(0, filename.Length - 5) + ".pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph header = new Paragraph("Vehicles")
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(20);

                document.Add(header);

                Paragraph subheader = new Paragraph("Information")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(15);
                document.Add(subheader);

                Table table = new Table(cols, false);

                for (int iRow = 1; iRow <= rows; iRow++)
                {
                    for (int iCol = 1; iCol <= cols; iCol++)
                    {
                        xlWS.Cell(iRow, iCol).Value = data[iRow - 1, iCol - 1];
                        xlWS.Cell(iRow, iCol).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        Cell cell = new Cell(1, 1);
                        cell.Add(new Paragraph(data[iRow - 1, iCol - 1]));

                        if (iRow == 1)
                        {
                            xlWS.Cell(iRow, iCol).Style.Font.Bold = true;
                            cell.SetBackgroundColor(ColorConstants.GRAY);
                            cell.SetTextAlignment(TextAlignment.CENTER);
                            
                        }
                        else
                        {
                            xlWS.Cell(iRow, iCol).Style.Font.Bold = false;
                            cell.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

                            if (iRow % 2 == 0)
                            {
                                xlWS.Cell(iRow, iCol).Style.Fill.SetBackgroundColor(XLColor.Cyan);
                                cell.SetBackgroundColor(ColorConstants.CYAN);
                            }
                           
                        }
                        table.AddCell(cell);
                     
                    }
                }

                xlWS.Columns().AdjustToContents();

                IXLRange range = xlWS.Range(xlWS.Cell(1, 1).Address, xlWS.Cell(rows, cols).Address);

                range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                xlWB.SaveAs(filename);

                document.Add(table);
                document.Close();


            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
