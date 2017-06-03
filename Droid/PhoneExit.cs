using Android.Content;
using Android.Telephony;
using PhoneHKS.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneExit))]
namespace PhoneHKS.Droid
{
    public class PhoneExit : ExitApp
    {
        public void closeApplication()
        {
            //var activity = (MainActivity)Forms.Context;
            //activity.FinishAffinity();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
