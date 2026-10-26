namespace Counter.Models;

class AddCounter
{
    public string IdText => "Id:";
    public string NameText => "Name:";
    public string CountOriginText => "Starting Number:";
    public string ColorText => "Color:";
    public string RedSliderText => "Red";
    public string GreenSliderText => "Green";
    public string BlueSliderText => "Blue";
    public string ReturnButtonText => "RETURN";
    public string SaveButtonText => "SAVE";

    public string EmptyNameErrorText => "You must name your counter!";
    public string EmptyCountOriginErrorText => "You must specify the count origin of your counter!";
    public string InvalidCountOriginValueErrorText => "You provided an illegal value for count origin of your counter!";

    public Color SlidersColor { get; set; } = Color.FromRgb(0, 0, 0);
}
