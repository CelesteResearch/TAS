using System;
using Celeste.Core.Rendering;

namespace Celeste.Core.Rendering.Diagnostics
{
    public interface ISourceTasHitboxProcessEnvironment
    {
        ISourceTasHitboxProcess TasHitboxes { get; }
    }
}
