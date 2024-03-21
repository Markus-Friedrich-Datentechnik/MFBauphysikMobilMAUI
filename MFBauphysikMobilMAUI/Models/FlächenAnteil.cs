using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;
using MFBauphysikMobilMAUI.Models;

namespace MFBauphysikMobilMAUI.Models
{
    public class FlächenAnteil : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double Anteil_Basis { get; set; }    
        public double Anteil_Sparren { get; set; }
        public double Anteil_Gefach {  get; set; }
        public double Anteil_Ständer {  get; set; }

        //Foreign Key
        [ForeignKey(typeof(MainModel))]
        public int ModelID { get; set; }
    }
}
