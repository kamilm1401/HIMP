using HIMP;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Newtonsoft.Json;


namespace HIMP
{
    internal class MethodsManagmentForRegisterAndLogin : LoginClass
    {
        LoginClass LoginAndPassword = new LoginClass();
        List<LoginClass> LoginData = new();
        InventoryManagement ManagmentFromOtherCalss = new InventoryManagement();
        internal void RegisterAndLogin()
        {
            LoadUserDatabase();
            while (true)
            {
                if (!LoginData.Any(user => user.IsRegistered))
                {
                    RegisterAccountMethod();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Login menu");
                    Console.WriteLine("If you forgot your password or login you can register account again but this action this action will delete all of your data");
                    Console.WriteLine("login - 1");
                    Console.WriteLine("Resrat account - 2");
                    if(byte.TryParse(Console.ReadLine(), out byte chose) && chose == 1 || chose == 2)
                    {
                        if(chose == 1)
                        {
                            UserLogin();
                            break;
                        }
                        else if(chose ==2)
                        {
                            
                            DeleteAccount();
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect data provided. Please try again.");
                        continue;
                    }

                }
            }
        }

        private void SaveUserDatabase()
        {
            string dataBaseLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserRegisterDataBase.json");
            string HandlingTheDataBase = JsonConvert.SerializeObject(LoginData, Formatting.Indented);
            File.WriteAllText(dataBaseLocation, HandlingTheDataBase);
        }

        private void LoadUserDatabase()
        {
            string dataBaseLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserRegisterDataBase.json");

            try
            {
                if (File.Exists(dataBaseLocation))
                {
                    string HandlingTheDataBase = File.ReadAllText(dataBaseLocation);
                    List<LoginClass> loadedData = JsonConvert.DeserializeObject<List<LoginClass>>(HandlingTheDataBase);

                    if (loadedData != null)
                    {
                        LoginData = loadedData;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Error loading user database: {ex.Message}");
                Thread.Sleep(3000);
            }
        }

        private void DeleteAccount()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Deleting the account will also result in the deletion of your database. Are you sure?");
                Console.WriteLine("1 - yes");
                Console.WriteLine("2 - no");
                Console.WriteLine("Chose: ");
                if (byte.TryParse(Console.ReadLine(), out byte chose))
                {
                    switch (chose)
                    {
                        case 1:
                            LoginData.Clear();
                            ManagmentFromOtherCalss.DeleteAllInventoryElements();
                            SaveUserDatabase();
                            ManagmentFromOtherCalss.SaveInventoryToJson();
                            Console.WriteLine("Your account has been deleted, and the database has been cleared.");
                            Thread.Sleep(3000);
                            break;
                        case 2:
                            Console.WriteLine("Back to last menu");
                            break;

                    }
                    break;

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Chose bad value try again");
                    continue;
                }
            }
        }

        private void RegisterAccountMethod()
        {
            Console.Clear();
            Console.WriteLine("It has been detected that you don't have an account. Please register your login and password for the program");
            Console.WriteLine("Remember to save your login details, otherwise, you will lose all your progress.");
            Thread.Sleep(4000);

            
            do
            {
                Console.Clear();
                Console.WriteLine("Enter your login:");
                LoginAndPassword.Login = Console.ReadLine();
            } while (string.IsNullOrEmpty(LoginAndPassword.Login) || LoginAndPassword.Login.Length < 6 || LoginData.Any(u => u.Login == LoginAndPassword.Login));

            do
            {
                Console.Clear();
                Console.WriteLine("Enter your password:");
                LoginAndPassword.Password = Console.ReadLine();
            } while (string.IsNullOrEmpty(LoginAndPassword.Password) || LoginAndPassword.Password.Length < 8);

            LoginAndPassword.IsRegistered = true;
            LoginData.Add(LoginAndPassword);
            SaveUserDatabase();

            Console.Clear();
            Console.WriteLine("Your account is registered");
            Thread.Sleep(3000);
        }


        private void UserLogin()
        {
            int counter = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Login:");
                string userLogin = Console.ReadLine();
                Console.WriteLine("Password:");
                string userPassword = Console.ReadLine();

                // Znajdź użytkownika w liście LoginData
                LoginClass user = LoginData.Find(u => u.Login == userLogin);

                if (user != null && user.Password == userPassword)
                {
                    Console.WriteLine("Logged into the program");
                    Thread.Sleep(3000);
                    break;
                }
                else if (counter < 3)
                {
                    Console.WriteLine("Bad login or password, try again");
                    Thread.Sleep(3000);
                    counter++;
                }
                else if (counter == 3)
                {
                    Console.WriteLine("Bad login or password, one more attempt will reset the program to factory settings.");
                    counter++;
                }
                else
                {
                    LoginData.Clear();
                    ManagmentFromOtherCalss.DeleteAllInventoryElements();
                    SaveUserDatabase();
                    ManagmentFromOtherCalss.SaveInventoryToJson();
                    Console.WriteLine("Your account has been deleted, and the database has been cleared.");
                    Thread.Sleep(3000);
                    break;
                }
            }
        }
    }
}
