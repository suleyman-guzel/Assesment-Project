using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Utilities.Converters.ToExcel
{
    public class ToExcelConverter
    {
        private DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public string ConvertToExcel<T>(IEnumerable<T> parameter, string path, string fileName)
        {
            if (parameter == null || parameter.Count() <= 0)
            {
                throw new Exception("Report parameter list is empty!");
            }
            XLWorkbook wb = new XLWorkbook();
            DataTable dt = this.ConvertToDataTable(parameter);
            wb.Worksheets.Add(dt, "WorksheetName");
            wb.SaveAs(path + fileName + ".xlsx");
            return Path.GetFullPath(path + fileName + ".xlsx");
        }
    }
}
