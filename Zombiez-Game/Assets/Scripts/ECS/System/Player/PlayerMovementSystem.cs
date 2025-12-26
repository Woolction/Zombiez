using Unity.Entities;
using Unity.Physics;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LocalPlayerTag>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        foreach ((PlayerMovementAspect playerMovementAspect, RefRO<BodyParts> bodyParts) in
        SystemAPI.Query<PlayerMovementAspect, RefRO<BodyParts>>().WithAll<LocalPlayerTag>())
        {
            playerMovementAspect.CheckGround(
                SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld,
                SystemAPI.GetComponentRO<PhysicsCollider>(bodyParts.ValueRO.CheckGround)
                );

            /*if (SystemAPI.HasBuffer<Child>(player))
            {
                DynamicBuffer<Child> children = SystemAPI.GetBuffer<Child>(player);

                foreach (Child child in children)
                {
                    if (SystemAPI.HasComponent<CheckGroundTag>(child.Value))
                    {
                        playerMovementAspect.CheckGround(
                        SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld,
                        SystemAPI.GetComponentRO<PhysicsCollider>(child.Value)
                        );

                        break;
                    }
                }
            }*/

            playerMovementAspect.Move();
        }
    }
}