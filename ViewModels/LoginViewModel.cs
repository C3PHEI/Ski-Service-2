using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Ski_Service_2
{
    public class LoginViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private int loginAttempts;
        private RelayCommand loginCommand;

        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand(Login);

                return loginCommand;
            }
        }

        private void Login()
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-2MEVUSI\SQLEXPRESS01;Initial Catalog=SkiService;Integrated Security=True"))
                {
                    sqlCon.Open();

                    String query = "SELECT * FROM dbo.Benutzer WHERE Benutzername=@Username AND Passwort=@Password AND Blockiert=0";
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCmd.Parameters.AddWithValue("@Username", Username);
                        sqlCmd.Parameters.AddWithValue("@Password", Password);

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Hier könnte Logik für den Übergang zur Hauptansicht platziert werden
                            }
                            loginAttempts = 0;
                        }
                        else
                        {
                            loginAttempts++;

                            if (loginAttempts >= 3)
                            {
                                SetAccountBlocked(Username);
                            }

                            MessageBox.Show("Username oder Passwort ist falsch");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Login: {ex.Message}");
            }
        }

        private void SetAccountBlocked(string username)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-2MEVUSI\SQLEXPRESS01;Initial Catalog=SkiService;Integrated Security=True"))
                {
                    sqlCon.Open();

                    String updateQuery = "UPDATE dbo.Benutzer SET Blockiert = 1 WHERE Benutzername = @Username";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, sqlCon))
                    {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

