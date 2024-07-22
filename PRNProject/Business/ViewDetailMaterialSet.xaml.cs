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
    /// Interaction logic for ViewDetailMaterialSet.xaml
    /// </summary>
    public partial class ViewDetailMaterialSet : Page
    {
        private User _user;
        private MaterialSet _materialSet;
        private readonly AppDbContext _context;
        private readonly MaterialSetRepository _materialSetRepository;
        private readonly ProductionRequest _productionRequest;

        public ViewDetailMaterialSet(User user, MaterialSet materialSet)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _materialSetRepository = new MaterialSetRepository(_context);
            _user = user;
            _materialSet = materialSet;
            _productionRequest = _context.ProductionRequest.FirstOrDefault(j => j.ProductionRequestID == _materialSet.JewelryID);

            LoadData();
        }

        private void LoadData()
        {
            MaterialSetIDTextBlock.Text = _materialSet.MaterialSetID.ToString();
            CreatedDateTextBlock.Text = _materialSet.CreatedDate.ToShortDateString();
            TotalPriceTextBlock.Text = _materialSet.TotalPrice.ToString("N0");

            var material = _context.Material.Find(_materialSet.MaterialID);
            if (material != null)
            {
                MaterialNameTextBlock.Text = material.MaterialName;
                MaterialPriceTextBlock.Text = material.MaterialPrice.ToString("N0"); 
            }

            var gemstone = _context.Gemstone.Find(_materialSet.GemstoneID);
            if (gemstone != null)
            {
                GemstoneNameTextBlock.Text = gemstone.GemstoneName;
                GemstonePriceTextBlock.Text = gemstone.GemstonePrice.ToString("N0"); 
                GemstoneWeightTextBlock.Text = gemstone.GemstoneWeight.ToString() + " carat";
            }
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new ViewJewelry(_user));
        }
    }
}
