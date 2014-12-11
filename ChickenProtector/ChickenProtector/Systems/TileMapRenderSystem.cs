#region File description

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderSystem.cs" company="GAMADU.COM">
//     Copyright © 2013 GAMADU.COM. All rights reserved.
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

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using ChickenProtector.Components;
    using ChickenProtector.Spatials;

    #endregion

    /// <summary>The render system.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 0)]
    public class TileMapRenderSystem : EntityProcessingSystem<TileMapComponent, TransformComponent>
    {
        /// <summary>The content manager.</summary>
        private ContentManager contentManager;

        /// <summary>The spatial name.</summary>
        private int[,] tilemap;

        /// <summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;

        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
        }

        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        protected override void Process(Entity entity, TileMapComponent tileMapComponent, TransformComponent transformComponent)
        {
            if (tileMapComponent != null)
            {
                this.tilemap = tileMapComponent.MapArray;
                
                for (int y = 0; y < tilemap.GetLength(0); y++)
                {
                    for (int x = 0; x < tilemap.GetLength(1); x++)                    {                        TransformComponent tileMapTransform = new TransformComponent(
                            x * tileMapComponent.TileWidth + tileMapComponent.TileWidth / 2 + transformComponent.X,
                            y * tileMapComponent.TileHeight + tileMapComponent.TileHeight / 2 + transformComponent.Y,
                            transformComponent.Width,
                            transformComponent.Height                        );                        TileMapComponent.TileMapSpatial tile = (TileMapComponent.TileMapSpatial)tilemap[y, x];                        string spatialName = tileMapComponent.GetTileMapSpatial(tile);

                        if (string.Compare("GrassTile", spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            GrassTile.Render(this.spriteBatch, this.contentManager, tileMapTransform);
                        }
                        else if (string.Compare("DirtTile", spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            DirtTile.Render(this.spriteBatch, this.contentManager, tileMapTransform);
                        }
                        else if (string.Compare("WaterTile", spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            WaterTile.Render(this.spriteBatch, this.contentManager, tileMapTransform);
                        }
                        else if (string.Compare("SnowTile", spatialName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            SnowTile.Render(this.spriteBatch, this.contentManager, tileMapTransform);
                        }
                    }                }
            }
        }
    }
}