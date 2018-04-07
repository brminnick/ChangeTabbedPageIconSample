using ChangeTabbedPageIconSample;
using ChangeTabbedPageIconSample.iOS;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyTabbedPage), typeof(MyTabbedPageCustomRenderer))]
namespace ChangeTabbedPageIconSample.iOS
{
    public class MyTabbedPageCustomRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (ViewController is UITabBarController tabbarController)
                tabbarController.ViewControllerSelected += OnTabBarReselected;
        }

        void OnTabBarReselected(object sender, UITabBarSelectionEventArgs e)
        {
            var tabbedPage = Xamarin.Forms.Application.Current.MainPage as MyTabbedPage;

            var currentNavigationPage = tabbedPage.CurrentPage as NavigationPage;

            if (currentNavigationPage.RootPage is PuzzlePage)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (currentNavigationPage.Title.Equals("Puzzle"))
                    {
                        currentNavigationPage.Icon = "Settings";
                        currentNavigationPage.Title = "Settings";
                    }
                    else
                    {
                        currentNavigationPage.Icon = "Puzzle";
                        currentNavigationPage.Title = "Puzzle";
                    }
                });
            }
        }
    }
}
