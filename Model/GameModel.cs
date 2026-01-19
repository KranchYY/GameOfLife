using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model
{
    public class GameModel
    {
        private int size;
        private GameField[,] gameFields = new GameField[15,15];
        public event EventHandler<EventArgs>? GameAdvanced;
        private ITimer timer;
        public GameModel() {
            size = 15;
            timer=new GameTimerInheritance();
            gameFields = new GameField[size, size];
            for (int i = 0; i < size; i++) { 
                for(int j = 0; j<size;  j++) { gameFields[i,j] = GameField.DEAD; }
            }
            timer.Interval = 1000;
            timer.Elapsed += new EventHandler(Timer_Elapsed);
        }

        private void Timer_Elapsed(object? sender, EventArgs e)
        {
            Progress();
        }

        public int GetNeighbors(int x, int y, GameField gamefield) {
            int n = 0;
            if (x != 0) { if (gameFields[y,x - 1] == gamefield) { n++;} }
            if (x != size - 1) { if (gameFields[y,x + 1] == gamefield) n++; }
            if(y != size - 1) { if (gameFields[y+1,x]==gamefield) n++; }
            if (y != 0) { if (gameFields[y - 1,x] == gamefield) n++; }
            return n;
        }
        public GameField[,] killCells() {
            GameField[,] newState = new GameField[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newState[i,j] = gameFields[i,j];
                }
            }
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    if (gameFields[j, i] == GameField.ALIVE)
                    {
                        int n = GetNeighbors(i, j, GameField.ALIVE);
                        if (n < 2 || n > 3)
                        {
                            newState[j, i] = GameField.DEAD;
                        }
                    }
                    else {
                        int n = GetNeighbors(i, j, GameField.ALIVE);
                        if (n == 3)
                        {
                            newState[j, i] = GameField.ALIVE;
                        }
                    }

                }
            }
            return newState;
        }
        public void SpawnCells() {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (gameFields[j, i] == GameField.DEAD)
                    {
                        int n = GetNeighbors(i, j, GameField.ALIVE);
                        if (n==3)
                        {
                            gameFields[j, i] = GameField.ALIVE;
                        }
                    }
                }
            }
        }
        public void Progress() { 
            gameFields=killCells();
            //SpawnCells();
            GameAdvanced?.Invoke(this,new EventArgs());
        }
        public void ModifySpeed(int interval) { 
            timer.Interval = interval;
        }
        public void AutoSimulation() {
            timer.Start();
        }
        public void SimPause() {
            timer.Stop();
        }
        public void Step(int x, int y) {
            if (gameFields[y, x] == GameField.DEAD) { gameFields[y, x] = GameField.ALIVE; }
            else {
                gameFields[y,x] = GameField.DEAD;
            }
            GameAdvanced?.Invoke(this, new EventArgs());
        }
        public GameField[,] GameFields { get => gameFields; set => gameFields = value; }
    }
}
