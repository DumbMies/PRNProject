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
    /// Interaction logic for CreateQuotationRequest.xaml
    /// </summary>
    public partial class CreateQuotationRequest : Page
    {
        private readonly AppDbContext _context;
        private readonly JewelryRepository _jewelryRepository;
        private User _user;
        private Jewelry _jewelry;

        public CreateQuotationRequest(User user, Jewelry jewelry)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _jewelryRepository = new JewelryRepository(_context);
            _user = user;
            _jewelry = jewelry;
        }

        private void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuotationRequestNameTextBox.Text) || string.IsNullOrWhiteSpace(LaborPriceTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!decimal.TryParse(LaborPriceTextBox.Text, out decimal laborPrice) || laborPrice <= 0)
            {
                MessageBox.Show("Please enter a labor price.");
                return;
            }

            string quotationRequestName = QuotationRequestNameTextBox.Text;

            int latestQuotationRequestId = _context.QuotationRequest
                .OrderByDescending(qr => qr.QuotationRequestID)
                .Select(qr => qr.QuotationRequestID)
                .FirstOrDefault() + 1;

            var materialSets = _jewelryRepository.GetMaterialSetsByJewelryID(_jewelry.JewelryID);
            decimal totalPrice = materialSets.Sum(ms => ms.TotalPrice) + laborPrice;

            QuotationRequest newQuotationRequest = new QuotationRequest
            {
                QuotationRequestID = latestQuotationRequestId,
                QuotationRequestName = quotationRequestName,
                QuotationRequestStatus = "Unapprove",
                LaborPrice = laborPrice,
                TotalPrice = totalPrice,
                CreatedDate = DateTime.Now,
                JewelryID = _jewelry.JewelryID
            };

            _context.QuotationRequest.Add(newQuotationRequest);
            _context.SaveChanges();

            var productionRequest = _jewelryRepository.GetProductionRequestByJewelryID(_jewelry.JewelryID);
            ViewDetailJewelry viewDetailJewelryPage = new ViewDetailJewelry(_user, productionRequest);
            NavigationService.Navigate(viewDetailJewelryPage);
        }
    }
}
