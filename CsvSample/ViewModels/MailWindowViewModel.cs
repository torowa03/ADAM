using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using CsvSample.Models;
using System.Data;
using System.ComponentModel;

namespace CsvSample.ViewModels
{
    class MailWindowViewModel : BindableBase
    {
        private DisasterService service = new DisasterService();

        public DataView Results
        {
            get
            {
                return this.service.Results.DefaultView;
            }
        }

        private DelegateCommand readCommand;
        public DelegateCommand ReadCommand
        {
            get
            {
                return this.readCommand ?? (this.readCommand = new DelegateCommand(ReadExecute));
            }
        }

        /// <summary>
        /// 非同期でボタンの処理を実行するためasyncで実装する
        /// </summary>
        private async void ReadExecute()
        {
            await this.service.ReadCSV();
        }


        public MailWindowViewModel()
        {
            this.service.PropertyChanged += Service_PropertyChanged;
        }


        private void Service_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //コマンド実行でGridViewの内容を変更するためには、プロパティChangeを実装する必要がある
            OnPropertyChanged(e.PropertyName);
        }
    }
}
