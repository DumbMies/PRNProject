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
using System.Windows.Shapes;

namespace PRNProject.Presentation
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private User _user;
        public Menu()
        {
            InitializeComponent();
        }
        public Menu(User user)
        {
            InitializeComponent();
            WelcomeTextBlock.Text = $"Welcome {user.UserName}";
            if (user.UserRole == "customer")
            {
                CreateProductionRequestButton.Visibility = Visibility.Visible;
                ViewProductionRequestButton.Visibility = Visibility.Visible;
                ViewJewelryButton.Visibility = Visibility.Visible;
            }
            if (user.UserRole == "sales")
            {
                ViewProductionRequestButton.Visibility = Visibility.Visible;
                ViewJewelryButton.Visibility = Visibility.Visible;
            }
            if (user.UserRole == "manager")
            {
                ViewProductionRequestButton.Visibility = Visibility.Visible;
                ViewJewelryButton.Visibility = Visibility.Visible;
            }
            if (user.UserRole == "design")
            {
                ViewProductionRequestButton.Visibility = Visibility.Visible;
                ViewJewelryButton.Visibility = Visibility.Visible;
            }
            if (user.UserRole == "production")
            {
                ViewProductionRequestButton.Visibility = Visibility.Visible;
                ViewJewelryButton.Visibility = Visibility.Visible;
            }
            _user = user;
        }
        private void CreateProductionRequestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToCreateProductionRequest(_user);
        }
        private void ViewProductionRequestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToViewProductionRequest(_user);
        }
        private void CreateJewelryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToViewJewelry(_user);
        }
        private void ViewJewelryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToViewJewelry(_user);
        }

        //Navigate
        public void NavigateToMenu(User user)
        {
            _user = user;
            WelcomeTextBlock.Text = $"Welcome {user.UserName}";
            ContentFrame.Content = null;
        }

        public void NavigateToViewProductionRequest(User user)
        {
            ViewProductionRequest viewProductionRequestPage = new ViewProductionRequest(user);
            ContentFrame.Navigate(viewProductionRequestPage);
        }

        public void NavigateToCreateProductionRequest(User user)
        {
            CreateProductionRequest createProductionRequestPage = new CreateProductionRequest(_user);
            ContentFrame.Navigate(createProductionRequestPage);
        }

        public void NavigateToViewJewelry(User user)
        {
            ViewJewelry viewJewelry = new ViewJewelry(user);
            ContentFrame.Navigate(viewJewelry);
        }
    }
}
