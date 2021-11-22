using Notes_Clone.FireBase;
using Notes_Clone.Localdb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes_Clone.ViewModel
{
    public class CloudSyncPageVM : BaseVM
    {
        private  bool _switch { get; set; }
        private List<Notes> notedb { get; set; }
        public ICommand Switch_toggled { get; set; }
        public List<Notes> Notedb
        {
            get
            {
                return notedb;
            }
            set
            {
                notedb = value;
                OnPropertyChanged();
            }
        }
        public  bool Switch
        {
            get
            {
                return _switch;
            }
            set
            {
                _switch = value;
                OnPropertyChanged();
            }
        }
        
        public CloudSyncPageVM()
        {
            Notedb = new List<Notes>(Database.Onget().Result);
            Switch_toggled = new Command(switch_toggled);
            var tf = Preferences.Get("CloudSync_Switch", "");
            if (tf == "true")
            {
                Switch = true;
            }
            else
            {
                Switch = false;
            }
        }
        public async  void switch_toggled()
        {
            if (Switch == true)
            {                
                foreach (var item in Notedb)
                {
                    await FireBaseHelper.Addnote(item);
                }
                Preferences.Set("CloudSync_Switch", "true");
            }
            else
            {                
                Preferences.Clear();
            }            
        }

    }
}
