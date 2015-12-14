using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Microsoft.Xna.Framework;
using CocosSharp;
using Android.Widget;

namespace BouncingGame
{
    [Activity(Label = "BouncingGame.Android", AlwaysRetainTaskState = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait/*| ScreenOrientation.ReversePortrait*/, LaunchMode = LaunchMode.SingleInstance, MainLauncher = false, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class GameActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //Xamarin.Insights.Initialize (XamarinInsights.ApiKey, this);
            //base.OnCreate (bundle);
            //var application = new CCApplication ();
            //application.ApplicationDelegate = new GameAppDelegate ();
            //SetContentView (application.AndroidContentView);
            //application.StartGame ();
            base.OnCreate(bundle);
            var application = new CCApplication();
            application.ApplicationDelegate = new GameAppDelegate();
            SetContentView(application.AndroidContentView);
            application.StartGame();
            //MainMenu ();
        }

        public override void OnBackPressed()
        {
            Finish();
        }

        /** THIS CODE IS NOW IN MainActivity
		void MainMenu ()
		{
			SetContentView (Android.Resource.Layout.Menu);
			Button startGameBtn = (Button)FindViewById (Android.Resource.Id.StartGameBtn);
			Button highScoresBtn = (Button)FindViewById (Android.Resource.Id.HighScoresBtn);
			Button helpBtn = (Button)FindViewById (Android.Resource.Id.HelpBtn);
			Button quitGameBtn = (Button)FindViewById (Android.Resource.Id.QuitGameBtn);
			startGameBtn.Click += delegate {
				// Change screen to Main Game view
				//SetContentView(Android.Resource.Layout.Main);
				// Configure the "Return to Main Menu" Button
				//Button mainMenuBtn = (Button)FindViewById(Android.Resource.Id.GameViewToMenuBtn);
				//mainMenuBtn.Click += delegate {
				// Return to the Main Menu
				//MainMenu();
				//};
				// Get our game view from the layout resource,
				// and attach the view created event to it
				//CCGameView gameView = (CCGameView)FindViewById (Android.Resource.Id.GameView);
				//gameView.ViewCreated += LoadGame;

			};
			highScoresBtn.Click += delegate {
				// Change Screen to HighScores view
				SetContentView (Android.Resource.Layout.HighScores);
				// Configure the "Return to Main Menu" Button
				Button mainMenuBtn = (Button)FindViewById (Android.Resource.Id.ScoresToMenuBtn);
				mainMenuBtn.Click += delegate {
					// Return to the Main Menu
					MainMenu ();
				};
			};
			helpBtn.Click += delegate {
				// Change Screen to Help view
				SetContentView (Android.Resource.Layout.Help);
				// Configure the "Return to Main Menu" Button
				Button mainMenuBtn = (Button)FindViewById (Android.Resource.Id.HelpToMenuBtn);
				mainMenuBtn.Click += delegate {
					// Return to the Main Menu
					MainMenu ();
				};
			};
			quitGameBtn.Click += delegate {
				// Close the App
				Finish ();
			};
		}
		/*void LoadGame (object sender, EventArgs e)
		{
			CCGameView gameView = sender as CCGameView;
			if (gameView != null) {
				var contentSearchPaths = new List<string> () {
					"Fonts",
					"Sounds"
				};
				CCSizeI viewSize = gameView.ViewSize;
				int width = 1024;
				int height = 768;
				// Set world dimensions
				gameView.DesignResolution = new CCSizeI (width, height);
				// Determine whether to use the high or low def versions of our images
				// Make sure the default texel to content size ratio is set correctly
				// Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
				contentSearchPaths.Add("Images");
				if (width < viewSize.Width) {
					contentSearchPaths.Add ("Images/Hd");
					CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
				} else {
					contentSearchPaths.Add ("Images/Ld");
					CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
				}
				gameView.ContentManager.SearchPaths = contentSearchPaths;
				CCScene gameScene = new CCScene (gameView);
				gameScene.AddLayer (new GameLayer ());
				gameView.RunWithScene (gameScene);
			}
		}*/

    }
}
