using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Devourers
{
    internal class GameBoard
    {
        private int _boardSize;
        private Grid _destinationGrid;

        private int[,] _board;

        private Button[,] gridButtons;

        private bool _playerOneMove = true;

        private Brush _playerOneBrush;
        private Brush _playerTwoBrush;

        public Path _pawnVectorsP1;
        public Path _pawnVectorsP2;

        string buttonBackgroundColor = "#454545";
        string buttonHoverBackgroundColor = "#454545";

        private char actualPawn = 'X';
        private string actualPawnSize = "big";

        public Button lastActiveButton = new Button();

        public GameBoard(int BoardSize, Grid destinationGrid)
        {
            _boardSize = BoardSize;
            _destinationGrid = destinationGrid;

            _board = new int[BoardSize, BoardSize];
            gridButtons = new Button[BoardSize, BoardSize];
        }

        public void GenerateBoard()
        {
            Grid gameBoardGrid = new Grid();

            gameBoardGrid.Background = new SolidColorBrush(Colors.Black);

            gameBoardGrid.HorizontalAlignment = HorizontalAlignment.Center;
            gameBoardGrid.VerticalAlignment = VerticalAlignment.Center;

            int btnSize = CalculateButtonSize(_boardSize);

            // Define rows and columns
            for (int i = 0; i < _boardSize; i++)
            {
                gameBoardGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(btnSize + 0)
                });
                gameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(btnSize + 0)
                });
            }

            // Add buttons to the grid
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    gridButtons[row, col] = new Button
                    {
                        Tag = row.ToString() + ',' + col.ToString(),
                        Width = btnSize,
                        Height = btnSize,
                        Margin = new Thickness(2),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Style = SetButtonStyle()
                    };

                    gridButtons[row, col].Click += GameBoardButton_Clicked;
                    gridButtons[row, col].MouseEnter += GameBoardButton_MouseEnter;
                    gridButtons[row, col].MouseLeave += GameBoardButton_MouseLeave;

                    Grid.SetRow(gridButtons[row, col], row);
                    Grid.SetColumn(gridButtons[row, col], col);
                    gameBoardGrid.Children.Add(gridButtons[row, col]);
                }
            }

            // Set grid to the second row and column of "GameScreen" Grid
            if (_destinationGrid == null)
            {
                MessageBox.Show("GameScreen Grid not found!");
                return;
            }

            // Ensure second row and column exists
            while (_destinationGrid.RowDefinitions.Count < 2) _destinationGrid.RowDefinitions.Add(new RowDefinition());
            while (_destinationGrid.ColumnDefinitions.Count < 2) _destinationGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // Place the gameBoardGrid in the second row and column
            Grid.SetRow(gameBoardGrid, 1);
            Grid.SetColumn(gameBoardGrid, 1);

            _destinationGrid.Children.Clear();

            // Add the generated grid to the "_destinationGrid"
            _destinationGrid.Children.Add(gameBoardGrid);
        }

        private void DisplayUpdatedBoard()
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j] == 0) continue;

                    // Clone the Path to avoid reuse errors
                    Path clonedPath = new Path
                    {
                        Data = _pawnVectorsP1.Data.Clone(),
                        Stroke = _pawnVectorsP1.Stroke,
                        StrokeThickness = _pawnVectorsP1.StrokeThickness,
                        Width = _pawnVectorsP1.Width,
                        Height = _pawnVectorsP1.Height,
                        HorizontalAlignment = _pawnVectorsP1.HorizontalAlignment,
                        VerticalAlignment = _pawnVectorsP1.VerticalAlignment
                    };

                    var viewbox = new Viewbox()
                    {
                        Child = clonedPath,
                        Stretch = Stretch.None,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    gridButtons[i, j].Content = viewbox;

                    if (_playerOneMove == true) _playerOneMove = false;
                    else _playerOneMove = true;
                } 
            }
        }

        private void UpdateBoardArray(int row, int col)
        {
            int value = 0;

            if (actualPawnSize == "big"    && actualPawn == 'X') { value = 1; }
            if (actualPawnSize == "medium" && actualPawn == 'X') { value = 2; }
            if (actualPawnSize == "small"  && actualPawn == 'X') { value = 3; }
            if (actualPawnSize == "big"    && actualPawn == 'O') { value = 4; }
            if (actualPawnSize == "medium" && actualPawn == 'O') { value = 5; }
            if (actualPawnSize == "small"  && actualPawn == 'O') { value = 6; }

            _board[row, col] = value;
        }

        private bool IsBoardFieldEmpty(string tag)
        {
            var parts = tag.Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            return (_board[row, col] == 0) ? true : false;
        }

        public void GameBoardButton_Clicked(object sender, RoutedEventArgs e) 
        {
            if (sender is Button button && button.Tag is String tag)
            {
                var parts = tag.Split(',');
                int row = int.Parse(parts[0]);
                int col = int.Parse(parts[1]);

                UpdateBoardArray(row, col);

                DisplayUpdatedBoard();

                ChangeBackgroundColor();
            }
        }

        private void GameBoardButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            PutVectorOnAButton(sender, _pawnVectorsP1);

            if (sender is Button button && button.Tag is String tag)
            {
                if (IsBoardFieldEmpty(tag))
                {
                    lastActiveButton = button;
                }
            }
        }

        private void GameBoardButton_MouseLeave(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is String tag)
            {
                if (IsBoardFieldEmpty(tag))
                {
                    button.Content = null;
                    lastActiveButton = null;
                }
            }
        }

        public void PutVectorOnAButton(object sender, Path vector)
        {
            if (sender is Button button)
            {
                PutVectorOnAButton(button, vector);
            }
        }

        public void PutVectorOnAButton(Button button, Path vector)
        {
            if (button == null) return; 
            
            if (!IsBoardFieldEmpty(button.Tag.ToString())) return;

            // Cloning Path to avoid logic tree conflict
            Path clonedPath = new Path
            {
                Data = vector.Data.Clone(),
                Stroke = vector.Stroke,
                StrokeThickness = vector.StrokeThickness,
                Width = vector.Width,
                Height = vector.Height,
                HorizontalAlignment = vector.HorizontalAlignment,
                VerticalAlignment = vector.VerticalAlignment
            };

            double scaleFactor = 2.8;

            if (actualPawnSize == "big")
            {
                scaleFactor = 2.8;
            }
            if (actualPawnSize == "medium")
            {
                scaleFactor = 1.6;
            }
            if (actualPawnSize == "small")
            {
                scaleFactor = 1.0;
            }

            clonedPath.RenderTransform = new ScaleTransform(scaleFactor, scaleFactor);
            clonedPath.RenderTransformOrigin = new Point(0.5, 0.5);

            var viewbox = new Viewbox()
            {
                Child = clonedPath,
                Stretch = Stretch.None,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            button.Content = viewbox;
        }

        private void ChangeBackgroundColor()
        {
            _destinationGrid.Background = (_playerOneMove == true) ? _playerOneBrush : _playerTwoBrush;
        }

        private int CalculateButtonSize(int boardSize)
        {
            switch (boardSize)
            {
                case 3: return 150;
                case 4: return 110;
                case 5: return 90;
            }
            return -1;
        }

        private ControlTemplate GetCustomButtonTemplate()
        {
            // Create the Border element for the button's visual representation
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.Name = "ButtonBorder";
            borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Button.BorderBrushProperty));
            borderFactory.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(Button.BorderThicknessProperty));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(0)); // Optional: Keep corners square.

            // Create the ContentPresenter for the button's content
            FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            contentPresenterFactory.SetValue(ContentPresenter.ContentProperty, new TemplateBindingExtension(Button.ContentProperty));

            // Add the ContentPresenter to the Border
            borderFactory.AppendChild(contentPresenterFactory);

            // Create the ControlTemplate and add triggers for hover effects
            ControlTemplate template = new ControlTemplate(typeof(Button));
            template.VisualTree = borderFactory;

            // Add the hover trigger
            Trigger hoverTrigger = new Trigger
            {
                Property = Button.IsMouseOverProperty,
                Value = true
            };
            // Background color on hover
            hoverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(buttonHoverBackgroundColor)), "ButtonBorder"));

            // Add triggers to the template
            template.Triggers.Add(hoverTrigger);

            return template;
        }

        public int GetBoardSize()
        {
            return _boardSize;
        }

        private Style SetButtonStyle()
        {
            Style buttonStyle = new Style(typeof(Button));

            // Set default properties
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(buttonBackgroundColor))));
            buttonStyle.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.Transparent));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            buttonStyle.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(0)));
            buttonStyle.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(0)));
            buttonStyle.Setters.Add(new Setter(Button.TemplateProperty, GetCustomButtonTemplate()));

            return buttonStyle;
        }

        public void SetPlayerOneColor(Brush color)
        {
            _playerOneBrush = color;
        }

        public void SetPlayerTwoColor(Brush color)
        {
            _playerTwoBrush = color;
        }

        public void SetP1Vectors(Path vectorArray)
        {
            _pawnVectorsP1 = vectorArray;
        }

        public void SetP2Vectors(Path vectorArray)
        {
            _pawnVectorsP2 = vectorArray;
        }

        public void SetActualPawnSize(string newSize)
        {
            actualPawnSize = newSize;
        }
    }
}
