
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class CheckGroundAuthoring : MonoBehaviour
{
    public float radius = 0.5f;
    private class Baker : Baker<CheckGroundAuthoring>
    {
        public override void Bake(CheckGroundAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            CollisionFilter collisionFilter = new()
            {
                BelongsTo = 1u << 7,
                CollidesWith = 1u << 6,
                GroupIndex = 0
            };

            AddComponent<CheckGroundTag>(entity);
            AddComponent(entity, new PhysicsCollider()
            {
                Value = Unity.Physics.SphereCollider.Create(new SphereGeometry
                {
                    Center = float3.zero,
                    Radius = authoring.radius
                },
                collisionFilter )
            });
        }
    }
}

public struct CheckGroundTag : IComponentData {}