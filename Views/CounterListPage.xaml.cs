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
        if(BindingContext is Models.CounterList counterListModel)
        {
            counterListModel.LoadCounters();
            SubHeadingText.Text = counterListModel.GetSubHeadingText();
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
        if(BindingContext is Models.CounterList counterListModel)
        {
            List<Models.Counter> counterList = counterListModel.CounterCollection.ToList();
            int newCounterId = counterList.Count > 0 ? counterList.Last().Id + 1 : 0;
            await Shell.Current.GoToAsync($"{nameof(AddCounterPage)}?{nameof(AddCounterPage.NewCounterId)}={newCounterId}");
        }
    }
}