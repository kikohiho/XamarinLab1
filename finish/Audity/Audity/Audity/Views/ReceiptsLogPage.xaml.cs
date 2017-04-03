using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audity.Models;
using Audity.ViewModel;
using Xamarin.Forms;

namespace Audity.Views
{
    public partial class ReceiptsLogPage : ContentPage
    {
        ReceiptsLogViewModel vm;
        
        public ReceiptsLogPage()
        {
            InitializeComponent();
            ObservableCollection<Receipt> lista = new ObservableCollection<Receipt>();
            
            receiptsListView.ItemsSource = lista;
            vm = new ReceiptsLogViewModel();
            BindingContext = vm;
        }
    }
}
