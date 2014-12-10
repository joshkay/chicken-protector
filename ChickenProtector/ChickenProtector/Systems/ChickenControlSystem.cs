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
    using ChickenProtector.Templates;

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
            if (keyboardState.IsKeyDown(Keys.A))
            {
                transformComponent.X -= keyMoveSpeed;
                if (transformComponent.X < 32)
                {
                    transformComponent.X = 32;
                }
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                transformComponent.X += keyMoveSpeed;
                if (transformComponent.X > this.graphicsDevice.Viewport.Width - 32)
                {
                    transformComponent.X = this.graphicsDevice.Viewport.Width - 32;
                }
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                transformComponent.Y -= keyMoveSpeed;
                if (transformComponent.Y < 32)
                {
                    transformComponent.Y = 32;
                }
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                transformComponent.Y += keyMoveSpeed;
                if (transformComponent.Y > this.graphicsDevice.Viewport.Height - 32)
                {
                    transformComponent.Y = this.graphicsDevice.Viewport.Height - 32;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (this.eggLaunchTimer.IsReached(this.EntityWorld.Delta))
                    AddMissile(transformComponent, entity.Tag, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (this.eggLaunchTimer.IsReached(this.EntityWorld.Delta))
                    AddMissile(transformComponent, entity.Tag, 180);
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (this.eggLaunchTimer.IsReached(this.EntityWorld.Delta))
                    AddMissile(transformComponent, entity.Tag, 90);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (this.eggLaunchTimer.IsReached(this.EntityWorld.Delta))
                    AddMissile(transformComponent, entity.Tag, -90);
            }
        }

        private void AddMissile(TransformComponent transformComponent, string tag, float angle = 90.0f, float offsetX = 0.0f)
        {
            Entity egg = this.EntityWorld.CreateEntityFromTemplate(EggTemplate.Name);

            egg.GetComponent<TransformComponent>().X = transformComponent.X;
            egg.GetComponent<TransformComponent>().Y = transformComponent.Y;

            egg.GetComponent<VelocityComponent>().Speed = -0.5f;
            egg.GetComponent<VelocityComponent>().Angle = angle;

            egg.GetComponent<ProjectileComponent>().ShooterTag = tag;
            egg.GetComponent<ProjectileComponent>().ShooterImmune = true;
        }
    }
}
