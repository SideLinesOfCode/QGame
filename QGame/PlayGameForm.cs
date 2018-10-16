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
    public partial class PlayGameForm : Form
    {
        private OpenFileDialog openFile = new OpenFileDialog();

        public PlayGameForm()
        {
            InitializeComponent();
        }

        enum SquareType
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

        private SquareType _squaretype = SquareType.Blank;

        private void DoLoad(string fileName)
        {
            StreamReader loadLevel = new StreamReader(fileName);
            string file = loadLevel.ReadToEnd();
            loadLevel.Close();
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            openFile.Filter = "Text Files (*.txt|*.txt";
            openFile.DefaultExt = "txt";
            openFile.AddExtension = true;
            switch (result)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        string fileName = openFile.FileName;
                        DoLoad(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Please choose a file to load!",@"Error loading file!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

        private void returnToMainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainMenu = new MainMenuForm();
            this.Hide();
            mainMenu.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {

        }

        private void btnDown_Click(object sender, EventArgs e)
        {

        }

        private void btnUp_Click(object sender, EventArgs e)
        {

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            
        }
    }
}
