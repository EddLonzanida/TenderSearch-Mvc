using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TenderSearch.Contracts.Dto
{
    public class UiMessage
    {
        private string _methodName;
        private readonly List<string> _messages;
        private readonly List<KeyValuePair<string, string>> _htmlTagsWithNoPairToReplace;

        public bool Any => _messages.Any();


        public UiMessage(string methodName, IEnumerable<string> messages)
        {
            _methodName = methodName;
            _messages = messages.ToList();
        }

        public UiMessage(IEnumerable<string> messages)
            : this(string.Empty, messages)
        {
        }

        public UiMessage()
        {
            _messages = new List<string>();
        }

        public UiMessage(string methodName, IEnumerable<string> messages, List<KeyValuePair<string, string>> htmlTagsWithNoPairToReplace)
            : this(methodName, messages)
        {
            _htmlTagsWithNoPairToReplace = htmlTagsWithNoPairToReplace;
        }

        public UiMessage(IEnumerable<string> messages, List<KeyValuePair<string, string>> htmlTagsWithNoPairToReplace)
            : this(string.Empty, messages)
        {
            _htmlTagsWithNoPairToReplace = htmlTagsWithNoPairToReplace;
        }


        public void SetMethodName(string methodName)
        {
            _methodName = methodName;
        }

        public List<string> GetPrivateMessages()
        {
            return _messages;
        }
        /// <summary>
        /// For users view only. No debugging details.
        /// </summary>
        /// <returns></returns>
        public string GetHtmlMessages()
        {
            var messages = _messages.ConvertAll(r => r.Replace(Environment.NewLine, "<br>"));
            var message = string.Join("<br>", messages.ToArray());
            return message;
        }

        /// <summary>
        /// For system logging. Contains information for debugging purposes. Removes all html tags.
        /// </summary>
        /// <returns></returns>
        public string GetMessages()
        {
            const string pairsOfHtmlTags = @"<.*?>|</.*?>";
            var regex = new Regex(pairsOfHtmlTags, RegexOptions.IgnoreCase);
            var messages = _messages.ConvertAll(r => regex.Replace(r, string.Empty));

            if (!string.IsNullOrWhiteSpace(_methodName))
            {
                messages.Insert(0, $"Method: {regex.Replace(_methodName, string.Empty)}");
            }

            _htmlTagsWithNoPairToReplace?.ForEach(tag =>
            {
                messages = messages.ConvertAll(r => r.Replace(tag.Key, tag.Value));
            });
            var message = string.Join(Environment.NewLine, messages.ToArray());
            return message;
        }

        public static KeyValuePair<string, string> GetHtmlTagToReplace(string htmlTagsWithNoPair, string replaceString)
        {
            return new KeyValuePair<string, string>(htmlTagsWithNoPair, replaceString);
        }

        public static KeyValuePair<string, string> GetHtmlTagToReplace(string htmlTagsWithNoPair)
        {
            return GetHtmlTagToReplace(htmlTagsWithNoPair, string.Empty);
        }

        public static List<KeyValuePair<string, string>> GetHtmlTagsToReplace(IEnumerable<string> tags)
        {
            return tags.ToList().ConvertAll(GetHtmlTagToReplace);
        }
    }
}
