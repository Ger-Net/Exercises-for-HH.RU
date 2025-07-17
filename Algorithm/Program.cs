using System.Text;

string str = "ssddeeeweetaa";
string comstr = "s2d2e3we2ta2";

Console.WriteLine(Compress(str));
Console.WriteLine(Decompress(comstr));
string Compress(string str)
{
    if (string.IsNullOrEmpty(str))
        return str;
    
    StringBuilder compressedString = new StringBuilder();
    compressedString.Append(str[0]);
    int count = 1;

    for (int i = 1; i < str.Length; i++)
    {
        if (str[i] != str[i - 1])
        {
            if (count > 1)
                compressedString.Append(count);
            compressedString.Append(str[i]);
            count = 1;
        }
        else
        {
            count++;
        }
    }

    if (count > 1)
        compressedString.Append(count);

    return compressedString.ToString();
}

string Decompress(string str)
{
    if (string.IsNullOrEmpty(str))
        return str;

    StringBuilder deCompressedString = new StringBuilder();
    deCompressedString.Append(str[0]);
    char lastChar = str[0];
    for (int i = 1; i < str.Length; i++)
    {
        if (char.IsDigit(str[i]))
        {
            for(int j = 1; j < int.Parse(str[i].ToString());j++)
                deCompressedString.Append(lastChar);
        }
        else
        {
            lastChar = str[i];
            deCompressedString.Append(lastChar);
        }
            
    }
    return deCompressedString.ToString();
}
