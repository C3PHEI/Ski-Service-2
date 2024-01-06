using Ski_Service_2.Orders;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace Ski_Service_2
{
    public partial class Login : Window
    {
        private int loginAttempts = 0;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(AppConfig.ConnectionString);

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                String query = "SELECT * FROM dbo.Benutzer WHERE Benutzername=@Username AND Passwort=@Password AND Blockiert=0";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password.Trim());

                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MainWindow dashboard = new MainWindow();
                        dashboard.Show();
                        this.Close();
                    }
                    loginAttempts = 0;
                }
                else
                {
                    loginAttempts++;

                    if (loginAttempts >= 3)
                    {
                        SetAccountBlocked(txtUsername.Text.Trim());
                    }

                    MessageBox.Show("Username oder Passwort ist falsch");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void SetAccountBlocked(string username)
        {
            // Setze Blockiert auf 1 (True) in der Datenbank
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(AppConfig.ConnectionString))
                {
                    sqlCon.Open();

                    String updateQuery = "UPDATE dbo.Benutzer SET Blockiert = 1 WHERE Benutzername = @Username";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, sqlCon);
                    updateCmd.Parameters.AddWithValue("@Username", username);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Konto wurde blockiert.");
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Blockieren des Kontos.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
