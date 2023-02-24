using System.Windows;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        #region Private Members 
        
        // Holds current results of cells in game.
        private MarkType[] mResults;

        //True if it's player 1's turn (X) or player 2's (O).
        private bool mPlayer1Turn;

        //True is game is over.
        private bool mGameEnded;

        #endregion

        // Starts a new game and clears values back to start.
        private void NewGame()
        {
            // Creates new array for free cells.
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Player 1 has to start the game.
            mPlayer1Turn = true;

            // Iterate every button for grid.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // This will change the background and foreground colors to their default values.
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game has not ended.
            mGameEnded = false;
        }

        // Handles button click event.
    
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game by clicking any button after finish.
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button.
            var button = (Button)sender;

            // Find button position in array.
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            // Lays out index by adding column to row and accounting for the row's spaces.
            var index = column + (row * 3);

            // No action needed if cell already has a value in it.
            if (mResults[index] != MarkType.Free)
                return;

            // Set cell value based on which player's turn it is.
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to a result.
            button.Content = mPlayer1Turn ? "X" : "O";

            // Changes noughts to green.
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggles player's turns.
            if (mPlayer1Turn)
                mPlayer1Turn = false;
            else
                mPlayer1Turn = true;

            // Check winner.
            CheckForWinner();
        }

        // Checks for a win via 3-line straight.
        private void CheckForWinner()
        {
            // -- Check horizontal wins
            // Row 0.

                                                // This function is going to check if every result is equal to the same value (e.g. all X's or O's accross the board).
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                // Highlight winning cells in green.
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            // Row 1.
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            // Row 2.
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            // -- Check vertical wins.
            // Column 0.
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            // Column 1.
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            // Column 2.
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            // -- Check diagonal wins.
            // Top left to bottom right.
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            // Top right to bottom left.
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            // -- Check for full board with no winner.
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                // Turn cells orange to indicate that both players lost.
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                   
                });
            }
        }
    }
}
