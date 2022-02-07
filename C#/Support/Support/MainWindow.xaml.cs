using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;

namespace Support
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<SupptorControlVM> supportList = new List<SupptorControlVM>();
        public MainWindow()
        {
            InitializeComponent();
            int count = 100;
            int rowCount = 1;
            int columnCount = 1;
            while (true)//根据支架数量 计算所需grid 行和列
            {
                if (columnCount*rowCount>=count)
                {
                    break;
                }
                else
                {
                    columnCount++;
                    if (columnCount>1.77*rowCount)//16:9 排列
                    {
                        rowCount++;
                    }
                }
            }
            //添加Grid行
            GridForSupport.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < rowCount; i++)
            {
                GridForSupport.RowDefinitions.Add(new RowDefinition());
            }
            GridForSupport.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            //添加Grid列
            GridForSupport.ColumnDefinitions.Add(new ColumnDefinition() { Width=new GridLength(30) });
            for (int i = 0; i < columnCount; i++)
            {
                GridForSupport.ColumnDefinitions.Add(new ColumnDefinition());
            }
            GridForSupport.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            //设置按钮位置
            btnclose.SetValue(Grid.RowProperty,0);
            btnclose.SetValue(Grid.ColumnProperty, columnCount);
            //动态加载支架控件
            for (int i = 0; i < rowCount; i++)//先填满行
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if ((i * columnCount + j+1) <= count)
                    {
                        SupptorControl support = CreatSupport();
                        SupptorControlVM vm = new SupptorControlVM();
                        support.DataContext = vm;
                        vm.SupportNo = $"{i * columnCount + j+1}#";

                        GridForSupport.Children.Add(support);
                        support.SetValue(Grid.RowProperty, i + 1);
                        support.SetValue(Grid.ColumnProperty, j + 1);

                        supportList.Add(vm);
                    }
                }
            }

            Task.Run(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    supportList[i].IsLighten = true;
                    System.Threading.Thread.Sleep(2000);
                    supportList[i].IsLighten = false;
                }
            });
        }

        public SupptorControl CreatSupport()
        {
            string supportName = "Support.SupptorControl";
            Type t = Type.GetType(supportName);
            return (SupptorControl)Activator.CreateInstance(t);
        }
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void SupportGraph_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
