
using Unity.Entities;
using UnityEngine;

public class CubeAuthoring : MonoBehaviour
{
    private class Baker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Cube());
        }
    }
}

public struct Cube : IComponentData { }