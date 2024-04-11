using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToolWindow
{
    public partial class NameInput : UserControl
    {
        public string InputText { get; private set; }
        public bool DialogResult { get; private set; }
        public event EventHandler<bool?> DialogResultChanged;
        public NameInput()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResultChanged?.Invoke(this, true);
            InputText = InputTextBox.Text;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResultChanged?.Invoke(this, false);
            this.DialogResult = false;
        }
    }
}
