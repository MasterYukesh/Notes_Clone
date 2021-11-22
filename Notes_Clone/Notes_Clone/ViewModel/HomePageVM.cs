using Notes_Clone.Localdb;
using Notes_Clone.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes_Clone.ViewModel
{
    public class HomePageVM : BaseVM
    {
        private List<Notes> note { get; set; }   
        private Notes selectedNoteData { get; set; }  
        private string cloudsync { get; set; }
        private string list_grid { get; set; }
        private bool is_listview { get; set; }
        private bool is_gridview { get; set; }
        private bool option_frame { get; set; }
        private bool frame_visibility { get; set; }        
        public ICommand Note_mode { get; set; }
        public ICommand OC_Frame { get; set; }
        public ICommand Newnote { get; set; }
        public ICommand SearchPage { get; set; }
        public ICommand CloudSyncPage { get; set; }
        public ICommand AboutPage { get; set; }
        public ICommand UpdateNote { get; set; }
        public ICommand DelNote { get; set; }        
        public List<Notes> Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                OnPropertyChanged();
            }
        }
        public bool Frame_visibility
        {
            get
            {
                return frame_visibility;
            }
            set
            {
                frame_visibility = value;
                OnPropertyChanged();
            }
        }
        public Notes SelectedNoteData
        {
            get
            {
                return selectedNoteData;
            }
            set
            {
                selectedNoteData = value;
                OnPropertyChanged();
            }
        }
        public string Cloudsync
        {
            get
            {
                return cloudsync;
            }
            set
            {
                cloudsync = value;
                OnPropertyChanged();
            }
        }
        public string List_Grid
        {
            get
            {
                return list_grid;
            }
            set
            {
                list_grid = value;
                OnPropertyChanged();
            }
        }
        public bool Is_listview
        {
            get
            {
                return is_listview;
            }
            set
            {
                is_listview = value;
                OnPropertyChanged();
            }
        }
        public bool Is_gridview
        {
            get
            {
                return is_gridview;
            }
            set
            {
                is_gridview = value;
                OnPropertyChanged();
            }
        }
        public bool Option_frame
        {
            get
            {
                return option_frame;
            }
            set
            {
                option_frame = value;
                OnPropertyChanged();
            }
        }
        
        public HomePageVM()
        {
            Note = new List<Notes>(Database.Onget().Result);            
            Newnote = new Command(navigate_newnote);
            CloudSyncPage = new Command(navigate_cloudsyncpage);
            UpdateNote = new Command(navigate_newnote_update);
            DelNote = new Command(deletenote);
            OC_Frame = new Command(oc_frame);
            Note_mode = new Command(note_mode);
            SearchPage = new Command(navigate_searchpage);
            AboutPage = new Command(navigate_aboutpage);
            List_Grid = "Gridview";
            Frame_visibility = false;
            Is_listview = true;
            Is_gridview = false;
            var cs = Preferences.Get("CloudSync_Switch", "");
            if(cs == "true")
            {
                Cloudsync = "Notes Synced";
            }
            else
            {
                Cloudsync = "Enable Cloud Service";
            }
        }
        public async void navigate_newnote()
        {
           await App.Current.MainPage.Navigation.PushAsync(new NotePage(null));
        }
        public async void navigate_cloudsyncpage()
        {
          await App.Current.MainPage.Navigation.PushAsync(new CloudSyncPage());
        }
        public async void navigate_newnote_update(object obj)
        {
            var selecteddata = SelectedNoteData;            
            await App.Current.MainPage.Navigation.PushAsync(new NotePage(selecteddata));                        
        }
        public async void deletenote(object obj)
        {
            bool choice = await App.Current.MainPage.DisplayAlert("Alert !", "Do you want to delete this Note ?", "Yes", "No");
            if(choice )
            {
                var delnote = obj as Notes;
                await Database.Ondelete(delnote);
            }
            else
            {
                return;
            }
        }        
        public void oc_frame()
        {
            if(Frame_visibility == false)
            {
                Frame_visibility = true;
            }
            else if(Frame_visibility == true)
            {
                Frame_visibility = false;
            }
        }
        public void note_mode()
        {
            if(List_Grid == "Listview")
            {
                Is_listview = true;
                Is_gridview = false;
                List_Grid = "Gridview";
            }
            else
            {
                Is_gridview = true;
                Is_listview = false;
                List_Grid = "Listview";
            }
            Frame_visibility = false;
        }
        public async void navigate_searchpage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SearchPage());
        }
        public async void navigate_aboutpage()
        {
            Frame_visibility = false;
            await App.Current.MainPage.Navigation.PushAsync(new AboutPage());
        }
    }
}
