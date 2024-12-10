using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Devourers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MenuLogicUI _menuLogic;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMenuLogicUI();
        }

        private void InitializeMenuLogicUI()
        {
            _menuLogic = new MenuLogicUI(PlayerOneLabel, PlayerTwoLabel, PlayerOneButtonContainer, PlayerTwoButtonContainer);

            foreach (UIElement element in PlayerOneButtonContainer.Children)
            {
                if (element is Button button)
                {
                    button.Click += _menuLogic.PlayerOneButton_Clicked;
                    button.MouseEnter += _menuLogic.PlayerOneButton_MouseEnter;
                    //button.MouseLeave += _menuLogic.PlayerOneButton_MouseLeave;
                }
            }

            foreach (UIElement element in PlayerTwoButtonContainer.Children)
            {
                if (element is Button button)
                {
                    button.MouseEnter += _menuLogic.PlayerTwoButton_MouseEnter;
                    //button.MouseLeave += _menuLogic.PlayerTwoButton_MouseLeave;
                }
            }
        }
    }
}