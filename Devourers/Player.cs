using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Devourers
{
    internal class Player
    {
        Grid _destinationGrid;
        private Brush _playerColor;
        char _pawn;

        private int _boardSize;

        private int _bigPawn;
        private int _mediumPawn;
        private int _smallPawn;

        private Path [] _pawnVectors;

        public Player(Grid destinationGrid, Brush playerColor, char pawn, int boardSize)
        {
            _destinationGrid = destinationGrid;
            _playerColor = playerColor;
            _pawn = pawn;
            _boardSize = boardSize;

            _pawnVectors = new Path[3];

            SetDefaultPawnQuantity();
        }

        public void DisplayPlayerInfo(int GridRow, int GridCol)
        {
            // Create a new StackPanel to contain the sections
            StackPanel playerPanel = new StackPanel
            {
                Width = 150,
                Background = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Orientation = Orientation.Vertical
            };

            int SectionSize = 170;
            int VectorSize = 110;
            int ArmLength = VectorSize;
            int VectorThickness = 15;

            // Add 3 sections with vector graphics and labels
            for (int i = 0; i < 3; i++)
            {
                // Decrease vector size and arm length proportionally
                int currentVectorSize = VectorSize - (i * 35);
                int currentArmLength = ArmLength - (i * 35);
                int currentVectorThickness = VectorThickness - (i * 5);


                // Create a section container as a Grid
                Grid sectionGrid = new Grid
                {
                    Height = SectionSize,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Top
                };

                SectionSize = SectionSize - 30;

                Path sectionVector = new Path
                {
                    Stroke = _playerColor,
                    StrokeThickness = currentVectorThickness,
                    Width = currentVectorSize,
                    Height = currentVectorSize,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                };

                if (_pawn == 'X')
                {
                    sectionVector.Data = Geometry.Parse(
                        $"M{(currentVectorSize - currentArmLength) / 2},{(currentVectorSize - currentArmLength) / 2} " +
                        $"L{(currentVectorSize + currentArmLength) / 2},{(currentVectorSize + currentArmLength) / 2} " +
                        $"M{(currentVectorSize + currentArmLength) / 2},{(currentVectorSize - currentArmLength) / 2} " +
                        $"L{(currentVectorSize - currentArmLength) / 2},{(currentVectorSize + currentArmLength) / 2}");
                }
                if (_pawn == 'O')
                {
                    double adjustedSize = currentVectorSize - currentVectorThickness; // Adjust size for stroke thickness

                    // Center the ellipse inside the section (taking the stroke thickness into account)
                    sectionVector.Data = new EllipseGeometry
                    {
                        Center = new Point(currentVectorSize / 2, currentVectorSize / 2), // Keep it centered in the available space
                        RadiusX = adjustedSize / 2,
                        RadiusY = adjustedSize / 2
                    };
                }

                _pawnVectors[i] = sectionVector;


                // Create a label for the section
                Label sectionLabel = new Label
                {
                    Name = $"LabelPawn_{i + 1}",
                    FontSize = 25,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                };

                if (i == 0) { sectionLabel.Content = _bigPawn; }
                if (i == 1) { sectionLabel.Content = _mediumPawn; }
                if (i == 2) { sectionLabel.Content = _smallPawn; }

                // Add the vector and label to the section grid
                sectionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                sectionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                Grid.SetRow(sectionVector, 0);
                Grid.SetRow(sectionLabel, 1);

                sectionGrid.Children.Add(sectionVector);
                sectionGrid.Children.Add(sectionLabel);

                // Add the section Grid to the StackPanel
                playerPanel.Children.Add(sectionGrid);
            }

            // Check if _destinationGrid is valid
            if (_destinationGrid == null)
            {
                MessageBox.Show("Destination Grid not found!");
                return;
            }

            // Add the StackPanel to the specified Grid cell
            Grid.SetRow(playerPanel, GridRow);
            Grid.SetColumn(playerPanel, GridCol);

            // Add the StackPanel to the _destinationGrid
            _destinationGrid.Children.Add(playerPanel);
        }

        public Brush GetPlayerColor()
        {
            return _playerColor;
        }

        public Path [] Get3VectorsOfPawns()
        {
            return _pawnVectors;
        }

        private void SetDefaultPawnQuantity()
        {
            if (_boardSize == 3) { SetValueForEachPawn(3, 3, 3); }
            if (_boardSize == 4) { SetValueForEachPawn(5, 5, 5); }
            if (_boardSize == 5) { SetValueForEachPawn(8, 8, 8); }
        }

        private void SetValueForEachPawn(int p1, int p2, int p3)
        {
            _bigPawn = p1;
            _mediumPawn = p2;
            _smallPawn = p3;
        }
    }
}
