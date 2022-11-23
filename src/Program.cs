// See https://aka.ms/new-console-template for more information

using DIExtractor;

DIExtract de = new DIExtract(@"C:\Temp\di\Engine.mpkinfo");

for (int i = 0; i < de.TotalFiles; i++)
{
    de.ExtractFile(i);
}