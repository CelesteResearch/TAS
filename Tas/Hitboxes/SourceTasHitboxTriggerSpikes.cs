using System.Numerics;
using Celeste.Core.Rendering;
using Celeste.Everest.Gameplay.Hazards.TriggerSpikes;
using Celeste.Simulation.Gameplay.Hazards.TriggerSpikes;
using Celeste.Graphics.Rendering;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed partial class SourceTasHitboxTriggerSpikes
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasHitboxTriggerSpikes(TasPlaybackDriver driver) => this.driver = driver;

        private void DrawInstalledSpikesHitboxes(TriggerSpikesState triggerSpikes, object camera,
            ISourceRenderContext context)
        {
            triggerSpikes.Collider?.Render(camera, driver.SourceHitboxColor.EntityColorInverselyLessAlpha, context);

            Vector2 offset, value;
            bool vertical = false;
            switch (triggerSpikes.SourceDirection)
            {
                case TriggerSpikesDirection.Up:
                    offset = new Vector2(-2f, -4f);
                    value = new Vector2(1f, 0f);
                    break;
                case TriggerSpikesDirection.Down:
                    offset = new Vector2(-2f, 0f);
                    value = new Vector2(1f, 0f);
                    break;
                case TriggerSpikesDirection.Left:
                    offset = new Vector2(-4f, -2f);
                    value = new Vector2(0f, 1f);
                    vertical = true;
                    break;
                case TriggerSpikesDirection.Right:
                    offset = new Vector2(0f, -2f);
                    value = new Vector2(0f, 1f);
                    vertical = true;
                    break;
                default:
                    return;
            }

            TriggerSpikesState.SpikeInfo[] spikes = triggerSpikes.SourceSpikes;
            for (int i = 0; i < spikes.Length; i++)
            {
                TriggerSpikesState.SpikeInfo spikeInfo = spikes[i];
                if (spikeInfo.Triggered && spikeInfo.Lerp >= 1f)
                {
                    Vector2 position = triggerSpikes.Position + value * (2 + i * 4) + offset;

                    bool startFromZero = i == 0;
                    int num = 1;
                    for (int j = i + 1; j < spikes.Length; j++)
                    {
                        TriggerSpikesState.SpikeInfo nextSpikeInfo = spikes[j];
                        if (nextSpikeInfo.Triggered && nextSpikeInfo.Lerp >= 1f)
                        {
                            num++;
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    float totalWidth = 4f * (vertical ? 1 : num);
                    float totalHeight = 4f * (vertical ? num : 1);
                    if (!startFromZero)
                    {
                        if (vertical)
                        {
                            position.Y -= 1;
                            totalHeight += 1;
                        }
                        else
                        {
                            position.X -= 1;
                            totalWidth += 1;
                        }
                    }

                    SourcePrimitiveDrawing.HollowRect(context, triggerSpikes, position.X, position.Y, totalWidth,
                        totalHeight, driver.SourceHitboxColor.GetCustomColor(triggerSpikes));
                }
            }
        }

        private void DrawOriginalSpikesHitboxes(EverestOriginalTriggerSpikesState triggerSpikes, object camera,
            ISourceRenderContext context)
        {
            triggerSpikes.Collider?.Render(camera, driver.SourceHitboxColor.EntityColorInverselyLessAlpha, context);

            Vector2 offset;
            float width, height;
            bool vertical = false;
            switch (triggerSpikes.SourceDirection)
            {
                case EverestOriginalTriggerSpikesState.Directions.Up:
                    width = 8f;
                    height = 3f;
                    offset = new Vector2(-4f, -4f);
                    break;
                case EverestOriginalTriggerSpikesState.Directions.Down:
                    width = 8f;
                    height = 3f;
                    offset = new Vector2(-4f, 1f);
                    break;
                case EverestOriginalTriggerSpikesState.Directions.Left:
                    width = 3f;
                    height = 8f;
                    offset = new Vector2(-4f, -4f);
                    vertical = true;
                    break;
                case EverestOriginalTriggerSpikesState.Directions.Right:
                    width = 3f;
                    height = 8f;
                    offset = new Vector2(1f, -4f);
                    vertical = true;
                    break;
                default:
                    return;
            }

            EverestOriginalTriggerSpikesState.SpikeInfo[] spikes = triggerSpikes.SourceSpikes;
            for (int i = 0; i < spikes.Length; i++)
            {
                EverestOriginalTriggerSpikesState.SpikeInfo spikeInfo = spikes[i];

                if (spikeInfo.Triggered && spikeInfo.Lerp >= 1)
                {
                    bool startFromZero = i == 0;
                    int num = 1;
                    for (int j = i + 1; j < spikes.Length; j++)
                    {
                        EverestOriginalTriggerSpikesState.SpikeInfo nextSpikeInfo = spikes[j];
                        if (nextSpikeInfo.Triggered && nextSpikeInfo.Lerp >= 1)
                        {
                            num++;
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    Vector2 position = spikeInfo.Position + triggerSpikes.Position + offset;
                    float totalWidth = width * (vertical ? 1 : num);
                    float totalHeight = height * (vertical ? num : 1);
                    if (!startFromZero)
                    {
                        if (vertical)
                        {
                            position.Y -= 1;
                            totalHeight += 1;
                        }
                        else
                        {
                            position.X -= 1;
                            totalWidth += 1;
                        }
                    }

                    SourcePrimitiveDrawing.HollowRect(context, triggerSpikes, position.X, position.Y, totalWidth,
                        totalHeight, driver.SourceHitboxColor.GetCustomColor(triggerSpikes));
                }
            }
        }
    }
}
