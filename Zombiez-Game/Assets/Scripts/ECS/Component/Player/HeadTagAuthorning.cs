using Unity.Entities;
using UnityEngine;

public class HeadTagAuthoring : MonoBehaviour
{
    private class Baker : Baker<HeadTagAuthoring>
    {
        public override void Bake(HeadTagAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<HeadTag>(entity);
        }
    }
}

public struct HeadTag : IComponentData {}