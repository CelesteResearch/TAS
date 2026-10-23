using System;
using System.Numerics;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Celeste.Core.Rendering;
using Celeste.Core.Scheduling;
using Celeste.Graphics.Animation;
using Celeste.Simulation.Gameplay.Player.Presentation;
using Celeste.Simulation.Gameplay.Player.Presentation.Sprites;
using Celeste.Game.Execution.Graphics;
using Player = Celeste.Simulation.RoomPlayerSession;

namespace Celeste.Mods.Tas.DesyncFix
{
    internal sealed partial class SourceTasSkinModFixProcess
    {
        private Hook playerUpdate, spriteConstructor, spriteUpdate, playerSpriteRender;
        private ILHook metadataHook;
        private Hook spritePlay, spritePlayOffset, spriteReverse;

        private delegate void PlayerUpdateHook(Action<Player> orig, Player self);

        private delegate void SpriteConstructorHook(Action<PlayerSprite, ISourceEngineContext, PlayerSpriteMode> orig,
            PlayerSprite self, ISourceEngineContext engine, PlayerSpriteMode mode);

        private delegate void SpriteUpdateHook(Action<Sprite> orig, Sprite self);

        private delegate void SpriteRenderHook(Action<PlayerSprite, ISourceRenderContext> orig,
            PlayerSprite self, ISourceRenderContext context);

        private delegate void SpritePlayHook(Action<Sprite, string, bool, bool> orig,
            Sprite self, string id, bool restart, bool randomizeFrame);

        private delegate void SpritePlayOffsetHook(Action<Sprite, string, float, bool> orig,
            Sprite self, string id, float offset, bool randomizeFrame);

        private delegate void SpriteReverseHook(Action<Sprite, string, bool> orig,
            Sprite self, string id, bool restart);

        private delegate bool HairHook(Func<PlayerSprite, bool> orig, PlayerSprite self);

        private delegate Vector2 HairOffsetHook(Func<PlayerSprite, Vector2> orig, PlayerSprite self);

        private delegate int HairFrameHook(Func<PlayerSprite, int> orig, PlayerSprite self);

        private delegate float CarryOffsetHook(Func<PlayerSprite, float> orig, PlayerSprite self);

        internal void Load()
        {
            playerUpdate = new Hook(typeof(Player).GetMethod(nameof(Player.Update), Type.EmptyTypes),
                (PlayerUpdateHook)On_Player_Update);
            using (new DetourConfigContext(new DetourConfig("CelesteTAS", priority: int.MaxValue)).Use())
                spriteConstructor = new Hook(typeof(PlayerSprite).GetConstructor(new[]
                        { typeof(ISourceEngineContext), typeof(PlayerSpriteMode) }),
                    (SpriteConstructorHook)On_PlayerSprite_ctor);
            spriteUpdate = new Hook(typeof(Sprite).GetMethod(nameof(Sprite.Update), Type.EmptyTypes),
                (SpriteUpdateHook)On_Sprite_Update);
            playerSpriteRender = new Hook(typeof(PlayerSprite).GetMethod(nameof(PlayerSprite.Render),
                new[] { typeof(ISourceRenderContext) }), (SpriteRenderHook)On_PlayerSprite_Render);
            metadataHook = new ILHook(typeof(OriginalGameGraphicsResources).GetMethod(
                nameof(OriginalGameGraphicsResources.CreateFramesMetadata)), IL_PlayerSprite_CreateFramesMetadata);
            spritePlay = new Hook(typeof(Sprite).GetMethod(nameof(Sprite.Play),
                new[] { typeof(string), typeof(bool), typeof(bool) }), (SpritePlayHook)On_Sprite_Play);
            spritePlayOffset = new Hook(typeof(Sprite).GetMethod(nameof(Sprite.PlayOffset),
                new[] { typeof(string), typeof(float), typeof(bool) }), (SpritePlayOffsetHook)On_Sprite_PlayOffset);
            spriteReverse = new Hook(typeof(Sprite).GetMethod(nameof(Sprite.Reverse),
                new[] { typeof(string), typeof(bool) }), (SpriteReverseHook)On_Sprite_Reverse);
            driver.SourceHooks.OnHook(typeof(PlayerSprite).GetProperty(nameof(PlayerSprite.HasHair)).GetMethod,
                (HairHook)On_PlayerSprite_getHasHair);
            driver.SourceHooks.OnHook(typeof(PlayerSprite).GetProperty(nameof(PlayerSprite.HairOffset)).GetMethod,
                (HairOffsetHook)On_PlayerSprite_getHairOffset);
            driver.SourceHooks.OnHook(typeof(PlayerSprite).GetProperty(nameof(PlayerSprite.HairFrame)).GetMethod,
                (HairFrameHook)On_PlayerSprite_getHairFrame);
            driver.SourceHooks.OnHook(typeof(PlayerSprite).GetProperty(nameof(PlayerSprite.CarryYOffset)).GetMethod,
                (CarryOffsetHook)On_PlayerSprite_getCarryYOffset);
        }

        internal void Unload()
        {
            playerUpdate.Dispose();
            spriteConstructor.Dispose();
            spriteUpdate.Dispose();
            playerSpriteRender.Dispose();
            metadataHook.Dispose();
            spritePlay.Dispose();
            spritePlayOffset.Dispose();
            spriteReverse.Dispose();
        }
    }
}
