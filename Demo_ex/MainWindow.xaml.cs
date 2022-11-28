using Demo_ex.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Demo_ex.Model;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Path = System.IO.Path;

namespace Demo_ex
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ImageFolderPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\resources\"));
        public ObservableCollection<ProductsPanel> ProductsPanelCollection {get;set;}

        public string Sort = "ProductName";

        public string Search = string.Empty;

        public string Filter = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            getManufacturer();
            getProducts(Sort, Filter, Search);
            CmbSort.Items.Add("По возрастанию");
            CmbSort.Items.Add("По убыванию");
        }

        public void getManufacturer()
        {
            DB db = new DB();
            db.openConnection();
            string query = "select Product.ProductManufacturer from Product group by Product.ProductManufacturer";
            SqlCommand command = new SqlCommand(query, db.getConnection());
            var rd = command.ExecuteReader();
            CmbFilter.Items.Add("");
            while(rd.Read())
            {
                CmbFilter.Items.Add(rd.GetString(0));
            }
        }

        public void getProducts(string sort, string filter, string search)
        {
            DB db = new DB();
            db.openConnection();
            string fil = "";
            if(filter != string.Empty)
            {
                fil = "and Product.ProductManufacturer = '" + filter + "' ";
            }

            string src = "";
            if (search != String.Empty)
            {
                src = $"and Product.ProductName LIKE '%{search}%' or Product.ProductManufacturer LIKE '%{search}%' or Product.ProductDescription LIKE '%{search}%' or Product.ProductCost LIKE '%{search}%' ";
            }

            string query = $"Select Product.ProductName, Product.ProductDescription, Product.ProductManufacturer, Product.ProductCost, Product.ProductQuantityInStock, Product.ProductPhoto from Product where Product.ProductName = Product.ProductName {fil}{src} group by Product.ProductName, Product.ProductDescription, Product.ProductManufacturer, Product.ProductCost, Product.ProductQuantityInStock, Product.ProductPhoto order by {sort} ";

            SqlCommand command = new SqlCommand(query, db.getConnection());
            var rd = command.ExecuteReader();
            ProductsPanelCollection = new ObservableCollection<ProductsPanel>();
            ListProduct.ItemsSource = ProductsPanelCollection;
            while(rd.Read())
            {
                var record = (IDataReader)rd;
                var imgSrc = "";
                if(record.GetString(5) == "")
                {
                    imgSrc = ImageFolderPath + "picture.png";
                }
                else
                {
                    imgSrc = ImageFolderPath + rd.GetString(5);
                }

                ProductsPanelCollection.Add(new ProductsPanel()
                {
                    ProductName = record.GetString(0),
                    ProductDescription = record.GetString(1),
                    ProductManufacturer = record.GetString(2),
                    ProductCost = record.GetDecimal(3),
                    ProductQuantity = record.GetInt32(4),
                    ProductImage = imgSrc
                }); 
            }
            db.closeConnection();
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search = TextSearch.Text;
            getProducts(Sort, Filter, Search);
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter = (string)CmbFilter.SelectedItem;
            getProducts(Sort, Filter, Search);
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((string)CmbSort.SelectedItem == "По возрастанию")
            {
                var sort = "Product.ProductCost";
                getProducts(sort, Filter, Search);
            }else if ((string)CmbSort.SelectedItem == "По убыванию")
            {
                var sort = "Product.ProductCost DESC";
                getProducts(sort, Filter, Search);
            }
        }
    }
}
