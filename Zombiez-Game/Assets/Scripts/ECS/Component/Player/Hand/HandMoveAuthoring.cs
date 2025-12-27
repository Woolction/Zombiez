
using System.ComponentModel;
using System.Diagnostics;
using Unity.Entities;
using UnityEngine;

public class HandMoveAuthoring : MonoBehaviour
{
    private class Baker : Baker<HandMoveAuthoring>
    {
        public override void Bake(HandMoveAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<HandMove>(entity);
        }
    }
}

public struct HandMove : IComponentData
{
    public bool IsMove;
}