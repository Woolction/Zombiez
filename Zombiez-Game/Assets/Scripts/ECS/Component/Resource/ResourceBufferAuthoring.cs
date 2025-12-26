
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class ResourceBufferAuthoring : MonoBehaviour
{
    private class Baker : Baker<ResourceBufferAuthoring>
    {
        public override void Bake(ResourceBufferAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddBuffer<AppleBuffer>(entity);
            AddBuffer<ActarBuffer>(entity);
            AddBuffer<BoardBuffer>(entity);
            AddBuffer<StoneBuffer>(entity);
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