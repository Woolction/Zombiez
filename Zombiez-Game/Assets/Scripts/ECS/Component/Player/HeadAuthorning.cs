using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class HeadAuthoring : MonoBehaviour
{
    public Vector2 BaseAndDown;

    private class Baker : Baker<HeadAuthoring>
    {
        public override void Bake(HeadAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Head()
            {
                IsBase = true,
                IsDown = false,
                ShakeBaseAndDown = authoring.BaseAndDown
            });
        }
    }
}

public struct Head : IComponentData
{
    public bool IsBase;
    public bool IsDown;

    public float2 ShakeBaseAndDown;
}