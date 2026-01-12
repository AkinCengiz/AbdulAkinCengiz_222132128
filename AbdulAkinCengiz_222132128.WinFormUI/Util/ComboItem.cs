using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.WinFormUI.Util;

public sealed class ComboItem<T>
{
    public T Value { get; set; }
    public string Text { get; set; }
    public ComboItem(T value, string text)
    {
        Value = value;
        Text = text;
    }
    public override string ToString()
    {
        return Text;
    }
}
