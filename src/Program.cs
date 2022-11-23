using DIExtractor;


if (args.Length == 0)
{
    Console.WriteLine("Usage: DIextractor file.mpkinfo");
}
else
{
    DIExtract de = new DIExtract(args[0]);

    for (int i = 0; i < de.TotalFiles; i++)
    {
        Console.Write("Extracting {0} ... ", de.FileNames[i].Filename);
        de.ExtractFile(i);
        Console.WriteLine("Completed!");
    }
}