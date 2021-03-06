#region File description

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderSystem.cs" company="GAMADU.COM">
//     Copyright � 2013 GAMADU.COM. All rights reserved.
//
//     Redistribution and use in source and binary forms, with or without modification, are
//     permitted provided that the following conditions are met:
//
//        1. Redistributions of source code must retain the above copyright notice, this list of
//           conditions and the following disclaimer.
//
//        2. Redistributions in binary form must reproduce the above copyright notice, this list
//           of conditions and the following disclaimer in the documentation and/or other materials
//           provided with the distribution.
//
//     THIS SOFTWARE IS PROVIDED BY GAMADU.COM 'AS IS' AND ANY EXPRESS OR IMPLIED
//     WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GAMADU.COM OR
//     CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
//     ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//     NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
//     ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//     The views and conclusions contained in the software and documentation are those of the
//     authors and should not be interpreted as representing official policies, either expressed
//     or implied, of GAMADU.COM.
// </copyright>
// <summary>
//   The render system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion File description

namespace ChickenProtector.Systems
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;
    using Artemis.Utils;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using ChickenProtector.Components;
    using ChickenProtector.Spatials;

    #endregion

    /// <summary>The render system.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 0)]
    public class RenderSystem : EntityProcessingSystem<SpatialFormComponent,TransformComponent>
    {
        /// <summary>The content manager.</summary>
        private ContentManager contentManager;

        private GraphicsDevice graphicsDevice;

        /// <summary>The spatial name.</summary>
        private string spatialName;

        /// <summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;

        private Texture2D hitBox;

        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
            this.graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");

            hitBox = new Texture2D(graphicsDevice, 1, 1);
            hitBox.SetData(new Color[] { Color.Red });
        }

        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        protected override void Process(Entity entity,SpatialFormComponent spatialFormComponent,TransformComponent transformComponent)
        {
            if (spatialFormComponent != null)
            {
                this.spatialName = spatialFormComponent.SpatialFormFile;

                if (transformComponent.X >= -10 &&
                    transformComponent.Y >= -10 &&
                    transformComponent.X < this.spriteBatch.GraphicsDevice.Viewport.Width + 10 &&
                    transformComponent.Y < this.spriteBatch.GraphicsDevice.Viewport.Height + 10)
                {
                    ///very naive render ...
                    if (string.Compare("PlayerShip", this.spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        PlayerChicken.Render(this.spriteBatch, this.contentManager, transformComponent);
                    }
                    else if (string.Compare("Egg", this.spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        Egg.Render(this.spriteBatch, this.contentManager, transformComponent);
                    }
                    else if (string.Compare("Mosquito", this.spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        Mosquito.Render(this.spriteBatch, this.contentManager, transformComponent);
                    }
                    else if (string.Compare("Barn", this.spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        Barn.Render(this.spriteBatch, this.contentManager, transformComponent);
                    }
                    else if (string.Compare("Spider", this.spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        Spider.Render(this.spriteBatch, this.contentManager, transformComponent);
                    }
                    else if (string.Compare("BloodPool", this.spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        BloodPool.Render(this.spriteBatch, this.contentManager, transformComponent);
                    }
                    else if (string.Compare("CrackedEgg", this.spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        CrackedEgg.Render(this.spriteBatch, this.contentManager, transformComponent);
                    }
                }

#if DEBUG
                Vector2 pos = new Vector2((int)transformComponent.X - (int)transformComponent.Width / 2, (int)transformComponent.Y - (int)transformComponent.Height / 2);
                Rectangle hitBoxRect = new Rectangle(
                    (int)pos.X, (int)pos.Y,
                    (int)transformComponent.Width, 
                    (int)transformComponent.Height
                );
                
                spriteBatch.Draw(hitBox, pos, new Rectangle(hitBoxRect.Left, hitBoxRect.Top, hitBoxRect.Width, 1), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(hitBox, pos + new Vector2(0, hitBoxRect.Height), new Rectangle(hitBoxRect.Left, hitBoxRect.Bottom, hitBoxRect.Width, 1), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(hitBox, pos, new Rectangle(hitBoxRect.Left, hitBoxRect.Top, 1, hitBoxRect.Height), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(hitBox, pos + new Vector2(hitBoxRect.Width, 0), new Rectangle(hitBoxRect.Right, hitBoxRect.Top, 1, hitBoxRect.Height + 1), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
#endif
            }
        }
    }
}