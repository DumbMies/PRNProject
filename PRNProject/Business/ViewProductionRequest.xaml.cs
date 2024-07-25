using PRNProject.Data;
using PRNProject.Data.Repository;
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
    /// Interaction logic for ViewProductionRequest.xaml
    /// </summary>
    public partial class ViewProductionRequest : Page
    {
        private User _user;
        private readonly AppDbContext _context;
        private readonly ProductionRequestRepository _productionRequestRepository;
        public ViewProductionRequest(User user)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _productionRequestRepository = new ProductionRequestRepository(_context);
            _user = user;
            if (_user.UserRole != "sales")
            {
                var deliverColumn = FindDataGridTemplateColumn("Deliver");
                if (deliverColumn != null)
                {
                    deliverColumn.Visibility = Visibility.Collapsed;
                }
            }
            LoadData();
        }

        private void LoadData()
        {
            var ProductionRequest = _productionRequestRepository.GetAll();
            ProductionRequestDataGrid.ItemsSource = ProductionRequest;
        }

        private DataGridTemplateColumn FindDataGridTemplateColumn(string header)
        {
            foreach (var column in ProductionRequestDataGrid.Columns)
            {
                if (column.Header.ToString() == header && column is DataGridTemplateColumn)
                {
                    return (DataGridTemplateColumn)column;
                }
            }
            return null;
        }

        private void ProductionRequestDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            ProductionRequest selectedProductionRequest = ProductionRequestDataGrid.SelectedItem as ProductionRequest;
            if (selectedProductionRequest != null)
            {
                var jewelryCount = _context.Jewelry
                    .Count(j => j.ProductionRequestID == selectedProductionRequest.ProductionRequestID);

                if (jewelryCount < 1 && _user.UserRole == "customer")
                {
                    MessageBox.Show("Jewelry has not been created.");
                }
                else if (jewelryCount < 1 && _user.UserRole == "sales")
                {
                    CreateJewelry createJewelryPage = new CreateJewelry(_user, selectedProductionRequest);
                    NavigationService.Navigate(createJewelryPage);
                }  
                else
                {
                    ViewDetailJewelry viewDetailJewelryPage = new ViewDetailJewelry(_user, selectedProductionRequest);
                    NavigationService.Navigate(viewDetailJewelryPage);
                }
            }
            
        }

        private void DeliverButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int productionRequestId = (int)button.Tag;

            var productionRequest = _context.ProductionRequest.Find(productionRequestId);
            var jewelryList = _context.Jewelry.Where(j => j.ProductionRequestID == productionRequestId).ToList();

            if (jewelryList.Count != productionRequest.ProductionRequestQuantity)
            {
                MessageBox.Show("Production Request isn't ready to deliver");
                return;
            }

            if (!jewelryList.All(j => j.JewelryStatus == "Manufactured"))
            {
                MessageBox.Show("All Jewelry must be manufactured before deliver.");
                return;
            }

            productionRequest.ProductionRequestStatus = "Delivered";
            _context.SaveChanges();

            LoadData();
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = (Menu)Window.GetWindow(this);
            mainWindow.NavigateToMenu(_user);
        }
    }
}
