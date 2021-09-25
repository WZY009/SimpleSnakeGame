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
        enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }
        Direction direction = Direction.UP;
        // the state of the game
        enum State
        {
            NONE,
            GAMEING,
            PAUSE,
            STOP
        }
        State state = State.NONE;

        List<SnakeNode> snakelist1 = new List<SnakeNode>();        // the list of the snake's body
        Fruit myFruit;                                            // initialize the position of the snake
        System.Windows.Controls.Canvas myCanvas;
        Random rnd = new Random();        // produce random number
        GameState(Fruit fruit,System.Windows.Controls.Canvas canvas)
        {
            myFruit = fruit;
            myCanvas = canvas;
        }

        public Point SetFruitPosition()//Pay attention! the position of the fruit can not overlap with any section of the snake
        {
            bool flag = true;
            Point pos = new Point();
            while (flag)
            {
                flag = false;
                pos = new Point(rnd.Next(0, CELLWIDTH), rnd.Next(0, CELLHEIGHT));
                foreach (var node in snakelist1)
                {
                    if (pos.X == node._pos.X && pos.Y == node._pos.Y)
                    {
                        flag = true;
                        break;
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
        }

        public void IsEat()
        {
            //Only the snake's head will "eat" the fruit, you only need judge whether the head's position is same as the fruit's.
            // When the snake eats the fruit, the fruit will be on the other position, if not, we can delete the end of the snake so form the perspective of users, they feel the snake move.
            if (snakelist1[SNAKEHEAD]._pos.X == myFruit._pos.X && snakelist1[SNAKEHEAD]._pos.Y == myFruit._pos.Y)//the snake eats the fruit.         
                myFruit.SetPostion(SetFruitPosition());
            else
            {
                if(myCanvas.Children.Contains(snakelist1[snakelist1.Count-1]._rect))
                    myCanvas.Children.Remove(snakelist1[snakelist1.Count - 1]._rect));//Removes the first occurrence of a specific object from the List<T>
                snakelist1.RemoveAt(snakelist1.Count - 1);// Removes the element at the specified index of the List<T>.
            }



        }

    }
}
