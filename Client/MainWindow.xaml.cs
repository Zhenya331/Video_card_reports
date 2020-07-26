using Client.SQLResult;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client
{
    public partial class MainWindow : Window
    {
        public const string ip = "127.0.0.1";
        public const int port = 8080;

        public MainWindow()
        {
            InitializeComponent();
        }

        public static StringBuilder tcpGetData(Socket tcpSocket)
        {
            var buffer = new byte[256];
            int size = 0;
            var answer = new StringBuilder();
            do
            {
                size = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (tcpSocket.Available > 0);
            return answer;
        }

        public static void tcpSendData(Socket tcpSocket, string data)
        {
            tcpSocket.Send(Encoding.UTF8.GetBytes(data));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            bool check = LoginClass.CheckDelimiter(User.Text) && LoginClass.CheckDelimiter(Password.Password.ToString());
            bool check1 = (User.Text != "") && (Password.Password.ToString() != "");
            bool check2 = Code.Check(User.Text) && Code.Check(Password.Password.ToString());
            bool check3 = Code.CheckSQL(User.Text);

            if (check && check1 && check2 && check3)
            {
                using (LoginClass loginClass = new LoginClass(User.Text, Password.Password.ToString()))
                {
                    if (loginClass.Result())
                    {
                        Autorisation.Visibility = Visibility.Collapsed;
                        Queries.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                MessageBox.Show("Неправильный формат заданных данных");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            using (Exit exit = new Exit(User.Text))
            {
                exit.Result();
            }
            Queries.Visibility = Visibility.Collapsed;
            Autorisation.Visibility = Visibility.Visible;
        }

        private void Ready_Click(object sender, RoutedEventArgs e)
        {
            string path = @Path.Text;
            bool checkPath0 = (path != "");
            bool checkPath1 = checkPath0? path[path.Length - 1] == '\\': false;

            if (checkPath0 && checkPath1)
            {
                switch (QueriesTab.SelectedIndex)
                {
                    case 0:
                        InpArchCostAvg data;
                        bool check0 = Code.CheckSQL(ArchNameSQL1.Text);
                        if (check0)
                        {
                            data.Arch = ArchNameSQL1.Text;
                            using (AvgCostArch avgCostArch = new AvgCostArch(data, User.Text, path))
                            {
                                avgCostArch.Result();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неправильный формат заданных данных");
                        }
                        break;

                    case 1:
                        InpManEnergBeetw data1;
                        data1.ManName = ManufactNameSQL2.Text;
                        bool check11 = Int32.TryParse(MinEnergySQL2.Text, out data1.MinEnerg);
                        bool check12 = Int32.TryParse(MaxEnergySQL2.Text, out data1.MaxEnerg);
                        bool check13 = Code.CheckSQL(ManufactNameSQL2.Text);

                        if (check11 && check12 && check13)
                        {
                            using (ManEnergyBetween manEnergyBetween = new ManEnergyBetween(data1, User.Text, path))
                            {
                                manEnergyBetween.Result();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неправильный формат заданных данных");
                        }
                        break;

                    case 2:
                        InpManMemEachYear data2;
                        data2.ManName = ManufactNameSQL3.Text;
                        bool check21 = Code.CheckSQL(ManufactNameSQL3.Text);
                        if (check21)
                        {
                            using (ManMemEachYear manMemEachYear = new ManMemEachYear(data2, User.Text, path))
                            {
                                manMemEachYear.Result();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неправильный формат заданных данных");
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неправильно указан путь");
            }
        }
    }
}
