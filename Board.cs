using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp10
{
    class Board
    {
        public static readonly int NODE_COUNT = 9;

        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);//沒有符合

        private static readonly int OFFSET = 75; // 棋盤邊緣的長度
        private static readonly int NODE_RADIUS = 10; // R
        private static readonly int NODE_DISTANCE = 75; // 每條線間的長度

        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];  //建一個9X9的二維陣列

        private Point lastPlacedNode = NO_MATCH_NODE;
        public Point LastPlacedNode  { get { return lastPlacedNode; } } //只能看不能寫

        public PieceType GetPieceType(int NoIdX ,int NoIdY) //目的:需要回傳目前交叉點的顏色是什麼 (pieces是Piece的類別，但想要回傳的是enum的PieceType)
        {
            if (pieces[NoIdX, NoIdY] == null)
                return PieceType.NONE;
            else
                return pieces[NoIdX, NoIdY].GetPieceType();

        }

        public bool CanBePlaced(int x , int y )
        {
            //找出最近的點(Node)

            Point NodeId = FindTheCloseNode(x, y);  //NodeIdX 可能會回傳出超過8的數字，例如會傳9，NodeId 方向x或y座標就會跑出9的數字，再執行到 " if (pieces[NodeId.X, NodeId.Y] != null)" 的 pieces陣列就會出錯


            //如果沒有的話，回傳false

            if (NodeId == NO_MATCH_NODE)
                return false;

            //如果有的話，檢查是否已經存在棋子

            if (pieces[NodeId.X, NodeId.Y] != null) //在這個位置上是否有放置棋子。 != 如果有則return null (已經有棋子，不能再放棋子了)
                return false;

            return true; 

        }

        public Piece PlaceAPiece(int x,int y,PieceType type) //(int x, int y, bool isBlack)把bool isBlack換成PieceType ，因為bool IsBlack意思偏向為除了黑色還有好幾種顏色，PieceType先定義了只有黑色和白色兩種，也只能傳黑色和白色
        {
            //找出最近的點(Node)

            Point NodeId = FindTheCloseNode(x, y);


            //如果沒有的話，回傳false

            if (NodeId == NO_MATCH_NODE)
                return null;  //null=沒有物件

            //如果有的話，檢查是否已經存在棋子

            if (pieces[NodeId.X, NodeId.Y] != null) //在這個位置上是否有放置棋子。 != 如果有則return null (已經有棋子，不能再放棋子了)
                return null;

            // TODO: 根據 type 產生對應的棋子

            Point formPos = convertToFormPosition(NodeId);//一個點在視窗上實際的位置
            if (type == PieceType.BLACK)
                pieces[NodeId.X, NodeId.Y] = new BlackPiece(formPos.X,formPos.Y);
            else if (type == PieceType.WHITE)
                pieces[NodeId.X, NodeId.Y] = new WhitePiece(formPos.X, formPos.Y);

            //紀錄最後下棋子的位置
            lastPlacedNode = NodeId;

            return pieces[NodeId.X, NodeId.Y];


           


        }

        private Point convertToFormPosition(Point nodeId) //目標:把 棋盤座標 轉換成 視窗座標
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeId.Y * NODE_DISTANCE + OFFSET;
            return formPosition;
        }


        private Point FindTheCloseNode(int x , int y) // (把下面一維的套到二維)
        {
            int NodeIdX = FindTheCloseNode(x); //沒有符合           //NodeIdX 可能會回傳出超過8的數字
            if (NodeIdX == -1 || NodeIdX>=NODE_COUNT)
                return NO_MATCH_NODE;

            int NodeIdY = FindTheCloseNode(y); //沒有符合
            if (NodeIdY == -1 || NodeIdY >= NODE_COUNT)
                return NO_MATCH_NODE;

            return new Point(NodeIdX, NodeIdY); 

        }

        private int FindTheCloseNode(int pos) //pos 滑鼠點的長度 (一維)
        {
            if (pos < OFFSET - NODE_RADIUS)//如果滑鼠點的長度小於 棋盤邊-R 回傳不符合任何點
                return -1;

            pos -= OFFSET; //滑鼠點到的長度-棋盤邊的長度

            int quotient = pos / NODE_DISTANCE; //商數=編號
            int remainder = pos % NODE_DISTANCE; //餘數=與左邊編號的距離(x)

            if (remainder <= NODE_RADIUS)
                return quotient;            //商數=編號
            else if (remainder >= NODE_DISTANCE - NODE_RADIUS) // x >= D-R
                return quotient + 1; //編號+1
            else
                return -1; //沒有任何點符合

            /*  如果編號小於等R依舊等於編號
             *  如果x大於等於D-R，編號加一
             *  不是的話則不符合任何編號
             */ 


        }
    }
}
