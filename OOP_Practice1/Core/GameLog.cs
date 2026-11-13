using System.Collections.Generic;

namespace OOP_Practice1.Core
{
    public class GameLog
    {
        private readonly List<string> _history = new List<string>();

        public void AddEntry(string entry)
        {
            _history.Add(entry);
        }

        public IReadOnlyList<string> GetHistory() => _history.AsReadOnly();

        public void Clear()
        {
            _history.Clear();
        }
    }
}