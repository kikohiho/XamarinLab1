using Audity.Models;
using Audity.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Audity.ViewModel
{
    class ReceiptsLogViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Receipt> Receipts { get; set; }
        public Command GetReceiptsCommand { get; set; }
        public ReceiptsLogViewModel()
        {

            Receipts = new ObservableCollection<Receipt>();
            GetReceiptsCommand = new Command(
                async () => await GetReicipts(),
                () => !IsBusy);

        }

        bool busy;

        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();

                GetReceiptsCommand.ChangeCanExecute();
            }
        }



        async Task GetReicipts()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<AzureReceiptService>();
                var items = await service.GetReceiptsAsync();

                Receipts.Clear();
                foreach (var item in items)
                    Receipts.Add(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }

            if (error != null)
                await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            var changed = PropertyChanged;

            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
