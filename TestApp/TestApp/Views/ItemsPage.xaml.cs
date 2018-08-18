using System;

using Xamarin.Forms;

namespace App.UI.Views
{
	public partial class ItemsPage : ContentPage
	{
		//ItemsViewModel viewModel;

		public ItemsPage()
		{
			InitializeComponent();

			//BindingContext = viewModel = new ItemsViewModel();
		}

		//async void AddItem_Clicked(object sender, EventArgs e)
		//{
		//	await Navigation.PushAsync(new NewItemPage());
		//}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			//if (viewModel.Items.Count == 0)
			//	viewModel.LoadItemsCommand.Execute(null);
		}
	}
}
