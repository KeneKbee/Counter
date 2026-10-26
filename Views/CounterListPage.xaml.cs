namespace Counter.Views;

public partial class CounterListPage : ContentPage
{
	public CounterListPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(BindingContext is Models.CounterList model)
        {
            model.LoadCounters();
            SubHeadingText.Text = model.GetSubHeadingText();
        }
    }

    private async void CounterCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection.Count == 0)
        {
            return;
        }

        var counter = (Models.Counter) e.CurrentSelection[0];
        await Shell.Current.GoToAsync($"{nameof(CounterPage)}?{nameof(CounterPage.Filename)}={counter.Filename}");
    }

    private async void AddCounterButton_Clicked(object sender, EventArgs e)
    {
        if(BindingContext is Models.CounterList model)
        {
            List<Models.Counter> CounterList = model.CounterCollection.ToList();
            int newCounterId = CounterList.Count > 0 ? CounterList.Last().Id + 1 : 0;
            await Shell.Current.GoToAsync($"{nameof(AddCounterPage)}?{nameof(AddCounterPage.NewCounterId)}={newCounterId}");
        }
    }
}