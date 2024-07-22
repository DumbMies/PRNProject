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
    /// Interaction logic for ViewDetailQuotationRequest.xaml
    /// </summary>
    public partial class ViewDetailQuotationRequest : Page
    {
        private User _user;
        private QuotationRequest _quotationRequest;
        private readonly QuotationRequestRepository _quotationRequestRepository;
        private readonly ProductionRequest _productionRequest;
        private readonly AppDbContext _context;


        public ViewDetailQuotationRequest(User user, QuotationRequest quotationRequest)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _user = user;
            _quotationRequest = quotationRequest;
            _quotationRequestRepository = new QuotationRequestRepository(_context);
            _productionRequest = _context.ProductionRequest.FirstOrDefault(j => j.ProductionRequestID == _quotationRequest.JewelryID);

            LoadData();
        }

        private void LoadData()
        {
            QuotationRequestIDTextBlock.Text = _quotationRequest.QuotationRequestID.ToString();
            QuotationRequestNameTextBlock.Text = _quotationRequest.QuotationRequestName;
            QuotationRequestStatusTextBlock.Text = _quotationRequest.QuotationRequestStatus;
            LaborPriceTextBlock.Text = _quotationRequest.LaborPrice.ToString();
            TotalPriceTextBlock.Text = _quotationRequest.TotalPrice.ToString();
            CreatedDateTextBlock.Text = _quotationRequest.CreatedDate.ToShortDateString();
            JewelryIDTextBlock.Text = _quotationRequest.JewelryID.ToString();

            if ((_user.UserRole == "manager" && _quotationRequest.QuotationRequestStatus == "Unapprove") || (_user.UserRole == "customer" && _quotationRequest.QuotationRequestStatus == "Approved by Manager"))
            {
                ApproveButton.Visibility = Visibility.Visible;
            }
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user.UserRole == "manager")
            {
                _quotationRequest.QuotationRequestStatus = "Approved by Manager";
                _quotationRequestRepository.Update(_quotationRequest);
                _context.SaveChanges();
            }
            else if (_user.UserRole == "customer" && _quotationRequest.QuotationRequestStatus == "Approved by Manager")
            {
                _quotationRequest.QuotationRequestStatus = "Approved";
                _quotationRequestRepository.Update(_quotationRequest);
                _context.SaveChanges();
            }
            NavigationService.Navigate(new ViewJewelry(_user));

        }
        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            NavigationService.Navigate(new ViewJewelry(_user));
        }
    }
}
