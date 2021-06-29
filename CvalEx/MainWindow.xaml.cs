using System;
using System.Data.SqlClient;
using System.Windows;


namespace CvalEx
{

    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        public MainWindow()
        {

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = @"DESKTOP-ARHG322\SQLEXPRESS",
                    InitialCatalog = "CvalEx",
                    IntegratedSecurity = true
                };
                connection = new SqlConnection(builder.ConnectionString);
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
            connection.Open();
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
                            new MainMenu(connection, this);
        }
    }
}
