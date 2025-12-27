using System.ComponentModel;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct HeadRotatingSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Head>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityQuery query = SystemAPI.QueryBuilder().WithAll<LocalPlayerTag, PlayerTag, PlayerLook>().Build();

        if (!query.HasSingleton<Entity>())
            return;

        Entity player = query.GetSingletonEntity();

        HeadRotatingJob headRotatingJob = new()
        {
            playerLook = SystemAPI.GetComponentRO<PlayerLook>(player).ValueRO,
            playerInput = SystemAPI.GetComponentRO<PlayerInput>(player).ValueRO,
            isGraunded = SystemAPI.GetComponentRO<IsGraunded>(player).ValueRO,
            elapsed = (float)SystemAPI.Time.ElapsedTime
        };

        state.Dependency = headRotatingJob.ScheduleParallel(state.Dependency);
    }

    [BurstCompile]
    public partial struct HeadRotatingJob : IJobEntity
    {
        [Unity.Collections.ReadOnly] public PlayerLook playerLook;
        [Unity.Collections.ReadOnly] public PlayerInput playerInput;
        [Unity.Collections.ReadOnly] public IsGraunded isGraunded;
        public float deltaTime;
        public float elapsed;

        public float value;

        public void Execute(ref LocalTransform transform, ref Head head)
        {
            transform.Rotation = quaternion.Euler(math.radians(playerLook.Look.y), 0f, 0f);

            if ((playerInput.Move.x != 0 || playerInput.Move.y != 0) && isGraunded.IsGraund)
            {
                float speed = playerInput.IsDownShift == true ? 7 : 5;

                float offsetY = math.sin(elapsed * speed) / 12;

                transform.Position.y = 1.5f + offsetY;
            }
            else if (math.abs(transform.Position.y - 1.5f) > 0.001f)
            {
                transform.Position.y = math.lerp(transform.Position.y, 1.5f, 5f * deltaTime);
            }
        }
    }
}