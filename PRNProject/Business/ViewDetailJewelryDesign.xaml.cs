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
    /// Interaction logic for ViewDetailJewelryDesign.xaml
    /// </summary>
    public partial class ViewDetailJewelryDesign : Page
    {
        private User _user;
        private JewelryDesign  _jewelryDesign;
        private readonly JewelryDesignRepository _jewelryDesignRepository;
        private readonly AppDbContext _context;

        public ViewDetailJewelryDesign(User user, JewelryDesign jewelryDesign)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _user = user;
            _jewelryDesign = jewelryDesign;
            _jewelryDesignRepository = new JewelryDesignRepository(_context);

            LoadData();
        }

        private void LoadData()
        {
            JewelryDesignIDTextBlock.Text = _jewelryDesign.JewelryDesignID.ToString();
            JewelryDesignNameTextBlock.Text = _jewelryDesign.JewelryDesignName;
            JewelryDesignStatusTextBlock.Text = _jewelryDesign.JewelryDesignStatus.ToString();
            CreatedDateTextBlock.Text = _jewelryDesign.CreatedDate.ToShortDateString();
            JewelryIDTextBlock.Text = _jewelryDesign.JewelryID.ToString();

            if (!string.IsNullOrEmpty(_jewelryDesign.JewelryDesignImage))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_jewelryDesign.JewelryDesignImage);
                bitmap.EndInit();
                JewelryDesignImage.Source = bitmap;
            }

            if (_user.UserRole == "customer" && _jewelryDesign.JewelryDesignStatus == "Unapprove")
            {
                ApproveButton.Visibility = Visibility.Visible;
            }
            if (_user.UserRole == "design")
            {
                DeleteButton.Visibility = Visibility.Visible;
            }

        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            _jewelryDesign.JewelryDesignStatus = "Approved";
            _jewelryDesignRepository.Update(_jewelryDesign);
            _context.SaveChanges();
            NavigationService.Navigate(new ViewJewelry(_user));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this Jewelry Design?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var jewelry = _context.Jewelry.Find(_jewelryDesign.JewelryID);
                if (jewelry.JewelryStatus == "Manufactured")
                {
                    MessageBox.Show("Cannot delete Jewelry Design as the Jewelry has been manufactured.");
                    return;
                }

                _jewelryDesignRepository.Delete(_jewelryDesign.JewelryDesignID);
                _context.SaveChanges();
                NavigationService.Navigate(new ViewJewelry(_user));
            }    
                
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            NavigationService.Navigate(new ViewJewelry(_user));
        }
    }
}
