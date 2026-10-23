using Celeste.Game.Save.Data;
using Celeste.Game.Save.Runtime;
using Celeste.Everest.Modules.Persistence.GameSaves;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        private bool PrepareSourceSaveSlot(int slot, string[] args)
        {
            if (slot >= 0)
            {
                if (process.SaveContext.CurrentSave?.FileSlot != slot)
                {
                    if (process.UserIOStorage.Load<OriginalSaveDataState>(
                            process.SaveInitialization.GetFilename(slot)) is { } data)
                    {
                        if (CheckValid(data, args))
                            ((IOriginalSaveStartEnvironment)process).SaveStart.Start(
                                data, slot, process.SaveInitializationServices);
                        else
                            return false;
                    }
                    else
                    {
                        AbortTas($"Save file {slot + 1} does not exist");
                        return false;
                    }
                }
            }
            else if (process.SaveContext.CurrentSave == null ||
                     driver.SourceSafeCommand.DisallowUnsafeInput && process.SaveContext.CurrentSave.FileSlot != -1)
            {
                var data = process.SaveContext.CurrentSave ??
                           process.UserIOStorage.Load<OriginalSaveDataState>(process.SaveInitialization
                               .GetFilename(-1)) ??
                           process.SaveAllocation.CreateSave();
                if (!CheckValid(data, args))
                    return false;

                if (process.SaveContext.CurrentSave?.FileSlot is { } currentSlot && currentSlot != -1)
                {
                    process.SaveInitialization.AfterInitialize(data, process.SaveInitializationServices);
                    ((IOriginalSaveDeletionEnvironment)process).SaveDeletion.TryDelete(-1,
                        ((IOriginalSaveDeletionEnvironment)process).SaveDeletionServices);
                    ((IEverestSaveModuleStateEnvironment)process).SaveModuleState.LoadedModSaveDataIndex = -1;
                    foreach (var module in process.Modules)
                    {
                        if (module._Session != null)
                            module._Session.Index = -1;
                        if (module._SaveData != null)
                            module._SaveData.Index = -1;
                    }

                    process.SaveContext.PublishCurrentSave(data);
                    process.SaveContext.CurrentSave.FileSlot = -1;
                    process.UserIO.SaveHandler(true, true);
                }
                else
                {
                    // Original source repeats this validation before SaveData.Start.
                    if (CheckValid(data, args))
                        ((IOriginalSaveStartEnvironment)process).SaveStart.Start(
                            data, -1, process.SaveInitializationServices);
                    else
                        return false;
                }
            }

            return true;
        }

        private bool CheckValid(OriginalSaveDataState saveData, string[] args)
        {
            if (args.Length == 0)
            {
                if (((IOriginalSaveSessionSelectionEnvironment)process).SaveSessionSelection
                    .GetCurrentSessionSafe(saveData) is { InArea: true } session)
                {
                    if (process.AreaKeys.GetArea(process.AreaKeys.GetSessionArea(session)) != null)
                        return true;
                    AbortTas($"Map {process.AreaKeys.GetSID(process.AreaKeys.GetSessionArea(session))} does not exist");
                }
                else
                    AbortTas("Save file is not in chapter");

                return false;
            }

            if (!TryGetAreaId(args[0], out int _))
            {
                AbortTas($"Map {args[0]} does not exist");
                return false;
            }

            return true;
        }
    }
}
