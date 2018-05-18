using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Active_Directory_Management
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
            foreach(TreeNode node in treeBox.Nodes)
            {
                treeBoxCache.Nodes.Add((TreeNode)node.Clone());
            }
        }

         
       

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == string.Empty)
            {
                searchBoxPlaceholder.Visible = true;
            }
            else
                searchBoxPlaceholder.Visible = false;

            treeBox.BeginUpdate();
            treeBox.Nodes.Clear();

            if (searchBox.Text == "Поиск" || searchBox.Text == string.Empty)
            {

                foreach (TreeNode node in treeBoxCache.Nodes)
                    treeBox.Nodes.Add((TreeNode)node.Clone());                
            }
            else
            {
                foreach (TreeNode node in treeBoxCache.Nodes)
                    Populate(node, searchBox.Text.ToLower());
            }

            treeBox.EndUpdate();
        }

        private void Populate(TreeNode currNode, string name)
        {
            if (currNode == null) return;
            if(currNode.Nodes.Count > 0) 
                foreach(TreeNode node in currNode.Nodes)
                    Populate(node, name);
            else
                foreach(string str in currNode.Text.ToLower().Split(' '))
                    if (str.StartsWith(name))
                        treeBox.Nodes.Add(currNode);
        }

       

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            Form detailView = new DetailView();
            detailView.ShowDialog();
        }

        private void TreeBox_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeBox.SelectedNode.Nodes.Count == 0)
            {
                firstBox.Text = treeBox.SelectedNode.Text;
                cdCheck.Enabled = true;
                usbDiskCheck.Enabled = true;
                usbDeviceCheck.Enabled = true;
                cloudCheck.Enabled = true;
                internetLabel.Enabled = true;
                internetCombo.Enabled = true;
                detailBtn.Enabled = true;
                saveBtn.Enabled = true;
            }
        }

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            Form detailView = new DetailView(new DirectoryEntry());
            detailView.ShowDialog();
        }
    }
}
