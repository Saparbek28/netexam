using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace examnet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                string html = await client.DownloadStringTaskAsync(new Uri(Url.Text));
                HtmlParser parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(html);
                var filesList = document.QuerySelectorAll("*");
                foreach (var file in filesList)
                {
                    foreach (var attribute in file.Attributes)
                    {
                        if (attribute.Name == "href")
                        {
                            if (attribute.Value.Contains(".exe"))
                            {
                                file.AddEventListener(attribute.Value);
                            }
                        }
                    }
                }
            }
            //var uri = new Uri(@"https://www.rarlab.com/"); // Путь откуда скчивать
            //var fileName = System.IO.Path.GetFileName(uri.AbsolutePath); // Только имя скачиваемого имя файла (без пути)
            //var destinationDir = "z:\\"; // Папка в которую скачивать

            //WebClient wc = new WebClient();
            //wc.Proxy = null;
            //wc.DownloadFile(uri, System.IO.Path.Combine(destinationDir, fileName));
        }

        private void buttonDownload_Click(object sender, RoutedEventArgs e)
        {

            using (WebClient client = new WebClient())
            {
                 var destinationDir = "z:\\";
                try
                {
                    client.DownloadFileAsync(new Uri(Url.Text + (file.SelectedItem as string)), destinationDir);
                    MessageBox.Show("file is downloaded");
                    
                }
                catch (Exception ex)
                {                
                    MessageBox.Show(ex.Message);
                }
            }          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string files = @"z:\\";

                    string[] rarList = Directory.GetFiles(files, FileDelete.Name);

                    foreach (string delete in rarList)
                    {
                        File.Delete(delete);
                    }
                }
                catch (DirectoryNotFoundException dirNotFound)
                {
                    Console.WriteLine(dirNotFound.Message);
                }
            }
        }
    }
}
