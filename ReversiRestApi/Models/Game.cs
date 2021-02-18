using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ReversiRestApi.Enums;
using ReversiRestApi.Interfaces;

namespace ReversiRestApi.Models
{
    public class Game : IGame
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public string Player1Token { get; set; }
        public string Player2Token { get; set; }
        public Color Player1Color { get; set; }
        public Color Player2Color { get; set;}
        public Color[,] Board { get; set; }
        public Color Moving { get; set; }
        public int Size { get; set; } = 8;
        public int MoveCount { get; set; }
        public GameStatus Status { get; set; }
        public string Winner { get; set; }


        /**
         * Initializes the game
         */
        public Game()
        {
            Board = new Color[Size, Size];

            RunForAllCells((row, col) =>
            {
                Board[row, col] = Color.None;
                return false;
            });

            Board[Size / 2 - 1, Size / 2 - 1] = Color.White;
            Board[Size / 2 - 1, Size / 2] = Color.Black;
            Board[Size / 2, Size / 2 - 1] = Color.Black;
            Board[Size / 2, Size / 2] = Color.White;

        }

        /**
         * Checks if pass is viable. if so skips turn
         */
        public bool Pass()
        {
            var possible = false;

            foreach (var emptyCell in GetEmptyCells())
            {
                if (IsPossible(emptyCell.Row, emptyCell.Col))
                    possible = true;
            }

            if (!possible)
            {
                SwapMoving();
                return true;
            }

            return false;
        }
        
        /**
         * Checks if the game has finished
         */
        public bool Finished()
        {
            Color notMoving = Color.None;
            if (Moving == Color.Black)
                notMoving = Color.White;
            else
                notMoving = Color.Black;
        
            if (MoveCount >= 60)
                return true;

            return !PlayerHasPossibleMoves(Moving) && !PlayerHasPossibleMoves(notMoving);
        }

        /**
         * Gets all the empty cells on the board
         *
         * @return All the cells that are empty
         */
        public Cell[] GetEmptyCells()
        {
            var cells = new List<Cell>();

            RunForAllCells((row, col) =>
            {
                var color = Board[row, col];
                if (color == Color.None)
                    cells.Add(new Cell(row, col, color));
                return false;
            });
            return cells.ToArray();
        }
        /**
         * Checks if the player has possible moves
         *
         * @param Color player The player to check for
         * 
         * @return The player can move
         */
        public bool PlayerHasPossibleMoves(Color player)
        {
            var possibleMoves = false;
            RunForAllCells((row, col) =>
            {
                if (IsPossible(row, col))
                    possibleMoves = true;

                return false;
            });

            return possibleMoves;
        }

        /**
         * Checks which color is winning
         *
         * @return The winning color
         */
        public Color WinningColor()
        {
            int blackScore = 0;
            int whiteScore = 0;

            RunForAllCells((row, col) =>
            {
                if (Board[row, col] == Color.White)
                    whiteScore++;
                if (Board[row, col] == Color.Black)
                    blackScore++;
                return false;
            });

            if (blackScore == whiteScore)
                return Color.None;
            if (whiteScore > blackScore)
                return Color.White;
            return Color.Black;

        }

        /**
         * Checks if a move is possible
         *
         * @param int rowMove the row
         * @param int colMove the column
         *
         * @return The move is possible
         */
        public bool IsPossible(int rowMove, int colMove)
        {
            if (!OnBoard(rowMove, colMove))
                return false;

            if (Board[rowMove, colMove] != Color.None)
                return false;

            return GetPossibleDirections(rowMove, colMove).Length >= 1;
        }

        /**
         * Makes a move
         *
         * @param int rowMove the row
         * @param int colMove the column
         *
         * @return Makes the move
         */
        public bool Move(int rowMove, int colMove)
        {
            if (!IsPossible(rowMove, colMove))
                return false;

            SetCell(rowMove, colMove, Moving);
            ChangeColorOnPieces(rowMove, colMove, GetPossibleDirections(rowMove, colMove));

            MoveCount++;
            SwapMoving();
            return true;
        }

        /**
         * Prints the board in its current state
         */
        public void Print()
        {
            RunForAllCells((row, col) =>
            {
                Console.Write($"{Board[row, col],-5} ");
                if(col == Size -1)
                    Console.WriteLine();
                return false;
            });
        }

        /**
         * Sets the color of a cell
         *
         * @param int rowMove The row
         * @param int colMove The column
         * @param Color color The color the cell has to change to
         */
        public void SetCell(int row, int col, Color color)
        {
            if (OnBoard(row, col) && GetCell(row, col) == Color.None)
                Board[row, col] = color;
        }

        /**
         * Swaps the moving color
         */
        public void SwapMoving()
        {
            if (Moving == Color.White)
                Moving = Color.Black;
            else
                Moving = Color.White;
        }

        /**
         * Goes over all the cells on the board
         *
         * @param Func<int, int, bool> function Needs to be done to every cell
         */
        public bool RunForAllCells(Func<int, int, bool> function)
        {
            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    function(row, col);
                }
            }

            return false;
        }

        /**
         * Changes the color of the pieces where needed
         *
         * @param int rowMove The row
         * @param int colMove The column
         * @param Vector[] directions The array with all the directions 
         */
        public void ChangeColorOnPieces(int row, int col, Vector2[] directions)
        {
            foreach (var direction in directions)
            {
                var startRow = row;
                var startCol = col;

                while (OnBoard(startRow, startCol))
                {
                    startRow += (int)direction.X;
                    startCol += (int)direction.Y;

                    var cell = GetCell(startRow, startCol);

                    if (cell == Moving && (startRow != row || startCol != col))
                        break;

                    Board[startRow, startCol] = Moving;
                }
            }
        }

        /**
         * Checks if the coords are on the board
         *
         * @param int rowMove The row
         * @param int colMove The column
         *
         * @return The cell is on the board
         */
        public bool OnBoard(int row, int col)
        {
            return row < Size && col < Size && row >= 0 && col >= 0;
        }

        /**
         * Gets all the possible directions
         *
         * @param int rowMove The row
         * @param int colMove The column
         *
         * @return Array of possible directions
         */
        public Vector2[] GetPossibleDirections(int rowMove, int colMove)
        {
            var vectors = new List<Vector2>();

            foreach (var direction in new[]
            {
                new Vector2(0, -1), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1),
                new Vector2(1, -1), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(-1, -1)
            })
            {
                var startRow = rowMove;
                var startCol = colMove;
                var foundOpp = false;
                var valid = false;

                while (OnBoard(startRow, startCol))
                {
                    startRow += (int)direction.X;
                    startCol += (int)direction.Y;

                    var cell = GetCell(startRow, startCol);
                    var opp = Moving == Color.White ? Color.Black : Color.White;

                    if (cell == Color.None && (startRow != rowMove || startCol != colMove))
                        break;

                    if (cell == opp)
                        foundOpp = true;

                    if (foundOpp && cell == Moving)
                    {
                        valid = true;
                        break;
                    }
                }

                if (valid)
                    vectors.Add(direction);
            }

            return vectors.ToArray();
        }

        /**
         * Gets a specific cell
         *
         * @param int rowMove The row
         * @param int colMove The column
         *
         * @return Color of the cell
         */
        public Color GetCell(int row, int col)
        {
            if(OnBoard(row, col))
                return Board[row, col];
            return Color.None;
        }

        /**
         * Gets the PlayerColor
         *
         * @param string token The playerToken
         *
         * @return The player color
         */
        public Color GetPlayerColor(string token)
        {
            if(token == Player1Token)
                return Player1Color;
            if (token == Player2Token)
                return Player2Color;
            return Color.None;
        }

        /**
         * Gets the color of the player that is not moving
         *
         * @param Color color The Color of the moving player
         *
         * @return Color of the player not moving
         */
        public Color GetOppositeColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        /**
         * Gives a color to all the players
         */
        public void AssignColorToPlayers()
        {
            if (Player1Token == null || Player2Token == null)
                return;

            var randomColor = new Random().Next(0, 1);

            Player1Color = randomColor == 1 ? Color.White : Color.Black;
            Player2Color = GetOppositeColor(Player1Color);

        }

        /**
         * Starts the Game
         *
         * @param string token The token of the game
         *
         * @return Game has started
         */
        public bool StartGame(string token)
        {
            if (Player2Token == null)
                return false;

            AssignColorToPlayers();
            Moving = Player1Color;

            Status = GameStatus.Running;
            return true;
        }

        /**
         * Finishes the game
         *
         * @param string winner The token of the winning player
         */
        public void FinishGame(string winner)
        {
            Winner = winner;
            Status = GameStatus.Finished;
        }

        /**
         * Ends the game because a player surrendered
         *
         * @param string loser The token of the player that surrendered
         */
        public void SurrenderGame(string loser)
        {
            FinishGame(loser == Player1Token ? Player2Token : Player1Token);
        }

        /**
         * A player joins the game
         *
         * @param string player The token of the player that Joined
         *
         * @return Player joined the game
         */
        public bool Join(string player)
        {
            if (Player2Token is null && Status == GameStatus.Waiting)
            {
                Player2Token = player;
                return true;
            }

            return false;
        }
    }
}
