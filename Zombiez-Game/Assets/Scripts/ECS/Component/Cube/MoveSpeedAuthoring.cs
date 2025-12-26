
using Unity.Entities;
using UnityEngine;

public class MoveSpeedAuthoring : MonoBehaviour
{
    public float speed;

    private class Baker : Baker<MoveSpeedAuthoring>
    {
        public override void Bake(MoveSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new MoveSpeed()
            {
                value = authoring.speed
            });
        }
    }
}

public struct MoveSpeed : IComponentData
{
    public float value;
}