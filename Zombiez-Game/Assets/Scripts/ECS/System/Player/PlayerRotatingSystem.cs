using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerRotatingSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        PlayerRotatingJob playerRotatingJob = new();

        state.Dependency = playerRotatingJob.ScheduleParallel(state.Dependency);
    }

    [BurstCompile]
    [WithAll(typeof(LocalPlayerTag))]
    public partial struct PlayerRotatingJob : IJobEntity
    {
        public void Execute(ref LocalTransform transform, PlayerLookAspect playerLookAspect)
        {
            playerLookAspect.UpdateLook();

            transform.Rotation = quaternion.Euler(0f, math.radians(playerLookAspect.playerLook.ValueRO.Look.x), 0f);
        }
    }
}
