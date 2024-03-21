using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MFBauphysikMobilMAUI
{
    public class Constants
    {
        public const string DatabaseFileName = "BauphysikMobil";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        /* public static string DatabasePath
         {
             get
             {
                 var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                 return Path.Combine(basePath, DatabaseFileName);
             }
         }*/
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
