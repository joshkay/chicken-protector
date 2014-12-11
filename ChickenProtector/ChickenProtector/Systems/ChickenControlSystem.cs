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
    using Microsoft.Xna.Framework.Input;

    using ChickenProtector.Components;
    using ChickenProtector.Templates;
    using ChickenProtector.Spatials;
    #endregion

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ChickenControlSystem : TagSystem
    {
        private Helper.Timer eggTimer;
        private const float EGG_TIME = 0.2f;

        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;

        private bool left, right, up, down;

        public ChickenControlSystem()
            : base("PLAYER")
        {
            left = false;
            right = false;
            up = false;
            down = false;

            eggTimer = new Helper.Timer(EGG_TIME);
        }

        public override void LoadContent()
        {
            this.graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            this.contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
        }

        public override void Process(Entity entity)
        {
            MovePlayer(entity);
        }

        private void MovePlayer(Entity entity)
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
                left = true;
            else
                left = false;

            if (keyboardState.IsKeyDown(Keys.Right))
                right = true;
            else
                right = false;

            if (keyboardState.IsKeyDown(Keys.Up))
                up = true;
            else
                up = false;

            if (keyboardState.IsKeyDown(Keys.Down))
                down = true;
            else
                down = false;

            float ms = (float)TimeSpan.FromTicks(this.EntityWorld.Delta).Milliseconds / 1000.0f;
            eggTimer.update(ms);

            if (left || right || up || down)
                eggTimer.Running = true;

            if (!this.eggTimer.Completed)
                return;

            if (!(left || right || up || down))
                return;

            if (left)
                AddMissile(transformComponent, entity.Tag, 0);
            else if (right)
                AddMissile(transformComponent, entity.Tag, 180);
            else if (up)
                AddMissile(transformComponent, entity.Tag, 90);
            else if (down)
                AddMissile(transformComponent, entity.Tag, -90);
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

            PoopSound.PlaySound(contentManager);

            eggTimer.reset();
        }
    }
}
