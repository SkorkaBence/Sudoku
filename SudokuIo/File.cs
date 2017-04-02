using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuCore;
using System.IO;

namespace SudokuIo {
    public class SudokuFile : ISaver, ILoader {

        private string fn;

        public SudokuFile(string filename) {
            fn = filename;
        }

        public Sudoku Load() {
            if (!File.Exists(fn)) {
                throw new FileNotFoundException("SudokuLoader", fn);
            }
            string[] lines = File.ReadAllLines(fn);
            int[,] data = new int[9, 9];
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    data[x, y] = int.Parse(lines[y][x] + "");
                }
            }
            return new Sudoku(data);
        }

        public void Save(Sudoku s) {
            string[] lines = new string[9];
            for (int y = 0; y < 9;  y++) {
                string d = "";
                for (int x = 0; x < 9; x++) {
                    d += s.GetCell(x, y);
                }
                lines[y] = d;
            }
            File.WriteAllLines(fn, lines);
        }


    }
}
