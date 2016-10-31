using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CommandLine;

namespace OsigInjector
{
    class Program
    {
        private static Options options;
        private static string targetFileName;

        static void Main(string[] args)
        {
            options = new Options();
            if (Parser.Default.ParseArguments(args, options))
                Start();
        }

        private static void Start()
        {
            try
            {
                targetFileName = new FileInfo(options.InputFile).Name;

                Console.WriteLine("Using apktool to extract this apk...");
                Console.Write(RunProcess("apktool.bat", $"d -f -o intermediate/ {options.InputFile}"));
                Console.WriteLine("Copying over signature files...");
                foreach (var path in options.OculusSignaturePaths)
                {
                    if(!File.Exists(path))
                        throw new FileNotFoundException("File not found!", path);

                    File.Copy(path, Path.Combine("intermediate/assets", new FileInfo(path).Name));
                }

                Console.WriteLine("Repacking apk...");
                Console.Write(RunProcess("apktool.bat", $"b -o output/unsigned/{targetFileName} intermediate/"));

                Console.WriteLine("Signing apk...");
                if(!File.Exists(options.KeystorePath) || !File.Exists(options.KeystorePassFile) || !File.Exists(options.KeyPassFile))
                    throw new FileNotFoundException();

                var storePass = File.ReadAllText(options.KeystorePassFile);
                var aliasPass = File.ReadAllText(options.KeyPassFile);

                var v = $"-sigalg SHA1withRSA -digestalg SHA1 -keystore {options.KeystorePath} -storepass {storePass} -keypass {aliasPass} output/unsigned/{targetFileName} {options.KeyAlias}";
                Console.WriteLine(v);
                Console.Write(RunProcess("jarsigner", v));

                Console.Write(RunProcess(Path.Combine(options.BuildToolsPath, "zipalign"), $"-v 4 \"output/unsigned/{targetFileName}\" output/{targetFileName}"));

                Console.WriteLine("\r\nEND...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static string RunProcess(string executable, string arguments)
        {
            var psi = new ProcessStartInfo(executable)
            {
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = new Process()
            {
                StartInfo = psi
            };
            process.Start();
            process.WaitForExit();
            var so = process.StandardOutput.ReadToEnd();
            var er = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(er))
                throw new Exception(er);
            return so;
        }

        public static string Name { get; } = "OsigInjector";
        public static string Version { get; } = "0.1.0";
        public static string Author { get; } = "Robin Lankhorst";
        public static int Year { get; } = 2016;
    }
}
