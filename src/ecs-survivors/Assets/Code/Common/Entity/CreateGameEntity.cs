namespace Code.Common.Entity
{
    public static class CreateGameEntity
    {
        public static GameEntity Empty()
        {
            return Contexts.sharedInstance.game.CreateEntity();
        }
    }

    public static class CreateInputEntity
    {
        public static InputEntity Empty()
        {
            return Contexts.sharedInstance.input.CreateEntity();
        }
    }

    public static class CreateMetaEntity
    {
        public static MetaEntity Empty()
        {
            return Contexts.sharedInstance.meta.CreateEntity();
        }
    }
}