using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace WpfDZ1
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string county { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string nomer { get; set; }
        public string homeTelephone { get; set; }
        public string rabTelephone { get; set; }

        public void editClient(Client client)
        {
            id = client.id;
            name = client.name;
            lastName = client.lastName;
            firstName = client.firstName;
            county = client.county;
            city = client.city;
            street = client.street;
            nomer = client.nomer;
            homeTelephone = client.homeTelephone;
            rabTelephone = client.rabTelephone;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Client> clients = new List<Client>();
        public string pathToFile = "dd.txt";
        public bool delete = false;
        public Client newClient = new Client();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void enableButtun()
        {
            Name.IsEnabled = true;
            Lastname.IsEnabled = true;
            Firstname.IsEnabled = true;
            Counter.IsEnabled = true;
            Sity.IsEnabled = true;
            Street.IsEnabled = true;
            Number.IsEnabled = true;
            HomeTelephone.IsEnabled = true;
            RabTelephone.IsEnabled = true;
            Button_Edits.IsEnabled = false;
            Button_News.IsEnabled = false;
            Button_Saves.IsEnabled = true;
            
        }

        public void disableButtun()
        {
            Name.IsEnabled = false;
            Lastname.IsEnabled = false;
            Firstname.IsEnabled = false;
            Counter.IsEnabled = false;
            Sity.IsEnabled = false;
            Street.IsEnabled = false;
            Number.IsEnabled = false;
            HomeTelephone.IsEnabled = false;
            RabTelephone.IsEnabled = false;
            Button_Edits.IsEnabled = true;
            Button_News.IsEnabled = true;
            Button_Saves.IsEnabled = false;
            
        }

       

            private void Button_Save(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            client.id = Convert.ToInt32(ID.Text);
            client.name = Name.Text;
            client.lastName = Lastname.Text;
            client.firstName = Firstname.Text;
            client.county = Counter.Text;
            client.city = Sity.Text;
            client.street = Street.Text;
            client.nomer = Number.Text;
            client.homeTelephone = HomeTelephone.Text;
            client.rabTelephone = RabTelephone.Text;
            clients.FindAll(s => s.id == client.id)
                .ForEach(x => x.editClient(client));
            disableButtun();
            Button_Cancels.IsEnabled = false;
            GridClients.ItemsSource = null;
            GridClients.ItemsSource = clients;
        }
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            enableButtun();
            Button_Cancels.IsEnabled = true;
            delete = false;
        }
        private void Button_New(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            string temp = "";
            int id = clients[clients.Count - 1].id + 1;
            client.id = id;
            clients.Add (client);
            ID.Text = Convert.ToString(id);
            Name.Text = temp;
            Lastname.Text = temp;
            Firstname.Text = temp;
            Counter.Text = temp;
            Sity.Text = temp;
            Street.Text = temp;
            Number.Text = temp;
            HomeTelephone.Text = temp;
            RabTelephone.Text = temp;
            newClient = client;
            delete = true;
            Button_Cancels.IsEnabled = true;
            enableButtun();
        }
        private void Button_ImportFile(object sender, RoutedEventArgs e)
        {
            FileStream file = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);

            string s_line = reader.ReadToEnd();
            clients = JsonSerializer.Deserialize<List<Client>>(s_line);
            GridClients.ItemsSource = null;
            GridClients.ItemsSource = clients;
        }
        private void Button_ExportFile(object sender, RoutedEventArgs e)
        {
            FileStream file = new FileStream(pathToFile, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);

            writer.Write(JsonSerializer.Serialize<List<Client>>(clients));
            writer.Close();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            if (newClient != null)
            {
                clients.Remove(newClient);
                newClient = new Client();
            }else if (delete)
            {
                var select = GridClients.SelectedIndex;
                Client client = clients[select];
                clients.Remove(client);
                delete = false;
                GridClients.ItemsSource = null;
                GridClients.ItemsSource = clients;
            }
           
                Button_Edits.IsEnabled = true;
                Button_News.IsEnabled = true;
                Button_Saves.IsEnabled = false;
                Button_Cancels.IsEnabled = false;
                Button_Edits.IsEnabled = false;
            
            disableButtun();

        }

        private void GridClients_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var select = GridClients.SelectedIndex;
            if (select < clients.Count)
            {
                Client client = clients[select];
                ID.Text = Convert.ToString(client.id);
                Name.Text = client.name;
                Lastname.Text = client.lastName;
                Firstname.Text = client.firstName;
                Counter.Text = client.county;
                Sity.Text = client.city;
                Street.Text = client.street;
                Number.Text = client.nomer;
                HomeTelephone.Text = client.homeTelephone;
                RabTelephone.Text = client.rabTelephone;
                delete = true;
                Button_Cancels.IsEnabled = true;
                Button_Edits.IsEnabled = true;
            }
        }
    }
}
