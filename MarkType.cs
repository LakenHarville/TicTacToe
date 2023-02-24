using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{

    /// <summary>
    /// Current cell value.
    /// </summary>
    public enum MarkType
    {
  
        // The cell hasn't been clicked yet.
        Free,

        // The cell is O.
        Nought,

        // The cell is X.
        Cross
    }
}
