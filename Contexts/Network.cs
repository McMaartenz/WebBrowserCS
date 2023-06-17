using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebBrowser.Contexts
{
    public class Network : INotifyPropertyChanged
    {
        public Dictionary<Request, TextBlock> TextBlocks { get; set; } = new();
        public List<TextBlock> TextBlocksView { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void UpdateView()
        {
            TextBlocksView = TextBlocks.Select(tb => tb.Value).ToList();
            PropertyChanged?.Invoke(this, new(""));
        }
    }
}
