using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab_2
{
    /// <summary>
    /// Логика взаимодействия для ChangedDataWindow.xaml
    /// </summary>
    public partial class ChangedDataWindow : Window
    {
        private ObservableCollection<ChangedData> changedData;

        public ChangedDataWindow(ObservableCollection<ChangedData> Table)
        {
            InitializeComponent();
            changedData = Table;
            ShowChangedData();
            counterLabel.Content = $"Updated fields: {changedData.Count}";
        }


        private void ShowChangedData()
        {
            ObservableCollection<Data> oldData = new ObservableCollection<Data>();
            ObservableCollection<Data> newData = new ObservableCollection<Data>();

            for (int i = 0; i < changedData.Count; i++)
            {
                oldData.Add(changedData[i].OldData);
                newData.Add(changedData[i].NewData);
            }


            dataGridBefore.ItemsSource = oldData;

            dataGridAfter.ItemsSource = newData;
        }
    }
}
