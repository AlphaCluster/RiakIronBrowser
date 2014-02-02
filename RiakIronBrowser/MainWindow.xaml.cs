using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace RiakIronBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RiakBase riak = null;

        public MainWindow()
        {
            InitializeComponent();

            riak = new RiakBase();

            LoadBucketList();
        }

        private void BucketLoad_Click(object sender, RoutedEventArgs e)
        {
            if (BucketListBox.SelectedIndex >= 0)
            {
                LoadKeyList();
            }
        }

        private void DeleteBucketButton_Click(object sender, RoutedEventArgs e)
        {
            if (BucketListBox.SelectedIndex >= 0)
            {
                IList<string> errors = riak.DeleteBucket(GetBucket());
                StringBuilder builder = new StringBuilder("Following errors occured:");
                foreach (string error in errors)
                    builder.AppendLine(error);
                MessageBox.Show(builder.ToString());
                LoadBucketList();
            }
        }

        private void LoadBucketList()
        {
            IList<string> list = riak.GetAllBuckets();

            BucketListBox.Items.Clear();
            foreach (string bucket in list)
                BucketListBox.Items.Add(bucket);
        }

        private void LoadKeyList()
        {
            IList<string> keyList = riak.GetAllKeys(GetBucket());

            CountLabel.Content = "Count: " + keyList.Count();

            KeyListBox.Items.Clear();
            foreach (string key in keyList)
                KeyListBox.Items.Add(key);
        }

        private string GetBucket()
        {
            return (string)BucketListBox.SelectedValue;
        }

        private string GetKey()
        {
            return (string)KeyListBox.SelectedValue;
        }

        private void DeleteKeyButton_Click(object sender, RoutedEventArgs e)
        {
            if ((KeyListBox.SelectedIndex >= 0) &&
                (BucketListBox.SelectedIndex >= 0))
            {
                riak.DeleteKey(GetBucket(), GetKey());
                LoadKeyList();
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }
    }
}
