using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Prism.Mvvm;
using CommonUtilities;

namespace CsvSample.Models
{
    class DisasterService : BindableBase
    {
        private DataTable results = new DataTable();
        public DataTable Results
        {
            get
            {
                return this.results;
            }
            set
            {
                SetProperty(ref results, value);
            }
        }

        /// <summary>
        /// CSVHelperを使って汎用型
        /// 汎用データテーブルに格納
        /// </summary>
        /// <returns></returns>
        public async Task ReadCSV()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                string fn = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "earthquake.csv");
                Results = CSVFileUtility.Read(fn);
            });
        }

        /// <summary>
        /// CSVHelperとコンバータを使用
        /// Disasterデータクラスに格納
        /// </summary>
        /// <returns></returns>
        public async Task ReadCSV2()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                string fn = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "earthquake.csv");
                Results = CustomCSVReader.Read(fn);
            });
        }


    }
}
