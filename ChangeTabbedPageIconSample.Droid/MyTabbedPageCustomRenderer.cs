using System;
using System.Linq;

using Android.Support.Design.Widget;

using ChangeTabbedPageIconSample;
using ChangeTabbedPageIconSample.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MyTabbedPage), typeof(MyTabbedPageCustomRenderer))]
namespace ChangeTabbedPageIconSample.Droid
{
	public class MyTabbedPageCustomRenderer : TabbedPageRenderer, TabLayout.IOnTabSelectedListener
	{
		public MyTabbedPageCustomRenderer(Android.Content.Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{
			base.OnElementChanged(e);

			Element.CurrentPageChanged += HandleCurrentPageChanged;
		}

		void HandleCurrentPageChanged(object sender, EventArgs e)
		{
			var currentNavigationPage = Element.CurrentPage as NavigationPage;
			if (!(currentNavigationPage.RootPage is PuzzlePage))
				return;

			var tabLayout = (TabLayout)ViewGroup.GetChildAt(1);

			for (int i = 0; i < tabLayout.TabCount; i++)
			{
				var currentTab = tabLayout.GetTabAt(i);
				var currentTabText = currentTab.Text;

				if (currentTabText.Equals("Puzzle") || currentTabText.Equals("Settings")){
					Device.BeginInvokeOnMainThread(() => UpdateTab(currentTabText, currentTab, currentNavigationPage));
					break;
				}
			}
		}

		void TabLayout.IOnTabSelectedListener.OnTabReselected(TabLayout.Tab tab)
		{
			System.Diagnostics.Debug.WriteLine("Tab Reselected");

			var mainPage = Application.Current.MainPage as MyTabbedPage;

			var currentNavigationPage = mainPage.CurrentPage as NavigationPage;

			if (currentNavigationPage.RootPage is PuzzlePage)
				Device.BeginInvokeOnMainThread(() => UpdateTab(currentNavigationPage.Title, tab, currentNavigationPage));
		}

		void UpdateTab(string currentTabText, TabLayout.Tab tab, NavigationPage currentNavigationPage)
		{
			if (currentTabText.Equals("Puzzle"))
			{
				tab.SetIcon(IdFromTitle("Settings", ResourceManager.DrawableClass));
				currentNavigationPage.Title = "Settings";
			}
			else
			{
				tab.SetIcon(IdFromTitle("Puzzle", ResourceManager.DrawableClass));
				currentNavigationPage.Title = "Puzzle";
			}
		}

		int IdFromTitle(string title, Type type)
		{
			string name = System.IO.Path.GetFileNameWithoutExtension(title);
			int id = GetId(type, name);
			return id;
		}

		int GetId(Type type, string memberName)
		{
			object value = type.GetFields().FirstOrDefault(p => p.Name == memberName)?.GetValue(type)
				?? type.GetProperties().FirstOrDefault(p => p.Name == memberName)?.GetValue(type);
			if (value is int)
				return (int)value;
			return 0;
		}
	}
}