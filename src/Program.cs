using DIExtractor;


if (args.Length == 0)
{
    Console.WriteLine("Usage: DIextractor file.mpkinfo");
}
else
{
    DIExtract de = new DIExtract(args[0]);

    for (int i = 0; i < de.Files.Count; i++)
    {
        Console.Write("Extracting {0} ... ", de.Files[i].Filename);
        de.ExtractFile(i);
        Console.WriteLine("Completed!");
    }
}