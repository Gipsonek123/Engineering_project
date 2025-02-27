using Engineering_project.Core;
using Engineering_project.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Linq.Expressions;


namespace Engineering_project.MVVM.ViewModel
{
    internal class CreateAccountViewModel : ObservableObject
    {
        private string _username;
        private string _password;
        private string _email;
        public ICommand CreateAccountCommand { get; }


        public string CAUsernameText
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(CAUsernameText));
                }
            }

        }

        public string CAEmailText
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(CAEmailText));
                }
            }

        }



        public string CAPasswordText
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(CAPasswordText));
                }
            }

        }



        public CreateAccountViewModel()
        {
            CreateAccountCommand = new RelayCommand(CreateAccount);
        }

        private async void CreateAccount()
        {
            // DLA KLASY 

 

            string HashedPassword = Hash(CAPasswordText);

            Users user = new Users(CAUsernameText, HashedPassword, CAEmailText, false);
            await Insert(user);
           

        }
        public async Task Insert(Users user)
        {
            using var db = new Database();
            await db.Users.AddAsync(user);
            try
            {
                await db.SaveChangesAsync();   // zabezpieczyc na blad z rejestracja z zajetym loginem
                MessageBox.Show("Stworzono konto");
            } catch
            {
                MessageBox.Show("Nazwa jest zajęta");
                return;
            }
        }
        
        // HASZOWANIE
        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: password!,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8));
            string saltBase64 = Convert.ToBase64String(salt); 
            string result = saltBase64 + ":" + hashed;
            return result;
        }


          

    }
}
