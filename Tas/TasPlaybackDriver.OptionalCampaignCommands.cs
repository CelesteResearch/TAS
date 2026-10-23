using System;
using Celeste.Mods.Tas.Commands;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasGunProcess? sourceGunCommand;
        private SourceTasSelectCampaignProcess? sourceSelectCampaignCommand;

        internal SourceTasGunProcess SourceGunCommand => sourceGunCommand ??
                                                         throw new InvalidOperationException(
                                                             "Source Gun command has not been constructed.");

        internal SourceTasSelectCampaignProcess SourceSelectCampaignCommand => sourceSelectCampaignCommand ??
                                                                               throw new InvalidOperationException(
                                                                                   "Source SelectCampaign command has not been constructed.");

        private void InitializeSourceOptionalCampaignCommands()
        {
            if (sourceGunCommand != null || sourceSelectCampaignCommand != null)
                throw new InvalidOperationException("Source optional campaign commands have already been constructed.");
            sourceGunCommand = new SourceTasGunProcess(this);
            sourceSelectCampaignCommand = new SourceTasSelectCampaignProcess(this);
        }
    }
}
