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
    /// Interaction logic for CreateMaterialSet.xaml
    /// </summary>
    public partial class CreateMaterialSet : Page
    {
        private readonly AppDbContext _context;
        private User _user;
        private Jewelry _jewelry;
        private ProductionRequest _productionRequest;

        public CreateMaterialSet(User user, Jewelry jewelry, ProductionRequest productionRequest)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _user = user;
            _jewelry = jewelry;
            _productionRequest = productionRequest;

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            var gemstones = _context.Gemstone.ToList();
            var materials = _context.Material.ToList();

            GemstoneComboBox.ItemsSource = gemstones;
            GemstoneComboBox.DisplayMemberPath = "GemstoneName";
            GemstoneComboBox.SelectedValuePath = "GemstoneID";

            MaterialComboBox.ItemsSource = materials;
            MaterialComboBox.DisplayMemberPath = "MaterialName";
            MaterialComboBox.SelectedValuePath = "MaterialID";
        }

        private void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GemstoneComboBox.SelectedValue == null || MaterialComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!decimal.TryParse(WeightTextBox.Text, out decimal weight) || weight <= 0)
            {
                MessageBox.Show("Please enter a valid weight.");
                return;
            }

            int gemstoneId = (int)GemstoneComboBox.SelectedValue;
            int materialId = (int)MaterialComboBox.SelectedValue;

            var selectedGemstone = _context.Gemstone.Find(gemstoneId);
            var selectedMaterial = _context.Material.Find(materialId);

            int latestMaterialSetId = _context.MaterialSet
                .OrderByDescending(ms => ms.MaterialSetID)
                .Select(ms => ms.MaterialSetID)
                .FirstOrDefault() + 1;

            decimal totalPrice = (selectedMaterial.MaterialPrice * weight) + (selectedGemstone.GemstonePrice * selectedGemstone.GemstoneWeight);

            MaterialSet newMaterialSet = new MaterialSet
            {
                MaterialSetID = latestMaterialSetId,
                CreatedDate = DateTime.Now,
                TotalPrice = totalPrice,
                JewelryID = _jewelry.JewelryID,
                MaterialID = materialId,
                GemstoneID = gemstoneId,
            };
            _context.Attach(_jewelry);
            _context.MaterialSet.Add(newMaterialSet);
            _context.SaveChanges();

            ViewDetailJewelry viewDetailJewelryPage = new ViewDetailJewelry(_user, _productionRequest);
            NavigationService.Navigate(viewDetailJewelryPage);
        }
    }
}
