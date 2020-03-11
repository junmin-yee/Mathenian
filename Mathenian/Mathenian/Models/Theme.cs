using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mathenian.Models
{
    public class Theme : BindableBase
    {
        // Light theme colors
        public static readonly Color TextColorLight = Color.Gray;
        public static readonly Color BackgroundColorLight = Color.White;

        // Dark theme colors
        public static readonly Color TextColorDark = Color.White;
        public static readonly Color BackgroundColorDark = Color.FromRgb(23, 23, 23);

        private Color _textColor;
        public Color TextColor { get => _textColor; set { SetProperty(ref _textColor, value); } }

        private Color _backgroundColor;
        public Color BackgroundColor { get => _backgroundColor; set { SetProperty(ref _backgroundColor, value); } }

        public Theme()
        {
            _textColor = TextColorLight;
            _backgroundColor = BackgroundColorLight;
        }

        public Theme(int theme)
        {
            UpdateTheme(theme);
        }

        public void UpdateTheme(int theme)
        {
            if (theme == 0)
            {
                TextColor = TextColorLight;
                BackgroundColor = BackgroundColorLight;
            }
            else if (theme == 1)
            {
                TextColor = TextColorDark;
                BackgroundColor = BackgroundColorDark;
            }
        }
    }
}
