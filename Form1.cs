using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        private Game game = new Game();


        private bool isBlack = true;
        public Form1()
        {
            InitializeComponent();

           // this.Controls.Add(new BlackPiece(10, 20));//把棋子加入斥窗元件之中 Controls是視窗本身的元件 是一種清單 然後用Add加入新的元件
           // this.Controls.Add(new WhitePiece(100, 200));
            
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Piece piece = game.PlaceAPiece(e.X, e.Y);//簡化
            if (piece != null)
            {
                this.Controls.Add(piece);

                //檢查是否有人獲勝
                if(game.Winner == PieceType.BLACK)
                {
                    MessageBox.Show("黑色獲勝");
                    Application.Restart();
                } else if (game.Winner == PieceType.WHITE)
                {
                    MessageBox.Show("白色獲勝");
                    Application.Restart();
                }
                
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           if(game.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }else
            {
                this.Cursor = Cursors.Default;
            }
        }

      
    }
}
