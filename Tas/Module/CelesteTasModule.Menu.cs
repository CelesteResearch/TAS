using Celeste.Audio.Fmod.Events;
using Celeste.Mods.Tas.UI.Menus;
using Celeste.Simulation.UI.Menus.Text;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasModule
    {
        private CelesteTasMenu sourceMenu;

        public override void CreateModMenuSection(TextMenuPresentation menu, bool inGame, FmodEventReference snapshot)
        {
            CreateModMenuSectionHeader(menu, inGame, snapshot);
            (sourceMenu ??= new CelesteTasMenu(Driver)).CreateMenu(this, menu, inGame);
        }
    }
}
