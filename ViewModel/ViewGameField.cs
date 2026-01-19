using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GameOfLife.Model;

namespace GameOfLife.ViewModel
{
    public class ViewGameField : ViewModelBase
    {
        private Brush background = Brushes.White;
        private GameField gameField = GameField.DEAD;
        private int x;
        private int y;
        public Brush Background
        {
            get => background;
            set
            {
                if (background != value)
                {
                    background = value;
                    OnPropertyChanged(nameof(Background));
                }
            }
        }
        public GameField GameField { get => gameField; set { if (gameField != value) { gameField = value; OnPropertyChanged(); } } }
        public Tuple<Int32, Int32> XY
        {
            get { return new(X, Y); }
        }
        public DelegateCommand? StepCommand { get; set; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
