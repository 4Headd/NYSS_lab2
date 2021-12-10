using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace lab_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path = null;

        private int currentPage = 1;

        private int pageLength = 15;

        private ReadMode readMode = ReadMode.Full;

        private bool isPathChanged;


        private ObservableCollection<Data> Table { get; set; } = new ObservableCollection<Data>();

        private ObservableCollection<ShortData> ShortTable { get; set; } = new ObservableCollection<ShortData>();

        public MainWindow()
        {
            InitializeComponent();
            comboBoxPages.Items.Add("15");
            comboBoxPages.SelectedItem = "15";
            comboBoxPages.Items.Add("20");
            comboBoxPages.Items.Add("50");


        }

        private void DownloadMenuItem_click(object sender, RoutedEventArgs e)
        {

            try
            {
                WebClient client = new WebClient();


                client.DownloadFile("https://bdu.fstec.ru/files/documents/thrlist.xlsx", @"thrlist.xlsx");

                MessageBox.Show("File was downloaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong while downloading a file!\n{ex.Message}");
            }
            
        }

        private void OpenMenuItem_click(object sender, RoutedEventArgs e)
        {
            

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                DefaultExt = "xlsx",

                Filter = "Excel files(*.xlsx)|*.xlsx"
            };


            fileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(fileDialog.FileName))
            {
                isPathChanged = path != fileDialog.FileName.ToString();
            }
            


            if (!string.IsNullOrEmpty(fileDialog.FileName))
            {
                readMode = ReadMode.Full;

                string temp = path;

                path = fileDialog.FileName.ToString();

                try
                {
                    //isPathChanged = true;
                    ShowDataGridStart();
                    ShortTable.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong while opening a file!\n{ex.Message}");
                    path = temp;
                }

                
            }
            




            
        }


        private void SaveMenuItem_click(object sender, RoutedEventArgs e)
        {
            if (Table.Count == 0)
            {
                MessageBox.Show("Choose local data base:\nFile -> Open file");
            }
            else
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = "xlsx",
                    Filter = "Excel files(*.xlsx)|*.xlsx"
                };

                saveFileDialog.ShowDialog();


                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    string savePath = saveFileDialog.FileName;

                    using (XLWorkbook workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.AddWorksheet("Sheet1");


                        worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 5)).Merge().Value = "Общая информация";
                        worksheet.Range(worksheet.Cell(1, 6), worksheet.Cell(1, 8)).Merge().Value = "Последствия";

                        worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 5)).Style.Font.Bold = true;
                        worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 5)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        worksheet.Range(worksheet.Cell(1, 6), worksheet.Cell(1, 8)).Style.Font.Bold = true;
                        worksheet.Range(worksheet.Cell(1, 6), worksheet.Cell(1, 8)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                       worksheet.Cell(2, 1).Value = "Идентификатор УБИ";
                        worksheet.Cell(2, 2).Value = "Наименование УБИ";
                        worksheet.Cell(2, 3).Value = "Описание";
                        worksheet.Cell(2, 4).Value = "Источник угрозы (характеристика и потенциал нарушителя)";
                        worksheet.Cell(2, 5).Value = "Объект воздействия";
                        worksheet.Cell(2, 6).Value = "Нарушение конфиденциальности";
                        worksheet.Cell(2, 7).Value = "Нарушение целостности";
                        worksheet.Cell(2, 8).Value = "Нарушение доступности";

                        for (int row = 3; row <= Table.Count + 2; row++)
                        {

                            worksheet.Cell(row, 1).Value = Table[row - 3].ThreatId;
                            worksheet.Cell(row, 2).Value = Table[row - 3].ThreatName;
                            worksheet.Cell(row, 3).Value = Table[row - 3].ThreatDescription;
                            worksheet.Cell(row, 4).Value = Table[row - 3].ThreatSource;
                            worksheet.Cell(row, 5).Value = Table[row - 3].ThreatTarget;
                            worksheet.Cell(row, 6).Value = Table[row - 3].ConfidentialityViolation ? "1" : "0";
                            worksheet.Cell(row, 7).Value = Table[row - 3].IntegrityViolation ? "1" : "0";
                            worksheet.Cell(row, 8).Value = Table[row - 3].AvailabilityViolation ? "1" : "0";

                        }
                        workbook.SaveAs(savePath);
                    }

                }
            }

        }


        private void ExitMenuItem_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Table.Count == 0)
            {
                MessageBox.Show("Choose local data base:\nFile -> Open file");
            }
            else
            {
                readMode = ReadMode.Full;
                ShowDataGridStart();
            }
            
        }

        private void ShowShortMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Table.Count == 0)
            {
                MessageBox.Show("Choose local data base:\nFile -> Open file");
            }
            else
            {
                readMode = ReadMode.Short;
                ShowDataGridStart();
            }

        }

        private void UpdateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Table.Count == 0)
            {
                MessageBox.Show("Choose local data base:\nFile -> Open file");
            }
            else
            {
                try
                {
                    WebClient client = new WebClient();


                    client.DownloadFile("https://bdu.fstec.ru/files/documents/thrlist.xlsx", @"thrlist.xlsx");

                    MessageBox.Show("File was downloaded successfully!");
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong while downloading a file!");
                }


                var updatedData = TableParser.ParseTable(@"thrlist.xlsx");
                ObservableCollection<ChangedData> changedData = DataComparatorNode.CompareData(Table, updatedData);


                if (changedData.Count == 0)
                {
                    MessageBox.Show("Your data base version is up to date!");
                }
                else
                {
                    MessageBox.Show("Your data base version has been updated!");

                    ChangedDataWindow changedDataWindow = new ChangedDataWindow(changedData);

                    changedDataWindow.Show();

                    Table = updatedData;

                    ShortTable = TableParser.ToShortParseTable(Table);

                    ShowDataGridStart();
                }
            }




        }


        private void Button_PaginateLeft_Click(object sender, RoutedEventArgs e)
        {
            Pagination(PagingMode.Previous);
        }

        private void Button_PaginateRight_Click(object sender, RoutedEventArgs e)
        {
            Pagination(PagingMode.Next);
        }




        private void Pagination(PagingMode pagingMode)
        {
            int counter;
            switch (pagingMode)
            {
                case PagingMode.Previous:
                    if (currentPage > 1)
                    {
                        --currentPage;

                        if (currentPage == 1)
                        {
                            if (readMode == ReadMode.Full)
                            {
                                dataGrid1.ItemsSource = Table.Take(pageLength);
                            }
                            else
                            {
                                dataGrid1.ItemsSource = ShortTable.Take(pageLength);
                            }
                            counter = Table.Take(pageLength).Count();
                            SetPageCounterLabel(counter, Table.Count);
                        }
                        else
                        {
                            if (readMode == ReadMode.Full)
                            {
                                dataGrid1.ItemsSource = Table.Skip((currentPage - 1) * pageLength).Take(pageLength);
                            }
                            else
                            {
                                dataGrid1.ItemsSource = ShortTable.Skip((currentPage - 1) * pageLength).Take(pageLength);
                            }

                            counter = Math.Min(currentPage * pageLength, Table.Count);
                            SetPageCounterLabel(counter, Table.Count);
                        }
                    }

                    button_PaginateLeft.IsEnabled = currentPage != 1;

                    button_PaginateRight.IsEnabled = Table.Count > (currentPage * pageLength);

                    break;
                case PagingMode.Next:

                    if (Table.Count > (currentPage * pageLength))
                    {
                        if (readMode == ReadMode.Full)
                        {
                            dataGrid1.ItemsSource = Table.Skip(currentPage * pageLength).Take(pageLength);
                        }
                        else
                        {
                            dataGrid1.ItemsSource = ShortTable.Skip(currentPage * pageLength).Take(pageLength);
                        }
                        
                        counter = (currentPage * pageLength) + Table.Skip(currentPage * pageLength).Take(pageLength).Count();
                        ++currentPage;

                        SetPageCounterLabel(counter, Table.Count);
                    }


                    if (Table.Count <= (currentPage * pageLength))
                    {
                        button_PaginateRight.IsEnabled = false;
                    }

                    if (currentPage != 1)
                    {
                        button_PaginateLeft.IsEnabled = true;
                    }
                        
                    break;
            }
        }




        private void ShowDataGridStart()
        {
            if (!string.IsNullOrEmpty(path))
            {
                currentPage = 1;

                if (readMode == ReadMode.Full)
                {
                    if (isPathChanged)
                    {
                        Table = TableParser.ParseTable(path);
                        isPathChanged = false;
                    }
                    dataGrid1.ItemsSource = Table.Take(pageLength);
                }
                else
                {
                    if (ShortTable.Count == 0)
                    {
                        ShortTable = TableParser.ToShortParseTable(Table);
                    }

                    dataGrid1.ItemsSource = ShortTable.Take(pageLength);
                }

                button_PaginateRight.IsEnabled = Table.Count > (currentPage * pageLength);


                button_PaginateLeft.IsEnabled = currentPage != 1;


                SetPageCounterLabel(Table.Take(pageLength).Count(), Table.Count);
            }
        }


        private void SetPageCounterLabel(int currentPage, int allPages)
        {
            pageCounterLabel.Content = $"{currentPage} / {allPages}";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageLength = Convert.ToInt32(comboBoxPages.SelectedItem.ToString());
            ShowDataGridStart();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Table = TableParser.ParseTable("thrlist.xlsx");
                path = "thrlist.xlsx";
                ShowDataGridStart();
            }
            catch (Exception)
            {
                MessageBox.Show("Local thrlist.xlsx was not found\nDownload file and open it manually:\n\tFile -> Download\n\tFile -> Open File" +
                    "\nor open existing file manually:" +
                    "\n\tFile -> Open file");
                
            }
        }
    }


    public enum PagingMode
    {
        Previous,
        Next
    }


    public enum ReadMode
    {
        Full,
        Short
    }
}
