using Unity.Burst;
using Unity.Entities;

public partial struct RotateSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        RotateSystemJob rotateSystemJob = new()
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };

        state.Dependency = rotateSystemJob.ScheduleParallel(state.Dependency);

        /*foreach ((CubeRotateAspect cubeRotateAspect, Entity entity)
        in SystemAPI.Query<CubeRotateAspect>().WithAll<Cube>().WithDisabled<Stunned>().WithEntityAccess())
        {
            cubeRotateAspect.RotateAndMove(SystemAPI.Time.DeltaTime);
            //cubeRotateAspect.localTransform.ValueRW = cubeRotateAspect.localTransform.ValueRO.RotateY(cubeRotateAspect.rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }*/
    }

    [BurstCompile]
    [WithAll(typeof(Cube))]
    public partial struct RotateSystemJob : IJobEntity
    {
        public float deltaTime;
        public void Execute(CubeRotateAspect cubeRotateAspect)
        {
            cubeRotateAspect.RotateAndMove(deltaTime);
        }
    }
}