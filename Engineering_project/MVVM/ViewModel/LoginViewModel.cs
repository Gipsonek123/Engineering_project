using Engineering_project.Core;
using Engineering_project.MVVM.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Engineering_project.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject
    {

        private string _username;
        private string _password;
        public ICommand LoginCommand { get; }
        public string LUsernameText
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(LUsernameText));
                }
            }

        }

        public string LPasswordText
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(LPasswordText));
                }
            }

        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        public async Task<List<Users>> CorrectUser(string name)
        {
            using (var db = new Database())
            { 
                return await db.Users.Where(t => t.Username == name).ToListAsync();
            }
        }

        private async void Login()
        {
            List<Users> correctUser = await CorrectUser(LUsernameText);

            if (correctUser.Count == 1)
            {
                string temp = correctUser[0].Password;
                string[] pass = temp.Split(":");

                byte[] salt = Convert.FromBase64String(pass[0]);
                string passwordHash = pass[1];
                string inputPassword = LPasswordText.Trim();
                string hashedInputPassword = Hash(salt, inputPassword);


                if (hashedInputPassword.Equals(passwordHash, StringComparison.Ordinal))
                {
                    MessageBox.Show("Zalogowano");
                    if (correctUser[0].IsAdmin)
                    {
                        MessageBox.Show("Zalogowano jako ADMIN");
                    }
                    return;
                }
                MessageBox.Show("Błedne dane");
            }
        }

        public string Hash(byte[] salt, string password)
        {

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: password!,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8));

            return hashed;
        }


    }
}
