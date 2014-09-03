using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nhom_11_DuongBaVan_11CDTM1
{
    public partial class frmMain : Form
    {
        List<List<int>> Adjacent;
        int Vertices; // so dinh
        
        GraphicsTools g; // khai bao 1 graphicsTools
        List<Point> lstPointVertices; // danh sach toa do cac dinh
        string FileName;
        public const int PicturePadding = 50; // picture padding

        public frmMain()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            About frmAbout = new About();
            frmAbout.StartPosition = FormStartPosition.CenterScreen;
            frmAbout.Show(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            BFS bfs = new BFS(this.Adjacent);

            int start = this.cboFrom.SelectedIndex;
            int end = this.cboTo.SelectedIndex;

            // reset picGraphics va txtResult
            this.picGraphics.Image = this.g.Reset(this.Adjacent, this.lstPointVertices);
            this.txtResult.Clear();

            if (start == end)
            {
                MessageBox.Show("Vertices are duplicate. Please choose again!", "Error vertices Selected");
                return;
            }
            List<int> res = bfs.findPathbyBfs(this.Vertices, start, end);
           
            if (res == null)
            {
                string text = "Can't find any path from {0} to {1}.";
                MessageBox.Show(string.Format(text, start + 1, end + 1),"Find Path");
                return;
            }
            else
            {
               
                int index;
                this.txtResult.Text = "";

                // reset bit map
               
                // xuat ket qua ra text box
                for (index = 0; index < res.Count - 1; ++index)
                    this.txtResult.Text += (1 + res[index]).ToString() + " ---> ";
                this.txtResult.Text += (1 + res[index]).ToString();

                // ve duong di len bitmap
                this.picGraphics.Image = this.g.DrawPath(res, this.lstPointVertices);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Document File(*.txt)|*.txt|All File(*.*)|*.*";
       
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // enable button findpath va button save image
                    this.btnFindPath.Enabled = true;
                    this.btnSaveImage.Enabled = true;

                    // reset control values
                    this.cboFrom.Items.Clear();
                    this.cboTo.Items.Clear();
                    this.lvMatrix.Items.Clear();
                    this.lvMatrix.Columns.Clear();
                    this.txtResult.Clear();
                    this.picGraphics.Image = null;
                    //
                    Matrix m = new Matrix();
                    //
                    this.FileName = ofd.FileName;
                    this.Adjacent = m.LoadFile(this.FileName, this.lvMatrix, this.cboFrom, this.cboTo);
                    this.cboFrom.SelectedIndex = 0;
                    this.cboTo.SelectedIndex = 1;

                    this.Vertices = m.Vertices;

                    this.g = new GraphicsTools(this.picGraphics.Width, this.picGraphics.Height);// khoi tao graphics tool

                    // lay danh sach toa do cac dinh
                    this.lstPointVertices = new Vector2D(this.Vertices, this.picGraphics.Width - frmMain.PicturePadding,
                                                this.picGraphics.Height - frmMain.PicturePadding).getRandomPoint();

                    // tao bitmap do thi voi danh sach ke va danh sach toa cac dinh
                  
                    this.picGraphics.Image = this.g.CreateGraphics(this.Adjacent, this.lstPointVertices);
                    
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message, "Error");

                }
            }
            ofd.Dispose();
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImgDialog = new SaveFileDialog();
            saveImgDialog.DefaultExt = "png";
            saveImgDialog.Filter = "Bitmap Image (*.png)|*.png|All File (*.*)|*.*";
            saveImgDialog.AddExtension = true;
            saveImgDialog.RestoreDirectory = true;
            saveImgDialog.Title = "Save graph to image";
            string initFileName = System.IO.Path.GetFileNameWithoutExtension(this.FileName);
            saveImgDialog.FileName = initFileName;

            try
            {
                if (saveImgDialog.ShowDialog() == DialogResult.OK)
                {
                    string imgPath = saveImgDialog.FileName;
                    
                    if (imgPath != null)
                    {
                        if (this.picGraphics.Image != null)
                        {
                            this.picGraphics.Image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            saveImgDialog.Dispose();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnOpenFile_Click(sender, e);
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnSaveImage_Click(sender, e);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // disable button findpath va button save image
            this.btnFindPath.Enabled = false;
            this.btnSaveImage.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
