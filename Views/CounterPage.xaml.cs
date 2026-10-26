namespace Counter.Views;

[QueryProperty(nameof(Filename), "Filename")]
public partial class CounterPage : ContentPage
{
    public string Filename { set => LoadCounter(value); }
	public CounterPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        SetColorSlidersValue();
        base.OnAppearing();
    }

    private void LoadCounter(string filename)
    {
        BindingContext = Models.Counter.LoadFromFilename(filename);
        SetColorSlidersValue();
    }

    private void UpdateCounterCount()
    {
        CountLabel.Text = ((Models.Counter)BindingContext).Count.ToString();
    }

    private void UpdateCounterColor()
    {
        CountLabel.TextColor = ((Models.Counter)BindingContext).CounterColor;
    }

    private void SetColorSlidersValue()
    {
        Color counterColor = ((Models.Counter)BindingContext).CounterColor;

        RedSlider.Value = counterColor.Red * 255;
        GreenSlider.Value = counterColor.Green * 255;
        BlueSlider.Value = counterColor.Blue * 255;
    }

    private void DecrementButton_Clicked(object sender, EventArgs e)
    {
        ((Models.Counter) BindingContext).Decrement();
        UpdateCounterCount();
    }

    private void IncrementButton_Clicked(object sender, EventArgs e)
    {
        ((Models.Counter)BindingContext).Increment();
        UpdateCounterCount();
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        ((Models.Counter)BindingContext).Reset();
        UpdateCounterCount();
    }

    private async void DeleteButton_Clicked(Object sender, EventArgs e)
    {
        if (BindingContext is Models.Counter counter)
        {
            string counterName = counter.Name;
            bool shouldFileBeDeleted = await DisplayAlert($"Deletion of \"{counterName}\"", $"Warning!\nDeleting \"{counterName}\" cannot be reversed!\nDo you wish to proceed?", "Proceed", "Cancel");

            if (shouldFileBeDeleted)
            {
                counter.Delete();
                await Shell.Current.GoToAsync("..");
            }
        }
    }

    private void ColorSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        ((Models.Counter)BindingContext).SetColorRgb((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value);
        UpdateCounterColor();
    }
}