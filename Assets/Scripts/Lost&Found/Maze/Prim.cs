using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostAndFound
{

/// <summary>
/// 算法实现
/// </summary>
    public class Prim : CreateMazeAlgoritnm
    {
        //维护的墙列表
        private static List<KeyValuePair<MeshArea, MeshArea>> walls = new List<KeyValuePair<MeshArea, MeshArea>>();

        public static MazeWall Generate(MazeWall wall)
        {
            mazeWall = wall;
            walls.Clear();

            //封闭全部墙壁

            //随机选择一个开始区域
            MeshArea fistArea = RandChooseBeginArea();
            AddNerabyWall(fistArea);

            while (walls.Count > 0)
            {
                int randomIndex = Random.Range(0, walls.Count);
                var _wall = walls[randomIndex];
                if (checkWall(_wall))
                {
                    mazeWall.OpenArea(_wall.Key, _wall.Value);
                    if (checkArea(_wall.Key))
                    {
                        AddNerabyWall(_wall.Key);
                    }
                    if (checkArea(_wall.Value))
                    {
                        AddNerabyWall(_wall.Value);
                    }

                }
                walls.Remove(_wall);
            }

            //随机选择迷宫起点终点
            mazeWall.start = mazeWall.StartRom();
            mazeWall.exit = mazeWall.StartRom();
            //避免出口和入口重叠
            while (mazeWall.start == mazeWall.exit) { }
            return mazeWall;
        }

        /// <summary>
        /// 把区域附近未打通的墙加入维护的墙列表
        /// </summary>
        /// <param name="area"></param>
        private static void AddNerabyWall(MeshArea area)
        {
            List<MeshArea> areas = GetNearbyArea(area);
            for (int i = 0; i < areas.Count; ++i)
                walls.Add
                (
                    new KeyValuePair<MeshArea, MeshArea>
                    (
                        area, areas[i]
                    )
                );
        }
    }

}
