using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

using CsvHelper;

namespace CommonUtilities
{
    public class CSVFileUtility
    {
        /// <summary>
        /// フリー形式のCSVを読み込む
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="enc">文字エンコード</param>
        /// <returns>データテーブル</returns>
        public static DataTable Read(string filePath, Encoding enc = null)
        {

            //文字エンコードの指定がなければUTF8
            if (enc == null)
            {
                enc = Encoding.UTF8;
            }

            var parser = new CsvReader(new StreamReader(filePath, enc));

            //CSVの設定
            parser.Configuration.Encoding = enc;
            parser.Configuration.AllowComments = true;
            parser.Configuration.Comment = '#';
            parser.Configuration.HasHeaderRecord = false;
            parser.Configuration.Delimiter = ",";

            //データの読み込み
            int fieldCount = 0;
            List<List<string>> lst = new List<List<string>>();
            while (parser.Read())
            {
                if(fieldCount < parser.CurrentRecord.Length)
                {
                    fieldCount = parser.CurrentRecord.Length;
                }

                List<string> row = new List<string>();
                for (var i = 0; i < parser.CurrentRecord.Length; i++)
                {
                    row.Add(parser.CurrentRecord.ElementAt(i));
                }
                lst.Add(row);

            }

            string tablename = Path.GetFileNameWithoutExtension(filePath);
            DataTable dt = new DataTable(tablename);

            for (var i = 0; i < fieldCount; ++i)
            {
                dt.Columns.Add(new DataColumn(string.Format("Field{0}", i+1), typeof(string)));
            }

            parser.ClearRecordCache();

            foreach(List<string> r in lst)
            {
                var row = dt.NewRow();
                for(int i=0;i < r.Count;i++)
                {
                    row[i] = r[i];
                }
                dt.Rows.Add(row);

            }

            return dt;

        }



    }
}
