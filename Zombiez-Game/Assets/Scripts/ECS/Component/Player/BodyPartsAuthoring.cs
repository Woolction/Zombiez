
using Unity.Entities;
using UnityEngine;

public class BodyPartsAuthoring : MonoBehaviour
{
    public GameObject Eyes;
    public GameObject Head;
    public GameObject Hands;
    public GameObject CheckGround;
    public GameObject CheckNazar;

    private class Baker : Baker<BodyPartsAuthoring>
    {
        public override void Bake(BodyPartsAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BodyParts()
            {
                Eyes = GetEntity(authoring.Eyes, TransformUsageFlags.Dynamic),
                Head = GetEntity(authoring.Head, TransformUsageFlags.Dynamic),
                Hands = GetEntity(authoring.Hands, TransformUsageFlags.Dynamic),
                CheckGround = GetEntity(authoring.CheckGround, TransformUsageFlags.Dynamic),
                CheckNazar = GetEntity(authoring.CheckNazar, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct BodyParts : IComponentData
{
    public Entity Eyes;
    public Entity Head;
    public Entity Hands;
    public Entity CheckGround;
    public Entity CheckNazar;
}