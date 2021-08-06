namespace OnlineShopK19PR01.Common
{
    public class Format
    {
        public static string FormatNumber(decimal _strInput)
        {
            string strInput = _strInput.ToString();
            int Length = 0;
            if (strInput.IndexOf('.') > 0)
                Length = strInput.Length - (strInput.Length - strInput.IndexOf('.'));
            else
                Length = strInput.Length;
            string afterFormat = "";
            if (Length <= 3)
                afterFormat = strInput;
            else if (Length > 3)
            {
                afterFormat = strInput.Insert(Length - 3, ".");
                Length = afterFormat.IndexOf(".");
                while (Length > 3)
                {
                    afterFormat = afterFormat.Insert(Length - 3, ".");
                    Length = Length - 3;
                }
            }
            return afterFormat;
        }
    }
}