using Notes_Clone.Localdb;
using Notes_Clone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes_Clone.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        public NotePage()
        {
            InitializeComponent();           
        }
        public NotePage(Notes update)
        {
            InitializeComponent();
            BindingContext = new NotePageVM(update);
        }
        protected override bool OnBackButtonPressed()
        {
            var z = (NotePageVM)BindingContext;
            z.backbutton();
            return false;
        }

    }
}