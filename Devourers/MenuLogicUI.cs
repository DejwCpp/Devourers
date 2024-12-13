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

        private Brush _playerOneBrush;
        private Brush _playerTwoBrush;

        private Brush _playerOneTempBrush;
        private Brush _playerTwoTempBrush;

        private StackPanel _labelStackPanel;
        private StackPanel _buttonsStackPanel;

        public MenuLogicUI(Label playerOneLabel, Label playerTwoLabel,
                           UniformGrid playerOneButtons, UniformGrid playerTwoButtons,
                           StackPanel labelStackPanel, StackPanel buttonsStackPanel)
        {
            _playerOneLabel = playerOneLabel;
            _playerTwoLabel = playerTwoLabel;
            _playerOneButtons = playerOneButtons;
            _playerTwoButtons = playerTwoButtons;
            _labelStackPanel = labelStackPanel;
            _buttonsStackPanel = buttonsStackPanel;
        }

        /* Player one buttons */

        public void PlayerOneButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                _playerOneTempBrush = clickedButton.Background;

                if (IsTheSameColor(_playerOneTempBrush, _playerTwoBrush))
                {
                    return;
                }

                _playerOneBrush = _playerOneTempBrush;

                if (_playerOneBrush != null && _playerTwoBrush != null)
                {
                    DisplaySection();
                }

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
                clickedButton.BorderThickness = new Thickness(15);
                clickedButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222831"));
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
            if (_playerOneBrush == null)
            {
                _playerOneLabel.Foreground = new SolidColorBrush(Colors.White);
                return;
            }

            if (IsTheSameColor(_playerOneBrush, _playerTwoBrush))
            {
                _playerOneLabel.Foreground = _playerOneBrush;
                return;
            }

            _playerOneLabel.Foreground = _playerOneBrush;
        }

        /* Player two buttons */

        public void PlayerTwoButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                _playerTwoTempBrush = clickedButton.Background;

                if (IsTheSameColor(_playerTwoTempBrush, _playerOneBrush))
                {
                    return;
                }

                _playerTwoBrush = _playerTwoTempBrush;

                if (_playerOneBrush != null && _playerTwoBrush != null)
                {
                    DisplaySection();
                } 

                foreach (var child in _playerTwoButtons.Children)
                {
                    if (child is Button button)
                    {
                        // Reset the border for all buttons
                        button.BorderThickness = new Thickness(0);
                        button.BorderBrush = null;
                    }
                }
                // Set the border for clicked button
                clickedButton.BorderThickness = new Thickness(15);
                clickedButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222831"));
            }
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
            if (_playerTwoBrush == null)
            {
                _playerTwoLabel.Foreground = new SolidColorBrush(Colors.White);
                return;
            }

            if (IsTheSameColor(_playerOneBrush, _playerTwoBrush))
            {
                _playerTwoLabel.Foreground = _playerTwoBrush;
                return;
            }

            _playerTwoLabel.Foreground = _playerTwoBrush;
        }

        public Brush GetPlayerOneBrush()
        {
            return _playerOneBrush;
        }

        public Brush GetPlayerTwoBrush()
        {
            return _playerTwoBrush;
        }

        private bool IsTheSameColor(Brush first, Brush second)
        {
            return (first is SolidColorBrush brush1 &&
                    second is SolidColorBrush brush2 &&
                    brush1.Color == brush2.Color);
        }

        private void DisplaySection()
        {
            _labelStackPanel.Visibility = Visibility.Visible;
            _buttonsStackPanel.Visibility = Visibility.Visible;
        }
    }
}