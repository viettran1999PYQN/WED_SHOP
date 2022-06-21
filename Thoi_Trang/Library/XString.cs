using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Thoi_Trang.Library
{
    public class XString
    {
        public static string str_slug(string s)
        {
            String[][] symbols =
           {
                new String[]{ "[áàãảạấầẩậắằẳặ]","a" },
                new String[]{ "[đ]","d" },
                new String[]{ "[éèẻẽẹếềểẽệ]", "e"},
                new String[]{ "[íìỉĩị]", "i"},
                new String[]{ "[óòỏõọốồổỗộớờởỡợ]", "o" },
                new String[]{ "[úùủũụứừữửự]","u" },
                new String[]{ "[ýỳỷỹỵ]", "y"},
                new string[]{"[\\s'\";,]","-"}

            };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static string Str_limit(string str,int? length)
        {
            int lengt = (length ?? 20);
            if (str.Length > lengt)
            {
                str = str.Substring(0, lengt) + "....";
            }
            return str;
        }
    }
}