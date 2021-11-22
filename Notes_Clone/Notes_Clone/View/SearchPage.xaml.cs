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
    public partial class SearchPage : ContentPage
    {
        public List<Notes> x;
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchPageVM();
             x = new List<Notes>(Database.Onget().Result);
        }      

        private void sb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var sr = x.Where(z => z.TITLE.ToLower().Contains(sb.Text.ToLower()));
            w.ItemsSource=sr;            
        }
    }
}