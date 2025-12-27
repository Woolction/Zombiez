
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CheckNazarAuthoring : MonoBehaviour
{
    public Vector3 ForwardPos;
    public Vector3 ItemPos;

    public GameObject LeftHand;
    public Vector3 LeftHandBasePos;
    public Vector3 LeftHandPos;

    public GameObject RightHand;
    public Vector3 RightHandBasePos;
    public Vector3 RightHandPos;

    private class Baker : Baker<CheckNazarAuthoring>
    {
        public override void Bake(CheckNazarAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new CheckNazar()
            {
                IsForward = false,
                ForwardPos = authoring.ForwardPos,
                ItemPos = authoring.ItemPos,
                LeftHand = GetEntity(authoring.LeftHand, TransformUsageFlags.Dynamic),
                LeftHandBasePos = authoring.LeftHandBasePos,
                LeftHandPos = authoring.LeftHandPos,
                RightHand = GetEntity(authoring.RightHand, TransformUsageFlags.Dynamic),
                RightHandBasePos = authoring.RightHandBasePos,
                RightHandPos = authoring.RightHandPos,
            });
        }
    }
}

public struct CheckNazar : IComponentData
{
    public bool IsMove;
    public bool IsForward;
    public float3 ForwardPos;
    public float3 ItemPos;

    public Entity LeftHand;
    public float3 LeftHandBasePos;
    public float3 LeftHandPos;

    public Entity RightHand;
    public float3 RightHandBasePos; 
    public float3 RightHandPos;
}