using Notes_Clone.Localdb;
using Notes_Clone.View;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Drawing;
using Xamarin.Essentials;
using Notes_Clone.FireBase;

namespace Notes_Clone.ViewModel
{
    public class NotePageVM : BaseVM
    {
        private System.Drawing.Color lightyellow { get; set; }
        private System.Drawing.Color lightsalmon { get; set; }
        private System.Drawing.Color lightpink { get; set; }
        private System.Drawing.Color lightgreen { get; set; }
        private Notes Note;
        private string colors { get; set; }
        private string option { get; set; }
        private string title { get; set; }
        private string info { get; set; }
        private string date { get; set; }
        private string title_color { get; set; }
        private string info_color { get; set; }
        public ICommand Save_Note { get; set; }
        public ICommand Backbutton { get; set; }
        public ICommand Delete_Note { get; set; }
        public ICommand Choose_Colourgreen { get; set; }
        public ICommand Choose_Colouryellow { get; set; }
        public ICommand Choose_Colourred { get; set; }
        public ICommand Choose_Colourpink { get; set; }
        public string Title_color
        {
            get
            {
                return title_color;
            }
            set
            {
                title_color = value;
                OnPropertyChanged();
            }
        }
        public string Info_color
        {
            get
            {
                return info_color;
            }
            set
            {
                info_color = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                OnPropertyChanged();
            }
        }
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
        public string Option
        {
            get
            {
                return option;
            }
            set
            {
                option = value;
                OnPropertyChanged();
            }
        }
        public System.Drawing.Color Lightgreen
        {
            get
            {
                return lightgreen;
            }
            set
            {
                lightgreen = value;
                OnPropertyChanged();
            }
        }
        public System.Drawing.Color Lightyellow
        {
            get
            {
                return lightyellow;
            }
            set
            {
                lightyellow = value;
                OnPropertyChanged();
            }
        }
        public System.Drawing.Color Lightsalmon
        {
            get
            {
                return lightsalmon;
            }
            set
            {
                lightsalmon = value;
                OnPropertyChanged();
            }
        }
        public System.Drawing.Color Lightpink
        {
            get
            {
                return lightpink;
            }
            set
            {
                lightpink = value;
                OnPropertyChanged();
            }
        }
        public string Colors
        {
            get
            {
                return colors;
            }
            set
            {
                colors = value;
                OnPropertyChanged();
            }
        }
        public NotePageVM(Notes x)
        {
            Lightgreen = System.Drawing.Color.LightGreen;
            Lightyellow = System.Drawing.Color.LightYellow;
            Lightpink = System.Drawing.Color.LightPink;
            Lightsalmon = System.Drawing.Color.LightSalmon;
            Backbutton = new Command(backbutton);
            Save_Note = new Command(savenote);
            Choose_Colourgreen = new Command(chose_colorgreen);
            Choose_Colourred = new Command(choose_Colourred);
            Choose_Colourpink = new Command(choose_Colourpink);
            Choose_Colouryellow = new Command(choose_Colouryellow);
            Note = x;
            if (x != null)
            {
                Option = "Update";
                Title = x.TITLE;
                Info = x.INFO;
                Date = x.DATE;
                Colors = x.COLOR;
                Delete_Note = new Command(delnote);
                Info_color = "Black";
                Title_color = "Black";

            }
            else
            {
                Colors = "Black";
                Option = "Save";
                Info_color = "White";
                Title_color = "White";
            }
        }
        public NotePageVM()
        {
            Backbutton = new Command(backbutton);
        }
        public async void savenote()
        {
            Date = string.Format("{0: MMMM dd',' hh':'mm tt}", DateTime.Now);
            var cloudsync = Preferences.Get("CloudSync_Switch", "");
            if(Option =="Save")
            {
                Notes x = new Notes()
                {
                    TITLE = Title,
                    DATE = Date,
                    INFO = Info,
                    COLOR = Colors
                };
                await Database.OnSave(x);
                if(cloudsync == "true")
                { await FireBaseHelper.Addnote(x); }                
            }
            else
            {
                Note.TITLE = Title;
                Note.INFO = Info;
                Note.DATE = Date;
                Note.COLOR = Colors;
                await Database.OnSave(Note);
                if (cloudsync == "true")
                { await FireBaseHelper.UpdateNote(Note); }
            }            
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        public async void delnote()
        {
            //var cloudsync = Preferences.Get("CloudSync_Switch", "");
            bool choice = await App.Current.MainPage.DisplayAlert("Alert !", "Do you want to delete this Note ?", "Yes", "No");
            if (choice)
            {               
                await Database.Ondelete(Note);                
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                return;
            }
        }
        public async void backbutton()
        {
            Date = string.Format("{0: MMMM dd',' hh':'mm tt}", DateTime.Now);
            var cloudsync = Preferences.Get("CloudSync_Switch", "");
            if (Option == "Save")
            {
                Notes x = new Notes()
                {
                    TITLE = Title,
                    DATE = Date,
                    INFO = Info,
                    COLOR = Colors
                };
                await Database.OnSave(x);
                if (cloudsync == "true")
                { await FireBaseHelper.Addnote(x); }
            }
            else
            {
                Note.TITLE = Title;
                Note.INFO = Info;
                Note.DATE = Date;
                Note.COLOR = Colors;
                await Database.OnSave(Note);
                if (cloudsync == "true")
                { await FireBaseHelper.UpdateNote(Note); }
            }
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        public void chose_colorgreen()
        {
            Colors = "LightGreen";
            Info_color = "Black";
            Title_color = "Black";
        }
        public void choose_Colourpink()
        {
            Colors = "LightPink";
            Info_color = "Black";
            Title_color = "Black";
        }
        public void choose_Colourred()
        {
            Colors = "LightSalmon";
            Info_color = "Black";
            Title_color = "Black";
        }
        public void choose_Colouryellow()
        {
            Colors = "LightYellow";
            Info_color = "Black";
            Title_color = "Black";
        }
        
    }
}
