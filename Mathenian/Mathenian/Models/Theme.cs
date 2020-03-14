using Prism.Mvvm;
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

        private int _type;
        public int Type { get => _type; set => _type = value; }

        private Color _textColor;
        public Color TextColor { get => _textColor; set { SetProperty(ref _textColor, value); } }

        private Color _backgroundColor;
        public Color BackgroundColor { get => _backgroundColor; set { SetProperty(ref _backgroundColor, value); } }

        public Theme()
        {
            TextColor = TextColorLight;
            BackgroundColor = BackgroundColorLight;
            _type = 0;
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
            _type = theme;
        }
    }
}
