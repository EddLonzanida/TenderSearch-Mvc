using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Dto
{
    public class PersonSuggestion
    {
        public int? Id { get; }

        public string DisplayName { get; }

        public PersonSuggestion(int? id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }
    }

    public static class PersonSuggestionExtension
    {
        public static PersonSuggestion CreateSuggestion(this PersonBase person)
        {
            var id = person.Id != 0 ? person.Id : (int?) null;
            var displayName = person.DisplayName;

            return new PersonSuggestion(id, displayName);
        }
    }
}
