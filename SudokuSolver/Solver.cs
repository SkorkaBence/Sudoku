using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuCore;

namespace SudokuSolver {
    public class Solver {

        private Sudoku sud;
        private int[,][] possibilities;
        private Sudoku solution;

        public Solver(Sudoku sud) {
            this.sud = sud;
            this.solution = sud;
            possibilities = new int[9, 9][];
        }

        public bool Solve() {
            int minV = 10;
            int minX = 0;
            int minY = 0;
            int missing = 0;

            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    if (!sud.IsCellFilled(x, y)) {
                        if (possibilities.Length > 0) {
                            missing++;
                            possibilities[x, y] = sud.GetPossibilities(x, y);
                            if (possibilities[x, y].Length < minV) {
                                minV = possibilities[x, y].Length;
                                minX = x;
                                minY = y;
                            }
                        }
                    }
                }
            }

            if (missing > 0) {
                for (int i = 0; i < possibilities[minX, minY].Length; i++) {
                    Sudoku test = sud.Clone();
                    test.SetCell(minX, minY, possibilities[minX, minY][i]);
                    Solver sv = new Solver(test);
                    if (sv.Solve()) {
                        solution = sv.getSolution();
                        return true;
                    }
                }
                return false;
            } else {
                return sud.GetResult() == SudokuResult.Correct;
            }
        }

        public Sudoku getSolution() {
            return solution;
        }

    }
}
