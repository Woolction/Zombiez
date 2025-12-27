
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/*public partial struct MoveHandSystem : ISystem
{
    private float progress;
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRO<BodyParts> bodyParts in SystemAPI.Query<RefRO<BodyParts>>().WithAll<LocalPlayerTag>())
        {
            RefRO<CheckNazar> checkNazar = SystemAPI.GetComponentRO<CheckNazar>(bodyParts.ValueRO.CheckNazar);

            if (checkNazar.ValueRO.IsMove)
            {
                RefRW<LocalTransform> nazarTransform = SystemAPI.GetComponentRW<LocalTransform>(bodyParts.ValueRO.CheckNazar);

                RefRW<LocalTransform> leftHand = SystemAPI.GetComponentRW<LocalTransform>(checkNazar.ValueRO.LeftHand);
                RefRW<LocalTransform> rightHand = SystemAPI.GetComponentRW<LocalTransform>(checkNazar.ValueRO.RightHand);

                float t = math.saturate((progress += SystemAPI.Time.DeltaTime) * 4f);

                if (checkNazar.ValueRO.IsForward)
                {
                    nazarTransform.ValueRW.Position = checkNazar.ValueRO.ItemPos;

                    leftHand.ValueRW.Position = math.lerp(leftHand.ValueRO.Position, checkNazar.ValueRO.LeftHandPos, t);
                    rightHand.ValueRW.Position = math.lerp(rightHand.ValueRO.Position, checkNazar.ValueRO.RightHandPos, t);
                }
                else
                {
                    nazarTransform.ValueRW.Position = checkNazar.ValueRO.ForwardPos;

                    leftHand.ValueRW.Position = math.lerp(leftHand.ValueRO.Position, checkNazar.ValueRO.LeftHandBasePos, t);
                    rightHand.ValueRW.Position = math.lerp(rightHand.ValueRO.Position, checkNazar.ValueRO.RightHandBasePos, t);
                }

                if (t >= 1)
                {
                    SystemAPI.GetComponentRW<CheckNazar>(bodyParts.ValueRO.CheckNazar).ValueRW.IsMove = false;
                }
            }
        }
    }
}*/