using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using VooDoo.ViewModel;

namespace VooDoo
{
    /// <summary>
    /// Interaction logic for NotesDialog.xaml
    /// </summary>
    public partial class NotesDialog : Window
    {
        public NotesDialog(int index, string s, Action<int, String> a)
        {
            InitializeComponent();

            textBox.Text = s;
            textBox.SelectAll();

            DataContext = new NotesViewModel(index, a);
        }
    }
}
