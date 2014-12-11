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
            if (entity.GetComponent<FollowComponent>().Follow == null )
            {
                entity.GetComponent<FollowComponent>().Follow = spiders.Get(0);
            }
        }
    }
}
