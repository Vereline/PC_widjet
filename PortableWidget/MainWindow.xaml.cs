using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PortableWidget.Core;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace PortableWidget
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Analyser Analyser;

        public MainWindow()
        {
            InitializeComponent();
            Analyser = new Analyser(1000);
            Analyser.Start();
            Closing += this.OnWindowClosing;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Analyser.Stop();
            Process.GetCurrentProcess().Kill();
        }
    }
}
