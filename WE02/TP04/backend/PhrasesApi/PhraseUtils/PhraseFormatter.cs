using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhraseUtils
{
    public static class PhraseFormatter
    {
        public static string FormatDate(DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy HH:mm");
        }
    }
}

