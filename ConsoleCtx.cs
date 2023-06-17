using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebBrowser
{
    public class ConsoleCtx : INotifyPropertyChanged
    {
        public Dictionary<TextBlock, ConsoleWarningLevel> TextBlocks { get; set; } = new();
        public List<TextBlock> TextBlocksView { get; set; } = new();

        public bool showLog = true;
        public bool showWarning = true;
        public bool showError = true;

        public string? filterText;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void UpdateView()
        {
            var filtered = TextBlocks.AsEnumerable();
            if (!showLog)
            {
                filtered = filtered.Where(tb => tb.Value != ConsoleWarningLevel.Log);
            }

            if (!showWarning)
            {
                filtered = filtered.Where(tb => tb.Value != ConsoleWarningLevel.Warn);
            }

            if (!showError)
            {
                filtered = filtered.Where(tb => tb.Value != ConsoleWarningLevel.Error);
            }

            if (filterText is not null)
            {
                filtered = filtered.Where(tb => tb.Key.Text.Contains(filterText));
            }

            TextBlocksView = filtered.Select(tb => tb.Key).ToList();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
