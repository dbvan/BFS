using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
namespace Nhom_11_DuongBaVan_11CDTM1
{
    class Matrix
    {
        private int _vertices; // dinh
        private List<List<int>> _adjacent; // danh canh ke

        public List<List<int>> Adjacent
        {
            get { return _adjacent; }
            set { _adjacent = value; }
        }

        public int Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        // them columnheader cho list view
        private ColumnHeader AddColunmListView(string text)
        {
            ColumnHeader headerCol = new ColumnHeader();
            headerCol.Text = text;
            headerCol.Width = 24;
            headerCol.TextAlign = HorizontalAlignment.Center;
            headerCol.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            return headerCol;
        }


        public List<List<int>> LoadFile(string fileName, ListView lv, ComboBox  cboFrom, ComboBox cboTo)
        {
            StreamReader fin = new StreamReader(fileName);

            // danh sach can ke
            this._adjacent = new List<List<int>>();
            this.Vertices = Convert.ToInt32(fin.ReadLine());
            string line = "";
            List<int> row;

            lv.Columns.Add(AddColunmListView(""));

            for (int iX = 0; iX < this.Vertices; ++iX)
            {
                line = fin.ReadLine();
                string []words = line.Split(' ');

                cboFrom.Items.Add((iX+1).ToString()); // them item vao comboBox From
                cboTo.Items.Add((iX + 1).ToString()); // them item vao comboBox To

                lv.Columns.Add(AddColunmListView((iX+1).ToString()));

                // them dong vao list view
                ListViewItem lvi = new ListViewItem((iX+1).ToString());
                lvi.UseItemStyleForSubItems = false;

                row = new List<int>();
                for (int iY = 0; iY < this.Vertices; ++iY)
                {
                    // list view sub item
                    ListViewItem.ListViewSubItem col =  lvi.SubItems.Add(words[iY]);
                    col.ForeColor = Color.Blue; // mau cua list view subitem
                    if (words[iY] != "0")
                        row.Add(iY);
                }
                this._adjacent.Add(row);
                lv.Items.Add(lvi);
            }
            fin.Close();
            return this._adjacent;
        }
    }
}
