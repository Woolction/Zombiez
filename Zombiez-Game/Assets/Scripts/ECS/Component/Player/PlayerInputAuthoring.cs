
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInputAuthoring : MonoBehaviour
{
    private class Baker : Baker<PlayerInputAuthoring>
    {
        public override void Bake(PlayerInputAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerInput>(entity);
        }
    }
}

public struct PlayerInput : IComponentData
{
    public float2 Mouse;
    public float2 Move;
    public float3 MoveTarget;
    public bool IsDownShift;
    public bool IsUpShift;
    public bool IsDownCTRL;
    public bool IsUpCTRL;
    public bool IsDownSpace;
    public bool IsDownE;

}