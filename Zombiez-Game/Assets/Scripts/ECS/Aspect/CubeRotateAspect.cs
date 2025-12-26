using Unity.Transforms;
using Unity.Entities;

public readonly partial struct CubeRotateAspect : IAspect
{
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<RotateSpeed> rotateSpeed;

    public void RotateAndMove(float deltaTime)
    {
        localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * deltaTime);
    }
}