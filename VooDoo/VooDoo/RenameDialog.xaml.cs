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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RenameDialog : Window
    {
        public RenameDialog(string name, Action<string> a)
        {
            InitializeComponent();
            DataContext = new RenameViewModel(a);

            textBox1.Text = name;
            textBox1.Focus();
            textBox1.SelectAll();
        }
    }
}
