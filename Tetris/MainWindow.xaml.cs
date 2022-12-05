using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    public partial class MainWindow : Window
    {
        private ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };
        private ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };
        private MediaPlayer mediaPlayer = new MediaPlayer();

        private GameState gameState = new GameState();
        private Image[,] imageControls;
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }
        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    Image image = new Image { Width = cellSize, Height = cellSize };
                    Canvas.SetTop(image, (i - 2) * cellSize);
                    Canvas.SetLeft(image, j * cellSize);
                    GameCanvas.Children.Add(image);
                    imageControls[i, j] = image;
                }
            }
            return imageControls;
        }
        private void DrawGrid(GameGrid grid)
        {
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    int id = grid.Cell(i, j);
                    imageControls[i, j].Source = tileImages[id];
                }
            }
        }
        private void DrawBlock(Block block)
        {
            Element[] elements = block.GetBlock();

            foreach (Element element in elements)
            {
                imageControls[element.Row, element.Column].Source = tileImages[block.Id];
            }
        }
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
            HighScoreText.Text = $"Highscore:\n{gameState.Highscore}";
            ScoreText.Text = $"Score:\n {gameState.Score}";
            LevelText.Text = $"Level:\n{gameState.BlockCounter / 100}";
            RowsClearedText.Text = $"Rows\ncleared:\n{gameState.RowsCleared}";
            BlocksText.Text = $"Blocks\ndropped:\n{gameState.BlockCounter}";
            NextImage.Source = blockImages[gameState.NextBlock.Id];
        }
        private async Task GameLoop()
        {
            Draw(gameState);

            mediaPlayer.Open(new Uri("C:/Users/Vitaly/source/repos/Tetris/Tetris/Assets/Korobeiniki.mp3"));
            mediaPlayer.Play();
            mediaPlayer.MediaEnded += new EventHandler(Media_Ended);

            while (!gameState.IsGameOver)
            {

                await Task.Delay(gameState.Speed);

                if (gameState.IsHardDrop)
                {
                    while (gameState.IsHardDrop)
                    {
                        gameState.IsHardDrop = gameState.MoveDown();
                        await Task.Delay(10);
                        Draw(gameState);
                    }
                }
                else
                {
                    gameState.MoveDown();
                    Draw(gameState);
                }
            }
            mediaPlayer.Stop();
            gameState.WriteScoresToFile();
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScore.Text = $"Score: {gameState.Score}\nHighscore: {gameState.Highscore}";
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.IsGameOver) return;

            if (e.Key == Key.Left) gameState.MoveLeft();
            if (e.Key == Key.Right) gameState.MoveRight();
            if (e.Key == Key.Down)
            {
                gameState.PressedDown = true;
                gameState.MoveDown();
            }
            if (e.Key == Key.Up) gameState.Rotate();
            if (e.Key == Key.Space) gameState.IsHardDrop = true;

            Draw(gameState);
        }
        private async void GameCanvasLoaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }
        private async void PlayAgainClick(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }
    }
}
