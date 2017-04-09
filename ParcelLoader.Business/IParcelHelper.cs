using ParcelLoader.Business.DomainData;
using System.Collections.Generic;

namespace ParcelLoader.Business
{
    public interface IParcelHelper
    {
        List<ParcelProperties> ReadFile();
        ParcelProperties GetCost(ParcelDimensions parcelDimension);
    }
}