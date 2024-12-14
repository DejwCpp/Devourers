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
        private GameBoard _gameBoard;
        private Player _playerOne;
        private Player _playerTwo;

        /* TO DO 
         * 
         * ------- last updated in GameBoard.cs line 120
         *
         * - 2 dimentional array with the logic
         * - display label which players round on top
         *
         */

        public MainWindow()
        {
            InitializeComponent();
            InitializeMenuLogicUI();

            this.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                case Key.NumPad1:
                    _gameBoard.SetActualPawnSize("big");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    _gameBoard.SetActualPawnSize("medium");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    _gameBoard.SetActualPawnSize("small");
                    break;
            }

            _gameBoard.PutVectorOnAButton(_gameBoard.lastActiveButton, _gameBoard._pawnVectorsP1);
        }

        public void GenerateBoardButtonClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tagValue && int.TryParse(tagValue, out int size))
            {
                _gameBoard = new GameBoard(size, GameScreen);

                _playerOne = new Player(GameScreen, _menuLogic.GetPlayerOneBrush(), 'X', _gameBoard.GetBoardSize());
                _playerTwo = new Player(GameScreen, _menuLogic.GetPlayerTwoBrush(), 'O', _gameBoard.GetBoardSize());

                _gameBoard.GenerateBoard();

                SwitchFromMenuToGameMode();

                _gameBoard.SetPlayerOneColor(_playerOne.GetPlayerColor());
                _gameBoard.SetPlayerTwoColor(_playerTwo.GetPlayerColor());

                _gameBoard.SetP1Vectors(_playerOne.GetVectorOfPawn());
                _gameBoard.SetP2Vectors(_playerTwo.GetVectorOfPawn());

                GameScreen.Background = _playerOne.GetPlayerColor();
            }
        }

        private void SwitchFromMenuToGameMode()
        {
            MenuScreen.Visibility = Visibility.Collapsed;
            GameScreen.Visibility = Visibility.Visible;

            _playerOne.DisplayPlayerInfo(1, 0);
            _playerTwo.DisplayPlayerInfo(1, 2);
        }

        private void InitializeMenuLogicUI()
        {
            _menuLogic = new MenuLogicUI(PlayerOneLabel, PlayerTwoLabel,
                                         PlayerOneButtonContainer, PlayerTwoButtonContainer,
                                         SelectBoardSizeSection, BoardSizeButtonsSection);

            foreach (UIElement element in PlayerOneButtonContainer.Children)
            {
                if (element is Button button)
                {
                    button.Click += _menuLogic.PlayerOneButton_Clicked;
                    button.MouseEnter += _menuLogic.PlayerOneButton_MouseEnter;
                    button.MouseLeave += _menuLogic.PlayerOneButton_MouseLeave;
                }
            }

            foreach (UIElement element in PlayerTwoButtonContainer.Children)
            {
                if (element is Button button)
                {
                    button.Click += _menuLogic.PlayerTwoButton_Clicked;
                    button.MouseEnter += _menuLogic.PlayerTwoButton_MouseEnter;
                    button.MouseLeave += _menuLogic.PlayerTwoButton_MouseLeave;
                }
            }
        }


    }
}