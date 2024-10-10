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
    /// ChesssPiece.xaml 的交互逻辑
    /// </summary>
    public partial class ChessPiece : UserControl
    {
        /// <summary>
        /// 棋子名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 棋子颜色
        /// </summary>
        public Color Color { get; set; }

        //棋子是否被掀开，false为背面，true为正面
        public bool Status = false;

        //棋子大小序号，判断吃子问题
        public int Index { get; set; }

        //棋子所在网格行列数
        public int PieceRow = 0;
        public int PieceCol = 0;
        //棋子正面
        Panel2 p2 = new Panel2();
        //棋子是否被选中
        public bool isSelected = false;

        public ChessPiece()
        {
            InitializeComponent();
        }
        public ChessPiece(string name,Color color,int index):this()
        {
            this.Name = name;
            this.Color = color;
            this.Index = index;

            this.my3D.Children.Clear();
            this.my3D.Children.Add(new Panel1());

            OpenChess(this);

            this.my3D.Children.Add(p2);
        }

        /// <summary>
        /// 掀开棋子
        /// </summary>
        /// <param name="chessPiece"></param>
        public void OpenChess(ChessPiece chessPiece)
        {
            if (chessPiece.Status == false)
            {
                //根据棋子信息判断棋子名称及颜色
                TextBlock text = new TextBlock();
                text.Text = chessPiece.Name;
                text.FontSize = 18;
                text.Foreground = new SolidColorBrush(chessPiece.Color);
                p2.LayP2.Children.Add(text);

                //根据index判断棋子正面图片
                switch (chessPiece.Index)
                {
                    case 0:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/mouse.jpg", UriKind.RelativeOrAbsolute))

                        }; break;
                    case 1:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/cat.jpg", UriKind.RelativeOrAbsolute))

                        }; break;
                    case 2:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/dog.jpg", UriKind.RelativeOrAbsolute))

                        }; break;
                    case 3:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/wolf.jpg", UriKind.RelativeOrAbsolute))

                        }; break;
                    case 4:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/Panther.jpg", UriKind.RelativeOrAbsolute))

                        }; break;
                    case 5:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/tiger.jpg", UriKind.RelativeOrAbsolute))

                        }; break;
                    case 6:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/lion.jpg", UriKind.RelativeOrAbsolute))

                        }; break;

                    default:
                        p2.Background = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("Image/Elephant.jpg", UriKind.RelativeOrAbsolute))

                        }; break;
                }
            }
        }
        private void myChess_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            
        }

        //public virtual bool CanMove(int row,int col,List<ChessPiece> chessPiece)
        //{
        //    if (Math.Abs(row - PieceRow) == 1)
        //    {

        //    }
        //}
    }
}
