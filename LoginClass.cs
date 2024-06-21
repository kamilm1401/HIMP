namespace HIMP
{
    //Class for login and register user in program data base
    internal class LoginClass : MethodsForTheManagementClass
    {
        private string _login;
        private string _password;
        private bool _isRegistered; 

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                if (value.Length >= 6)
                {
                    SetMessage("login");
                    _login = value;
                }
                else
                {
                    LoginError("login", 6);
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value.Length >= 8)
                {
                    SetMessage("password");
                    _password = value;
                }
                else
                {
                    LoginError("password", 8);
                }
            }
        }

        public bool IsRegistered 
        {
            get
            {
                return _isRegistered;
            }
            set
            {
                _isRegistered = value;
            }
        }

        private static void LoginError(string name, int minimum)
        {
            Console.WriteLine($"Your {name} must contain a minimum of {minimum} characters");
            Console.WriteLine("Please try again");
            PressKey();
        }

        private static void SetMessage(string name)
        {
            Console.WriteLine($"Your {name} has been established");
            PressKey();
        }

        private static void PressKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
