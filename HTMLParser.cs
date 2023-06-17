using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WebBrowser.DOM;
using WebBrowser.DOM.Nodes;
using WebBrowser.DOM.Nodes.CharacterDatas;
using WebBrowser.DOM.Nodes.Elements;

namespace WebBrowser
{
    public class HTMLParser
    {
        public Document Document { get; init; }
        private Node _currentNode;
        private readonly StringBuilder _sb = new();
        private IConsole Console => Document.DefaultView.Console;

        /* Status */
        private bool _buildingNode = false;
        private bool _closingNode = false;
        private bool _textNode = false;
        private bool _inCDATA = false;

        private bool _previousIsTextNode = false;
        Node? _previousTextNode = null;

        private string HTML;
        private int pos;

        public HTMLParser(Document document, string HTML)
        {
            Document = document;
            _currentNode = document;

            this.HTML = HTML;
            pos = 0;
            Parse();
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
                _textNode = false;
                return false;
            }

            _buildingNode = false;
            string nodeName = _sb.ToString();
            _sb.Clear();

            if (_inCDATA)
            {
                if (nodeName != "]]")
                {
                    return false;
                }
                _inCDATA = false;
            }

            if (_textNode)
            {
                _textNode = false;

                if (_previousIsTextNode)
                {
                    _previousTextNode!.NodeValue += ' ' + nodeName;
                    return true;
                }

                _previousIsTextNode = true;

                Text textNode = (Text)Document.CreateElement(_currentNode, "#text");
                textNode.NodeValue = nodeName; // Not name but value
                _previousTextNode = textNode;

                return true;
            }

            _previousIsTextNode = false;

            if (nodeName == "--" || nodeName == "]]")
            {
                _closingNode = true;
            }

            if (_closingNode)
            {
                _currentNode = _currentNode.ParentNode ?? _currentNode;
                _closingNode = false;
                return true;
            }

            _currentNode = Document.CreateElement(_currentNode, nodeName);
            return true;
        }

        private string GetSurrounding()
        {
            return "node " + _currentNode.NodeName + " at '.." +
                HTML[
                Math.Max(0, pos - 5)..
                Math.Min(HTML.Length, pos + 5)] + "..'";
        }

        private void ParseError(string message)
        {
            Console.Error($"[HTML Parser]: {message} at {pos} in {GetSurrounding()}");
        }

        public void Parse()
        {
            const string charsWhitespace = " \t\r\n";
            const string charsAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!?[]-";
            const string charsASCII = charsAlphabet + charsWhitespace + "()@#$%^&*()_+={}\\|'\";:,./0123456789";

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
                        ParseError($"Unknown token '{c}'");
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
                        if (!_buildingNode)
                        {
                            _textNode = true;
                        }
                        _buildingNode = true;
                        _sb.Append(c);
                        break;
                    }

                    case Token.HtmlTagStart:
                    {
                        if (_buildingNode)
                        {
                            BuildNode();
                        }

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
                        ParseError($"Unknown token '{c}'");
                        break;
                    }
                }

                pos++;
            }
        }
    }
}
