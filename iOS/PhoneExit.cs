using PhoneHKS.iOS;
using Xamarin.Forms;


[assembly: Dependency(typeof(PhoneExit))]
namespace PhoneHKS.iOS
{
    public class PhoneExit : ExitApp
    {
        public void closeApplication()
        {
			//NSThread.Exit();
			//throw new System.Exception();

        }
    }
}
