using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;   
using System.Windows;
using Data_Management;

namespace AssignmentOleg
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        { 
            DatabaseBuilder build = new DatabaseBuilder();
            build.DataBaseBuilder();
            if (build.DoTableExist() == false)
            { 
                build.BuildDataBaseTable();
                build.SeedDataBaseTable();
            }
        }
    }
}
