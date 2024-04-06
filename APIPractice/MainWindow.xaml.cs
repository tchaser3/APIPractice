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
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Wasp.Cloud.Asset.API;
using Wasp.Cloud.Inventory.API;
using EX = Wasp.Cloud.Asset.API;

namespace APIPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Location
    {
        public string strSite { get; set; }
        public string strLocation { get; set; }
    }
    public class Repository
    {
        public string name { get; set; }
    }
    public partial class MainWindow : Window
    {
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        static HttpClient client = new HttpClient();

        private static string gstrMsg;

        public MainWindow()
        {
            InitializeComponent();
        }
        


        private async void btnFind_Click(object sender, RoutedEventArgs e)
        {
            await ProcessRepositories();

            txtMessage.Text = gstrMsg;

            TheMessagesClass.InformationMessage("Fuck Me");
        }
        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            gstrMsg = await stringTask;


            
        }
        static async Task<Location> GetProductAsync(string path)
        {
            string strLocation;

            Location location = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                strLocation = await response.Content.ReadAsStringAsync();
            }
            return location;
        }
        
              

    }
}
