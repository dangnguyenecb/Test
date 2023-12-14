using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ECBIM.API.Extensions;

namespace ECBIM.API
{
    public class InPlaceFamily
    {
        private Family _family;

        public Family Family { get => _family; set => _family = value; }
        public int Number { get; set; }
        public List<ElementId> Elements { get; set; }
        public string Name { get => _family.Name; }

        public InPlaceFamily(Family family)
        {
            Family = family;
            Elements = family.Document.GetFamilyElements(family);
            Number = Elements.Count;
        }

        public string GetResultMessage()
        {
            string message = $"{Name} : {Number} occurences\n" +
                $"Ids : [{string.Join(",", Elements.Select(i => i.IntegerValue))}]";

            return message;
        }
    }
}
