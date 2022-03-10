﻿using HarmonyLib;
using JumpKing;
using JumpKingMod.API;
using JumpKingMod.Components;
using JumpKingMod.Entities.Raven;
using JumpKingMod.Entities.Raven.States;
using Logging.API;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JumpKingMod.Entities
{
    /// <summary>
    /// An implementation of <see cref="IModEntity"/> which acts as a Raven
    /// </summary>
    public class GhostPlayerEntity : IModEntity, IDisposable
    {
        public enum RavenLogicState
        {
            Starting,
            FlyingToPoint,
            Messaging,
            FlyingAway,
            Ending
        }

        public Vector2 Transform;
        public Vector2 Velocity;
        public bool ReadyToBeDestroyed;

        protected readonly ModEntityManager modEntityManager;
        protected readonly ILogger logger;
        protected readonly Random random;

        protected IModEntityState activeAnimationState;
        protected LoopingAnimationComponent activeAnimation;
        protected float width;
        protected float height;
        protected SpriteEffects spriteEffects;

        /// <summary>
        /// Ctor for creating a <see cref="GhostPlayerEntity"/>
        /// Adds itself to the entity manager
        /// </summary>
        public GhostPlayerEntity(Vector2 transform, ModEntityManager modEntityManager, ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.modEntityManager = modEntityManager ?? throw new ArgumentNullException(nameof(modEntityManager));
            
            this.random = new Random();
            
            // Initialise the animation state machine for the raven
            InitialiseRavenAnimationStates();
            
            width = JKContentManager.PlayerSprites.idle.source.Width;
            height = JKContentManager.PlayerSprites.idle.source.Height;
            spriteEffects = SpriteEffects.None;

            Transform = transform;
            Velocity = new Vector2(0, 0);

            modEntityManager.AddEntity(this);
        }

        /// <summary>
        /// Implementation of <see cref="IDisposable.Dispose"/>
        /// Removes itself from the entity managera
        /// </summary>
        public virtual void Dispose()
        {
            modEntityManager?.RemoveEntity(this);
        }

        /// <summary>
        /// Called by the entity manager to update the logic of this entity
        /// </summary>
        public virtual void Update(float delta)
        {
            // Manual Control
            //DebugControl();

            // Update the transform based on the velocity
            Transform += Velocity;

            // Update the current state based on the velocity
            if (Velocity.Length() > float.Epsilon)
            {
                if (Velocity.X < 0)
                {
                    spriteEffects |= SpriteEffects.FlipHorizontally;
                }
                else
                {
                    spriteEffects &= ~SpriteEffects.FlipHorizontally;
                }
            }

            // Update the states
            if (activeAnimationState != null && activeAnimationState.EvaluateState(out IModEntityState nextState))
            {
                SetState(nextState);
            }

            // Update the animations
            activeAnimation?.Update(delta);

            // Reset the velocity
            Velocity = new Vector2(0, 0);
        }

        /// <summary>
        /// Called by the entity manager to draw this entity
        /// </summary>
        public virtual void Draw()
        {
            // If we have no active animation - there's nothing to draw!
            if (activeAnimation == null)
            {
                return;
            }

            // Get the active sprite from the active animation and draw it with our
            // positions and effects
            Sprite activeSprite = activeAnimation.GetActiveSprite();
            activeSprite.Draw(Camera.TransformVector2(Transform), spriteEffects);
        }

        /// <summary>
        /// Sets the currently active looping animation
        /// </summary>
        public void SetLoopingAnimation(LoopingAnimationComponent loopingAnimation)
        {
            activeAnimation = loopingAnimation;
        }

        /// <summary>
        /// Initialises the states used by the raven
        /// </summary>
        private void InitialiseRavenAnimationStates()
        {
            // Set up states
            var idleAnimation = new LoopingAnimationComponent(new[] { JKContentManager.PlayerSprites.idle }, 0.1f);
            var idleState = new GhostPlayerIdleState(this, idleAnimation);
            
            var flyingAnimation = new LoopingAnimationComponent(new[] { JKContentManager.PlayerSprites.jump_up }, 0.05f);
            var flyingState = new GhostPlayerJumpingState(this, flyingAnimation);

            idleState.TransitionToState = flyingState;
            flyingState.TransitionToState = idleState;

            SetState(idleState);
        }

        /// <summary>
        /// Sets the active state to the one provided
        /// </summary>
        private void SetState(IModEntityState state)
        {
            if (activeAnimationState == null || activeAnimationState != state)
            {
                state.Enter();
                activeAnimationState?.Exit();

                // Update our active state
                activeAnimationState = state;
            }
        }

        /// <summary>
        /// Allows manual control of the raven
        /// </summary>
        private void DebugControl()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Velocity.X -= 3f;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Velocity.Y += 3f;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Velocity.X += 3f;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Velocity.Y -= 3f;
            }
        }
    }
}
