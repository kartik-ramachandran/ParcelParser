using System.IO;
using ParcelLoader.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using ParcelLoader.Business.DomainData;
using System;

namespace ParcelLoader.Business
{
    [RegisterSingleton]
    public class ParcelHelper : IParcelHelper
    {
        public List<ParcelProperties> ReadFile()
        {
            var o1 = JObject.Parse(File.ReadAllText(JsonFileRepository.JsonFilePath));

            JArray a = (JArray)o1["d"];
            
            return a.ToObject<IList<ParcelProperties>>() as List<ParcelProperties>;           
        }

        public ParcelProperties GetCost(ParcelDimensions parcelDimension)
        {
            var lstparcelProperties = ReadFile();          

            var costOnHeight = lstparcelProperties.Find(h => Convert.ToInt32(parcelDimension.Height) >= Convert.ToInt32(h.MinHeight) &&
            Convert.ToInt32(h.Height) >= Convert.ToInt32(parcelDimension.Height));

            var costOnBreadth = lstparcelProperties.Find(h => Convert.ToInt32(parcelDimension.Breadth) >= Convert.ToInt32(h.MinBreadth) && 
            Convert.ToInt32(h.Breadth) >= Convert.ToInt32(parcelDimension.Breadth));

            var costOnLength = lstparcelProperties.Find(h => Convert.ToInt32(parcelDimension.Length) >= Convert.ToInt32(h.MinLength) && 
            Convert.ToInt32(h.Length) >= Convert.ToInt32(parcelDimension.Length));

            if (costOnHeight ==  null || costOnBreadth == null || costOnLength == null)
            {
                return null;
            }

            if (Convert.ToDecimal(costOnHeight.Cost) >= Convert.ToDecimal(costOnBreadth.Cost) &&
                Convert.ToDecimal(costOnHeight.Cost) >= Convert.ToDecimal(costOnLength.Cost) && 
                Convert.ToDecimal(costOnHeight.Weight) >= Convert.ToDecimal(parcelDimension.Weight))
            {
                return costOnHeight;
            }

            if (Convert.ToDecimal(costOnBreadth.Cost) >= Convert.ToDecimal(costOnHeight.Cost) &&
                Convert.ToDecimal(costOnBreadth.Cost) >= Convert.ToDecimal(costOnLength.Cost) &&
                Convert.ToDecimal(costOnBreadth.Weight) >= Convert.ToDecimal(parcelDimension.Weight))
            {
                return costOnBreadth;
            }

            if (Convert.ToDecimal(costOnLength.Cost) >= Convert.ToDecimal(costOnBreadth.Cost) &&
                Convert.ToDecimal(costOnLength.Cost) >= Convert.ToDecimal(costOnHeight.Cost) &&
                Convert.ToDecimal(costOnLength.Weight) >= Convert.ToDecimal(parcelDimension.Weight))
            {
                return costOnLength;
            }

            return null;
        }
    }
}
