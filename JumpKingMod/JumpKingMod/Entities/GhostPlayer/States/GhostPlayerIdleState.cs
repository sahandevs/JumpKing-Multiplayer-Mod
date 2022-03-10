using JumpKingMod.API;
using JumpKingMod.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpKingMod.Entities.Raven.States
{
    /// <summary>
    /// An implementation of <see cref="IModEntityState"/> which represents the 
    /// <see cref="RavenEntity"/> currently being idle
    /// </summary>
    public class GhostPlayerIdleState : IModEntityState
    {
        private readonly GhostPlayerEntity player;
        private readonly LoopingAnimationComponent idleAnimation;
        public IModEntityState TransitionToState;

        /// <summary>
        /// Constructor for creating a <see cref="GhostPlayerIdleState"/>
        /// </summary>
        public GhostPlayerIdleState(GhostPlayerEntity raven, LoopingAnimationComponent idleAnimation)
        {
            this.player = raven ?? throw new ArgumentNullException(nameof(raven));
            this.idleAnimation = idleAnimation ?? throw new ArgumentNullException(nameof(idleAnimation));
        }

        /// <summary>
        /// Evlauates the current state, returning a new state if it changes
        /// </summary>
        /// <returns></returns>
        public bool EvaluateState(out IModEntityState nextState)
        {
            nextState = null;
            if (TransitionToState == null)
            {
                return false;
            }

            if (player.Velocity.Length() > float.Epsilon)
            {
                nextState = TransitionToState;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called when entering the state, sets the correct animation
        /// </summary>
        public void Enter()
        {
            player.SetLoopingAnimation(idleAnimation);
        }

        /// <summary>
        /// Called when exiting the state, resets the current animation
        /// </summary>
        public void Exit()
        {
            idleAnimation.ResetAnimation();
        }
    }
}
