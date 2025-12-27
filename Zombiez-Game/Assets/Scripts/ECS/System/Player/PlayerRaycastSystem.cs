using Unity.Physics.Systems;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using Unity.Physics;
using Unity.Burst;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
//[UpdateAfter(typeof(BuildPhysicsWorld))]
public partial struct PlayerRaycastSystem : ISystem
{
    private CollisionFilter collisionFilter;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        collisionFilter = new CollisionFilter
        {
            BelongsTo = ~0u,
            CollidesWith = 1u << 6 | 1u << 8,
            GroupIndex = 0
        };
    }

    [BurstCompile]
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

            Entity entity = hit.Entity;

            if (playerInput.ValueRO.IsDownE)
            {
                if (SystemAPI.HasComponent<ResourceData>(entity))
                {
                    RefRO<ResourceData> resource = SystemAPI.GetComponentRO<ResourceData>(entity);

                    Entity buffers = SystemAPI.GetSingletonEntity<AppleBuffer>();

                    switch (resource.ValueRO.resourceType)
                    {
                        case ResourceType.Apple:
                            DynamicBuffer<AppleBuffer> appleBuffer = SystemAPI.GetBuffer<AppleBuffer>(buffers);

                            appleBuffer.Add(new AppleBuffer() { Apple = entity });

                            inventoryData.ValueRW.AppleCount++;
                            break;
                        case ResourceType.Actar:
                            DynamicBuffer<ActarBuffer> actarBuffer = SystemAPI.GetBuffer<ActarBuffer>(buffers);

                            actarBuffer.Add(new ActarBuffer() { Actar = entity });

                            inventoryData.ValueRW.ActorCount++;
                            break;
                        case ResourceType.Board:
                            DynamicBuffer<BoardBuffer> BoardBuffer = SystemAPI.GetBuffer<BoardBuffer>(buffers);

                            BoardBuffer.Add(new BoardBuffer() { Board = entity });

                            inventoryData.ValueRW.BoardCount++;
                            break;
                        case ResourceType.Stone:
                            DynamicBuffer<StoneBuffer> StoneBuffer = SystemAPI.GetBuffer<StoneBuffer>(buffers);

                            StoneBuffer.Add(new StoneBuffer() { Stone = entity });

                            inventoryData.ValueRW.StoneCount++;
                            break;
                    }

                    RefRW<LocalTransform> transform = SystemAPI.GetComponentRW<LocalTransform>(entity);

                    transform.ValueRW.Position = float3.zero;
                }
                else if (inventoryData.ValueRO.Full == 0 && SystemAPI.HasComponent<ItemData>(entity))
                {
                    RefRW<ItemData> itemData = SystemAPI.GetComponentRW<ItemData>(entity);

                    inventoryData.ValueRW.Full++;

                    DynamicBuffer<PockedsBuffer> pockedsBuffers = SystemAPI.GetBuffer<PockedsBuffer>(player);

                    pockedsBuffers.Add(new PockedsBuffer() { itemInPok = entity });

                    if (!itemData.ValueRO.IsMove)
                        itemData.ValueRW.IsMove = true;
                }
            }

            /*if (SystemAPI.HasComponent<CheckNazar>(entity))
            {
                RefRW<CheckNazar> checkNazar = SystemAPI.GetComponentRW<CheckNazar>(entity);

                if (checkNazar.ValueRO.IsForward)
                {
                    checkNazar.ValueRW.IsMove = true;
                    checkNazar.ValueRW.IsForward = false;
                }
                else
                {
                    checkNazar.ValueRW.IsMove = true;
                    checkNazar.ValueRW.IsForward = true;
                }
            }*/
            
        }

    }
}