using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LocalPlayerTag>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((PlayerMovementAspect playerMovementAspect, RefRO<BodyParts> bodyParts) in
        SystemAPI.Query<PlayerMovementAspect, RefRO<BodyParts>>().WithAll<LocalPlayerTag>())
        {
            playerMovementAspect.CheckGround(
                SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld,
                SystemAPI.GetComponentRO<PhysicsCollider>(bodyParts.ValueRO.CheckGround)
                );

            playerMovementAspect.Move();
        }
    }
}