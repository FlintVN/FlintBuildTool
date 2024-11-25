using System;
using System.IO;
using System.Linq;

namespace FlintBuildTool {
    internal static class Program {
        static void Main(string[] args) {
            if(args == null || args.Length == 0)
                ShowHelp();
            else {
                try {
                    if(args[0] == "merge")
                        FlintMerge.MergeBinary(args.Skip(1).ToArray());
                    else if(args[0] == "utf8crc")
                        Uft8Crc.Calculate((args != null && args.Length > 1) ? args.Skip(1).ToArray() : null);
                    else if(args[0] == "help")
                        ShowHelp();
                    else
                        Console.WriteLine("Unknow command \"" + args[0] + "\"");
                }
                catch(FileNotFoundException ex) {
                    Console.WriteLine("File not found: " + ex.FileName);
                    Environment.ExitCode = 1;
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                    Environment.ExitCode = 1;
                }
            }
        }

        private static void ShowHelp() {
            Console.WriteLine("merge:   Merge multiple binary files into one file.");
            Console.WriteLine("         usage: FlintBuildTool merge [<options>] [<binary list>]");
            Console.WriteLine("                [<options>]: -o: output file");
            Console.WriteLine("                [<binary list>]: [<offset>] [<file name>]");
            Console.WriteLine();
            Console.WriteLine("utf8crc: Calculate Crc and length for FlintConstUtf8.");
            Console.WriteLine("         usage: FlintBuildTool utf8crc [<folder list>]");
        }
    }
}
