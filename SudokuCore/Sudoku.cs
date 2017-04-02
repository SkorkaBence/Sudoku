using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuCore {
    public class Sudoku {

        private int[,] cells;

        public Sudoku(int[,] cells) {
            if (cells.GetLength(0) != 9 || cells.GetLength(1) != 9) {
                throw new InvalidCoordinatesException();
            }
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    if (cells[x, y] < 0 || cells[x, y] > 9) {
                        throw new InvalidValueException();
                    }
                }
            }
            this.cells = cells;
        }

        public Sudoku() {
            cells = new int[9, 9];
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    cells[x, y] = 0;
                }
            }
        }

        public void SetCell(int x, int y, int v) {
            if (x < 0 || x >= 9 || y < 0 || y >= 9) {
                throw new InvalidCoordinatesException();
            }
            if (v < 0 || v > 9) {
                throw new InvalidValueException();
            }
            cells[x, y] = v;
        }

        public bool IsCellFilled(int x, int y) {
            if (x < 0 || x >= 9 || y < 0 || y >= 9) {
                throw new InvalidCoordinatesException();
            }
            return (cells[x, y] != 0);
        }

        public int GetCell(int x, int y) {
            if (x < 0 || x >= 9 || y < 0 || y >= 9) {
                throw new InvalidCoordinatesException();
            }
            return cells[x, y];
        }

        public int[] GetPossibilities(int x, int y) {
            if (x < 0 || x >= 9 || y < 0 || y >= 9) {
                throw new InvalidCoordinatesException();
            }

            bool[] used = new bool[9];
            for (int i = 0; i < 9; i++) {
                used[i] = false;
            }

            for (int ix = 0; ix < 9; ix++) {
                if (cells[ix, y] != 0) {
                    used[cells[ix, y] - 1] = true;
                }
            }

            for (int iy = 0; iy < 9; iy++) {
                if (cells[x, iy] != 0) {
                    used[cells[x, iy] - 1] = true;
                }
            }

            int sx = (x / 3) * 3;
            int sy = (y / 3) * 3;

            for (int ix = 0; ix < 3; ix++) {
                for (int iy = 0; iy < 3; iy++) {
                    if (cells[sx + ix, sy + iy] != 0) {
                        used[cells[sx + ix, sy + iy] - 1] = true;
                    }
                }
            }

            List<int> res = new List<int>();

            for (int i = 0; i < 9; i++) {
                if (used[i] == false) {
                    res.Add(i + 1);
                }
            }

            return res.ToArray();
        }

        public Sudoku Clone() {
            return new Sudoku((int[,])cells.Clone());
        }

        public SudokuResult GetResult() {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    if (cells[x, y] == 0) {
                        return SudokuResult.NotSolved;
                    }
                }
            }

            for (int x = 0; x < 9; x++) {
                bool[] used = new bool[9];
                for (int y = 0; y < 9; y++) {
                    if (used[cells[x, y] - 1] == true) {
                        return SudokuResult.Mistakes;
                    }
                    used[cells[x, y] - 1] = true;
                }
            }

            for (int y = 0; y < 9; y++) {
                bool[] used = new bool[9];
                for (int x = 0; x < 9; x++) {
                    if (used[cells[x, y] - 1] == true) {
                        return SudokuResult.Mistakes;
                    }
                    used[cells[x, y] - 1] = true;
                }
            }

            for (int sX = 0; sX < 9; sX += 3) {
                for (int sY = 0; sY < 9; sY += 3) {
                    bool[] used = new bool[9];
                    for (int x = sX; x < sX + 3; x++) {
                        for (int y = sY; y < sY + 3; y++) {
                            if (used[cells[x, y] - 1] == true) {
                                return SudokuResult.Mistakes;
                            }
                            used[cells[x, y] - 1] = true;
                        }
                    }
                }
            }

            return SudokuResult.Correct;
        }

        public void SetNext(int x, int y) {
            if (x < 0 || x >= 9 || y < 0 || y >= 9) {
                throw new InvalidCoordinatesException();
            }
            int[] pos = GetPossibilities(x, y);
            int rn = cells[x, y];
            for (int i = 0; i < pos.Length; i++) {
                if (pos[i] > rn) {
                    cells[x, y] = pos[i];
                    return;
                }
            }
            cells[x, y] = 0;
        }

    }
}
