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
using System.IO;

namespace Web_Link_opener
{
    /// <summary>
    /// Interaction logic for WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        Action _returnToEnabled;

        private readonly bool isFolder;
        public WindowAdd(Action ReturnToEnabled, bool IsFolder)
        {
            InitializeComponent();
            _returnToEnabled = ReturnToEnabled;
            isFolder = IsFolder;
            if (IsFolder)
            {
                Title = "New folder";
                NewLink.Visibility = Visibility.Hidden;
                NewFolder.Visibility = Visibility.Hidden;
                LinkLabel.Visibility = Visibility.Hidden;
                FolderLabel.Visibility = Visibility.Hidden;
            }
        }

        private void NewAdd_Click(object sender, RoutedEventArgs e)
        {
            if (isFolder && NewName.Text.Length == 0)
            {
                MessageBox.Show("Name must be specified!", "Warning");
                return;
            }
            else if (!isFolder)
            {
                if (NewLink.Text.Length == 0 || NewName.Text.Length == 0 || NewFolder.Text.Length == 0)
                {
                    MessageBox.Show("Name, Link and Folder must be specified!", "Warning");
                    return;
                }
                if (!NewLink.Text.Contains("https://") && !NewLink.Text.Contains("http://"))
                {
                    MessageBox.Show("This link is invalid!, maybe you forgot http:// ?", "Warning");
                    return;
                }
            }
            if (isFolder)
            {
                string folders = File.ReadAllText(@"folders.txt");
                if (folders.Contains(NewName.Text))
                {
                    MessageBox.Show("Folder with the same name already exists", "Warning");
                    return;
                }
                File.AppendAllText(@"folders.txt", "\n" + NewName.Text + " \" " + NewDescription.Text);
            }
            else
            {
                string folders = File.ReadAllText(@"folders.txt");
                string links = File.ReadAllText(@"links.txt");
                if (!folders.Contains(NewFolder.Text))
                {
                    MessageBox.Show("This folder does not exist", "Warning");
                    return;
                }
                if (links.Contains(NewName.Text + " \" " + NewFolder.Text))
                {
                    MessageBox.Show("A link with the same name already exists in current folder", "Warning");
                    return;
                }
                File.AppendAllText(@"links.txt", "\n" + NewName.Text + " \" " + NewFolder.Text + " \" " + NewLink.Text + " \" " + NewDescription.Text);
            }
            Close();
        }

        private void NewCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_returnToEnabled != null) _returnToEnabled();
        }
    }
}
