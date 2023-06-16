using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WebBrowser.DOM;
using WebBrowser.DOM.Nodes;
using WebBrowser.DOM.Nodes.Elements;

namespace WebBrowser
{
    public class HTMLParser
    {
        public Document Document { get; init; }
        private Node _currentNode;
        private StringBuilder _sb = new();

        /* Status */
        private bool _buildingNode = false;
        private bool _closingNode = false;

        public HTMLParser(Document document, string HTML)
        {
            Document = document;
            _currentNode = document;

            Parse(HTML);
        }

        public enum Token
        {
            Unknown,
            Whitespace,
            Character,
            HtmlTagStart,
            HtmlTagEnd,
            HtmlTagCloser,
        }

        public bool BuildNode()
        {
            if (!_buildingNode)
            {
                return false;
            }

            if (_closingNode)
            {
                _currentNode = _currentNode.ParentNode ?? _currentNode;
                return true;
            }

            _currentNode = Document.CreateElement(_currentNode, _sb.ToString());
            _sb.Clear();

            _buildingNode = false;
            return true;
        }

        public void Parse(string HTML)
        {
            const string charsWhitespace = " \t\r\n";
            const string charsAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!?[]-";
            const string charsASCII = charsAlphabet + charsWhitespace + "()@#$%^&*()_+={}\\|'\";:,./0123456789";

            int pos = 0;
            char c;

            while (pos < HTML.Length)
            {
                c = HTML[pos];

                Token token = c switch
                {
                    char a when charsAlphabet.Contains(a) => Token.Character,
                    char w when charsWhitespace.Contains(w) => Token.Whitespace,
                    '<' => Token.HtmlTagStart,
                    '>' => Token.HtmlTagEnd,
                    '/' => Token.HtmlTagCloser,
                    char x when charsASCII.Contains(x) => Token.Character,
                    _ => Token.Unknown
                };

                switch (token)
                {
                    case Token.Unknown:
                    {
                        MessageBox.Show($"Unknown token: {c}", "Invalid HTML", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    }

                    case Token.Whitespace:
                    {
                        if (BuildNode())
                        {
                            break;
                        }

                        break;
                    }

                    case Token.HtmlTagEnd:
                    {
                        if (BuildNode())
                        {
                            break;
                        }

                        break;
                    }

                    case Token.Character:
                    {
                        _sb.Append(c);
                        break;
                    }

                    case Token.HtmlTagStart:
                    {
                        _buildingNode = true;
                        _sb.Clear();
                        break;
                    }

                    case Token.HtmlTagCloser:
                    {
                        if (_buildingNode)
                        {
                            _closingNode = true;
                            break;
                        }

                        break;
                    }

                    default:
                    {
                        throw new NotImplementedException();
                    }
                }

                pos++;
            }
        }
    }
}
