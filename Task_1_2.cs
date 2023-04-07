string path = "C:\\recipes.txt";

Dictionary<string, List<List<string>>> cook_book = addFileToCookBook(path);

printDictionary(get_shop_list_by_dishes(cook_book, new List<string> { "Омлет", "Утка по-пекински", "Запеченный картофель" }, 1));

Dictionary<string, List<List<string>>> addFileToCookBook(string path)
{
    string[] linesOfText = File.ReadAllLines(path);
    Dictionary<string, List<List<string>>> cook_book = new Dictionary<string, List<List<string>>>();
    int a = 0, b = 0;
    bool endOfText = false;
    while (!endOfText)
    {
        string dishName = linesOfText[0 + a];
        List<List<string>> composition = new List<List<string>>();
        for (int i = 0; i < Convert.ToInt32(linesOfText[1 + a]); i++)
        {
            List<string> ingridient = new List<string>();

            ingridient.Add("ingredient_name");
            int iOFS = linesOfText[2 + a + i].IndexOf('|');                 
            ingridient.Add(linesOfText[2 + a + i].Substring(0, iOFS - 1));

            ingridient.Add("quantity");
            int iOSS = linesOfText[2 + a + i].IndexOf('|', iOFS + 1);      
            ingridient.Add(linesOfText[2 + a + i].Substring(iOFS + 2, iOSS - iOFS - 3));

            ingridient.Add("measure");
            ingridient.Add(linesOfText[2 + a + i].Substring(iOSS + 2, linesOfText[2 + a + i].Length - iOSS - 2));

            b++;
            composition.Add(ingridient);
        }
        b += 2;

        cook_book.Add(dishName, composition);
        if (b == linesOfText.Length)
            endOfText = true;
        b += 1;
        a = b;
    }
    return cook_book;

}




Dictionary<string, List<string>> get_shop_list_by_dishes(Dictionary<string, List<List<string>>> cookBook, List<string> dishes, int person_count)
{
    Dictionary<string, List<string>> shopList = new Dictionary<string, List<string>>();
    foreach (string dish in dishes)
    {
        foreach (List<string> ingridients in cook_book[dish])
        {
            if (shopList.ContainsKey(ingridients.ElementAt(1)))
            {
                int amountOfIngridient = Convert.ToInt32(shopList[ingridients.ElementAt(1)].ElementAt(3));
                amountOfIngridient += Convert.ToInt32(ingridients.ElementAt(3)) * person_count;
                shopList[ingridients.ElementAt(1)].RemoveAt(3);
                shopList[ingridients.ElementAt(1)].Add(amountOfIngridient.ToString());
            }
            else
            {
                List<string> lst = ingridients.GetRange(4, 2);
                lst.Add("quantity");
                lst.Add((Convert.ToInt32(ingridients.ElementAt(3)) * person_count).ToString());
                shopList.Add(ingridients.ElementAt(1), lst);
            }
        }
    }
    return shopList;
}



void printDictionary(Dictionary<string, List<string>> ShopList)
{
    foreach (string ingridient in ShopList.Keys)
    {
        Console.Write(ingridient);
        foreach (string prop in ShopList[ingridient])
        {
            Console.Write($" {prop}");
        }
        Console.WriteLine("");
    }

}