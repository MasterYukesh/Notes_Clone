using Notes_Clone.Localdb;
using Notes_Clone.View;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes_Clone
{
    public partial class App : Application
    {
        static Database localdatabase;
        public static Database Localdatabase
        {
            get
            {
                if (localdatabase == null)
                {
                   localdatabase = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Notes_Clone.db3"));
                }
                return localdatabase;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
