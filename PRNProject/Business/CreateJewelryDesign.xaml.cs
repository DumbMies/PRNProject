using PRNProject.Data;
using PRNProject.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.Windows.Shapes;

namespace PRNProject.Presentation
{
    /// <summary>
    /// Interaction logic for CreateJewelryDesign.xaml
    /// </summary>
    public partial class CreateJewelryDesign : Page
    {
        private readonly AppDbContext _context;
        private readonly JewelryDesignRepository _jewelryDesignRepository;
        private readonly JewelryRepository _jewelryRepository;
        private User _user;
        private Jewelry _jewelry;
        private string _uploadedImagePath;


        public CreateJewelryDesign(User user, Jewelry jewelry)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _jewelryDesignRepository = new JewelryDesignRepository(_context);
            _jewelryRepository = new JewelryRepository(_context);
            _user = user;
            _jewelry = jewelry;

        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string destinationPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", System.IO.Path.GetFileName(fileName));
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationPath));
                File.Copy(fileName, destinationPath, true);
                _uploadedImagePath = destinationPath;
                UploadStatusTextBlock.Text = "Uploaded Successfully";
                UploadStatusTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DesignNameTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (string.IsNullOrEmpty(_uploadedImagePath))
            {
                MessageBox.Show("Please upload a Design Image.");
                return;
            }
            string jewelryDesignName = DesignNameTextBox.Text;

            int latestJewelryDesignId = _context.JewelryDesign
                .OrderByDescending(jd => jd.JewelryDesignID)
                .Select(jd => jd.JewelryDesignID)
                .FirstOrDefault() + 1;

            JewelryDesign newJewelryDesign = new JewelryDesign
            {
                JewelryDesignID = latestJewelryDesignId,
                JewelryDesignName = jewelryDesignName,
                JewelryDesignImage = _uploadedImagePath,
                JewelryDesignFile = "",
                JewelryDesignStatus = "Unapprove",
                CreatedDate = DateTime.Now,
                UserID = _user.UserID,
                JewelryID = _jewelry.JewelryID
            };

            _context.JewelryDesign.Add(newJewelryDesign);
            _context.SaveChanges();

            var productionRequest = _jewelryRepository.GetProductionRequestByJewelryID(_jewelry.JewelryID);
            ViewDetailJewelry viewDetailJewelryPage = new ViewDetailJewelry(_user, productionRequest);
            NavigationService.Navigate(viewDetailJewelryPage);
        }
    }
}
