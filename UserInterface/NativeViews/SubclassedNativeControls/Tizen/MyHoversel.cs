using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace SubclassedNativeControls.Tizen
{
    class MyHoversel : ElmSharp.Hoversel
    {
        IList<string> items;
        string selectedItem;

        public IList<string> ItemsSource
        {
            get { return items; }
            set
            {
                if (items != value)
                {
                    items = value;
                    foreach (string item in items)
                    {
                        this.AddItem(item);
                    }
                }
            }
        }

        public string SelectedItem
        {
            get { return selectedItem; }
            internal set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    UpdateSelectedItem();
                }
            }
        }

        public MyHoversel () : base(Program.ElmWindow)
        {
            HoverParent = Program.ElmWindow;
            if (Device.Idiom == TargetIdiom.Phone)
                Color = ElmSharp.Color.Black;
            IsHorizontal = false;

            UpdateSelectedItem();
            
            ItemSelected += (s, e) =>
            {
                SelectedItem = e.Item.Label;
            };
        }

        void UpdateSelectedItem()
        {
            Text = SelectedItem;
        }
    }
}
