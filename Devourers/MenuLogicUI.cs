using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Devourers
{
    class MenuLogicUI
    {
        private Label _playerOneLabel;
        private Label _playerTwoLabel;

        private UniformGrid _playerOneButtons;
        private UniformGrid _playerTwoButtons;

        public MenuLogicUI(Label playerOneLabel, Label playerTwoLabel, UniformGrid playerOneButtons, UniformGrid playerTwoButtons)
        {
            _playerOneLabel = playerOneLabel;
            _playerTwoLabel = playerTwoLabel;
            _playerOneButtons = playerOneButtons;
            _playerTwoButtons = playerTwoButtons;
        }

        public void PlayerOneButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                foreach (var child in _playerOneButtons.Children)
                {
                    if (child is Button button)
                    {
                        // Reset the border for all buttons
                        button.BorderThickness = new Thickness(0);
                        button.BorderBrush = null;
                    }
                }
                // Set the border for clicked button
                clickedButton.BorderThickness = new Thickness(10);
                clickedButton.BorderBrush = Brushes.Black;
            }
        }

        public void PlayerOneButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button button && button.Background is SolidColorBrush brush)
            {
                _playerOneLabel.Foreground = brush;
            }
        }

        public void PlayerOneButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _playerOneLabel.Foreground = new SolidColorBrush(Colors.White);
        }

        public void PlayerTwoButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button button && button.Background is SolidColorBrush brush)
            {
                _playerTwoLabel.Foreground = brush;
            }
        }

        public void PlayerTwoButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _playerTwoLabel.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
