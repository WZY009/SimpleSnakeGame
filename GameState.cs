using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SimpleSnakeGame
{
    class GameState
    {
        const int CELLSIZE = 20;
        const int SNAKEHEAD = 0;
        const int CELLWIDTH = 640 / CELLSIZE; // how many cells on the direction of width   
        const int CELLHEIGHT = 480 / CELLSIZE; // how many cells on the direction of height
        // the moving direction of the snake
        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }
        public Direction direction = Direction.UP;
        // the state of the game
        public enum State
        {
            NONE,
            GAMEING,
            PAUSE,
            STOP
        }
        public State state = State.NONE;
        public enum Levels
        {
            HARD,
            MIDDLE,
            SIMPLE
        }
        public Levels levels = Levels.SIMPLE;

        List<SnakeNode> snakelist1;// definite the list of the snake's body
        List<Blockings> blocklist;//definite the list of blocks
        public Fruit myFruit;                                            // initialize the position of the snake
        private System.Windows.Controls.Canvas _canvas;
        Random rnd = new Random();        // produce random number

        public GameState(System.Windows.Controls.Canvas canvas)
        {
            _canvas = canvas;
        }
        public Point SetFruitPosition()//Pay attention! the position of the fruit can not overlap with any section of the snake and blocks
        {
            bool flag = true;
            Point pos = new Point();
            while (flag)
            {
                flag = false;
                pos = new Point(rnd.Next(0, CELLWIDTH), rnd.Next(0, CELLHEIGHT));
                if (snakelist1 != null && blocklist!=null)
                {
                    foreach (var node in snakelist1)
                    {
                        if (pos.X == node._pos.X && pos.Y == node._pos.Y)
                        {
                            flag = true;
                            break;
                        }
                    }
                    foreach (var block in blocklist)
                    {
                        if (pos.X == block._pos.X && pos.Y == block._pos.Y)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                else if (snakelist1 == null)
                {
                    foreach (var block in blocklist)
                    {
                        if (pos.X == block._pos.X && pos.Y == block._pos.Y)
                        {
                            flag = true;
                            break;
                        }
                    }
                }    
                else if (blocklist == null){
                    foreach (var node in snakelist1)
                    {
                        if (pos.X == node._pos.X && pos.Y == node._pos.Y)
                        {
                            flag = true;
                            break;
                        }
                    }
                }      
            }
            return pos;
        }
        public void GenNewSnakeNode()
        {
            //generate the whole snake
            //After eating fruit, the snake will enlarge for one node and its head will be on the position same as the fruit.
            //This function is to record the position of the snake's new head according to the snake's moving direction.
            SnakeNode snakeNode = null;
            switch (direction)
            {
                case Direction.UP:
                    snakeNode = new SnakeNode(new Point(snakelist1[SNAKEHEAD]._pos.X, snakelist1[SNAKEHEAD]._pos.Y - 1));
                    break;
                case Direction.DOWN:
                    snakeNode = new SnakeNode(new Point(snakelist1[SNAKEHEAD]._pos.X, snakelist1[SNAKEHEAD]._pos.Y + 1));
                    break;
                case Direction.LEFT:
                    snakeNode = new SnakeNode(new Point(snakelist1[SNAKEHEAD]._pos.X - 1, snakelist1[SNAKEHEAD]._pos.Y));
                    break;
                case Direction.RIGHT:
                    snakeNode = new SnakeNode(new Point(snakelist1[SNAKEHEAD]._pos.X + 1, snakelist1[SNAKEHEAD]._pos.Y));
                    break;
            }
            if (snakeNode != null)
            {
                snakelist1.Insert(0, snakeNode);
                _canvas.Children.Add(snakelist1[0]._rect);
            }
        }
        public void IsEat()
        {
            //Only the snake's head will "eat" the fruit, you only need judge whether the head's position is same as the fruit's.
            // When the snake eats the fruit, the fruit will be on the other position, if not, we can delete the end of the snake so form the perspective of users, they feel the snake move.
            if (snakelist1[SNAKEHEAD]._pos.X == myFruit._pos.X && snakelist1[SNAKEHEAD]._pos.Y == myFruit._pos.Y)//the snake eats the fruit.         
            {
                if (levels == Levels.HARD)
                    produceBlock(100);
                myFruit.SetPostion(SetFruitPosition());                
            }
            else
            {
                if (_canvas.Children.Contains(snakelist1[snakelist1.Count - 1]._rect))
                    _canvas.Children.Remove(snakelist1[snakelist1.Count - 1]._rect);//Removes the first occurrence of a specific object from the List<T>
                snakelist1.RemoveAt(snakelist1.Count - 1);// Removes the element at the specified index of the List<T>.
            }



        }
        public void RemoveAllSnake()
        {
            if (snakelist1 == null)//It means if there is no snake we don't have to delete,if we do not have this code, when we want to restart the last osnake will be left.            
                return;

            for (int i = 0; i < snakelist1.Count; i++)
            {
                if (_canvas.Children.Contains(snakelist1[i]._rect))
                    _canvas.Children.Remove(snakelist1[i]._rect);
            }
        }
        public void RemoveFruit()
        {
            if (myFruit == null)
                return;
            if (_canvas.Children.Contains(myFruit._ellipse))
                _canvas.Children.Remove(myFruit._ellipse);
        }
        public void RemoveAllBlocks()
        {
            if (blocklist == null)
                return;
            for(int i=0;i<blocklist.Count;i++)
            {
                if (_canvas.Children.Contains(blocklist[i]._rect))
                    _canvas.Children.Remove(blocklist[i]._rect);
            }
            blocklist.Clear();
        }
        public void StartGame()
        {           
            RemoveAllSnake();
            RemoveAllBlocks();
            RemoveFruit();
            snakelist1 = new List<SnakeNode>();

            int startX = rnd.Next(5, CELLWIDTH - 6);
            int startY = rnd.Next(5, CELLWIDTH - 6);
            direction = Direction.RIGHT;
            snakelist1.Add(new SnakeNode(new Point(startX, startY)));
            GenNewSnakeNode();
            switch (levels)
            {
                case Levels.SIMPLE:
                    break;
                case Levels.MIDDLE:
                    produceBlock(50);
                    break;
                case Levels.HARD:
                    produceBlock(100);
                    break;
            }
            myFruit = new Fruit(this.SetFruitPosition(), _canvas);
        }
        public bool IsGameOver()
        {
            if (snakelist1[SNAKEHEAD]._pos.X == -1 || snakelist1[SNAKEHEAD]._pos.X == CELLWIDTH
                || snakelist1[SNAKEHEAD]._pos.Y == -1 || snakelist1[SNAKEHEAD]._pos.Y == CELLHEIGHT)// the snake collide with the window's edge.
            {
                return true;
            }

            foreach (var node in snakelist1)
            {
                if (node == snakelist1[SNAKEHEAD])
                    continue;//pass the snake head
                if (node._pos.X == snakelist1[SNAKEHEAD]._pos.X && node._pos.Y == snakelist1[SNAKEHEAD]._pos.Y)//make sure the snake's head will not conflict with its body                
                    return true;
                
            }
            if(blocklist != null)
            {
                foreach(var block in blocklist)//to test if the snake collides with the blocks
                {
                    if (block._pos.X == snakelist1[SNAKEHEAD]._pos.X && block._pos.Y == snakelist1[SNAKEHEAD]._pos.Y)//make sure the snake's head will not conflict with its body               
                        return true;             
                }
            }
            return false;
        }
        private void produceBlock(int num)
        {
            RemoveAllBlocks();
            blocklist = new List<Blockings>();
            for (int i = 0; i < num; i++)
            {
                bool flag = true;
                int blockX = 0, blockY = 0;
                while (flag)//to make sure the blocks will never be overlapped with the snake.
                {
                    flag = false;
                    blockX = rnd.Next(0, CELLWIDTH);
                    blockY = rnd.Next(0, CELLHEIGHT);
                    foreach (var node in snakelist1)
                    {
                        if (blockX==node._pos.X&&blockY==node._pos.Y)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                blocklist.Add(new Blockings(new Point(blockX, blockY), _canvas));
            }
        }

    }
}
