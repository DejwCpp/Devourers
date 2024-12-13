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
        private GameBoardLogic _gameBoardLogic;
        private Player _playerOne;
        private Player _playerTwo;

        /* TO DO 
         * 
         * ------- last updated in GameBoard.cs line 104
         *
         * - display pawn on button hover
         * - display label which players round on top
         * - selecting figure on 1-3 keys clicked
         * - 2 dimentional array with the logic
         *
         */

        public MainWindow()
        {
            InitializeComponent();
            InitializeMenuLogicUI();
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

                _gameBoard.SetP1Vectors(_playerOne.Get3VectorsOfPawns());
                _gameBoard.SetP2Vectors(_playerTwo.Get3VectorsOfPawns());

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