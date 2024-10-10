using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Animal
{
    /// <summary>
    /// Panel1.xaml 的交互逻辑
    /// </summary>
    public partial class Panel1 : UserControl
    {
        public Panel1()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Panel1_MouseDown);
        }

        void Panel1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rotate3DContainer c = (Rotate3DContainer)ContainerUtils.GetNearestContainer(this);
            if (c != null)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    c.Turn(true);
                }
                else if (e.ChangedButton == MouseButton.Right)
                {
                    c.Turn(false);
                }
            }
        }
    }
}
