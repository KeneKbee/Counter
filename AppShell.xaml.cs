namespace Counter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.CounterPage), typeof(Views.CounterPage));
            Routing.RegisterRoute(nameof(Views.AddCounterPage), typeof(Views.AddCounterPage));
        }
    }
}
