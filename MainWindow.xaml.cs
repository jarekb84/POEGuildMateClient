using System;
using System.Diagnostics;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using MahApps.Metro.Controls;

namespace POEGuildMateClient
{
    public partial class MainWindow : MetroWindow
    {
        private IFirebaseClient _client;
        
        public MainWindow()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "nXukb5seeinSbhAA1YBMFY7YwbKegcNCqk5b2Xg1",
                BasePath = "https://poeguildmate.firebaseio.com/"
            };

            _client = new FirebaseClient(config);
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("PathOfExileSteam");

            if (pname.Length > 0)
            {
                try
                {
                    var user = new User
                    {
                        ID = Guid.Parse(UserIdTextBox.Text),
                        LastLogin = DateTime.UtcNow,
                        Name = UserNameTextBox.Text
                    };

                    SetResponse response = _client.Set("users/" + UserIdTextBox.Text, user);

                    var result = response.ResultAs<User>();

                    ResultLabel.Content = "Update Worked! Logged in at " + user.LastLogin.ToLocalTime();
                }
                catch (Exception)
                {

                    ResultLabel.Content = "System Error! o_0";
                }
                
            }
            else
            {
                ResultLabel.Content = "POE not running!";
            }
        }

        public class User
        {
            public Guid ID { get; set; }
            public String Name { get; set; }
            public DateTime LastLogin { get; set; }
        }
    }
}
