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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace Web_Link_opener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string? LastFolder { get; set; }
        private bool RemoveActive { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            RemoveActive = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(@"links.txt")) File.AppendAllText(@"links.txt", "LINKS_LIST\nGithub (preview link) \" Preview folder \" https://github.com/CrusaderSVK287/Web-Link-opener \" This is an example link, it will lead you to the creators gitbub profile where you can read more information about this application");
            if (!File.Exists(@"folders.txt")) File.AppendAllText(@"folders.txt", "FOLDERS_LIST\nPreview folder \" Preview desctiption");
            RefreshFolders();
        }
        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            utilityGrid.Visibility = Visibility.Visible;
            Window add = new WindowAdd(ReturnToEnabled, true);
            add.Show();
        }
        private void ReturnToEnabled()
        {
            utilityGrid.Visibility = Visibility.Collapsed;
            RefreshFolders();
            RefreshLinks(LastFolder);
        }

        private void CreateLink_Click(object sender, RoutedEventArgs e)
        {
            utilityGrid.Visibility = Visibility.Visible;
            Window add = new WindowAdd(ReturnToEnabled, false);
            add.Show();
        }
        private void RefreshFolders()
        {
            SPFolders.Children.Clear();
            string all_folders = File.ReadAllText(@"folders.txt");
            string[] folders = all_folders.Split('\n');
            if (folders.Length > 1) CreateLink.IsEnabled = true;
            foreach (string folder in folders)
            {
                if (folder.Contains("FOLDERS_LIST") || folder.Length==0) continue;
                CreateLink.IsEnabled = true;
                Button button = new Button();
                button.Height = 20;
                button.Margin = new Thickness(1, 1, 2, 0);
                string[] information = folder.Split('"');
                string? name = information[0].Trim(), desc;
                if (information.Length == 2) desc = information[1].Trim();
                else desc = "No description";
                button.Content = name;
                button.Click += (s, e) => {
                    if (((Button)s).Content == null) return;
                    LastFolder = ((Button)s).Content.ToString();
                    if (!RemoveActive) RefreshLinks(((Button)s).Content.ToString());
                    else Delete(((Button)s).Content.ToString(), null);
                };
                button.MouseEnter += (s, e) => { Description.Text = desc; };
                SPFolders.Children.Add(button);
            }
        }
        private void RefreshLinks(string? folder)
        {
            if (folder == null) return;
            SPLinks.Children.Clear();
            string all_links = File.ReadAllText(@"links.txt");
            string[] links = all_links.Split('\n');
            foreach (string link in links)
            {
                if (link.Contains("LINK_LIST")) continue;
                if (!link.Contains($"\" {folder} \"")) continue;
                string[] information = link.Split('"');
                string name = information[0].Trim(), address = information[2].Trim();
                string? desc = null;
                if (information.Length > 3) desc = information[3].Trim();
                Link linkobject = new Link(name,address,folder,desc);
                Button button = new Button();
                button.Height = 20;
                button.Margin = new Thickness(1, 1, 2, 0);
                button.Content = name;
                button.Click += (s, e) => {
                    if (linkobject.Address == null) return;
                    if (!RemoveActive)
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = linkobject.Address,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                    else Delete(folder, linkobject.Name);
                };
                button.MouseEnter += (s, e) => { Description.Text = linkobject.Description; };
                SPLinks.Children.Add(button);
            }
        }
        public class Link
        {
            public readonly string Name;
            public readonly string Address;
            public readonly string Folder;
            public readonly string Description;
            public Link(string name, string address, string folder, string? description)
            {
                Name = name;
                Address = address;
                Folder = folder;
                if (description!=null && description.Length>0) Description = description;
                else Description = "No description";
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Web Link opener.exe");
            Close();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveActive = !RemoveActive;
            if (RemoveActive) Remove.Foreground = new SolidColorBrush(Color.FromRgb(255,0,0));
            else Remove.Foreground = new SolidColorBrush(Color.FromRgb( 0, 0, 0));
        }
        private void Delete(string? folder, string? link)
        {
            if (folder == null) return;
            CreateLink.IsEnabled = false;
            File.Delete(@"links_backup.txt");
            File.Delete(@"folders_backup.txt");
            File.Copy(@"links.txt", @"links_backup.txt");
            File.Copy(@"folders.txt", @"folders_backup.txt");
            File.Delete(@"links.txt");
            if (link == null)
            {
                File.Delete(@"folders.txt");
                foreach (string line in File.ReadLines(@"links_backup.txt"))
                {
                    if (line.Contains($"\" {folder} \"")) continue;
                    File.AppendAllText(@"links.txt", line+"\n");
                }
                foreach (string line in File.ReadLines(@"folders_backup.txt"))
                {
                    if (line.Contains($"{folder}") || line.Length==0) continue;
                    File.AppendAllText(@"folders.txt", line + "\n");
                }
            }
            else
            {
                foreach (string line in File.ReadLines(@"links_backup.txt"))
                {
                    if (line.Contains($"{link} \" {folder} \"")) continue;
                    File.AppendAllText(@"links.txt", line + "\n");
                }
            }
            RefreshFolders();
            RefreshLinks(LastFolder);
        }
    }
}