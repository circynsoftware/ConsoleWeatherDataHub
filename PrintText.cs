
interface IPrint
{
    string textValue { set; }
    int sleepValue { set; }

    void TextPrinter(string textValue, int sleepValue);
}


class PrintText : IPrint
{
    public string textValue { get; set; }
    public int sleepValue { get; set; }


    public void TextPrinter() { }


    void IPrint.TextPrinter(string textValue, int sleepValue)
    {
        for (int i = 0; i < textValue.Length; i++)

        {
            Console.Write(textValue[i]);
            System.Threading.
            Thread.Sleep(sleepValue);
        }

        Console.WriteLine("");
    }
}
