using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostAndFound
{
    public class MazeWall 
    {
    //true表示墙壁存在，false表示墙壁不存在
        public bool[,] rowWall;//存储迷宫所有行的墙壁信息
        public bool[,] colWall;//存储迷宫所有列的墙壁信息
        public int rowsum;//迷宫有多少行
        public int colsum;//迷宫有多少列

        public (int, int) start;
        public (int, int) exit;
        public (int, int) origin;

        /// <summary>
        /// 判断区域是否连通，四面墙有一面墙打通即为连通
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="colindex"></param>
        /// <returns></returns>
        public bool this[int rowindex, int colindex]
        {
            get
            {
                //检查是否越界
                if (rowindex >= rowsum || colindex >= colsum)
                    Debug.LogError("越界");
                if (rowindex < 0 || colindex < 0)
                    Debug.LogError("越界");

                //有一面墙不存在即为连通
                return !(rowWall[rowindex, colindex] && rowWall[rowindex+1, colindex ] &&
                    colWall[rowindex, colindex] && colWall[rowindex, colindex + 1]);
            }
        }
        //构造函数，初始化迷宫信息，
        public MazeWall(int a,int b)
        {
            rowsum = a;
            colsum = b;
            rowWall = new bool[rowsum+1, colsum];
            colWall = new bool[rowsum, colsum + 1];
            
            for (int i = 0; i < rowsum + 1; i++)
            {
                for (int j = 0; j < colsum; j++)
                {
                    rowWall[i, j] = true;
                }
            }
            for (int i = 0; i < rowsum; i++)
            {
                for (int j = 0; j < colsum + 1; j++)
                {
                    colWall[i, j] = true;
                }
            }
        }
        /// <summary>
        /// 消除两相邻区域之间墙壁
        /// </summary>
        /// <param name="area1"></param>
        /// <param name="area2"></param>
        public void OpenArea(MeshArea area1, MeshArea area2)
        {
            if (area1.rownum == area2.rownum)
            {
                colWall[area1.rownum, Mathf.Max(area1.colnum, area2.colnum)] = false;
                return;
            }

            if (area1.colnum == area2.colnum)
            {
                rowWall[Mathf.Max(area1.rownum, area2.rownum),area1.colnum ] = false;
            
            }
        }
        /// <summary>
        /// 随机生成出口入口，返回为(int,int)元组，为了之后判断，防止出口和入口重叠
        /// </summary>
        /// <returns></returns>
        public (int,int) StartRom()
        {
            int i=Random.Range(1, 5);//在四条边随机选择
            int j;//在某条边上随机选择某一墙壁
            switch (i)
            {
                case 1:
                    j = Random.Range(0, colsum);
                    rowWall[0, j] = false;
                    return(0,j);
                case 2:
                    j = Random.Range(0, rowsum);
                    colWall[j, colsum] = false;
                    return (j, colsum);
                case 3:
                    j = Random.Range(0, colsum);
                    rowWall[rowsum, j] = false;
                    return (rowsum, j);
                default:
                    j = Random.Range(0, rowsum);
                    colWall[j, 0] = false;
                    return (j, 0);
            }
        }
    }

}
