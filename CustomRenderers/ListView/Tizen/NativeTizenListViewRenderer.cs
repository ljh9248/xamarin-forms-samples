using System.Collections.Generic;
using System.ComponentModel;
using CustomRenderer;
using CustomRenderer.Tizen;
using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using NListView = Xamarin.Forms.Platform.Tizen.Native.ListView;

[assembly: ExportRenderer(typeof(NativeListView), typeof(NativeTizenListViewRenderer))]
namespace CustomRenderer.Tizen
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
                UpdateItems(((NativeListView)e.NewElement).Items);
            }
        }

        private void UpdateItems(IEnumerable<DataSource> items)
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
            ((NativeListView)Element).NotifyItemSelected(e.Item.Data);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NativeListView.ItemsProperty.PropertyName)
            {
                UpdateItems(((NativeListView)Element).Items);
            }
        }
    }
}
