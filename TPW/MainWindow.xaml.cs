using System.Windows;

namespace TPW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
  
    
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
       
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Calculator.Add(float.Parse(a.Text) , float.Parse(b.Text)).ToString()) ;
        }
        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show( Calculator.Divide(float.Parse(a.Text) , float.Parse(b.Text)).ToString()) ;
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show( Calculator.Subtract(float.Parse(a.Text) , float.Parse(b.Text)).ToString()) ;
        }
        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show( Calculator.Multiply(float.Parse(a.Text) , float.Parse(b.Text)).ToString()) ;
        }
    }

   
}