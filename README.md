# Game Hackathon Design Doc

## 游戏名称：生存迷宫

游戏概述 《生存迷宫》是一款紧张刺激的迷宫生存游戏。玩家将置身于随机生成的迷宫中，需要尽快找到出口，并在限定时间内逃离，同时要小心管理血条和使用道具。


### **游戏目标**

* 寻找出口：玩家的目标是在迷宫中找到通往出口的路径。
* 逃离迷宫：玩家需要尽快找到出口并成功逃离迷宫。
* 生存血条：玩家需要谨慎管理血条，避免血量耗尽。
* 使用道具：玩家可以使用道具来辅助寻找出口和管理血条。

### **游戏机制**


3.1 游戏界面

* 游戏开始界面：显示游戏的标题、简要说明和开始按钮。
* 游戏主界面：展示玩家所处的随机生成的迷宫地图，包括墙壁、通道和角色。
* 状态栏：显示玩家的当前血量、游戏时间和道具状态。

3.2 随机迷宫生成

* 迷宫生成算法：使用随机算法生成迷宫地图，确保每次游戏开始时都有不同的迷宫结构。
* 墙壁和通道：迷宫地图由墙壁和通道组成，玩家需要通过通道来寻找出口。

3.3 探索与寻找出口

* 移动角色：玩家可以通过方向键或点击屏幕来移动角色，探索迷宫中的各个区域。
* 寻找出口：玩家需要通过探索迷宫，找到通向出口的路径。出口可以在迷宫的任何位置。

3.4 血条管理

* 血条减少：玩家的血条每秒钟递减，表示生命值的减少。
* 血条恢复：捡到血瓶可以恢复

3.5 道具系统

* 补给物品：在迷宫中可以找到补给箱，玩家可以打开它们来获得补给。
    * 血瓶：血瓶可以恢复玩家的血量一定数值，帮助玩家保持生命值。
    * 陷阱箱：陷阱箱有一定几率扣除玩家的血量，需要小心处理。

* 道具箱：在迷宫中可以找到道具箱，玩家可以打开它们来获取道具。
    * 地图：地图可以显示迷宫的一部分，帮助玩家更好地导航和找到出口。
    * 指南针：指南针可以指示出口的大致方向，帮助玩家朝着正确的方向前进。
    * 传送门：传送门可以立即将玩家传送到迷宫的其他位置，有机会缩短逃离迷宫的时间。

3.6 逃离时间限制

* 时间限制：玩家需要在规定的时间内尽快找到出口逃离迷宫。
* 时间显示：游戏界面显示逃离时间的计时器，玩家可以时刻注意时间的流逝。

**游戏结束**

* 逃离成功：当玩家成功找到出口并逃离迷宫时，游戏显示胜利画面，并展示玩家的分数和通关时间。
* 血量耗尽：如果玩家的血量耗尽，游戏显示失败画面，并提示玩家重新尝试。
* 重新开始：玩家可以选择重新开始游戏，进入新的迷宫挑战。



## Game Name: Survival Maze



1. **Game Overview**

"Survival Maze" is an intense and thrilling maze survival game. Players will find themselves in a randomly generated maze, where they must quickly find the exit and escape within a limited time while managing their health bar and utilizing various items.


1. **Game Objectives**

* Find the Exit: The goal for players is to find the path leading to the exit within the maze.
* Escape the Maze: Players must locate the exit as quickly as possible and successfully escape the maze.
* Manage Health Bar: Players need to carefully manage their health bar to avoid running out of health.
* Use Items: Players can use items to aid in finding the exit and managing their health.

1. Game Mechanics

**3.1 Game Interface**

* Start Screen: Displays the game title, brief instructions, and a start button.
* Main Game Screen: Shows the randomly generated maze map with walls, pathways, and the player character.
* Status Bar: Displays the player's current health, game time, and item status.

**3.2 Random Maze Generation**

* Maze Generation Algorithm: Utilizes a random algorithm to generate the maze map, ensuring a different maze structure each time the game starts.
* Walls and Pathways: The maze map consists of walls and pathways, and players must navigate through the pathways to find the exit.

**3.3 Exploration and Finding the Exit**

* Player Movement: Players can move the character using arrow keys or by clicking/tapping on the screen, exploring different areas of the maze.
* Finding the Exit: Players need to explore the maze to discover the pathway that leads to the exit. The exit can be located anywhere within the maze.

**3.4 Health Bar Management**

* Decreasing Health: The player's health bar decreases every second, representing the depletion of their life.
* Supply Items: Health supply boxes can be found in the maze, which players can open to acquire supplies.
* Health Potion: Health potions can restore a certain amount of the player's health, helping them maintain their life.
* Trap Box: Trap boxes have a chance to deduct the player's health, requiring careful handling.

**3.5 Item System**

* Item Boxes: Item boxes can be found in the maze, and players can open them to obtain items.
* Map: Maps can reveal a portion of the maze, assisting players in navigation and finding the exit.
* Compass: Compasses can indicate the general direction of the exit, helping players proceed in the right direction.
* Teleportation Gate: Teleportation gates instantly transport the player to a different location in the maze, providing an opportunity to shorten the escape time.

**3.6 Time Limit for Escape**

* Time Limit: Players need to find the exit and escape the maze within the designated time limit.
* Time Display: The game interface includes a timer displaying the remaining time for escape.

### Game Over

* Successful Escape: When players successfully find the exit and escape the maze, the game displays a victory screen, showcasing the player's score and completion time.
* Health Depletion: If the player's health runs out, the game displays a failure screen, prompting the player to retry.
* Restart: Players can choose to restart the game and embark on a new maze challenge.


