using CorrugatedIron.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RiakIronBrowser
{
    /// <summary>
    /// Interaction logic for NodeWindow.xaml
    /// </summary>
    public partial class NodeWindow : Window
    {
        NodeConfiguration localNode = null;
        bool isValid = false;

        public NodeWindow(NodeConfiguration node)
        {
            InitializeComponent();
            if (node != null)
                localNode = node;
            else
                localNode = new NodeConfiguration(null);

            PopulateFields();
        }

        private void PopulateFields()
        {
            NameTextBox.Text = localNode.Node.Name;
            AddressTextBox.Text = localNode.Node.HostAddress;
            PbcPortTextBox.Text = localNode.Node.PbcPort.ToString();
            RestSchemeTextBox.Text = localNode.Node.RestScheme;
            RestPortTextBox.Text = localNode.Node.RestPort.ToString();
            PoolSizeTextBox.Text = localNode.Node.PoolSize.ToString();
        }

        private void SaveFields()
        {
            RiakNodeConfiguration newNode = new RiakNodeConfiguration();
            newNode.Name = NameTextBox.Text;
            newNode.HostAddress = AddressTextBox.Text;
            newNode.PbcPort = int.Parse(PbcPortTextBox.Text);
            newNode.RestScheme = RestSchemeTextBox.Text;
            newNode.RestPort = int.Parse(RestPortTextBox.Text);
            newNode.PoolSize = int.Parse(PoolSizeTextBox.Text);
            localNode.Node = newNode;
        }

        public NodeConfiguration Node
        {
            get { return localNode; }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFields();
                isValid = true;
            }
            catch (Exception)
            { }
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!isValid)
                localNode = null;
        }
    }
}
