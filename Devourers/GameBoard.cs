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

        private Brush _playerOneBrush;
        private Brush _playerTwoBrush;

        private Path[] _pawnVectorsP1;
        private Path[] _pawnVectorsP2;

        public GameBoard(int BoardSize, Grid destinationGrid)
        {
            _boardSize = BoardSize;
            _destinationGrid = destinationGrid;
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
                    Button button = new Button
                    {
                        Width = btnSize,
                        Height = btnSize,
                        Margin = new System.Windows.Thickness(2),
                        Style = SetButtonStyle()
                    };

                    button.MouseEnter += GameBoardButton_MouseEnter;

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    gameBoardGrid.Children.Add(button);
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

        public void GameBoardButton_Clicked(object sender, RoutedEventArgs e) 
        {
            //_destinationGrid.Background = 
        }

        private void GameBoardButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            PutVectorOnAButton(sender, _pawnVectorsP1[0]);
        }

        private void PutVectorOnAButton(object sender, Path vector)
        {
            if (sender is Button button)
            {
                if (vector != null)
                {
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

                    var viewbox = new Viewbox()
                    {
                        Child = clonedPath,
                        Stretch = Stretch.Uniform,
                    };

                    button.Content = viewbox;
                }
                else
                {
                    button.Content = null;
                }
            }
        }

        private int CalculateButtonSize(int boardSize)
        {
            switch (boardSize)
            {
                case 3: { return 150; break; }
                case 4: { return 110; break; }
                case 5: { return 90; break; }
            }
            return -1;
        }

        public int GetBoardSize()
        {
            return _boardSize;
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
            hoverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535")), "ButtonBorder"));

            // Add triggers to the template
            template.Triggers.Add(hoverTrigger);

            return template;
        }

        private Style SetButtonStyle()
        {
            Style buttonStyle = new Style(typeof(Button));

            // Set default properties
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#454545"))));
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

        public void SetP1Vectors(Path[] vectorArray)
        {
            _pawnVectorsP1 = vectorArray;
        }

        public void SetP2Vectors(Path[] vectorArray)
        {
            _pawnVectorsP2 = vectorArray;
        }
    }
}
