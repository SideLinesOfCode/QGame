﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QGame
{
    public enum SquareType
    {
        Blank,
        BrickWall,
        RedSquare,
        BlueSquare,
        GreenSquare,
        YellowSquare,
        RedExit,
        BlueExit,
        GreenExit,
        YellowExit
    }

    public partial class LevelDesignForm : Form
    {
        /// <summary>
        /// Initial Board positions.
        /// </summary>
        private const int InitLeft = 20;
        private const int InitWidth = 50;
        private const int InitHeight = 50;
        private const int InitTop = 50;
        private const int SquareGap = 20;

        public Tile[,] _square;
        public int _rowSqaures;
        public int _columnSquares;
        
        /// <summary>
        /// Enumeration containing Square types
        /// </summary>
        

        private SquareType _squaretype = SquareType.Blank;

        private SaveFileDialog saveFile = new SaveFileDialog();

        public LevelDesignForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes StreamWriter for SaveFileDialog
        /// </summary>
        /// <param name="fileName"></param>
        private void DoSave(string fileName)
        {
            StreamWriter saveLevel = new StreamWriter(fileName);
            saveLevel.WriteLine(_rowSqaures);
            saveLevel.WriteLine(_columnSquares);
            
            for (int i = 0; i < _rowSqaures; i++)
            {
                for (int j = 0; j < _columnSquares; j++)
                {
                    saveLevel.WriteLine(i);
                    saveLevel.WriteLine(j);
                    saveLevel.WriteLine((int)_square[i, j].squareType);
                }
            }
            saveLevel.Close();
            MessageBox.Show("Game Saved Successfully!", "Game Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Exits program via tool strip button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Saves level via tool strip button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFile.ShowDialog();
            saveFile.Filter = "Text Files (*.txt|*.txt)";
            saveFile.DefaultExt = "txt";
            saveFile.AddExtension = true;
            switch (result)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        string fileName = saveFile.FileName;
                        DoSave(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Please enter a save file name!", @"Error saving file!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Returns to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnToMainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainMenu = new MainMenuForm();
            this.Hide();
            mainMenu.Show();
        }
        /// <summary>
        /// Creates a new column for every row indicated by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //Variable to hold the text for rows
                _rowSqaures = int.Parse(txtRows.Text);
                //Variable to hold the text for columns
                _columnSquares = int.Parse(txtColumns.Text);

                int xAxis = InitLeft;
                int yAxis = InitTop;

                //Generates new picturebox array that holds the textbox numbers
                _square = new Tile[_columnSquares, _rowSqaures];

                //Generates a vertical line, with the number of squares coming from the user's input  
                for (int r = 0; r < _rowSqaures; r++)
                {
                    //Generates a horizontal line, with the number of squares coming from the user's input 
                    
                        for (int c = 0; c < _columnSquares; c++)
                        {
                        _square[r, c] = new Tile();

                        _square[r, c].Image = Properties.Resources.blank_square;
                        _square[r, c].Left = xAxis + grpToolbox.Width;
                        _square[r, c].Top = yAxis + txtColumns.Height;
                        _square[r, c].Width = InitWidth;
                        _square[r, c].Height = InitHeight;
                        _square[r, c].row = r;
                        _square[r, c].col = c;
                        _square[r, c].squareType = SquareType.Blank;


                        _square[r, c].Click += square_click;

                       
                        Controls.Add(_square[r, c]);

                        yAxis += SquareGap + InitHeight;

                    }
                    xAxis += SquareGap + InitWidth;
                    yAxis = InitTop;
                }
            }
            //Checks for valid formatting, and throws an error if the input isn't a digit.
            catch (FormatException)
            {

                MessageBox.Show(@"Please enter a number.", @"Invalid Input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void square_click(object sender, EventArgs e)
        {
           Tile p = (Tile)sender;
            p.squareType = _squaretype;
            switch (_squaretype)
            {
                case SquareType.Blank:
                    p.Image = Properties.Resources.blank_square;
                    break;
                case SquareType.BrickWall:
                    p.Image = Properties.Resources.brick_wall;
                    break;
                case SquareType.RedSquare:
                    p.Image = Properties.Resources.red_square;
                    break;
                case SquareType.BlueSquare:
                    p.Image = Properties.Resources.blue_square;
                    break;
                case SquareType.GreenSquare:
                    p.Image = Properties.Resources.green_square;
                    break;
                case SquareType.YellowSquare:
                    p.Image = Properties.Resources.yellow_square;
                    break;
                case SquareType.RedExit:
                    p.Image = Properties.Resources.exit_red;
                    break;
                case SquareType.BlueExit:
                    p.Image = Properties.Resources.exit_blue;
                    break;
                case SquareType.GreenExit:
                    p.Image = Properties.Resources.exit_green;
                    break;
                case SquareType.YellowExit:
                    p.Image = Properties.Resources.exit_yellow;
                    break;
                default:
                    break;
            }
        }
        private void btnBlank_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.Blank;
        }

        private void btnBrickWall_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.BrickWall;
        }

        private void btnRedSquare_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.RedSquare;
        }

        private void btnBlueSquare_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.BlueSquare;
        }

        private void btnGreenSquare_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.GreenSquare;
        }

        private void btnYellowSquare_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.YellowSquare;
        }

        private void btnRedExit_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.RedExit;
        }

        private void btnBlueExit_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.BlueExit;
        }

        private void btnGreenExit_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.GreenExit;
        }

        private void btnYellowExit_Click(object sender, EventArgs e)
        {
            _squaretype = SquareType.YellowExit;
        }
    }
}
