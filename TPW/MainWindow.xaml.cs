using System;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;

namespace TPW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
  
    
    public partial class MainWindow
    {
        public Calendar calendar;
        public MainWindow()
        {
            InitializeComponent();
            calendar = new Calendar();
            calendar.SetUp();
        }
        
        private void Add_Wydarzenie(object sender, RoutedEventArgs e)
        {
           calendar.Add_Wydarzenie(namePicker.Text, datePicker.DisplayDate );
            icTodoList.ItemsSource = null;
            icTodoList.ItemsSource = calendar.items;
        }
    }
}