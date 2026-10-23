using Celeste.Everest;
using System;
using System.Numerics;
using Celeste.Core.Mathematics;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Simulation.Gameplay.Player.Intro;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Player = Celeste.Simulation.RoomPlayerSession;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameData
    {
        internal int? GetIntroTime()
        {
            if (process.Frame.Scenes.CurrentScene is not Level level)
            {
                return null;
            }

            var areaData = process.AreaKeys.GetArea(process.AreaKeys.GetSessionArea(level.Session));
            var player =
                level.Scheduler.Entities.GetTrackedEntity<Player>(typeof(global::Celeste.Simulation.RoomPlayerSession)
                    .FullName);


            int? wakeupTime;
            switch (areaData.IntroType)
            {
                // PlayerIntroType.Transition => expr,
                case PlayerIntroType.Respawn:
                {
                    wakeupTime =
                        (int)Math.Ceiling(0.6f /
                                          process
                                              .DeltaTime); // respawnTween = Tween.Create(Tween.TweenMode.Oneshot, null, 0.6f, start: true);
                    break;
                }

                case PlayerIntroType.WalkInRight:
                case PlayerIntroType.WalkInLeft:
                {
                    if (player == null)
                    {
                        wakeupTime = null;
                        break;
                    }

                    bool isRight = areaData.IntroType == PlayerIntroType.WalkInRight;
                    var direction = isRight ? 1 : -1;

                    var spawnPoint = level.DefaultSpawnPoint;
                    float startX = isRight
                        ? level.Bounds.Left - 16.0f
                        : level.Bounds.Right + 16.0f;
                    float targetX = spawnPoint.X;

                    // Simulate walk done by IntroWalkCoroutine
                    int walkFrames = 0;

                    var was = player.Collider;
                    player.Collider = player.World.Contacts.NormalHitbox;

                    float xPos = startX;
                    while (Math.Abs(Math.Round(xPos) - targetX) > 2.0f &&
                           !player.Body.CollideSolidAt(new Vector2(xPos + (float)direction, spawnPoint.Y)))
                    {
                        xPos = ScalarMath.Approach(xPos, targetX, 64.0f * process.DeltaTime);
                        walkFrames++;
                    }

                    player.Collider = was;

                    wakeupTime = 1 + // From state-machine coroutine
                                 (int)(Math.Ceiling(0.3f / process.DeltaTime) + 1) + // yield return 0.3f;
                                 (int)(Math.Ceiling(0.2f / process.DeltaTime) + 1) + // yield return 0.2f;
                                 walkFrames;
                    break;
                }

                case PlayerIntroType.Jump:
                {
                    if (player == null)
                    {
                        wakeupTime = null;
                        break;
                    }

                    var spawnPoint = level.DefaultSpawnPoint;
                    float startY = level.Bounds.Bottom + 16.0f;
                    float targetY = spawnPoint.Y - 8.0f;

                    // Simulate jump done by IntroJumpCoroutine
                    int jumpFrames = 0;

                    float yPos = startY;
                    while (yPos > targetY)
                    {
                        yPos += -120.0f * process.DeltaTime;
                        jumpFrames++;
                    }

                    yPos = MathF.Round(yPos);

                    float ySpeed = -100.0f;
                    while (ySpeed < 0.0f)
                    {
                        ySpeed += 800.0f * process.DeltaTime;
                        yPos += ySpeed * process.DeltaTime;
                        jumpFrames++;
                    }

                    ySpeed = 0.0f;

                    var was = player.Collider;
                    player.Collider = player.World.Contacts.NormalHitbox;

                    static bool CheckGround(Player player, Vector2 pos) =>
                        player.Body.CollideSolidAt(pos) || player.Body.CollideJumpThruAt(pos);

                    while (!CheckGround(player, new Vector2(spawnPoint.X, yPos)))
                    {
                        ySpeed += 800.0f * process.DeltaTime;
                        yPos += ySpeed * process.DeltaTime;
                        jumpFrames++;
                    }

                    player.Collider = was;

                    wakeupTime = 1 + // From state-machine coroutine
                                 (int)(Math.Ceiling(0.5f / process.DeltaTime) + 1) + // yield return 0.5f;
                                 (int)(Math.Ceiling(0.1f / process.DeltaTime) + 1) + // yield return 0.1f;
                                 jumpFrames;
                    break;
                }

                case PlayerIntroType.WakeUp:
                {
                    float wakeUpTime =
                        player != null && player.Sprite.Animations.TryGetValue("wakeUp", out var wakeUpAnim)
                            ? wakeUpAnim.Frames.Length * wakeUpAnim.Delay
                            : 24 * 0.1f; // Taken from vanilla Sprites.xml

                    wakeupTime = 1 + // From state-machine coroutine
                                 (int)(Math.Ceiling(0.5f / process.DeltaTime) + 1) + // yield return 0.5f;
                                 (int)(Math.Ceiling(wakeUpTime / process.DeltaTime) +
                                       1) + // yield return Sprite.PlayRoutine("wakeUp");
                                 (int)(Math.Ceiling(0.2f / process.DeltaTime) + 1); // yield return 0.2f;
                    break;
                }

                case PlayerIntroType.ThinkForABit:
                {
                    wakeupTime = 1 + // From state-machine coroutine
                                 (int)(Math.Ceiling(0.1f / process.DeltaTime) + 1) + // yield return 0.1f;
                                 (int)Math.Ceiling((8.0f / 32.0f) /
                                                   process.DeltaTime) + // MoveH(32f * process.DeltaTime);
                                 (int)(Math.Ceiling(0.3f / process.DeltaTime) + 1) + // yield return 0.3f;
                                 (int)(Math.Ceiling(0.8f / process.DeltaTime) + 1) + // yield return 0.8f;
                                 (int)(Math.Ceiling(0.1f / process.DeltaTime) + 1); // yield return 0.1f;
                    break;
                }

                case PlayerIntroType.None:
                default:
                    wakeupTime = null;
                    break;
            }

            if (wakeupTime == null)
            {
                driver.LogSourceCommunication(EverestLogLevel.Warn,
                    $"Couldn't determine wakeup time for intro type '{areaData.IntroType}'");
            }

            return wakeupTime;
        }
    }
}
