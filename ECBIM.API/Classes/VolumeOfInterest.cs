using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using ECBIM.API.Extensions;

namespace ECBIM.API
{
    public class VolumeOfInterest
    {
        private Element source;

        public Element Source { get => source; set => source = value; }
        public ElementId Id { get => source.Id; }
        public string Name { get => source.get_Parameter(BuiltInParameter.VOLUME_OF_INTEREST_NAME).AsString(); }

        public VolumeOfInterest(Element volumeOfInterest)
        {
            Source = volumeOfInterest;
        }

        public List<Level> GetAssociatedLevels()
        {
            var levelList = new FilteredElementCollector(source.Document).OfCategory(BuiltInCategory.OST_Levels)
                                                                         .Cast<Level>()
                                                                         .Where(i => i.get_Parameter(BuiltInParameter.DATUM_VOLUME_OF_INTEREST).AsElementId() == Id)
                                                                         .ToList();

            return levelList.OrEmptyIfNull();
        }

        public List<Grid> GetAssociatedGrids()
        {
            var gridList = new FilteredElementCollector(source.Document).OfCategory(BuiltInCategory.OST_Grids)
                                                                        .Cast<Grid>()
                                                                        .Where(i => i.get_Parameter(BuiltInParameter.DATUM_VOLUME_OF_INTEREST).AsElementId() == Id)
                                                                        .ToList();

            return gridList.OrEmptyIfNull();
        }
    }
}
