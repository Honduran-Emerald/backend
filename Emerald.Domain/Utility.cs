﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vitamin.Value.Domain
{
    public static class Utility
    {
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
