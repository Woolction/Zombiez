using System.Diagnostics;
using System.Drawing;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
//[UpdateAfter(typeof(BuildPhysicsWorld))]
public partial struct PlayerRaycastSystem : ISystem
{
    private CollisionFilter collisionFilter;
    public void OnCreate(ref SystemState state)
    {
        collisionFilter = new CollisionFilter
        {
            BelongsTo = ~1u << 7,
            CollidesWith = 1u << 6 | 1u << 8,
            GroupIndex = 0
        };
    }

    public void OnUpdate(ref SystemState state)
    {
        PhysicsWorld physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld;

        Entity player = SystemAPI.GetSingletonEntity<LocalPlayerTag>();

        RefRO<PlayerRay> playerRay = SystemAPI.GetComponentRO<PlayerRay>(player);

        float3 start = SystemAPI.GetComponentRO<LocalToWorld>(playerRay.ValueRO.RayStart).ValueRO.Position;
        float3 end = SystemAPI.GetComponentRO<LocalToWorld>(playerRay.ValueRO.RayEnd).ValueRO.Position;

        RaycastInput raycastInput = new()
        {
            Start = start,
            End = end,
            Filter = collisionFilter
        };

        UnityEngine.Debug.DrawLine(raycastInput.Start, raycastInput.End, UnityEngine.Color.yellow);

        if (physicsWorld.CastRay(raycastInput, out RaycastHit hit))
        {
            RefRW<InventoryData> inventoryData = SystemAPI.GetComponentRW<InventoryData>(player);
            RefRO<PlayerInput> playerInput = SystemAPI.GetComponentRO<PlayerInput>(player);

            if (playerInput.ValueRO.IsDownE)
            {
                if (SystemAPI.HasComponent<ResourceData>(hit.Entity))
                {
                    RefRO<ResourceData> resource = SystemAPI.GetComponentRO<ResourceData>(hit.Entity);

                    Entity buffers = SystemAPI.GetSingletonEntity<AppleBuffer>();

                    switch (resource.ValueRO.resourceType)
                    {
                        case ResourceType.Apple:
                            DynamicBuffer<AppleBuffer> appleBuffer = SystemAPI.GetBuffer<AppleBuffer>(buffers);

                            appleBuffer.Add(new AppleBuffer() { Apple = hit.Entity });

                            inventoryData.ValueRW.AppleCount++;
                            break;
                        case ResourceType.Actar:
                            DynamicBuffer<ActarBuffer> actarBuffer = SystemAPI.GetBuffer<ActarBuffer>(buffers);

                            actarBuffer.Add(new ActarBuffer() { Actar = hit.Entity });

                            inventoryData.ValueRW.ActorCount++;
                            break;
                        case ResourceType.Board:
                            DynamicBuffer<BoardBuffer> BoardBuffer = SystemAPI.GetBuffer<BoardBuffer>(buffers);

                            BoardBuffer.Add(new BoardBuffer() { Board = hit.Entity });

                            inventoryData.ValueRW.BoardCount++;
                            break;
                        case ResourceType.Stone:
                            DynamicBuffer<StoneBuffer> StoneBuffer = SystemAPI.GetBuffer<StoneBuffer>(buffers);

                            StoneBuffer.Add(new StoneBuffer() { Stone = hit.Entity });

                            inventoryData.ValueRW.StoneCount++;
                            break;
                    }

                    RefRW<LocalTransform> transform = SystemAPI.GetComponentRW<LocalTransform>(hit.Entity);

                    transform.ValueRW.Position = float3.zero;

                    /*EntityCommandBuffer ebc = new(Unity.Collections.Allocator.Temp);

                    if (SystemAPI.HasBuffer<Child>(hit.Entity))
                    {
                        DynamicBuffer<Child> children = SystemAPI.GetBuffer<Child>(hit.Entity);

                        foreach (var child in children)
                        {
                            ebc.DestroyEntity(child.Value);
                        }
                    }*/

                    /*ebc.DestroyEntity(hit.Entity);

                    ebc.Playback(state.EntityManager);
                    ebc.Dispose();*/
                }
            }
        }

    }
}