namespace Counter.Views;

[QueryProperty(nameof(NewCounterId), nameof(NewCounterId))]
public partial class AddCounterPage : ContentPage
{
    public int NewCounterId { set { IdEntry.Text = value.ToString(); } }
	public AddCounterPage()
	{
		InitializeComponent();
    }

    private string[] GetUserDataErrors()
    {
        List<string> errors = new();

        if(BindingContext is Models.AddCounter model)
        {
            bool isNameEmpty = string.IsNullOrWhiteSpace(NameEntry.Text);
            bool isCountOriginEmpty = string.IsNullOrEmpty(CountOriginEntry.Text);
            bool isCountOriginValid = long.TryParse(CountOriginEntry.Text, out _);
            
            if (isNameEmpty)
            {
                errors.Add(model.EmptyNameErrorText);
            }

            if (isCountOriginEmpty)
            {
                errors.Add(model.EmptyCountOriginErrorText);
            }

            
            if (!isCountOriginValid && !isCountOriginEmpty)
            {
                errors.Add(model.InvalidCountOriginValueErrorText);
            }
        }

        return errors.ToArray();
    }

    private async void ReturnButton_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string[] errors = GetUserDataErrors();
        if(errors.Length > 0)
        {
            await DisplayAlert("Incorrect data provided!", $"You have provided incorrect data:\n{string.Join("\n", errors)}", "OK");
            return;
        }

        int id = int.Parse(IdEntry.Text);
        string name = NameEntry.Text;
        long counterOrigin = long.Parse(CountOriginEntry.Text);
        Color color = ((Models.AddCounter)BindingContext).SlidersColor;

        Models.Counter.CreateFile(id, name, counterOrigin, color);

        await Shell.Current.GoToAsync($"../{nameof(CounterPage)}?{nameof(CounterPage.Filename)}={name}_{id}.json");
    }

    private void ColorSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if(BindingContext is Models.AddCounter model)
        {
            model.SlidersColor = Color.FromRgb((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value);

            SliderGrid.Background = model.SlidersColor;
        }
    }

    private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        char[] illegalChars = ((Models.AddCounter)BindingContext).IllegalNameChars;
        string text = e.NewTextValue;
        if(sender is Entry entry)
        {
            text = new string(text.Where(c => !illegalChars.Contains(c)).ToArray());
            entry.Text = text;
        }
    }

    private void NumericEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = e.NewTextValue;
        if (string.IsNullOrEmpty(text) || text == e.OldTextValue) return;

        bool isTextValid = long.TryParse(text, out _);
        if (!isTextValid && text != "-")
        {
            if(sender is Entry entry)
            {
                entry.Text = e.OldTextValue;
            }
        }
    }
}