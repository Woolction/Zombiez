
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class ResourceBufferAuthoring : MonoBehaviour
{
    public GameObject[] AppleGameObjects;
    public GameObject[] ActarGameObjects;
    public GameObject[] BoardGameObjects;
    public GameObject[] StoneGameObjects;
    private class Baker : Baker<ResourceBufferAuthoring>
    {
        public override void Bake(ResourceBufferAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            DynamicBuffer<AppleBuffer> appleBuffers = AddBuffer<AppleBuffer>(entity);

            for (int i = 0; i < authoring.AppleGameObjects.Length; i++)
            {
                appleBuffers.Add(
                    new AppleBuffer() { Apple = GetEntity(authoring.AppleGameObjects[i], TransformUsageFlags.Dynamic) });
            }

            DynamicBuffer<ActarBuffer> actarBuffers = AddBuffer<ActarBuffer>(entity);

            for (int i = 0; i < authoring.AppleGameObjects.Length; i++)
            {
                actarBuffers.Add(
                    new ActarBuffer() { Actar = GetEntity(authoring.AppleGameObjects[i], TransformUsageFlags.Dynamic) });
            }

            DynamicBuffer<BoardBuffer> boardBuffers = AddBuffer<BoardBuffer>(entity);

            for (int i = 0; i < authoring.AppleGameObjects.Length; i++)
            {
                boardBuffers.Add(
                    new BoardBuffer() { Board = GetEntity(authoring.AppleGameObjects[i], TransformUsageFlags.Dynamic) });
            }
            
            DynamicBuffer<StoneBuffer> stoneBuffers = AddBuffer<StoneBuffer>(entity);

            for (int i = 0; i < authoring.AppleGameObjects.Length; i++)
            {
                stoneBuffers.Add(
                    new StoneBuffer() { Stone = GetEntity(authoring.AppleGameObjects[i], TransformUsageFlags.Dynamic) });
            }
        }
    }
}

public struct AppleBuffer : IBufferElementData
{
    public Entity Apple;
}

public struct ActarBuffer : IBufferElementData
{
    public Entity Actar;
}

public struct BoardBuffer : IBufferElementData
{
    public Entity Board;
}

public struct StoneBuffer : IBufferElementData
{
    public Entity Stone;
}