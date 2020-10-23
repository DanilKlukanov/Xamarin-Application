using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    public class Good
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string PicPath { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public static SortedDictionary<string, Good> goods = new SortedDictionary<string, Good>();
        public CartPage()
        {
            InitializeComponent();
            goods_list.ItemsSource = goods.Select((thing) => { return thing.Value; }).ToList();
        }
        private void Delete_button(object _sender, EventArgs _e)
        {
            var item = (Xamarin.Forms.Button)_sender;
            var listitem = (from itm in goods
                            where itm.Value.Name == item.CommandParameter.ToString()
                            select itm).ToList().First();
            goods.Remove(listitem.Key);

            var buf = goods.Select((a) => { return a.Value; }).ToList();
            goods_list.ItemsSource = buf;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            if (goods.Count() > 0 && await DisplayAlert("Order", "Are you sure?", "Yes", "No"))
            {
                goods.Clear();
                await DisplayAlert("Oreder", "Ordered", "Ok");
                await Navigation.PopAsync();
;            }
        }
    }
}