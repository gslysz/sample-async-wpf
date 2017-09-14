using System;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Drawing;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace LedLightControl.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        #region Constructors And Destructors

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ToggleLightCommand = new RelayCommand(ToggleLight);

            ChangeLightColorCommand = new RelayCommand(ChangeLedLightColor);

            IsLightDisplayed = true;
            LedLightColor = Colors.Gold;

            _knownColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));

        }

        private void ChangeLedLightColor()
        {
            Random randomGen = new Random();
            KnownColor randomColorName = _knownColors[randomGen.Next(_knownColors.Length)];
            Color color = Color.FromKnownColor(randomColorName);

            System.Windows.Media.Color newColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);

            LedLightColor = newColor;
        }

        #endregion

        #region Commands

        public ICommand ToggleLightCommand { get; set; }

        #endregion

        #region Properties

        public bool IsLightDisplayed
        {
            get => _isLightDisplayed;
            set
            {
                _isLightDisplayed = value;
                RaisePropertyChanged();
            }
        }

        public System.Windows.Media.Color LedLightColor
        {
            get => _ledLightColor;
            set
            {
                _ledLightColor = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ChangeLightColorCommand { get; set; }

        #endregion

        #region Non-Public Methods

        private void ToggleLight()
        {
            IsLightDisplayed = !IsLightDisplayed;
        }

        #endregion

        #region Fields

        private bool _isLightDisplayed;
        private System.Windows.Media.Color _ledLightColor;
        private KnownColor[] _knownColors;

        #endregion
    }
}