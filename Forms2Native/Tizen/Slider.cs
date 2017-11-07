using Xamarin.Forms.Platform.Tizen;

namespace SimpleColorPicker.Tizen
{
    public class Slider : ElmSharp.Slider
    {
        public Slider() : base(Program.ElmWindow)
        {
            ValueChanged += SliderValueChanged;
        }

        private void SliderValueChanged(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("SliderValueChanged called, value: " + Value);
        }
    }
}
