using System.ComponentModel.Design;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct MoveItemSystem : ISystem
{
   
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb =
            SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);

        Entity player = SystemAPI.GetSingletonEntity<LocalPlayerTag>();

        Entity Hand = SystemAPI.GetComponentRO<BodyParts>(player).ValueRO.Hands;

        foreach ((RefRW<ItemData> itemData, RefRW<LocalTransform> transform, Entity item)
            in SystemAPI.Query<RefRW<ItemData>, RefRW<LocalTransform>>().WithEntityAccess())
        {
            if (!itemData.ValueRO.IsMove || itemData.ValueRO.IsAnim)
                continue;

            float t = math.saturate((itemData.ValueRW.Progress += SystemAPI.Time.DeltaTime) * 4f);

            if (!itemData.ValueRO.IsInHand)
            {
                itemData.ValueRW.IsInHand = true;

                AttachToPlayerKeepWorld(ecb, state.EntityManager, item, Hand);
            }

            transform.ValueRW.Position =
                math.lerp(transform.ValueRO.Position, itemData.ValueRO.PosInHand, t);

            transform.ValueRW.Rotation =
                quaternion.Euler(math.radians(itemData.ValueRO.RotInHand));

            if (t >= 1f)
            {
                itemData.ValueRW.IsMove = false;
            }
        }
    }

    private static void AttachToPlayerKeepWorld(EntityCommandBuffer ecb, EntityManager em, Entity item, Entity player)
    {
        LocalToWorld itemWorld = em.GetComponentData<LocalToWorld>(item);
        LocalToWorld parentWorld = em.GetComponentData<LocalToWorld>(player);

        float4x4 local =
            math.mul(
                math.inverse(parentWorld.Value),
                itemWorld.Value
            );

        ecb.AddComponent(item, new Parent { Value = player });
        ecb.SetComponent(item, LocalTransform.FromMatrix(local));
    }
}
