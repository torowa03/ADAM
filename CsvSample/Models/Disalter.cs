using System;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace CsvSample.Models
{
    /// <summary>
    /// 災害データクラス
    /// </summary>
    public class Disaster
    {
        [CSVField]
        public DateTime EventDate { get; set; }

        [CSVField]
        public String EventTime { get; set; }

        [CSVField]
        public string EventPlace { get; set; }

        [CSVField]
        public string Magunitude { get; set; }

        [CSVField]
        public string SeismicIntensity { get; set; }

        public DateTime RecordDate { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Disaster:");
            sb.Append(string.Format(" {0}={1}", "EventDate",EventDate.ToString("yyyy/MM/dd")));
            sb.Append(string.Format(",{0}={1}", "EventTime", EventTime));
            sb.Append(string.Format(",{0}={1}", "EventPlace", EventPlace));
            sb.Append(string.Format(",{0}={1}", "Magunitude", Magunitude));
            sb.Append(string.Format(",{0}={1}", "SeismicIntensity", SeismicIntensity));
            sb.Append(string.Format(",{0}={1}", "RecordDate", EventDate.ToString("yyyy/MM/dd")));
            return sb.ToString();
        }
    }

    /// <summary>
    /// マッピングクラス
    /// </summary>
    public class CustomClassMap : CsvClassMap<Disaster>
    {
        public CustomClassMap()
        {
            Map(m => m.EventDate).Name("EventDate");  // .Index(0);
            Map(m => m.EventTime).Name("EventTime");
            Map(m => m.EventPlace).Name("EventPlace");
            Map(m => m.Magunitude).Name("Magunitude");
            Map(m => m.SeismicIntensity).Name("SeismicIntensity");
        }
    }

    /// <summary>
    /// CSVリーダー
    /// </summary>
    public static class CustomCSVReader
    {
        /// <summary>
        /// CSVファイルの読み込み
        /// </summary>
        /// <param name="filepath">ファイルのパス</param>
        /// <param name="encode">エンコード</param>
        /// <returns></returns>
        public static DataTable Read(string filepath,Encoding encode=null)
        {
            if(encode == null)
            {
                encode = Encoding.UTF8;
            }

            DataTable dt = new DataTable();

            Type type = typeof(Disaster);

            PropertyInfo[] properties = type.GetProperties();
            foreach(PropertyInfo prop in properties)
            {
                dt.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
            }

            using (StreamReader sr = new StreamReader(filepath, encode))
            using (CsvReader csv = new CsvReader(sr))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<CustomClassMap>();

                foreach (var record in csv.GetRecords<Disaster>())
                {
                    var row = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        PropertyInfo pi = type.GetProperty(column.ColumnName);
                        Attribute attr = Attribute.GetCustomAttribute(pi, typeof(CSVFieldAttribute));
                        if (attr != null )
                        {
                            row[column.ColumnName] = csv.GetField(column.DataType, column.ColumnName);
                        }
                    }
                    dt.Rows.Add(row);
                }

            }

            return dt;
        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CSVFieldAttribute : Attribute
    {
    }


}


