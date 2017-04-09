using ParcelLoader.Business.DomainData;

namespace ParcelLoader.Models
{
    public class Map
    {
        public static ParcelDimensions MapParcelDimensions(ParcelProperties parcelModel)        
        {
            return new ParcelDimensions
            {
                Breadth = parcelModel.Breadth,
                Length = parcelModel.Length,
                Height = parcelModel.Height,
                Weight = parcelModel.Weight
            };
        }
    }
}