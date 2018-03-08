using System.Windows;
using System.Windows.Input;

namespace Fasetto.Word
{
    class WindowViewModel : BaseViewModel
    {
        #region Private Member
        /// <summary>
        /// The window this view model control
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The margin arround the window to allow drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// The angle radius
        /// </summary>
        private int mWindowRadius = 10;

        #endregion

        #region Public Property

        public int WindowMinimumWidht { get; set; } = 400;

        public int WindowMinimumHeight { get; set; } = 300;

        public bool BorderLess { get => mWindow.WindowState == WindowState.Maximized; }
        /// <summary>
        /// Size of the borders arround
        /// </summary>
        public int ResizeBorder { get => BorderLess ? 0 : 6; }

        public Thickness ResizeBorderThickness { get => new Thickness(ResizeBorder + OuterMarginSize); }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);
        
        /// <summary>
        /// The margin arround the window to allow drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get => new Thickness(OuterMarginSize); }

        /// <summary>
        /// The angle radius
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get => new CornerRadius(WindowRadius); }

        public int TitleHight { get; set; } = 42;

        public GridLength TitleHightGridLenght { get => new GridLength(TitleHight + ResizeBorder); }

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login;
        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to menu the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            //Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(TitleHightGridLenght));
            };

            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, mWindow.PointToScreen(Mouse.GetPosition(mWindow))));

            //Fix window resize issue
            var resizer = new WindowResizer(mWindow); 
        }

        #endregion
    }
}
