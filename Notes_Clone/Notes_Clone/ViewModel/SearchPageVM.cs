using Notes_Clone.Localdb;
using Notes_Clone.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes_Clone.ViewModel
{
    public class SearchPageVM : BaseVM
    {
        private string searchtext { get; set; }
        private List<Notes> note { get; set; }
        private Notes selectedNoteData { get; set; }
        public ICommand Searchbutton { get; set; }
        public ICommand UpdateNote { get; set; }
        public string Searchtext
        {
            get
            {
                return searchtext;
            }
            set
            {
                searchtext = value;
                OnPropertyChanged();
            }
        }
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
        public SearchPageVM()
        {            
            Searchbutton = new Command(search);
            UpdateNote = new Command(navigate_newnote_update);
            Note = new List<Notes>(Database.Onget().Result);
        }
        public void search()
        {
            var searchresult = Note.Where(a => a.TITLE.Contains(Searchtext));
            Note = new List<Notes>(searchresult);
        }
        public async void navigate_newnote_update(object obj)
        {
            var selecteddata = SelectedNoteData;
            await App.Current.MainPage.Navigation.PushAsync(new NotePage(selecteddata));
        }

    }
}
