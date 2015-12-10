using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using WikipediaViewer.Modells;

namespace WikipediaViewer.ViewModels
{
    class MainWindowViewModel :BindableBase
    {
        private WikipediaService service = new WikipediaService();

        private string keyword = string.Empty;
        public string Keyword
        {
            get
            {
                return this.keyword;
            }
            set
            {
                this.SetProperty(ref this.keyword, value);
                this.SearchCommand.RaiseCanExecuteChanged();
            }
        }

        private WikipediaItem selectedItem = null;
        public WikipediaItem SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.SetProperty(ref this.selectedItem, value);
            }
        }

        public List<WikipediaItem> Results
        {
            get
            {
                return this.service.Results;
            }
        }

        private DelegateCommand searchCommand;
        public DelegateCommand SearchCommand
        {
            get
            {
                return this.searchCommand ?? (this.searchCommand = new DelegateCommand(SearchExecute, CanSearchExecute));
            }
        }

        private bool CanSearchExecute()
        {
            return !String.IsNullOrWhiteSpace(this.keyword);
        }

        private async void SearchExecute()
        {
            await this.service.Search(this.Keyword);
        }

        public MainWindowViewModel()
        {
            this.service.PropertyChanged += Service_PropertyChanged;
        }

        private void Service_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
