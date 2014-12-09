namespace ChickenProtector.Systems
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;
    using Artemis.Utils;

    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using ChickenProtector.Components;
   //using ChickenProtector.Templates;

    #endregion

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ChickenControlSystem : TagSystem
    {
        private readonly Timer eggLaunchTimer;
        private GraphicsDevice graphicsDevice;

        public ChickenControlSystem()
            : base("PLAYER")
        {
            this.eggLaunchTimer = new Timer(new TimeSpan(0, 0, 0, 0, 150));
        }
        public override void LoadContent()
        {
            this.graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
        }

        public override void Process(Entity entity)
        {
            TransformComponent transformComponent = entity.GetComponent<TransformComponent>();
            KeyboardState keyboardState = Keyboard.GetState();
            float keyMoveSpeed = 0.3f * TimeSpan.FromTicks(this.EntityWorld.Delta).Milliseconds;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                transformComponent.X -= keyMoveSpeed;
                if (transformComponent.X < 32)
                {
                    transformComponent.X = 32;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                transformComponent.X += keyMoveSpeed;
                if (transformComponent.X > this.graphicsDevice.Viewport.Width - 32)
                {
                    transformComponent.X = this.graphicsDevice.Viewport.Width - 32;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                transformComponent.Y += keyMoveSpeed;
                if (transformComponent.Y > 32)
                {
                    transformComponent.Y = 32;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Down))
            {
                transformComponent.Y -= keyMoveSpeed;
                if (transformComponent.Y < 32)
                {
                    transformComponent.Y = 32;
                }
            }
        }
       /* private void AddMissile(TransformComponent transformComponent, float angle = 90.0f, float offsetX = 0.0f)
        {
            Entity missile = this.EntityWorld.CreateEntityFromTemplate(MissileTemplate.Name);

            missile.GetComponent<TransformComponent>().X = transformComponent.X + 1 + offsetX;
            missile.GetComponent<TransformComponent>().Y = transformComponent.Y - 20;

            missile.GetComponent<VelocityComponent>().Speed = -0.5f;
            missile.GetComponent<VelocityComponent>().Angle = angle;
        }*/
    }
}
