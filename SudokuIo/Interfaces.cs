using SudokuCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuIo {
    public interface ISaver {

        void Save(Sudoku s);

    }

    public interface ILoader {

        Sudoku Load();

    }
}
