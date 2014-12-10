namespace ChickenProtector.Components
{
    using Artemis.Interface;

    class ProjectileComponent : IComponent
    {
        public ProjectileComponent()
            : this("", false)
        {
        }

        public ProjectileComponent(string tag, bool immune)
        {
            this.ShooterImmune = immune;
            this.ShooterTag = tag;
        }

        public bool ShooterImmune { get; set; }
        public string ShooterTag { get; set; }
    }
}
