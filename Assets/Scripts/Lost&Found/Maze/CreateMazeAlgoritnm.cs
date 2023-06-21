using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LostAndFound
{

    public class CreateMazeAlgoritnm
    {       
            protected static MazeWall mazeWall;
            /// <summary>
            /// 随机选择一个开始区域
            /// </summary>
            protected static MeshArea RandChooseBeginArea()
            {
                int RandomRowIndex = Random.Range(0, mazeWall.rowsum);
                int RandomColIndex = Random.Range(0, mazeWall.colsum);

                mazeWall.origin = (RandomRowIndex, RandomColIndex);
                return new MeshArea(RandomRowIndex, RandomColIndex);
            }
            /// <summary>
            /// 获得未访问的邻接相通区域
            /// </summary>
            protected static List<MeshArea> GetNearbyArea(MeshArea area)
            {
                List<MeshArea> nerabyAreas = new List<MeshArea>();

                if (area.rownum > 0)
                    if (!mazeWall[area.rownum - 1, area.colnum])
                        nerabyAreas.Add(new MeshArea(area.rownum - 1, area.colnum));
                if (area.rownum < mazeWall.rowsum - 1)
                    if (!mazeWall[area.rownum + 1, area.colnum])
                        nerabyAreas.Add(new MeshArea(area.rownum + 1, area.colnum));
                if (area.colnum > 0)
                    if (!mazeWall[area.rownum, area.colnum - 1])
                        nerabyAreas.Add(new MeshArea(area.rownum, area.colnum - 1));
                if (area.colnum < mazeWall.colsum - 1)
                    if (!mazeWall[area.rownum, area.colnum + 1])
                        nerabyAreas.Add(new MeshArea(area.rownum, area.colnum + 1));

                return nerabyAreas;
            }

            /// <summary>
            /// 判断区域是否被打通
            /// </summary>
            /// <param name="area"></param>
            /// <returns></returns>
            protected static bool checkArea(MeshArea area)
            {
                return mazeWall[area.rownum, area.colnum];
            }

            /// <summary>
            /// 检测墙是否需要打通
            /// </summary>
            /// <param name="wall"></param>
            /// <returns></returns>
            protected static bool checkWall(KeyValuePair<MeshArea, MeshArea> wall)
            {
                bool Conduction1 = checkArea(wall.Key);
                bool Conduction2 = checkArea(wall.Value);

                return !Conduction1 || !Conduction2;
            }

        
    }

}
