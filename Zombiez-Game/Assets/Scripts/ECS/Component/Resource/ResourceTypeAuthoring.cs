
using Unity.Entities;
using UnityEngine;

public class ResourceTypeAuthoring : MonoBehaviour
{
    public ResourceType resourceType;
    private class Baker : Baker<ResourceTypeAuthoring>
    {
        public override void Bake(ResourceTypeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new ResourceData()
            {
                resourceType = authoring.resourceType
            });
        }
    }
}

public struct ResourceData : IComponentData
{
    public ResourceType resourceType;
}

public enum ResourceType
{
    Apple,
    Actar,
    Board,
    Stone
}