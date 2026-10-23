using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using StudioCommunication;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Commands
{
    /// Creates a command which can be used inside TAS files
    /// The signature of the target method **must** match <see cref="Execute"/>
    [AttributeUsage(AttributeTargets.Method)]
    public class SourceTasCommandAttribute(string name) : Attribute
    {
        /// Name of this command inside the TAS file
        public readonly string Name = name;

        /// Alternative names which are also recognized as this command
        public string[] Aliases = [];

        /// Whether this command affects the TAS or is purely informational
        public bool CalcChecksum = true;

        /// Whether this command changes the game in ways which are illegal outside of testing starts
        public bool LegalInFullGame = true;

        /// Timing when this command should be executed
        public SourceTasExecuteTiming ExecuteTiming = SourceTasExecuteTiming.Runtime;

        /// Optional type which implements the <see cref="ISourceTasCommandMeta"/> interface,
        /// to provide additional metadata about the command for Studio
        public Type? MetaDataProvider = null;

        internal ISourceTasCommandMeta? MetaData = null;

        internal SourceOrderedMethod m_Execute = null!;

        public void Execute(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            m_Execute.Invoke([commandLine, studioLine, filePath, fileLine]);
        }

#if DEBUG
    internal void Validate()
    {
        var executeMethod = typeof(SourceTasCommandAttribute).GetMethod(nameof(Execute))!;
        Debug.Assert(m_Execute != null);
        Debug.Assert(m_Execute.ActualMethod.GetParameters().Length == executeMethod.GetParameters().Length);
        for (int i = 0; i < m_Execute.ActualMethod.GetParameters().Length; i++)
        {
            Debug.Assert(m_Execute.ActualMethod.GetParameters()[i].ParameterType ==
                         executeMethod.GetParameters()[i].ParameterType);
        }

        if (MetaDataProvider != null)
        {
            Debug.Assert(typeof(ISourceTasCommandMeta).IsAssignableFrom(MetaDataProvider));
        }
    }
#endif

        public bool IsName(string name)
        {
            return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) ||
                   Aliases.Any(alias => alias.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
