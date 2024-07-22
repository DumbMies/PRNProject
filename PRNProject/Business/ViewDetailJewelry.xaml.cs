using PRNProject.Data.Repository;
using PRNProject.Data;
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

namespace PRNProject.Presentation
{
    /// <summary>
    /// Interaction logic for ViewDetailJewelry.xaml
    /// </summary>
    public partial class ViewDetailJewelry : Page
    {
        private User _user;
        private ProductionRequest _productionRequest;
        private readonly AppDbContext _context;
        private readonly JewelryRepository _jewelryRepository;

        public ViewDetailJewelry(User user, ProductionRequest productionRequest)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _jewelryRepository = new JewelryRepository(_context);
            _user = user;
            _productionRequest = productionRequest;
            
            if (_user.UserRole != "production")
            {
                var manufactureColumn = FindDataGridTemplateColumn("Manufacture");
                if (manufactureColumn != null)
                {
                    manufactureColumn.Visibility = Visibility.Collapsed;
                }
            }
            if (_user.UserRole != "sales")
            {
                CreateNewJewelryButton.Visibility = Visibility.Collapsed;
            }

            LoadData();
        }

        private void LoadData()
        {
            var jewelries = _jewelryRepository.GetByProductionRequestID(_productionRequest.ProductionRequestID);
            JewelryDataGrid.ItemsSource = jewelries;

            int jewelryCount = jewelries.Count();
            JewelryCountTextBlock.Text = $"Jewelry created: {jewelryCount}/{_productionRequest.ProductionRequestQuantity}";

            if (jewelryCount >= _productionRequest.ProductionRequestQuantity)
            {
                CreateNewJewelryButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private DataGridTemplateColumn FindDataGridTemplateColumn(string header)
        {
            foreach (var column in JewelryDataGrid.Columns)
            {
                if (column.Header.ToString() == header && column is DataGridTemplateColumn)
                {
                    return (DataGridTemplateColumn)column;
                }
            }
            return null;
        }

        private void MaterialSetButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int jewelryId = (int)button.Tag;

            var selectedJewelry = _context.Jewelry.Find(jewelryId);

            var materialSets = _context.MaterialSet.Where(ms => ms.JewelryID == jewelryId).ToList();

            if (materialSets.Count > 0)
            {
                var latestMaterialSet = materialSets.OrderByDescending(ms => ms.MaterialSetID).First();
                ViewDetailMaterialSet viewDetailMaterialSetPage = new ViewDetailMaterialSet(_user, latestMaterialSet);
                NavigationService.Navigate(viewDetailMaterialSetPage);
            }
            else if (_user.UserRole != "sales")
            {
                MessageBox.Show("Material Set is not created yet.");
                return;
            }
            else
            {
                CreateMaterialSet createMaterialSetPage = new CreateMaterialSet(_user, selectedJewelry, _productionRequest);
                NavigationService.Navigate(createMaterialSetPage);
            }
        }

        private void QuotationRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int jewelryId = (int)button.Tag;

            var selectedJewelry = _context.Jewelry.Find(jewelryId);

            var materialSet = _context.MaterialSet.FirstOrDefault(ms => ms.JewelryID == jewelryId);
            if (materialSet == null)
            {
                MessageBox.Show("Material Set is not created yet.");
                return;
            }

            var quotationRequests = _context.QuotationRequest.Where(qr => qr.JewelryID == jewelryId).ToList();

            if (quotationRequests.Count > 0)
            {
                var latestQuotationRequest = quotationRequests.OrderByDescending(qr => qr.QuotationRequestID).First();
                ViewDetailQuotationRequest viewDetailQuotationRequestPage = new ViewDetailQuotationRequest(_user, latestQuotationRequest);
                NavigationService.Navigate(viewDetailQuotationRequestPage);
            }
            else if (_user.UserRole != "sales")
            {
                MessageBox.Show("Quotation Request is not created yet.");
                return;
            }
            else
            {
                CreateQuotationRequest createQuotationRequestPage = new CreateQuotationRequest(_user, selectedJewelry);
                NavigationService.Navigate(createQuotationRequestPage);
            }
        }

        private void DesignButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int jewelryId = (int)button.Tag;

            var selectedJewelry = _context.Jewelry.Find(jewelryId);

            var quotationRequest = _context.QuotationRequest.FirstOrDefault(qr => qr.JewelryID == jewelryId);
            if (quotationRequest == null)
            {
                MessageBox.Show("Quotation Request is not created yet.");
                return;
            }
            if (_user.UserRole == "design" && quotationRequest.QuotationRequestStatus != "Approved")
            {
                MessageBox.Show("Quotation Request has not been Approved.");
                return;
            }

            var jewelryDesign = _context.JewelryDesign.Where(jd => jd.JewelryID == jewelryId).ToList();

            if (jewelryDesign.Count > 0)
            {
                var latestJewelryDesgin = jewelryDesign.OrderByDescending(jd => jd.JewelryDesignID).First();
                ViewDetailJewelryDesign viewDetailJewelryDesignPage = new ViewDetailJewelryDesign(_user, latestJewelryDesgin);
                NavigationService.Navigate(viewDetailJewelryDesignPage);
            }
            else if (_user.UserRole != "design")
            {
                MessageBox.Show("Jewelry Design is not created yet.");
                return;
            }
            else
            {
                CreateJewelryDesign createJewelryDesignPage = new CreateJewelryDesign(_user, selectedJewelry);
                NavigationService.Navigate(createJewelryDesignPage);
            }
        }
        private void ManufactureButton_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            int jewelryId = (int)button.Tag;

            var selectedJewelry = _context.Jewelry.Find(jewelryId);

            var jewelryDesign = _context.JewelryDesign.FirstOrDefault(jd => jd.JewelryID == jewelryId);
            if (jewelryDesign == null)
            {
                MessageBox.Show("Jewelry isn't ready to manufacture.");
                return;
            }

            if (selectedJewelry != null)
            {
                selectedJewelry.JewelryStatus = "Manufactured";
                _context.SaveChanges();
                LoadData();
            }
        }



        private void CreateNewJewelryButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateJewelry createJewelryPage = new CreateJewelry(_user, _productionRequest);
            NavigationService.Navigate(createJewelryPage);
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProductionRequest(_user));
        }
    }
}
