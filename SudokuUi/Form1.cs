using SudokuCore;
using SudokuIo;
using SudokuSolver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuUi {
    public partial class Form1 : Form {

        Sudoku sud;
        private Button[,] btns;
        private bool[,] solvedCell;

        public Form1() {
            InitializeComponent();

            sud = new Sudoku();

            btns = new Button[9, 9];
            solvedCell = new bool[9, 9];

            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    Button btn = new Button() {
                        Parent = sudokuPanel,
                        Width = 40,
                        Height = 40,
                        Top = y * 45,
                        Left = x * 45,
                        Tag = new int[] { x, y },
                        Font = new Font(FontFamily.GenericSansSerif, 20)
                    };
                    btn.Click += Btn_Click;
                    btn.KeyDown += Btn_KeyDown;
                    btns[x, y] = btn;

                    if (x % 3 == 2 && y % 3 == 2 && x != 8 && y != 8) {
                        new Panel() {
                            BackColor = Color.Black,
                            Top = 0,
                            Left = x * 45 + 40,
                            Height = 9 * 45 - 5,
                            Width = 5,
                            Parent = sudokuPanel
                        };
                        new Panel() {
                            BackColor = Color.Black,
                            Top = y * 45 + 40,
                            Left = 0,
                            Height = 5,
                            Width = 9 * 45 - 5,
                            Parent = sudokuPanel
                        };
                    }
                }
            }
            ReDraw();
        }

        private void Btn_KeyDown(object sender, KeyEventArgs e) {
            var num = 0;
            if (e.KeyValue >= 48 && e.KeyValue <= 57) {
                num = e.KeyValue - 48;
            } else if (e.KeyValue >= 96 && e.KeyValue <= 105) {
                num = e.KeyValue - 96;
            } else {
                return;
            }

            int[] tag = (int[])((Button)sender).Tag;
            sud.SetCell(tag[0], tag[1], num);
            ReDraw();
        }

        private void Btn_Click(object sender, EventArgs e) {
            int[] tag = (int[])((Button)sender).Tag;
            sud.SetNext(tag[0], tag[1]);
            ReDraw();
        }

        private void ReDraw() {
            ReDraw(true);
        }

        private void ReDraw(bool click) {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    var i = sud.GetCell(x, y);
                    if (i == 0) {
                        btns[x, y].Text = "";
                        if (click) {
                            solvedCell[x, y] = true;
                        }
                    } else {
                        btns[x, y].Text = (i) + "";
                        if (click) {
                            solvedCell[x, y] = false;
                        }
                    }
                    
                    if (solvedCell[x, y]) {
                        btns[x, y].ForeColor = Color.Black;
                    } else {
                        btns[x, y].ForeColor = Color.Blue;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Solver solv = new Solver(sud);
            bool res = solv.Solve();
            if (res) {
                //MessageBox.Show("Solved!");
                sud = solv.getSolution();
                ReDraw(false);
            } else {
                MessageBox.Show("Not Solvable!");
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "") {
                ISaver saver = new SudokuFile(saveFileDialog1.FileName);
                saver.Save(sud);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "") {
                ILoader loader = new SudokuFile(openFileDialog1.FileName);
                sud = loader.Load();
                ReDraw();
            }
        }
    }
}
