using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        List<string> images = new List<string> { "water.jpg", "sprite.jpg", "monster.jpg", "schweppes.jpg" };
        public MainPage()
        {
            InitializeComponent();
        }
        private void picker_selected_index_changed(object _sender, EventArgs _e)
        {
            desc.Text = (_sender as Picker)?.SelectedItem.ToString();
            img.Source = images[picker.SelectedIndex];
        }
        private void stepper_value_changed(object _sender, EventArgs _e)
        {
            amount.Text = (_sender as Stepper)?.Value.ToString();
        }
        private async void Order_button_clicked(object sender, EventArgs e)
        {
            order.IsEnabled = false;
            await Navigation.PushAsync(new CartPage());
            order.IsEnabled = true;
        }
        private async void Confirm_button_clicked(object sender, EventArgs e)
        {
            CartPage.goods[picker.SelectedItem?.ToString()] = new Good()
            {
                Name = picker.SelectedItem?.ToString(),
                Count = int.Parse(amount.Text),
                PicPath = images[picker.SelectedIndex].ToString()
            };
            await DisplayAlert("Order", "Successful", "Ok");
        }
        private void amount_text_changed(object sender, TextChangedEventArgs e)
        {
            if (amount.Text.Length == 0 || amount.Text.Length >= 3)
            {
                stepper.Value = 0;
            }
            else if (amount.Text.Length > 0 && amount.Text.All(char.IsDigit))
            {
                stepper.Value = Convert.ToInt32(amount.Text);
            }
            else if (amount.Text == "-")
            {
                amount.Text = amount.Text.Remove(amount.Text.Length - 1);
            }
            if (stepper.Value == 0)
            {
                amount.Text = "";
            }
        }
    }
}
