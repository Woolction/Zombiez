using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct HeadRotatingSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<HeadTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityQuery query = SystemAPI.QueryBuilder().WithAll<LocalPlayerTag, PlayerTag, PlayerLook>().Build();

        if (!query.HasSingleton<Entity>())
            return;

        Entity player = query.GetSingletonEntity();

        HeadRotatingJob headRotatingJob = new()
        {
            //playerLookLookup = SystemAPI.GetComponentLookup<PlayerLook>(true)
            playerLook = SystemAPI.GetComponentRO<PlayerLook>(player).ValueRO
        };

        state.Dependency = headRotatingJob.ScheduleParallel(state.Dependency);
    }

    [BurstCompile]
    [WithAll(typeof(HeadTag))]
    public partial struct HeadRotatingJob : IJobEntity
    {
        /*[Unity.Collections.ReadOnly]
        public ComponentLookup<PlayerLook> playerLookLookup;*/

        public PlayerLook playerLook;
        public void Execute(ref LocalTransform transform) //, in Parent parent)
        {
            //Entity player = parent.Value;

            /*if (!playerLookLookup.HasComponent(player))
                return;

            PlayerLook playerLook = playerLookLookup[player];*/

            transform.Rotation = quaternion.Euler(math.radians(playerLook.Look.y), 0f, 0f);
        }
    }
}