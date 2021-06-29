using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace CvalEx
{

    public partial class MainMenu : Window
    {
        SqlConnection connection;
        Window parent;
        public MainMenu(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.connection = connection;
            this.Show();
            parent.Visibility = Visibility.Hidden;     
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

    }
}


