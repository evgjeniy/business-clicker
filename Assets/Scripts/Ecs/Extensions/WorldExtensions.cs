using Leopotam.Ecs;

namespace Ecs.Extensions
{
    public static class WorldExtensions
    {
        public static void SendMessage<T>(this EcsWorld world, in T message = default) where T : struct
        {
            world.NewEntity().Get<T>() = message;
        }
        
        public static void SendMessage<T>(this EcsEntity entity, in T message = default) where T : struct
        {
            entity.Get<T>() = message;
        }
    }
}