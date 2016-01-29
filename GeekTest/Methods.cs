using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeekTest
{
    public static class Methods
    {
        public static string GetTitle(int index)
        {
            switch (index)
            {
                case 1:
                    return  "Тест C# основы";
                case 2:
                    return  "Тест C# - Средний уровень";
                case 3:
                    return  "Тест Java - Основы";
                case 4:
                    return "Тест Java- Средний уровень";
                default:
                    return "Ошибка";
            }
        }
    }
}