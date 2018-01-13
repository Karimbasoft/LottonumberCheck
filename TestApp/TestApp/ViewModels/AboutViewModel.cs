using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "About";
			OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
            OpenYouTubeLink = new Command(() => Device.OpenUri(new Uri("https://www.youtube.com/channel/UC7JRRcwULLqZdQdVnjJesSA")));
            OpenGooglePlusLink = new Command(() => Device.OpenUri(new Uri("https://plus.google.com/b/104431130737246337408/104431130737246337408")));

        }

		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
		public ICommand OpenWebCommand { get; }

        public ICommand OpenYouTubeLink { get; }

        public ICommand OpenGooglePlusLink { get; }
    }
}
