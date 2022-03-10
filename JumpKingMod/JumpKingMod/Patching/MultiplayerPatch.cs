using HarmonyLib;
using Logging.API;
using System;
using System.Reflection;
using JumpKing.Util;
using Microsoft.Xna.Framework;
using JumpKing;
using EntityComponent;
using JumpKingMod.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using JumpKingMod.Settings;
using Microsoft.Xna.Framework.Input;
using Settings;

namespace JumpKingMod.Patching
{
    /// <summary>
    /// An implementation of <see cref="IManualPatch"/> which enables free flying for the player
    /// </summary>
    public class MultiPlayerPatch : IManualPatch
    {
        private static ILogger logger;
        private static UITextEntity _uiEntity;
        private static ModEntityManager modEntityManager;

        public static UITextEntity UiEntity
        {
            get
            {
                if (_uiEntity == null)
                {
                    _uiEntity = new UITextEntity(modEntityManager, new Vector2(100f, 100f), "", Color.Yellow, UITextEntityAnchor.Center);
                }
                return _uiEntity;
            }
        }

        public MultiPlayerPatch(UserSettings userSettings, ModEntityManager newModEntityManager, ILogger newLogger)
        {
            logger = newLogger ?? throw new ArgumentNullException(nameof(newLogger));
            modEntityManager = newModEntityManager ?? throw new ArgumentNullException(nameof(newModEntityManager));
        }

        /// <summary>
        /// Sets up the manual patch to add <see cref="PrefixPatchMethod(object, float)"/> as the Prefix method for the <see cref="JumpKing.Player.BodyComp.Update(float)"/>
        /// </summary>
        /// <param name="harmony"></param>
        public void SetUpManualPatch(Harmony harmony)
        {
            var method = AccessTools.Method("JumpKing.Player.BodyComp:Update");
            var prefixMethod = AccessTools.Method("JumpKingMod.Patching.MultiPlayerPatch:PrefixPatchMethod");
            harmony.Patch(method, new HarmonyMethod(prefixMethod));

        }

        /// <summary>
        /// Runs before <see cref="JumpKing.Player.BodyComp.Update(float)"/> and allows us to override the velocity of the player after they press 'P' to toggle the free flying mode
        /// </summary>
        public static void PrefixPatchMethod(object __instance, float p_delta)
        {

            try
            {
                FieldInfo positionField = AccessTools.Field(__instance.GetType(), "position");
                FieldInfo velocityField = AccessTools.Field(__instance.GetType(), "velocity");
                Vector2 velocity = (Vector2)velocityField.GetValue(__instance);
                Vector2 position = (Vector2)positionField.GetValue(__instance);
                float curX = velocity.X;
                float curY = velocity.Y;
                velocity.X = curX;
                velocity.Y = curY;

                // UiEntity.ScreenSpacePosition = Camera.TransformVector2(new Vector2(10f, 10f));
                UiEntity.TextValue = $"POS: ({position.X}, {position.Y})\nVEL: ({velocity.X}, {velocity.Y})";
            }
            catch (Exception e)
            {
                logger.Error($"Exception on UpdatePatch {e.ToString()}");
            }
        }
    }
}
