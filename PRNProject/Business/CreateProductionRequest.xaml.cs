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
    /// Interaction logic for CreateProductionRequest.xaml
    /// </summary>
    public partial class CreateProductionRequest : Page
    {
        private readonly AppDbContext _context;
        private User _user;
        public CreateProductionRequest(User user)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _user = user;
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductionRequestNameTextBox.Text) || string.IsNullOrWhiteSpace(ProductionRequestQuantityTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            if (!int.TryParse(ProductionRequestQuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid number for Quantity.");
                return;
            }

            int latestProductionRequestId = _context.ProductionRequest
                .OrderByDescending(pr => pr.ProductionRequestID)
                .Select(pr => pr.ProductionRequestID)
                .FirstOrDefault() + 1;
            ProductionRequest newProductionRequest = new ProductionRequest
            {
                ProductionRequestID = latestProductionRequestId,
                ProductionRequestName = ProductionRequestNameTextBox.Text,
                ProductionRequestQuantity = int.Parse(ProductionRequestQuantityTextBox.Text),
                ProductionRequestStatus = "Not delivered", 
                ProductionRequestAddress = _user.UserAddress,
                CreatedDate = DateTime.Now,
                UserID = _user.UserID,
            };
            _context.Attach(_user);
            _context.ProductionRequest.Add(newProductionRequest);
            _context.SaveChanges();

            Menu menuWindow = Window.GetWindow(this) as Menu;
            if (menuWindow != null)
            {
                menuWindow.NavigateToViewProductionRequest(_user);
            }

        }

    }
}
