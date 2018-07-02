using System.Collections.Generic;
using System.ComponentModel;
using WorkingWithListviewNative;
using WorkingWithListviewNative.Tizen;
using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using NListView = Xamarin.Forms.Platform.Tizen.Native.ListView;

[assembly: ExportRenderer(typeof(NativeListView2), typeof(NativeTizenListViewRenderer))]
namespace WorkingWithListviewNative.Tizen
{
    public class NativeTizenListViewRenderer : ListViewRenderer
    {
        NListView nativeList = null;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            if (Control == null)
            {
                nativeList = new NListView(Forms.NativeParent)
                {
                    SelectionMode = GenItemSelectionMode.Always,
                };
                SetNativeControl(nativeList);
                nativeList.ItemSelected += OnItemSelected;
            }

            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateItems(((NativeListView2)e.NewElement).Items);
            }
        }

        private void UpdateItems(IEnumerable<DataSource2> items)
        {
            nativeList.Clear();
            foreach (var itemData in items)
            {
                var contentItem = new NativeContent();
                nativeList.Append(contentItem.Class, itemData);
            }
        }

        private void OnItemSelected(object sender, GenListItemEventArgs e)
        {
            ((NativeListView2)Element).NotifyItemSelected(e.Item.Data);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NativeListView2.ItemsProperty.PropertyName)
            {
                UpdateItems(((NativeListView2)Element).Items);
            }
        }
    }
}
