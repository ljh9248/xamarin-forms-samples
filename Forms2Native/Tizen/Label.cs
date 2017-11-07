using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace SimpleColorPicker.Tizen
{
    public class Label : ElmSharp.Label
    {
        public Label() : base(Program.ElmWindow)
        {
            if(Device.Idiom == TargetIdiom.Phone)
            {
                TextStyle = "DEFAULT = 'color=#000000FF font_style=oblique font_size=40 font_weight=semibold font_width=expanded align=center'";
            }
            else if(Device.Idiom == TargetIdiom.TV)
            {
                TextStyle = "DEFAULT = 'color=#FFFFFF font_style=oblique font_size=40 font_weight=semibold font_width=expanded align=center'";
            }
            else
            {
                TextStyle = "DEFAULT = 'color=#FFFFFF font_style=oblique font_size=15 font_weight=semibold font_width=expanded align=center'";
            }
        }
    }
}