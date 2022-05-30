using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormat.NSQLFormatter
{
    public class StringTokenizerEnumerator : IEnumerator<string>
    {
        private StringTokenizer _stokenizer;
        private int _cursor = 0;
        private String _next = null;

        public StringTokenizerEnumerator(StringTokenizer stok)
        {
            _stokenizer = stok;
        }

        #region IEnumerator<string> Members

        public string Current
        {
            get { return _next; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            _next = GetNext();
            return _next != null;
        }

        public void Reset()
        {
            _cursor = 0;
        }

        #endregion

        private string GetNext()
        {
            char c;
            bool isDelim;

            if (_cursor >= _stokenizer._origin.Length)
                return null;

            c = _stokenizer._origin[_cursor];
            isDelim = (_stokenizer._delim.IndexOf(c) != -1);

            if (isDelim)
            {
                _cursor++;
                if (_stokenizer._returnDelim)
                {
                    return c.ToString();
                }

                return GetNext();
            }

            int nextDelimPos = _stokenizer._origin.IndexOfAny(_stokenizer._delim.ToCharArray(), _cursor);
            if (nextDelimPos == -1)
            {
                nextDelimPos = _stokenizer._origin.Length;
            }

            string nextToken = _stokenizer._origin.Substring(_cursor, nextDelimPos - _cursor);
            _cursor = nextDelimPos;
            return nextToken;
        }
    }
}
