using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameOfLife.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private GameModel model;
        private int size;
        private bool started;
        public ObservableCollection<ViewGameField> Fields { get; set; }
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand SimulationStartCommand { get; set; }
        public DelegateCommand PauseCommand { get; set; }
        public DelegateCommand SimSpeed { get; set; }
        public DelegateCommand SimSpeedf { get; set; }
        public DelegateCommand Restart { get; set; }
        public int Size { get => size; set => size = value; }

        public GameViewModel() { 
            model=new GameModel();
            size = 15;
            Fields = new ObservableCollection<ViewGameField>();
            StartCommand = new DelegateCommand(param => OnStart());
            SimulationStartCommand = new DelegateCommand(param => OnSimStart());
            PauseCommand = new DelegateCommand(param => OnSimPause());
            SimSpeed = new DelegateCommand(param => SimSpeed1());
            SimSpeedf = new DelegateCommand(param => SimSpeed2());
            Restart = new DelegateCommand(param => Restartr());
            started = false;
            model.GameAdvanced += new EventHandler<EventArgs>(OnGameAdvanced);
            GenerateTable(size,size);
            RefreshTable();
        }

        private void Restartr()
        {   
            model = new GameModel();
            model.GameAdvanced += new EventHandler<EventArgs>(OnGameAdvanced);
            GenerateTable(size,size);
            RefreshTable();
            started = false;
        }

        private void SimSpeed2()
        {
            model.ModifySpeed(500);
        }

        private void SimSpeed1()
        {
            model.ModifySpeed(2000);
        }

        private void OnSimPause()
        {
            model.SimPause();
        }

        private void OnSimStart()
        {
            started = true;
            model.AutoSimulation();
        }

        private void OnGameAdvanced(object? sender, EventArgs e)
        {
            RefreshTable();
        }

        private void OnStart()
        {
            started = true;
            model.Progress();
        }

        private void GenerateTable(int height, int width)
        {
            Fields.Clear();
            //MessageBox.Show("Ref");
            for (int i = 0; i < height; i++) // inicializáljuk a mezőket
            {
                for (int j = 0; j < width; j++)
                {
                    Fields.Add(new ViewGameField
                    {
                        Background = Brushes.Red,
                        GameField = GameField.DEAD,
                        X = j,
                        Y = i,
                        StepCommand = new DelegateCommand(param =>
                        {
                            if (param is Tuple<Int32, Int32> position)
                                StepGame(position.Item1, position.Item2);
                        })
                    });
                }
            }
        }

        private void StepGame(int x, int y) 
        {
            if (started) return;
            model.Step(x, y);

        }

        public void RefreshTable()
        {
            foreach (var field in Fields)
            {
                switch (model.GameFields[field.Y, field.X])
                {
                    case GameField.DEAD:
                        field.Background = Brushes.Red;
                        field.GameField = GameField.DEAD;
                        break;
                    case GameField.ALIVE:
                        field.Background = Brushes.Green;
                        field.GameField = GameField.ALIVE;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
