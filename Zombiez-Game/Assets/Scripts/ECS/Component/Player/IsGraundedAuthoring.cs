
using Unity.Entities;
using UnityEngine;

public class IsGraundedAuthoring : MonoBehaviour
{
    private class Baker : Baker<IsGraundedAuthoring>
    {
        public override void Bake(IsGraundedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<IsGraunded>(entity);
        }
    }
}

public struct IsGraunded : IComponentData
{
    public bool IsGraund;
}