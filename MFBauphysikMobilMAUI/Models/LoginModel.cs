using MFBauphysikMobilMAUI.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.Models
{
    public class LoginModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string loginName;

        public string LoginName
        {
            get
            {
                return loginName;
            }
            set
            {
                loginName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LoginName"));
                //Settings.LastLogin = value;
            }
        }
        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                //Settings.LastLoginPass = value;
            }
        }

        public LoginModel()
        {
           // LoginName = Settings.LastLogin;
           // Password = Settings.LastLoginPass;
        }
    }
}
