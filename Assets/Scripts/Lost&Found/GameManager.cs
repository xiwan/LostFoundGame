using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LostAndFound
{

    public enum GameState
    {
        Begin = 0,
        Running = 1,
        Win = 2,
        Lose = 3,
        Pause = 4,
    }

    public class WallMark
    {
        public GameObject wallCache;

        public Color color;

        public int i;
        public int j;
    }

    public class GameManager : MonoBehaviour
    {

        public int row;//在编辑器中指定迷宫行数
        public int col;//在编辑器中指定迷宫列数
        public GameObject Wall;//在编辑器中指定墙体Prefeb
        public GameObject WallTorch;//在编辑器中指定墙体Prefeb
        public GameObject Floor;
        public GameObject BoxGood;
        public GameObject BoxBad;
        public GameObject BoxPortal;
        public GameObject player;
        public Collider exit;
        public Camera winCamera;
        public Camera mainCamera;
        public Camera initCamera;

        GameState gameSate = GameState.Begin;
        MazeWall mazeWall; 
        int corridorWidth = 2;
        int totalTorch = 0;
        int lightedTorch = 0;
        int winTorch = 200;
        int initOdds = 20;
        int boxOdds = 50;

        bool debugwin = false;
        List<WallMark> heartList = new List<WallMark>();

        public static int[,] heart = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,1,1,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0},
            {0,1,1,1,0,0,1,0,0,0,0,0,0,1,0,0,1,1,1,0,0,0,0},
            {1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0},
            {1,0,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,0,1,0,0,0},
            {1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,0,0,0},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0},
            {0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0},
            {0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0},
            {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };

        GameObject walls;  
        GameObject floors; 

        public void  DislayTorchStat(bool increment)
        {
            if (increment) lightedTorch++;

            string trochStat = string.Format("{0}/{1}", lightedTorch, SumHeart());
            TextHelper.Instance.DislayTorchStat(trochStat, lightedTorch*1.0f/totalTorch);
        }

        public bool IsInHeart(int i, int j)
        {
            return heart[i,j] == 1;
        }

        public int SumHeart()
        {
            int sum = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (heart[i,j] == 1)
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }
        
        void Start()
        {
            walls = GameObject.Find("Walls");
            floors = GameObject.Find("Floors");
            CreateMaze();
            //TextHelper.Instance.FlickerClue();
            //DislayTorchStat(false);
        }


        public void Pause()
        {
            gameSate = GameState.Pause;
            SwitchCamera(mainCamera, initCamera);
            player.SetActive(false);
            TextHelper.Instance.ShowPause(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (gameSate == GameState.Begin)
            {
                if(Input.anyKey)
                {
                    gameSate = GameState.Running;
                    SwitchCamera(initCamera, mainCamera);
                    CreatePlayer();
                }
            }
            else  if (gameSate == GameState.Running)
            {                
                CheckCondition();
            }
            else if (gameSate == GameState.Win)
            {
                player.SetActive(false);
                Win();
                
            } 
            else if (gameSate == GameState.Lose)
            {
                player.SetActive(false);
                Lose();
            } 
            else if (gameSate == GameState.Pause)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    SwitchCamera(initCamera, mainCamera);
                    gameSate = GameState.Running;
                    player.SetActive(true);
                    TextHelper.Instance.ShowPause(false);
                }
            }     
        }

        public void SwitchCamera(Camera from, Camera to)
        {
            to.gameObject.SetActive(true);
            from.gameObject.SetActive(false);
        }

        private void CheckCondition()
        {
            // 点亮了大部分的灯
            if (debugwin || lightedTorch*1.0f / SumHeart() > 0.8f)
            {
                TextHelper.Instance.HideInit();
                gameSate = GameState.Win;
            }
        }

        private void Win()
        {
            if (debugwin)
            {
                // foreach (Transform child in GameObject.Find("Walls").transform)
                // {
                //     int i = int.Parse(child.name.Split('-')[0]);
                //     int j = int.Parse(child.name.Split('-')[1]);

                //     string path = "Prefab/TorchBlue";
                //     if (GameManager.heart[i,j] == 1)
                //     {
                //         path = "Prefab/TorchBright";

                //         GameObject torchPrefab = Resources.Load<GameObject>(path);
                //         GameObject torch = Instantiate(torchPrefab);
                //         torch.transform.parent = child.transform;

                //         torch.transform.localPosition = new Vector3(0, 0, 0);
                //         torch.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); 
                //     }
                // }
            }
            

            TextHelper.Instance.DisplayWin("Do you find the love?");
            SwitchCamera(mainCamera, winCamera);
        }

        public void End()
        {
            gameSate = GameState.Lose;
        }

        private void Lose()
        {
            TextHelper.Instance.HideInit();
            TextHelper.Instance.DisplayWin("Find exit");
            SwitchCamera(mainCamera, winCamera);
        }

        IEnumerator WinCameraShow()
        {
            player.gameObject.transform.Find("Main Camera").gameObject.SetActive(false);        
            yield return new WaitForSeconds(1.0f);
            winCamera.gameObject.SetActive(true);    
        }

        //public void 

        private void CreateMaze()
        {
            mazeWall = new MazeWall(row,col);
            Prim.Generate(mazeWall);
            //生成行的所有墙壁
            for (int i = 0; i < mazeWall.rowWall.GetLength(0) ; i++)
            {
                for (int j = 0; j < mazeWall.rowWall.GetLength(1); j++)
                {
                    if (mazeWall.rowWall[i, j])
                    {
                        GameObject wallObj = Wall;
                        if (Random.Range(0,100) < initOdds) 
                        {
                            wallObj = WallTorch;
                            int boxOddsNum = Random.Range(0, 100);
                            if (boxOddsNum < 20)
                            {
                                wallObj = BoxGood;
                            }
                            else if (20 <= boxOddsNum && boxOddsNum < 50)
                            {
                                wallObj = BoxBad;
                            }
                            else if (50 <= boxOddsNum && boxOddsNum < 60)
                            {
                                wallObj = BoxPortal;
                            }
                        }
                        totalTorch++;
                        GameObject wall = Instantiate(wallObj, (new Vector3(j + 0.5f, 0.5f, i)) * corridorWidth, Quaternion.Euler(0, 90, 0));
                        wall.transform.parent = walls.transform;
                        wall.name = string.Format("{0}-{1}", i, j);
                    }
                }
            }
            //生成列的所有墙壁
            for (int i = 0; i < mazeWall.colWall.GetLength(0); i++)
            {
                for (int j = 0; j < mazeWall.colWall.GetLength(1) ; j++)
                {
                    if (mazeWall.colWall[i, j])
                    {
                        GameObject wallObj = Wall;
                        if (Random.Range(0,100) < initOdds)  
                        {
                            wallObj = WallTorch;
                            int boxOddsNum = Random.Range(0, 100);
                            if (boxOddsNum < 20)
                            {
                                wallObj = BoxGood;
                            }
                            else if (20 <= boxOddsNum && boxOddsNum < 50)
                            {
                                wallObj = BoxBad;
                            }
                            else if (50 <= boxOddsNum && boxOddsNum < 60)
                            {
                                wallObj = BoxPortal;
                            }
                        }
                        totalTorch++;
                        GameObject wall = Instantiate(wallObj, (new Vector3(j, 0.5f, i + 0.5f)) * corridorWidth, Quaternion.Euler(0, 0, 0));
                        wall.transform.parent = walls.transform;
                        wall.name = string.Format("{0}-{1}", i, j);                    }
                }
            }

            CreateExit();
            CreateFloor();
            //CreateHeart();

            // GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            // //GameObject floor = Instantiate(pfb);
            // floor.transform.localPosition = (new Vector3(10, 0, 10)) * corridorWidth;
            // floor.transform.localScale = (new Vector3(2, 1, 2)) * corridorWidth;
            // //var floor = Instantiate(Wall, new Vector3(j + 0.5f, 0.5f, i), Quaternion.Euler(0, 90, 0));
            // floor.transform.parent = GameObject.Find("Maze").transform;

        }

        private void CreateExit()
        {
            //生成出入口
            if (exit != null)
            {
                Vector3 exit1Pos = (new Vector3(mazeWall.start.Item2 + 0.5f, 0.1f, mazeWall.start.Item1 + 0.5f)) * corridorWidth;
                Vector3 exit2Pos = (new Vector3(mazeWall.exit.Item2 + 0.5f, 0.1f, mazeWall.exit.Item1 + 0.5f)) * corridorWidth;
                Collider exit1 = Instantiate(exit, exit1Pos, Quaternion.Euler(0, 0, 0));
                Collider exit2 = Instantiate(exit, exit2Pos, Quaternion.Euler(0, 0, 0));
                exit1.transform.parent = GameObject.Find("Exit").transform;
                exit2.transform.parent = GameObject.Find("Exit").transform;
            }
        }

        private void CreateFloor()
        {
            for (int i = -1; i <= mazeWall.rowsum ; i++)
            {
                for (int j = -1; j <= mazeWall.colsum; j++)
                {
                    GameObject floor = Instantiate(Floor, (new Vector3(j + 0.5f, 0, i)) * corridorWidth , Quaternion.Euler(0, 0, 0));
                    floor.transform.localScale *= corridorWidth; 
                    floor.transform.parent = floors.transform;
                }
            }
        }

        private void CreatePlayer()
        {
            
            if (player != null)
            {
                //GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                player.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                player.transform.localPosition = new Vector3(mazeWall.origin.Item2 + 0.5f, 0.25f, mazeWall.origin.Item1 + 0.5f);
                
            }
        }

        private void PortalPlayer()
        {

            if (player != null)
            {
                //GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                player.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                var randPortal = mazeWall.RandomPortal();

                player.transform.localPosition = new Vector3(randPortal.Item2 + 0.5f, 0.25f, randPortal.Item1 + 0.5f);

            }
        }
    }

}
