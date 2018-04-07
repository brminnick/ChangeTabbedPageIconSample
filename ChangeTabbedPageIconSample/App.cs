using System;

using Xamarin.Forms;

namespace ChangeTabbedPageIconSample
{
	public class App : Application
	{
		public App() => MainPage = new MyTabbedPage();
	}

	public class MyTabbedPage : TabbedPage
	{
		public MyTabbedPage()
		{
			Children.Add(new NavigationPage(new StarPage())
			{
				Icon = "Score",
				Title = "Star"
			});
			Children.Add(new NavigationPage(new PuzzlePage())
			{
				Icon = "Puzzle",
				Title = "Puzzle"
			});

		}
	}

	public class StarPage : ContentPage
	{
	}

	public class PuzzlePage : ContentPage
	{
	}
}
