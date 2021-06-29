using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;

namespace CvalEx
{

    public partial class valk : Window
    {
        public class valk_class
        {
            public string Name_points { get; set; }
            public string GPS_coordinates { get; set; }
            public string Existence_pavilion { get; set; }
            public string Availability_USB { get; set; }
}


        SqlConnection connection;
        Window parent;
        public valk(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.Show();
            this.connection = connection;
            this.parent = parent;
            parent.Visibility = Visibility.Hidden;
            function_show();

        }

        public void function_show()
        {
            string sqlExpression = "SELECT * FROM DESTINATION";
            List<valk_class> valk_list = new List<valk_class>();
            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        valk_class st_rec = new valk_class();

                        st_rec.Name_points = reader.GetValue(0).ToString();
                        st_rec.GPS_coordinates = reader.GetValue(1).ToString();
                        st_rec.Existence_pavilion = reader.GetValue(2).ToString();
                        st_rec.Availability_USB = reader.GetValue(3).ToString();

                        valk_list.Add(st_rec);
                    }

                    reader.Close();
                    grid.ItemsSource = valk_list;

            grid.Columns[1].Header = "Название остановки";
            grid.Columns[2].Header = "Координаты";
            grid.Columns[3].Header = "Наличие павильона";
            grid.Columns[4].Header = "Наличие USB";

        }

                else
                {
                    reader.Close();
                    grid.ItemsSource = null;
                }
            }

            catch (SqlException e)
            {
                MessageBox.Show("Произошла ошибка: " + e.Number + "." + e.Message);
                this.Close();
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void search_function_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string sqlExpression = String.Format($"SELECT * FROM DISTANATION WHERE (Name LIKE '%{search_function.Text}%' AND status <> 'Удален') ");
            List<valk_class> renters_list = new List<valk_class>();
            try
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        valk_class st_rec = new valk_class();

                        st_rec.Name_points = reader.GetValue(0).ToString();
                        st_rec.GPS_coordinates= reader.GetValue(1).ToString();
                        st_rec.Existence_pavilion = reader.GetValue(2).ToString();
                        st_rec.Availability_USB = reader.GetValue(3).ToString();


                        valk_list.Add(st_rec);
                    }

                    reader.Close();
                    grid.ItemsSource = valk_list;

                    grid.Columns[0].Visibility = Visibility.Hidden;
                    grid.Columns[1].Header = "Название остановки";
                    grid.Columns[2].Header = "Координаты";
                    grid.Columns[3].Header = "Наличие павильона";
                    grid.Columns[4].Header = "Наличие USB";
                }

                else
                {
                    reader.Close();
                    grid.ItemsSource = null;
                }
            }

            catch (SqlException er)
            {
                MessageBox.Show("Произошла ошибка: " + er.Number + "." + er.Message);
                this.Close();
            }



        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            search_function.Text = "";
            function_show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            valk_class row = (valk_class)grid.SelectedItem;
            if (row != null)
            {
                string tmp_id = row.Name_points;
                try
                {
                    string sqlexpression = "DELETE FROM POINTS WHERE  Name_points= @Name_points";
                    SqlCommand command = new SqlCommand(sqlexpression, connection);
                    SqlParameter id_param = new SqlParameter("@id_value", tmp_id);
                    command.Parameters.Add(id_param);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись была удалена! ");
                    function_show();
                }
                catch (SqlException er)
                {
                    MessageBox.Show(er.Message);
                }

            }
        }

        private void ToAddRenter_Click(object sender, RoutedEventArgs e)
        {
            new AddRenterWindow(connection, this);
        }

        private void ToEditRenter_Click(object sender, RoutedEventArgs e)
        {
            valk_class row = (valk_class)grid.SelectedItem;

            if (row != null)
            {
                string tmp_id = row.Name_points,
                new EditExistRenter(connection, this, tmp_id);
            }
        }
    }
}
