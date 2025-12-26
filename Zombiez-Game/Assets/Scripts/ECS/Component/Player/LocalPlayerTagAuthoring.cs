
using Unity.Entities;
using UnityEngine;

public class LocalPlayerTagAuthoring : MonoBehaviour
{
    class Baker : Baker<LocalPlayerTagAuthoring>
    {
        public override void Bake(LocalPlayerTagAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<LocalPlayerTag>(entity);
        }
    }
}

public struct LocalPlayerTag : IComponentData {}