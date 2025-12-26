using Unity.Entities;
using UnityEngine;

public class PlayerRayAuthoring : MonoBehaviour
{
    public GameObject RayStart;
    public GameObject RayEnd;

    private class Baker : Baker<PlayerRayAuthoring>
    {
        public override void Bake(PlayerRayAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerRay()
            {
                RayStart = GetEntity(authoring.RayStart, TransformUsageFlags.Dynamic),
                RayEnd = GetEntity(authoring.RayEnd, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct PlayerRay : IComponentData
{
    public Entity RayStart;
    public Entity RayEnd; 
}