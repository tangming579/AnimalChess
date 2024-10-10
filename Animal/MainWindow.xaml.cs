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
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;

namespace Animal
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 变量
        //棋盘高度
        public double BoardHeight = 0;
        //棋盘宽度
        public double BoardWidth = 0;
        //棋盘坐标
        Point BoardPoint = new Point(0, 0);
        //棋盘中所有棋子集合
        List<ChessPiece> listChess = new List<ChessPiece>();
        //当前选中的棋子
        ChessPiece selectedPiece;
        //判断走棋方
        int player = -1;
        //玩家1棋子颜色
        public Color Player0Color;
        public Color Player1Color;
        //标示当前走子玩家
        Ellipse ellipse = new Ellipse();

        #endregion

        public MainWindow()
        {
            InitializeComponent();            
        }

        #region 初始化

        private void ChessBoard_Loaded(object sender, RoutedEventArgs e)
        {
            //树叶与白云动画效果
            Storyboard sbd = Resources["leafLeave"] as Storyboard;
            sbd.Begin();
            Storyboard baiyun = Resources["cloudMove"] as Storyboard;
            baiyun.Begin();

            //获取棋盘大小
            BoardWidth = ChessBoard.ActualWidth;
            BoardHeight = ChessBoard.ActualHeight;
            DrawChess();
        }  

        /// <summary>
        /// 初始化棋盘部分数据
        /// </summary>
        private void DrawChess()
        {
            double bHeight = BoardHeight / 4;
            double bWidth = BoardWidth / 4;
            ellipse.Fill = new SolidColorBrush(Colors.Red);
            ellipse.Height = 30;
            ellipse.Width = 30;
            Grid.SetRow(ellipse, 1);
            Grid.SetColumn(ellipse, 1);
            Canvas.SetRight(ellipse, -140);
            Canvas.SetTop(ellipse, 90);
            myCanvas.Children.Add(ellipse);
        }

        /// <summary>
        /// 在棋盘中初始化棋子
        /// </summary>
        public void CreateChess()
        {
            
            double bHeight = BoardHeight / 4;
            double bWidth = BoardWidth / 4;
            player = -1;
            listChess.Clear();
            ChessBoard.Children.Clear();
            txbMessage.Text = string.Empty;

            #region 初始化棋子
            ChessPiece rElephant = new ChessPiece("象",Colors.Red,7);
            
            listChess.Add(rElephant);

            ChessPiece rLion = new ChessPiece("狮",Colors.Red,6);
            
            listChess.Add(rLion);

            ChessPiece rTiger = new ChessPiece("虎",Colors.Red,5);
            
            listChess.Add(rTiger);

            ChessPiece rPanther = new ChessPiece("豹",Colors.Red,4);
            
            listChess.Add(rPanther);

            ChessPiece rWolf = new ChessPiece("狼",Colors.Red,3);
           
            listChess.Add(rWolf);
            
            ChessPiece rDog = new ChessPiece("狗",Colors.Red,2);
            
            listChess.Add(rDog);

            ChessPiece rCat = new ChessPiece("猫",Colors.Red,1);
            
            listChess.Add(rCat);

            ChessPiece rMouse = new ChessPiece("鼠",Colors.Red,0);
            
            listChess.Add(rMouse);

            ChessPiece bElephant = new ChessPiece("象",Colors.Black,7);
           
            bElephant.Color = Colors.Black;

            listChess.Add(bElephant);

            ChessPiece bLion = new ChessPiece("狮",Colors.Black,6);
            
            listChess.Add(bLion);

            ChessPiece bTiger = new ChessPiece("虎",Colors.Black,5);
           
            listChess.Add(bTiger);

            ChessPiece bPanther = new ChessPiece("豹",Colors.Black,4);
            
            listChess.Add(bPanther);

            ChessPiece bWolf = new ChessPiece("狼",Colors.Black,3);
            
            listChess.Add(bWolf);

            ChessPiece bDog = new ChessPiece("狗",Colors.Black,2);
           
            listChess.Add(bDog);

            ChessPiece bCat = new ChessPiece("猫",Colors.Black,1);
            
            listChess.Add(bCat);

            ChessPiece bMouse = new ChessPiece("鼠",Colors.Black,0);
            
            listChess.Add(bMouse);
            #endregion

            //随机打乱棋子集合顺序
            listChess = ListRandom(listChess);

            for (int j = 0; j < 16; j++)
            {
                listChess[j].MouseLeftButtonDown += chessPiece_MouseLeftButtonDown;
                //listChess[j].Height = bHeight;
                //listChess[j].Width = bWidth;
                listChess[j].BorderThickness = new Thickness(2.5);
                listChess[j].BorderBrush = new SolidColorBrush(Colors.YellowGreen);
                listChess[j].PieceRow = (j + 4) % 4;
                listChess[j].PieceCol = j / 4;
                Grid.SetColumn(listChess[j], j / 4);
                Grid.SetRow(listChess[j], (j + 4) % 4);
                ChessBoard.Children.Add(listChess[j]);
            }
            txbMessage.Text = "游戏开始！请翻牌"+'\n';
                
        }
        #endregion

        #region 鼠标按下和抬起
        private void chessPiece_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChessPiece element = sender as ChessPiece;

            if (player == -1)
            {
                txbPlayer1.Foreground = new SolidColorBrush(element.Color);
                Player0Color = element.Color;
                if (element.Color == Colors.Red)
                {
                    txbPlayer2.Foreground = new SolidColorBrush(Colors.Black);
                    Player1Color = Colors.Black;
                    txbMessage.Text += "玩家0执红棋，玩家1执黑棋" + '\n';
                }
                else
                {
                    txbPlayer2.Foreground = new SolidColorBrush(Colors.Red);
                    Player1Color = Colors.Red;
                    txbMessage.Text += "玩家0执黑棋，玩家1执红棋" + '\n';
                }
                player++;
            }
            if (element != null)
            {
                //点击一个没有翻开的棋子
                if (element.Status == false)
                {             
                    element.Status = true;

                    //已经选中一个棋子，再掀开另一个，则为撞棋
                    if (selectedPiece != null)
                    {
                        if(CanMove(selectedPiece,element.PieceRow,element.PieceCol))
                        {
                            if (selectedPiece.Color != element.Color)
                            {
                                if (CanEat(selectedPiece, element))
                                {
                                    if (selectedPiece.Name == element.Name)
                                    {
                                        ChessBoard.Children.Remove(element);
                                        listChess.Remove(element);
                                        ChessBoard.Children.Remove(selectedPiece);
                                        listChess.Remove(selectedPiece);
                                        IsWin();
                                        txbMessage.Text += "撞棋！掀开棋子为对方" + element.Name + "," + "两子级别相同，都木有了" + '\n';
                                    }
                                    else
                                    {
                                        Grid.SetRow(selectedPiece, element.PieceRow);
                                        Grid.SetColumn(selectedPiece, element.PieceCol);
                                        selectedPiece.PieceRow = element.PieceRow;
                                        selectedPiece.PieceCol = element.PieceCol;
                                        ChessBoard.Children.Remove(element);
                                        listChess.Remove(element);
                                        IsWin();
                                        txbMessage.Text += "撞棋！掀开棋子为对方" + element.Name + "," + "被" + selectedPiece.Name + "吃" + '\n';
                                        
                                    }
                                }
                                else
                                {
                                    ChessBoard.Children.Remove(selectedPiece);
                                    listChess.Remove(selectedPiece);
                                    IsWin();
                                    txbMessage.Text += "撞棋！掀开棋子为对方" + element.Name+"," +selectedPiece.Name+"被吃"+ '\n';
                                }
                            }
                        }
                    }
                    ChangePlayer();
                }
                 //点击一个已经翻开的棋子
                else
                {
                    //只能选择自己颜色的棋子，不能选择对方的棋子
                    if ((player == 0 && element.Color == Player0Color) || (player == 1 && element.Color == Player1Color))
                    {
                        if (element.isSelected == false)
                        {
                            foreach (ChessPiece p in listChess)
                            {
                                p.BorderBrush = new SolidColorBrush(Colors.YellowGreen);
                                p.isSelected = false;
                            }
                            element.BorderBrush = new SolidColorBrush(Colors.Red);
                            element.Cursor = Cursors.Hand;
                            element.isSelected = true;
                            this.selectedPiece = element;
                            //ChangePlayer();
                            //MessageBox.Show(element.PieceRow.ToString() + "," + element.PieceCol.ToString());
                        }
                        else
                        {
                            foreach (ChessPiece p in listChess)
                            {
                                p.BorderBrush = new SolidColorBrush(Colors.YellowGreen);
                                this.selectedPiece = null;
                                element.isSelected = false;
                            }
                        }
                    }
                }
            }
        }

        ///<summary>
        ///判断当前坐标所在网格行列号
        /// </summary>
        private Point getGridIndex(double x, double y)
        {
            double bWidth = BoardWidth / 4;//每个网格宽度
            double bHeight = BoardHeight / 4;//每个网格高度
            double row = 0;
            double col = 0;
            for (int i = 0; i < 4; i++)
            {
                if (x >= bWidth * i && x < bWidth * (i + 1))
                {
                    col = i;
                    break;
                }
            }
            for (int j = 0; j < 4 * (j + 1); j++)
            {
                if (y >= bHeight * j && y < (j + 1) * bHeight)
                {
                    row = j;
                    break;
                }
            }
            return (new Point(row, col));
        }

        ///<summary>
        ///判断当前网格中的border序号
        /// </summary>
        private int getBorderIndex(double left, double top)
        {
            double bWidth = BoardWidth / 4;//每个网格宽度
            double bHeight = BoardHeight / 4;//每个网格高度
            int positionX = (int)((left + 1) / bWidth);
            int positionY = (int)((top + 1) / bHeight) * 4;
            return positionX + positionY;
        }

        
 
        /// <summary>
        /// 抬起鼠标时判断行棋规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChessBoard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedPiece != null)
            {
                //得到鼠标抬起网格行列数
                Point p = getGridIndex(e.GetPosition(ChessBoard).X, e.GetPosition(ChessBoard).Y);
                int mouseRow = (int)p.X;
                int mouseCol = (int)p.Y;

                if (CanMove(selectedPiece, mouseRow, mouseCol))
                {
                    ChessPiece target = GetPiece(mouseRow, mouseCol);
                    if (target != null)
                    {
                        if (CanEat(selectedPiece, target))
                        {
                            //级别相同，两个都消失
                            if (selectedPiece.Name == target.Name)
                            {
                                ChessBoard.Children.Remove(target);
                                listChess.Remove(target);
                                ChessBoard.Children.Remove(selectedPiece);
                                listChess.Remove(selectedPiece);
                                ChangePlayer();
                                IsWin();
                            }
                            else
                            {
                                ChessBoard.Children.Remove(target);
                                listChess.Remove(target);
                                Grid.SetRow(selectedPiece, mouseRow);
                                Grid.SetColumn(selectedPiece, mouseCol);
                                selectedPiece.PieceRow = mouseRow;
                                selectedPiece.PieceCol = mouseCol;
                                ChangePlayer();
                                IsWin();
                            }
                        }
                    }
                    else
                    {
                        Grid.SetRow(selectedPiece, mouseRow);
                        Grid.SetColumn(selectedPiece, mouseCol);
                        selectedPiece.PieceRow = mouseRow;
                        selectedPiece.PieceCol = mouseCol;
                        ChangePlayer();
                    }
                }
                
            }

        }
        #endregion

        #region 主要方法
        /// <summary>
        /// 交换执棋者
        /// </summary>
        public void ChangePlayer()
        {
            foreach (ChessPiece p in listChess)
            {
                p.BorderBrush = new SolidColorBrush(Colors.YellowGreen);
                p.isSelected = false;
            }
            selectedPiece = null;
            player = ++player % 2;
            if (player == 0)
            {
                //上边
                Canvas.SetRight(ellipse, -140);
                Canvas.SetTop(ellipse, 90);
            }
            else
            {
                //下边
                Canvas.SetRight(ellipse, -140);
                Canvas.SetTop(ellipse, 155);
            }
        }

        /// <summary>
        /// 判断网格中是否有棋子
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private ChessPiece GetPiece(int row, int col)
        {
            bool bExist = false;
            ChessPiece chessPiece=new ChessPiece();
            foreach (ChessPiece c in listChess)
            {
                if (c.PieceRow == row && c.PieceCol == col)
                {
                    bExist = true;
                    chessPiece = c;
                    break;
                }
            }
            if (bExist == false)
            {
                return null;
            }
            else
            {
                return chessPiece;
            }
        }

        /// <summary>
        /// 棋子互吃规则
        /// </summary>
        /// <param name="movePiece"></param>
        /// <param name="targetPiece"></param>
        /// <returns></returns>
        private bool CanEat(ChessPiece movePiece,ChessPiece targetPiece)
        {
            //互吃的子必须颜色不同
            if (movePiece.Color != targetPiece.Color)
            {
                if (movePiece.Name == "鼠" && targetPiece.Name == "象")
                {
                    return true;
                }
                else if (movePiece.Name == "象" && targetPiece.Name == "鼠")
                {
                    return false;
                }
                else if (movePiece.Index >= targetPiece.Index)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 判断能否移动
        /// </summary>
        /// <param name="chessPiece"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool CanMove(ChessPiece chessPiece, int row, int col)
        {
            if (chessPiece.PieceRow != row && chessPiece.PieceCol != col)
            {
                return false;
            }
            else
            {
                if (chessPiece.Name == "豹")
                {
                    foreach (ChessPiece c in listChess)
                    {
                        //向上移动了
                        if (selectedPiece.PieceRow == row && selectedPiece.PieceCol > col
                            && c.PieceRow == row && c.PieceCol > col && c.PieceCol < selectedPiece.PieceCol)
                        {
                            return false;
                        }
                        //向下移动了
                        else if (selectedPiece.PieceRow == row && selectedPiece.PieceCol < col
                            && c.PieceRow == row && c.PieceCol < col && c.PieceCol > selectedPiece.PieceCol)
                        {
                            return false;
                        }
                        //向左移动了
                        else if (selectedPiece.PieceCol == col && selectedPiece.PieceRow > row
                            && c.PieceCol == col && c.PieceRow > row && c.PieceRow < selectedPiece.PieceRow)
                        {
                            return false;
                        }
                        //向右移动了
                        else if (selectedPiece.PieceCol == col && selectedPiece.PieceRow < row
                            && c.PieceCol == col && c.PieceRow < row && c.PieceRow > selectedPiece.PieceRow)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if ((Math.Abs(col - selectedPiece.PieceCol) == 1 && row == selectedPiece.PieceRow) || (Math.Abs(row - selectedPiece.PieceRow) == 1 && col == selectedPiece.PieceCol))
                    {
                        return true;
                    }
                    return false;
                }                
            }
        }
        
        /// <summary>
        /// 判断获胜方
        /// </summary>
        public void IsWin()
        {
            List<ChessPiece> balck = new List<ChessPiece>();
            List<ChessPiece> red = new List<ChessPiece>();
            foreach (ChessPiece c in ChessBoard.Children)
            {
                if (c.Color == Colors.Black)
                {
                    balck.Add(c);
                }
                else
                {
                    red.Add(c);
                }
            }
            if (balck.Count == 0&&red.Count!=0)
            {
                MessageBox.Show("红棋获胜");
            }
            else if (red.Count == 0 && balck.Count != 0)
            {
                MessageBox.Show("黑棋获胜");
            }
            else if(red.Count==0&&balck.Count==0)
            {
                MessageBox.Show("和棋");
            }
        }

        /// <summary>
        /// 随机排列数组元素
        /// </summary>
        /// <param name="listChess"></param>
        /// <returns></returns>
        private List<ChessPiece> ListRandom(List<ChessPiece> listChess)
        {
            
            Random ran = new Random();
            List newList = new List();
            int k = 0;
            ChessPiece c = new ChessPiece();
            for (int i = 0; i < listChess.Count; i++)
            {

                k = ran.Next(0, 15);
                if (k != i)
                {
                    c = listChess[i];
                    listChess[i] = listChess[k];
                    listChess[k] = c;
                }
            }
            return listChess;
        }
#endregion

        #region 按钮事件
        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            CreateChess();
        }

        private void menuExist_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messBox = MessageBox.Show("确定要退出游戏？", "退出游戏", MessageBoxButton.OKCancel);
            if (messBox == MessageBoxResult.OK)
            {
                Close();
            }
        }

        private void menuChange_Click(object sender, RoutedEventArgs e)
        {
            if (player != -1)
            {
                ChangePlayer();
            }
        }
        #endregion

    }
}
