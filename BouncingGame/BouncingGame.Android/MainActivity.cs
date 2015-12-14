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
    [Activity(Label = "BouncingGame.Android", AlwaysRetainTaskState = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait/*| ScreenOrientation.ReversePortrait*/, LaunchMode = LaunchMode.SingleInstance, MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            MainMenu();
        }

        void MainMenu()
        {
            SetContentView(Android.Resource.Layout.Menu);
            Button startGameBtn = (Button)FindViewById(Android.Resource.Id.StartGameBtn);
            Button highScoresBtn = (Button)FindViewById(Android.Resource.Id.HighScoresBtn);
            Button helpBtn = (Button)FindViewById(Android.Resource.Id.HelpBtn);
            Button quitGameBtn = (Button)FindViewById(Android.Resource.Id.QuitGameBtn);
            startGameBtn.Click += delegate
            {
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

                StartActivity(typeof(GameActivity));
            };
            highScoresBtn.Click += delegate
            {
                // Change Screen to HighScores view
                SetContentView(Android.Resource.Layout.HighScores);
                // Configure the "Return to Main Menu" Button
                Button mainMenuBtn = (Button)FindViewById(Android.Resource.Id.ScoresToMenuBtn);
                mainMenuBtn.Click += delegate
                {
                    // Return to the Main Menu
                    MainMenu();
                };
            };
            helpBtn.Click += delegate
            {
                // Change Screen to Help view
                SetContentView(Android.Resource.Layout.Help);
                // Configure the "Return to Main Menu" Button
                Button mainMenuBtn = (Button)FindViewById(Android.Resource.Id.HelpToMenuBtn);
                mainMenuBtn.Click += delegate
                {
                    // Return to the Main Menu
                    MainMenu();
                };
            };
            quitGameBtn.Click += delegate
            {
                // Close the App
                Finish();
            };
        }
    }
}

