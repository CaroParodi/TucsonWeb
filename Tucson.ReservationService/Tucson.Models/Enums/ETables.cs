using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tucson.Models.Enums
{
    public class ETable
    {
        public string TypeOfTable { get; set; }
        public int Availability { get; set; }
        public int Seats { get; set; }

        public ETable() { }
        public ETable(string typeOfTable, int amountOfTables, int amountOfSeats)
        {
            TypeOfTable = typeOfTable;
            Availability = amountOfTables;
            Seats = amountOfSeats;
        }

        public static readonly ETable SmallTable = new ETable("SmallTable",18, 2);
        public static readonly ETable MediumTable = new ETable("MediumTable", 15, 4);
        public static readonly ETable LargeTable = new ETable("LargeTable", 7, 6);

    }
}
