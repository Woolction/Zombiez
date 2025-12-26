
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerLookAuthoring : MonoBehaviour
{
    private class Baker : Baker<PlayerLookAuthoring>
    {
        public override void Bake(PlayerLookAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerLook>(entity);
        }
    }
}

public struct PlayerLook : IComponentData
{
    public float2 Look; 
}