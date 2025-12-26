
using Unity.Entities;
using UnityEngine;

public class MouseSensivityAuthoring : MonoBehaviour
{
    public float Sensivity;
    private class Baker : Baker<MouseSensivityAuthoring>
    {
        public override void Bake(MouseSensivityAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new MouseSensivity()
            {
                value = authoring.Sensivity
            });
        }
    }
}

public struct MouseSensivity : IComponentData
{
    public float value;
}