List<string> File_text = new List<string>();
File_text.Add("C:\\1.txt");
File_text.Add("C:\\2.txt");
File_text.Add("C:\\3.txt");
Merge_files(File_text);
void Merge_files(List<string> list)
{
    SortedDictionary<int, KeyValuePair<string, string[]>> file = new SortedDictionary<int, KeyValuePair<string, string[]>>();
    string Text = string.Empty;
    foreach (string p in list)
    {
        file.Add(File.ReadAllLines(p).Length, new KeyValuePair<string, string[]>(p, File.ReadAllLines(p)));
    }
    foreach (int amountOfLines in file.Keys)
    {
        Text += file[amountOfLines].Key;
        Text += '\n';
        Text += amountOfLines;
        Text += '\n';
        Text += String.Join('\n', file[amountOfLines].Value);
        Text += "\n\n";
    }
    if (!File.Exists("C:\\a.txt"))
        File.Create("C:\\a.txt");
    File.WriteAllText("C:\\a.txt", Text);
}