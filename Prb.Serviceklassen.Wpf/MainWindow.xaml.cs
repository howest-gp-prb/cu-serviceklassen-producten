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
using Prb.Serviceklassen.Core;

namespace Prb.Serviceklassen.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ProductService productService;
        bool isNew;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            productService = new ProductService();
            DefaultSituation();
            PopulateListbox();
            PopulatePackingUnits();
            ClearControls();
            DisplayStats();
        }
        private void DefaultSituation()
        {
            grpProducts.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }
        private void NewEditSituation()
        {
            grpProducts.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }
        private void PopulateListbox()
        {
            lstProducts.ItemsSource = null;
            lstProducts.ItemsSource = productService.Products;
        }
        private void PopulatePackingUnits()
        {
            cmbPackingUnits.Items.Clear();
            cmbPackingUnits.Items.Add(PackingUnits.Piece);
            cmbPackingUnits.Items.Add(PackingUnits.Dozen);
            cmbPackingUnits.Items.Add(PackingUnits.Gross);
        }
        private void ClearControls()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            txtPriceEuro.Text = "";
            txtStock.Text = "";
            lblPriceDollar.Content = "";
            lblStockValueDollar.Content = "";
            lblStockValueEuro.Content = "";
            cmbPackingUnits.SelectedIndex = -1;
        }
        private void DisplayStats()
        {
            lblTotalStockValueEuro.Content = productService.StockValueEuro.ToString("€#,##0.00");
            lblTotalStockValueDollar.Content = productService.StockValueDollar.ToString("€#,##0.00");
        }
        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstProducts.SelectedItem != null)
            {
                PopulateControls((Product)lstProducts.SelectedItem);
            }
        }
        private void PopulateControls(Product product)
        {
            txtCode.Text = product.Code;
            txtDescription.Text = product.Description;
            txtPriceEuro.Text = product.PriceEuro.ToString("€#,##0.00");
            txtStock.Text = product.Stock.ToString();
            lblPriceDollar.Content = product.PriceDollar.ToString("€#,##0.00");
            lblStockValueEuro.Content = product.StockValueEuro.ToString("€#,##0.00");
            lblStockValueDollar.Content = product.StockValueDollar.ToString("€#,##0.00");
            cmbPackingUnits.SelectedItem = product.Packing;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            NewEditSituation();
            isNew = true;
            ClearControls();
            // instructie hieronder om zeker te zijn dat de gebruiker zeker
            // een verpakking selecteert
            cmbPackingUnits.SelectedIndex = 0;
            txtCode.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lstProducts.SelectedItem != null)
            {
                NewEditSituation();
                isNew = false;
                // instructie hieronder noodzakelijk om € uit tekstvak te verwijderen
                txtPriceEuro.Text = ((Product)lstProducts.SelectedItem).PriceEuro.ToString();
                txtCode.Focus();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstProducts.SelectedItem != null)
            {
                if(MessageBox.Show("Ben je zeker ?","Wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    productService.DeleteProduct((Product)lstProducts.SelectedItem);
                    PopulateListbox();
                    ClearControls();
                    DisplayStats();
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string code = txtCode.Text;
            string description = txtDescription.Text;
            PackingUnits packing = (PackingUnits)cmbPackingUnits.SelectedItem;
            int.TryParse(txtStock.Text, out int stock);
            decimal.TryParse(txtPriceEuro.Text, out decimal priceEuro);
            Product product;
            if(isNew)
            {
                product = new Product(code, description, packing, stock, priceEuro);
                productService.AddProduct(product);
            }
            else
            {
                product = (Product)lstProducts.SelectedItem;
                product.Code = code;
                product.Description = description;
                product.Packing = packing;
                product.PriceEuro = priceEuro;
                product.Stock = stock;
            }
            PopulateListbox();
            DefaultSituation();
            lstProducts.SelectedItem = product;
            lstProducts_SelectionChanged(null, null);
            DisplayStats();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DefaultSituation();
            lstProducts_SelectionChanged(null, null);

        }
    }
}
