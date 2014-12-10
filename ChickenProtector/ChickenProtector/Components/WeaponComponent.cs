namespace ChickenProtector.Components
{
    using Artemis.Interface;

    public class WeaponComponent : IComponent
    {
        public WeaponComponent()
        {
            this.ShotAt = 0L;
        }

        public long ShotAt { get; set; }
    }
}
