using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace lab_2
{
    public class TableParser
    {


        public static ObservableCollection<Data> ParseTable(string path)
        {
            ObservableCollection<Data> data = new ObservableCollection<Data>();

            using (XLWorkbook workbook = new XLWorkbook(path))
            {

                IXLWorksheet worksheet = workbook.Worksheets.Worksheet(1);

                for (int row = 3; !string.IsNullOrEmpty(worksheet.Cell(row, 1).GetString()); row++)
                {
                    Data dataRow = new Data
                    (
                        worksheet.Cell(row, 1).GetValue<int>(),
                        worksheet.Cell(row, 2).GetValue<string>().Replace("\r\n", "\n"),
                        worksheet.Cell(row, 3).GetValue<string>().Replace("\r\n", "\n"),
                        worksheet.Cell(row, 4).GetValue<string>().Replace("\r\n", "\n"),
                        worksheet.Cell(row, 5).GetValue<string>().Replace("\r\n", "\n"),
                        worksheet.Cell(row, 6).GetValue<string>() == "1" ? true : false,
                        worksheet.Cell(row, 7).GetValue<string>() == "1" ? true : false,
                        worksheet.Cell(row, 8).GetValue<string>() == "1" ? true : false
                    );

                    string a = worksheet.Cell(row, 8).GetValue<string>();
                    data.Add(dataRow);
                }
            }

            return data;
        }


        public static ObservableCollection<ShortData> ToShortParseTable(ObservableCollection<Data> table)
        {
            ObservableCollection<ShortData> shortData = new ObservableCollection<ShortData>();

            foreach (Data row in table)
            {
                shortData.Add(new ShortData($"УБИ.{row.ThreatId}", row.ThreatName));
            }

            return shortData;
        }

    }
}
