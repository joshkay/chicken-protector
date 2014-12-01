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
    public class HealthRenderSystem : EntityProcessingSystem<HealthComponent, TransformComponent>
    {
        private SpriteFont font;

        private SpriteBatch spriteBatch;

        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.font = BlackBoard.GetEntry<SpriteFont>("SpriteFont");
        }

        public override void Process(Entity entity, HealthComponent healthComponent, TransformComponent transformComponent)
        {
            if (healthComponent != null)
            {
                if (transformComponent != null)
                {
                    string text = healthComponent.HealthPercentage + "%";
                    float c = (float)(healthComponent.HealthPercentage / 100);
                    Color color = new Color(1.0f - c, c, 0.0f);
                    this.spriteBatch.DrawString(this.font, text, new Vector2(transformComponent.X, transformComponent.Y + 25), color, 0.0f, this.font.MeasureString(text) * 0.5f, 1.0f, SpriteEffects.None, 0.5f);
                }
            }
        
        }
    }
}
