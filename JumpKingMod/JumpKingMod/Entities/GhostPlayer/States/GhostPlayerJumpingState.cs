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
    /// <see cref="RavenEntity"/> currently flying
    /// </summary>
    public class GhostPlayerJumpingState : IModEntityState
    {
        private readonly GhostPlayerEntity player;
        private readonly LoopingAnimationComponent flyingAnimation;
        public IModEntityState TransitionToState;

        /// <summary>
        /// Constructor for creating a <see cref="GhostPlayerJumpingState"/>
        /// </summary>
        public GhostPlayerJumpingState(GhostPlayerEntity raven, LoopingAnimationComponent flyingAnimation)
        {
            this.player = raven ?? throw new ArgumentNullException(nameof(raven));
            this.flyingAnimation = flyingAnimation ?? throw new ArgumentNullException(nameof(flyingAnimation));
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

            if (player.Velocity.Length() < float.Epsilon)
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
            player.SetLoopingAnimation(flyingAnimation);
        }

        /// <summary>
        /// Called when exiting the state, resets the current animation
        /// </summary>
        public void Exit()
        {
            flyingAnimation.ResetAnimation();
        }
    }
}
