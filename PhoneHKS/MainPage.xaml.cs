using System;
using Xamarin.Forms;

namespace PhoneHKS
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;

        public MainPage()
        {
            Title = "Call by Alphabet !";
            //Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
            InitializeComponent();
            SetupLayout();
            RunDateClockLabel();

		}

        void SetupLayout()
        {
            //mainStackLayout.BackgroundColor = Color.SkyBlue;
            //MainPage.BackgroundColorProperty = Color.Silver;
            fontSizeLabel.TextColor = Color.Gray;
			fontSizeLabel.FontSize = 25;
			fontSizeLabel.VerticalTextAlignment = TextAlignment.Center;
			fontSizeLabel.HorizontalTextAlignment = TextAlignment.Center;
			phoneNumberText.HorizontalTextAlignment = TextAlignment.Center;
			//phoneNumberText.FontSize = 35;
			translateButon.BackgroundColor = Color.LightGreen;
			callButton.BackgroundColor = Color.DarkOrange;
            callHistoryButton.BackgroundColor = Color.Yellow;
            clearButton.BackgroundColor = Color.DarkRed;
			//translateButon.FontSize = 25;
            callButton.FontSize = 25;
            //clearButton.FontSize = 25;
			translateButon.TextColor = Color.White;
			callButton.TextColor = Color.White;
            clearButton.TextColor = Color.White;
            callHistoryButton.TextColor = Color.White;

			//dateLabel.FontSize = 25;
			dateLabel.HorizontalTextAlignment = TextAlignment.Center;
			dateLabel.FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
			dateLabel.TextColor = Color.DarkGreen;
			//clockLabel.FontSize = 25;
			clockLabel.HorizontalTextAlignment = TextAlignment.Center;
			clockLabel.FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
			clockLabel.TextColor = Color.DarkGreen;

            //it must be the bigger value first
            clockSlider.Maximum = 50;
            clockSlider.Minimum = 1;
            clockSlider.Value = 25.0;

        }

        void RunDateClockLabel()
        {
            dateLabel.Text = DateTime.Today.ToString("yy-MMM-dd ddd");

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                clockLabel.Text = DateTime.Now.ToString("h:mm:ss tt");
                return true;
            });
         }

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            fontSizeLabel.Text = string.Format("Font Size: {0:F2}", e.NewValue);
            dateLabel.FontSize = e.NewValue;
            clockLabel.FontSize = e.NewValue;
            translateButon.FontSize = e.NewValue;
			callButton.FontSize = e.NewValue;
            callHistoryButton.FontSize = e.NewValue;
			clearButton.FontSize = e.NewValue;
            phoneNumberText.FontSize = e.NewValue;
            //throw new NotImplementedException();
        }

        async void OnClear(object sender, EventArgs e)
        {
            phoneNumberText.Text = null;
            phoneNumberText.Focus();
            callButton.IsEnabled = false;
            clockSlider.Value = 25.0;
			await DisplayAlert("All cleared up!", "Click 'dismiss' to dismiss", "dismiss");
        }

		async void OnExit(object sender, EventArgs e)
		{
			if (await this.DisplayAlert(
					"Finished?",
					"Would you like to exit now?",
					"Yes",
					"No"))
			{
				var exitnow = DependencyService.Get<ExitApp>();
				if (exitnow != null)
					exitnow.closeApplication();
			}
		}

        async void OnCallHistory(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new CallHistoryPage());
        }

        void OnTranslate(object sender, EventArgs e)
		{
			translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
			if (!string.IsNullOrWhiteSpace(translatedNumber))
			{
				callButton.IsEnabled = true;
				callButton.Text = "Call " + translatedNumber;
			}
			else
			{
				callButton.IsEnabled = false;
				callButton.Text = "Call";
			}
		}

		async void OnCall(object sender, EventArgs e)
		{
			if (await this.DisplayAlert(
					"Dial a Number",
					"Would you like to call " + translatedNumber + "?",
					"Yes",
					"No"))
			{
				var dialer = DependencyService.Get<IDialer>();
				if (dialer != null)
					dialer.Dial(translatedNumber);
			}
		}
	}
}
