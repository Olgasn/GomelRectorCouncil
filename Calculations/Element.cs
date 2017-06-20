using System;
public class Element : IComparable
{

    public int Index { get; set; }
    public double Value { get; set; }

 
    public int CompareTo(object o)
    {
        Element p = o as Element;
        if (p != null)
            return Value.CompareTo(p.Value);
        else
            throw new Exception("Невозможно сравнить два объекта");
    }

    public String toString()
        {
            return "Element [index=" + Index + ", value=" + Value + "]";
        }
    }
}













    




