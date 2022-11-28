using Demo_ex.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Demo_ex.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
            Generate_captch.Content = GenerateRandomString();
        }
        private string GenerateRandomString() 
        { 
            Random random = new Random(); 
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"; 
            StringBuilder sb = new StringBuilder(); 
            for (int i = 0; i < 6; i++) 
                sb.Append(combination[random.Next(combination.Length)]); 
            return sb.ToString(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();

            string query = $"select UserSurname, UserLogin, UserPassword, RoleName from dbo.[User], dbo.[Role] where [User].UserRole = [Role].RoleID and [User].UserLogin = '{Login.Text}' and [User].UserPassword = '{Pass.Password}'";

            if (Login.Text != null && Pass.Password != null)
            {
                db.openConnection();
                SqlCommand command = new SqlCommand(query, db.getConnection());
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    if(Generate_captch.Content.ToString() == CheckCap.Text)
                    {
                        if (dataReader.GetString(3) == "Менеджер")
                        {

                            MainWindow main = new MainWindow();
                            main.Show();
                            this.Close();
                        }
                        if (dataReader.GetString(3) == "Клиент")
                        {
                            MainWindow main = new MainWindow();
                            main.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильная капча!");
                    }

                }
                else
                {
                    MessageBox.Show("Данные не корректны");
                }
            }
            else
            {
                MessageBox.Show("Введите данные");
            }
        }
    }
}
