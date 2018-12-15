using TenderSearch.Business.Common.Entities;

namespace TenderSearch.Business.Common.Dto
{
    public class BarangaySuggestion
    {
        public BarangaySuggestion(int? id, string barangayName, string cityMunicipalityName, string provinceName, string regionName, string zipCode)
        {
            Id = id;
            BarangayName = barangayName;
            CityMunicipalityName = cityMunicipalityName;
            ProvinceName = provinceName;
            RegionName = regionName;
            ZipCode = zipCode;
        }

        public int? Id { get; }

        public string BarangayName { get; }

        public string CityMunicipalityName { get; }

        public string ProvinceName { get; }

        public string RegionName { get; }

        public string ZipCode { get; }
    }

    public static class BarangaySuggestionExtension
    {
        public static BarangaySuggestion CreateSuggestion(this Barangay barangay)
        {
            var id = barangay.Id != 0 ? barangay.Id : (int?)null;
            var barangayName = barangay.Name;
            var cityMunicipalityName = barangay.CityMunicipality?.Name;
            var zipCode = barangay.CityMunicipality?.ZipCode;
            var provinceName = barangay.CityMunicipality?.Province?.Name;
            var regionName = barangay.CityMunicipality?.Province?.Region?.Name;

            return new BarangaySuggestion(id, barangayName, cityMunicipalityName, provinceName, regionName, zipCode);
        }
    }
}
