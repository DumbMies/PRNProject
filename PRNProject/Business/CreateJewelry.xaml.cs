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
    /// Interaction logic for CreateJewelry.xaml
    /// </summary>
    public partial class CreateJewelry : Page
    {
        private readonly AppDbContext _context;
        private User _user;
        private ProductionRequest _productionRequest;
        public CreateJewelry(User user, ProductionRequest productionRequest)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _user = user;
            _productionRequest = productionRequest;
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(JewelryNameTextBox.Text) || string.IsNullOrWhiteSpace(JewelryDescriptionTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            int latestJewelryId = _context.Jewelry
                .OrderByDescending(j => j.JewelryID)
                .Select(j => j.JewelryID)
                .FirstOrDefault() + 1;

            Jewelry newJewelry = new Jewelry
            {
                JewelryID = latestJewelryId,
                JewelryName = JewelryNameTextBox.Text,
                JewelryDescription = JewelryDescriptionTextBox.Text,
                JewelryStatus = "Manufacturing",
                JewelryImage = "",
                CreatedDate = DateTime.Now,
                ProductionRequestID = _productionRequest.ProductionRequestID,
            };

            _context.Attach(_productionRequest);
            _context.Jewelry.Add(newJewelry);
            _context.SaveChanges();

            ViewDetailJewelry viewJewelryPage = new ViewDetailJewelry(_user, _productionRequest);
            NavigationService.Navigate(viewJewelryPage);
        }
    }
}
