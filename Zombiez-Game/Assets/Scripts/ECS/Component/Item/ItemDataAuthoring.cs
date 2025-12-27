using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ItemDataAuthoring : MonoBehaviour
{
    public ItemType itemType;
    public Vector3 PosInHand;
    public Vector3 RotInHand;

    class ItemDataAuthoringBaker : Baker<ItemDataAuthoring>
    {
        public override void Bake(ItemDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new ItemData()
            {
                itemType = authoring.itemType,
                PosInHand = authoring.PosInHand,
                RotInHand = authoring.RotInHand,
            });
        }
    }

}

public struct ItemData : IComponentData
{
    public ItemType itemType;
    public float3 PosInHand;
    public float3 RotInHand;

    public bool IsMove;
    public bool IsAnim;
    public bool IsInHand;

    public float Progress;
}

public enum ItemType
{
    Axe
}
