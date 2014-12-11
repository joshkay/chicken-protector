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
    public class MosquitoSystem : TagSystem
    {
        public MosquitoSystem()
            : base("MOSQUITO")
        {

           
        }

        public override void Process(Entity entity)
        {
            Bag<Entity> spiders = this.EntityWorld.GroupManager.GetEntities("ANIMALS");

            if (spiders.Count == 0)
            { 
                entity.GetComponent<VelocityComponent>().Speed = 0.0f;
                return;
            }

            if (entity.GetComponent<FollowComponent>().Follow == null || !entity.GetComponent<FollowComponent>().Follow.IsActive)
            {
                Entity closest = spiders.Get(0);
                Vector2 barn = this.EntityWorld.TagManager.GetEntity("BARN").GetComponent<TransformComponent>().Position;

                foreach (Entity spider in spiders)
                {
                    Vector2 pos = spider.GetComponent<TransformComponent>().Position;
                    if (Vector2.Distance(pos, barn) < Vector2.Distance(closest.GetComponent<TransformComponent>().Position, barn))
                    {
                        closest = spider;
                    }
                }

                entity.GetComponent<FollowComponent>().Follow = closest;
                entity.GetComponent<VelocityComponent>().Speed = 0.8f;
            }
        }
    }
}
