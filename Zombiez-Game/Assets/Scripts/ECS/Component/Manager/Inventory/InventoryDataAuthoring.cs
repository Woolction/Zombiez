
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class InventoryDataAuthoring : MonoBehaviour
{
    private class Baker : Baker<InventoryDataAuthoring>
    {
        public override void Bake(InventoryDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<InventoryData>(entity);
        }
    }
}

public struct InventoryData : IComponentData
{
    public int AppleCount;
    public int ActorCount;
    public int BoardCount;
    public int StoneCount;
}