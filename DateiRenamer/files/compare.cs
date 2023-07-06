using System;
using System.Collections.Generic;

class compare : IComparer<string>
{

    public int Compare(string x, string y)
    {
        // Compare x to y
        return x.CompareTo(y);
    }
}
