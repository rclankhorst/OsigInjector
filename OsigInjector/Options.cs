using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace OsigInjector
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input APK file")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Target output APK path")]
        public string OutputFile { get; set; }

        [Option("keystore", Required = true, HelpText = "Path to the used key store")]
        public string KeystorePath { get; set; }

        [Option('s', "storepass", Required = true, HelpText = "Text file containing the password for the keystore")]
        public string KeystorePassFile { get; set; }

        [Option("keyalias", Required = true, HelpText = "Key alias within the store.")]
        public string KeyAlias { get; set; }

        [Option('k', "keypass", Required = true, HelpText = "Text file containing the password for the key alias")]
        public string KeyPassFile { get; set; }

        [Option("btool", Required = true, HelpText = "Path to Android build tools")]
        public string BuildToolsPath { get; set; }

        [ValueList(typeof(List<string>))]
        public IList<string> OculusSignaturePaths { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo(Program.Name, Program.Version),
                Copyright = new CopyrightInfo(Program.Author, Program.Year),
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: osiginject");
            help.AddPreOptionsLine("The application requires JarSigner from JDK, and that the bin/ folder is in the PATH variable.");
            help.AddOptions(this);
            return help;
        }
    }
}