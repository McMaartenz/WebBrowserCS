using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebBrowser.DOM;
using WebBrowser.DOM.Nodes;
using WebBrowser.DOM.Nodes.Elements;

namespace WebBrowser
{
    public class HTMLParser
    {
        public Document Document { get; init; }

        public HTMLParser(Document document, string HTML)
        {
            Document = document;

            Parse(HTML);
        }

        public enum Token
        {
            Whitespace,
            Character,
            HtmlTagStart,
            HtmlTagEnd,
            HtmlTagCloser,
        }

        public void Parse(string HTML)
        {
            const string charsWhitespace = " \t\n";
            const string charsAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!?[]-";

            int pos = 0;
            char c;
            StringBuilder sb = new();

            Node currentNode = Document;

            bool buildingTag = false;
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
                    _ => throw new Exception("Unknown")
                };

                switch (token)
                {
                    case Token.Whitespace:
                    {
                        if (buildingTag)
                        {
                            currentNode = Document.CreateElement(currentNode, sb.ToString());
                            sb.Clear();

                            buildingTag = false;
                            break;
                        }
                        break;
                    }

                    case Token.Character:
                    {
                        sb.Append(c);
                        break;
                    }

                    case Token.HtmlTagStart:
                    {
                        buildingTag = true;
                        sb.Clear();
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
