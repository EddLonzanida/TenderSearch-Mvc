namespace TenderSearch.Contracts.Infrastructure
{
    public enum eUserRoles
    {
        Users = 1,
        Admins = 4,
        UserManagers = 16
    }

    public enum eArea
    {
        Admins = 2,
        Users = 8,
        UserManagers = 16,
        Registration = 32
    }

    public static class MvcArea
    {
        public const string Users = "Users";

        public const string Admins = "Admins";

        public const string UserManagers = "UserManagers";
    }

    public static class Authorize
    {
        public const string Users = "Users";

        public const string Admins = "Admins";

        public const string UserManagers = "UserManagers";
    }
    
    public static class DuplicateNameAction
    {
        public const string Edit = "Edit";
    
        public const string Create = "Create";
    }


    public static class ActionNames
    {
        public const string Index = "Index";

        public const string Edit = "Edit";

        public const string Create = "Create";

        public const string Delete = "Delete";

        public const string Details = "Details";

        public const string Suggestions = "Suggestions";

        public const string IndexWithParent = "IndexWithParent";

        public const string SuggestionsWithParent = "SuggestionsWithParent";

        public const string CreateWithParent = "CreateWithParent";

        public const string EditWithParent = "EditWithParent";

        public const string BarangaySuggestionsWithParent = "BarangaySuggestionsWithParent";

        public const string BarangaySuggestions = "BarangaySuggestions";

        public const string PersonSuggestions = "PersonSuggestions";
    }
}