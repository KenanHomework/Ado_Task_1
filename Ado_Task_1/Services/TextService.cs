using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lesson_1.Services
{
    public static class TextService
    {
        public static string GetText(string text, char ch, int count)
        {
            if (text.Count(sym => (sym == ch)) <= count) return text;
            StringBuilder sb = new StringBuilder();
            foreach (char sym in text)
            {
                if (sym == ch)
                {
                    count--;
                    if (count <= 0)
                        sb.Append(ch);
                }
                else
                    sb.Append(sym);
            }
            return sb.ToString();
        }
    }
}
