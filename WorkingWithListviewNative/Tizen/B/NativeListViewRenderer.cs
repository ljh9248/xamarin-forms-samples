using System.Collections.Generic;
using ElmSharp;
using WorkingWithListviewNative;
using WorkingWithListviewNative.Tizen;
using Xamarin.Forms.Platform.Tizen;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(NativeListView), typeof(NativeListViewRenderer))]

namespace WorkingWithListviewNative.Tizen
{
    public class NativeListViewRenderer : ViewRenderer<NativeListView, List>
    {
        List nativeList = null;

        protected override void OnElementChanged(ElementChangedEventArgs<NativeListView> e)
        {
            if (Control == null)
            {
                nativeList = new List(Forms.NativeParent)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1
                };
                SetNativeControl(nativeList);
                nativeList.ItemSelected += OnItemSelected;
            }

            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateItems(e.NewElement.Items);
            }
        }

        private void UpdateItems(IEnumerable<string> items)
        {
            nativeList.Clear();
            foreach (var itemData in items)
            {
                nativeList.Append(itemData);
            }
        }

        private void OnItemSelected(object sender, ListItemEventArgs e)
        {
            e.Item.IsEnabled = false;
            Element.NotifyItemSelected(e.Item.Text);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NativeListView.ItemsProperty.PropertyName)
            {
                UpdateItems(Element.Items);
            }
        }
    }
}
