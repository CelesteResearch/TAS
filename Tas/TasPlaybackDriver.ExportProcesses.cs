using Celeste.Mods.Tas.Export;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasLibTasHelper sourceLibTasHelper;
        internal SourceTasLibTasHelper SourceLibTasHelper => sourceLibTasHelper ??= new SourceTasLibTasHelper(this);
        private SourceTasExportGameInfo sourceExportGameInfo;

        internal SourceTasExportGameInfo SourceExportGameInfo =>
            sourceExportGameInfo ??= new SourceTasExportGameInfo(this);

        private SourceTasExportRoomInfo sourceExportRoomInfo;

        internal SourceTasExportRoomInfo SourceExportRoomInfo =>
            sourceExportRoomInfo ??= new SourceTasExportRoomInfo(this);
    }
}
