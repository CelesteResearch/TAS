using StudioCommunication;

namespace Celeste.Mods.Tas.Commands
{
    public readonly record struct SourceTasCommand(
        CommandLine CommandLine,
        SourceTasCommandAttribute Attribute,
        string FilePath,
        int FileLine,
        int StudioLine,
        int Frame)
    {
        private const string DefaultSeparator = ", ";
        public void Invoke() => Attribute.Execute(CommandLine, StudioLine, FilePath, FileLine);
        public bool Is(string commandName) => Attribute.IsName(commandName);
        public string[] Args => CommandLine.Arguments;

        public string LineText => CommandLine.Arguments.Length == 0
            ? Attribute.Name
            : $"{Attribute.Name}{DefaultSeparator}{string.Join(DefaultSeparator, CommandLine.Arguments)}";

        internal void Setup(SourceTasCommandRegistry registry)
        {
            if ((Attribute.ExecuteTiming & SourceTasExecuteTiming.Parse) != 0)
            {
                registry.Parsing = true;
                Attribute.Execute(CommandLine, StudioLine, FilePath, FileLine);
                registry.Parsing = false;
            }
        }
    }
}
