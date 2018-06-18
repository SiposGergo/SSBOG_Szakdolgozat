using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string modelName) : base($"Nem található az adatbázisban a megadott azonosítóval rendelkező {modelName}.")
        {
            
        }
    }
}
