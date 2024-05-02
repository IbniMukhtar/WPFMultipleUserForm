using System;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Configuration;
namespace Multi_Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

/// <summary>
/// Login Functionality by calling VerifyUser Method
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    con.Open();
                    if (VerifyUser(txtUsername.Text, txtPassword.Password, con))
                    {
                        MessageBox.Show("Login Successful", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Verify User Method
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        private bool VerifyUser(string username, string password, SqlConnection con)
        {
            using (SqlCommand com = new SqlCommand("SELECT Status FROM Users WHERE username = @username AND password = @password", con))
            {
                com.Parameters.AddWithValue("@username", username);
                com.Parameters.AddWithValue("@password", password);
                using (SqlDataReader dr = com.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return Convert.ToBoolean(dr["Status"]);
                    }
                    return false;
                }
            }
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
