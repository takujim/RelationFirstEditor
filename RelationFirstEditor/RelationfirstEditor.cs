using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationFirstEditor
{
    public partial class RelationfirstEditor : Form
    {
        public RelationfirstEditor()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.SelectedPath = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();

                updateList();
            }
        }

        private void updateList()
        {
            if (folderBrowserDialog1.SelectedPath != string.Empty)
            {
                lstAllFile.Items.Clear();
                DirSearch(folderBrowserDialog1.SelectedPath, 0);
            }
        }

        private void DirSearch(string sDir, int count)
        {
            count++;

            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    //foreach (string f in Directory.GetFiles(d, txtFile.Text))
                    //{
                    //    lstFilesFound.Items.Add(f);
                    //}
                    if (count == 1)
                    {
                        lstAllFile.Items.Add("----------------");
                    }
                    lstAllFile.Items.Add(d.Replace(folderBrowserDialog1.SelectedPath, ""));
                    DirSearch(d, count);
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (isFolder())
            {
                System.Diagnostics.Process.Start(getSelectFolderPath());
            }
        }

        private string getSelectFolderPath()
        {
            if (isFolder())
            {
                return folderBrowserDialog1.SelectedPath + lstAllFile.SelectedItem.ToString();
            }

            MessageBox.Show("Folder not found");
            return "";
        }

        private bool isFolder()
        {
            return lstAllFile.SelectedIndex > -1 && lstAllFile.SelectedItem.ToString() != "----------------";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.SelectedPath;
            updateList();
            htmlView.DocumentText = "Hello!!";
        }

        private void lstAllFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFolder())
            {
                var files = Directory.GetFiles(getSelectFolderPath(), "*.html");
                if (files.Count() > 0)
                {
                    htmlView.Url = new Uri(files[0]);
                }
                else
                {
                    htmlView.DocumentText = "HTML file not found";
                    //MessageBox.Show("HTML file not found");
                }
            }
        }
    }
}
