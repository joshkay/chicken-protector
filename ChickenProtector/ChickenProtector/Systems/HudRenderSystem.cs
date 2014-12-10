namespace ChickenProtector.Systems
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using ChickenProtector.Components;

    #endregion
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 0)]
    public class HudRenderSystem : TagSystem
    {
        private SpriteFont font;

        private SpriteBatch spriteBatch;

        public HudRenderSystem()
            : base("PLAYER")
        {
        }

        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.font = BlackBoard.GetEntry<SpriteFont>("SpriteFont");
        }

        public override void Process(Entity entity)
        {
            HealthComponent healthComponent = entity.GetComponent<HealthComponent>();
            Vector2 textPosition = new Vector2(20, this.spriteBatch.GraphicsDevice.Viewport.Height);
            //when i build it stops mid build and this line that is commented is outlined in yellow but it doesnt show me any errors
            //this.spriteBatch.DrawString(this.font, "Health: " + healthComponent.HealthPercentage + "%", textPosition, Color.White);
        }
    }
}
