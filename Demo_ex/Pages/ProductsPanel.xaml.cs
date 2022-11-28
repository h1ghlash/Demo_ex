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

namespace Demo_ex.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPanel.xaml
    /// </summary>
    public partial class ProductsPanel : UserControl
    {
        public ProductsPanel()
        {
            InitializeComponent();
            this.DataContext = this;    
        }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductManufacturer { get; set; }
        public Decimal ProductCost { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductImage { get; set; }

    }
}
