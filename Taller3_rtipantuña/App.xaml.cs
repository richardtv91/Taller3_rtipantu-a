using Taller3_rtipantuña.Views;

namespace Taller3_rtipantuña
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
              
            return new Window(new NavigationPage(new Views.Home()));
        }
    }
}