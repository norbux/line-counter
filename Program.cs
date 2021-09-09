using System;
using System.IO;

namespace LineCounter
{
    class Program
    {
        public static ulong Total = 0;

        static void Main(string[] args)
        {
            foreach(var path in args)
            {
                if (File.Exists(path))
                {
                    // This path is a file
                    Total = Total + CountFile(path);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directoriy
                    ProcessDirectoriy(path);
                }
                else
                {
                    Console.WriteLine($"{path} no es un archivo o directoria valido.");
                }
            }
            
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Total final: {Total:N0}");
        }

        public static ulong CountFile(string path)
        {
            if (!path.EndsWith("favicon.ico")
                && !path.EndsWith("package-lock.json")
                && !path.EndsWith(".txt")
                && !path.EndsWith(".gitignore")
                && !path.EndsWith("README.md"))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    ulong i = 0;
                    while (reader.ReadLine() != null)
                        i++;
                    
                    return i;
                }
            }
            else
                return 0;
        }

        public static void ProcessDirectoriy(string directory)
        {
            if (!(directory.EndsWith("bin")) 
                && !(directory.EndsWith("obj"))
                && !(directory.EndsWith(".vs"))
                && !(directory.EndsWith(".vscode"))
                && !(directory.EndsWith(".git"))
                && !(directory.EndsWith("Doc"))
                && !(directory.EndsWith("Properties"))
                && !(directory.EndsWith("Migrations"))
                && !(directory.EndsWith("publish"))
                && !(directory.EndsWith("images"))
                && !(directory.EndsWith("lib"))
                && !(directory.EndsWith("cldr-data"))
                && !(directory.EndsWith("statsOLD"))
                && !(directory.EndsWith("_LOGO_")))
            {
                // Process the list of files found in the directory
                Console.WriteLine($"Reading path: {directory}");
                string [] fileEntries = Directory.GetFiles(directory);
                foreach(var file in fileEntries)
                    Total = Total + CountFile(file);
                
                // Recurse into subdirectories of this directory
                string [] subDirectoryEntries = Directory.GetDirectories(directory);
                foreach(var subDirectory in subDirectoryEntries)
                    ProcessDirectoriy(subDirectory);
            }
        }
    }
}
