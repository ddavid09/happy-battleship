using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    public interface IBoardCreator
    {
        Board CreateBoard();
    }
}
