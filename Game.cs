using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp10
{
    class Game //重構
    {
        private Board board = new Board();

        private PieceType currentPlayer = PieceType.BLACK;

        private PieceType winner = PieceType.NONE;
        public PieceType Winner { get { return winner; } }


        public bool CanBePlaced(int x , int y)
        {
            return board.CanBePlaced(x, y);
        }

        public Piece PlaceAPiece(int x,int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);// *簡化
            if (piece != null)
            {
                //檢查現在下棋的人是否獲勝
                checkWinner();

                //交換選手
                if (currentPlayer == PieceType.BLACK)
                    currentPlayer = PieceType.WHITE;
                else if (currentPlayer == PieceType.WHITE)
                    currentPlayer = PieceType.BLACK;

                return piece;

            }

            return null;
        }

        private void checkWinner()
        {
            int centerX = board.LastPlacedNode.X;
            int centerY = board.LastPlacedNode.Y;

            //檢查8個不同方向
            for (int xDir = -1; xDir <= 1; xDir++)
            {                                                                      
                for (int yDir = -1; yDir <= 1; yDir++)        // 3x3=9     (-1,0,1)    
                {
                    //排除中間情況   (x,y)
                    if (xDir == 0 & yDir == 0)
                        continue;



                    //紀錄現在看到幾顆相同的棋子
                        int count = 1; //count代表第幾顆棋子
                    while (count < 5)
                    {
                        int targetX = centerX + count * xDir; //xDir和yDir乘上第幾顆棋子再加上中心點位置
                        int targetY = centerY + count * yDir;

                        //檢查顏色是否相同
                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                            targetY < 0 || targetY >= Board.NODE_COUNT ||
                            board.GetPieceType(targetX, targetY) != currentPlayer)//請問交叉點上的棋子是什麼顏色 (如果現在的棋子不等於玩家棋子的顏色則跳出迴圈)
                            break;

                        count++;
                    }

                    //檢查是否看到五顆棋子
                    if (count == 5)
                        winner = currentPlayer;
                }
            }
        }

      
    }
}
