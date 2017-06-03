using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PhoneHKS
{
	public partial class App : Application
	{
        public static IList<string> PhoneNumbers { get; set; }
		public App()
		{
			InitializeComponent();
            PhoneNumbers = new List<string>();
            //This create a NavigationPage
            MainPage = new NavigationPage(new MainPage());
            //Build
			//MainPage = new MainPage(); //This is just a standard page
		}

		protected override void OnStart()
		{
			MobileCenter.Start("android=690edbac-6915-4c1c-bfc2-0a204dc20aeb;", typeof(Analytics), typeof(Crashes));
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
