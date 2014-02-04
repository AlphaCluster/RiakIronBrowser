using CorrugatedIron;
using CorrugatedIron.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        Configuration exeConfiguration = null;
        IRiakClusterConfiguration config = null;
        public SettingsWindow()
        {
            InitializeComponent();

            exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config = (IRiakClusterConfiguration)exeConfiguration.GetSection("riakConfig");

            LoadFromConfig();
        }
        
        private void LoadFromConfig()
        {
            NodePoolTimeTextBox.Text = config.NodePollTime.ToString();
            RetryCountTextBox.Text = config.DefaultRetryCount.ToString();
            RetryWaitTimeTextBox.Text = config.DefaultRetryWaitTime.ToString();

            foreach (IRiakNodeConfiguration node in config.RiakNodes)
                NodeListBox.Items.Add(new NodeConfiguration(node));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            exeConfiguration.Save();
            Close();
        }

        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            NodeWindow window = new NodeWindow(null);
            window.ShowDialog();
            if (window.Node != null)
                NodeListBox.Items.Add(window.Node);
        }

        private void RemoveNodeButton_Click(object sender, RoutedEventArgs e)
        {
            NodeListBox.Items.Remove(NodeListBox.SelectedItem);
        }

        private void EditNodeButton_Click(object sender, RoutedEventArgs e)
        {
            NodeWindow window = new NodeWindow((NodeConfiguration)NodeListBox.SelectedItem);
            window.ShowDialog();
            if (window.Node != null)
                NodeListBox.SelectedItem = window.Node;

            NodeListBox.Items.Refresh();
        }

        private void NodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NodeListBox.SelectedIndex >= 0)
            {
                RemoveNodeButton.IsEnabled = true;
                EditNodeButton.IsEnabled = true;
            }
            else
            {
                RemoveNodeButton.IsEnabled = false;
                EditNodeButton.IsEnabled = false;
            }
        }
    }
}
