using System.Windows;

namespace Primer.SampleApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            //Loaded += (o, e) => MessageBox.Show(string.Format("{0}", this.DataContext.GetType()));
        }

       
    }
}
