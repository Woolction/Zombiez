using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public readonly partial struct PlayerMovementAspect : IAspect
{
    public readonly RefRW<PhysicsVelocity> physicsVelocity;
    public readonly RefRW<IsGraunded> graunded;
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRW<MoveSpeed> Speed;
    public readonly RefRO<PlayerInput> input;


    public void CheckGround(PhysicsWorld physicsWorld, RefRO<PhysicsCollider> collider)
    {
        unsafe
        {
            ColliderCastInput colliderCastInput = new()
            {
                Collider = collider.ValueRO.ColliderPtr,
                Start = transform.ValueRO.Position + math.up() * 0.05f,
                End = transform.ValueRO.Position + -math.up() * 0.25f,
                Orientation = quaternion.identity
            };

            if (physicsWorld.CastCollider(colliderCastInput, out ColliderCastHit closestHit))
            {
                graunded.ValueRW.IsGraund = true;
            }
            else
            {
                graunded.ValueRW.IsGraund = false;
            }
        }
    }

    public void Move()
    {
        float3 velocity = physicsVelocity.ValueRW.Linear;
        velocity.x = input.ValueRO.MoveTarget.x;
        velocity.z = input.ValueRO.MoveTarget.z;

        CheckInput(ref velocity);

        physicsVelocity.ValueRW.Linear = velocity;
        physicsVelocity.ValueRW.Angular = 0;
    }

    private void CheckInput(ref float3 velocity)
    {
        if (input.ValueRO.IsDownShift)
        {
            Speed.ValueRW.value *= 1.5f;
        }
        if (input.ValueRO.IsUpShift)
        {
            Speed.ValueRW.value /= 1.5f;
        }
        if (input.ValueRO.IsDownSpace && graunded.ValueRO.IsGraund)
        {
            velocity.y = 7;
        }
        else
        {
            if (input.ValueRO.IsDownCTRL)
            {

            }
            if (input.ValueRO.IsUpCTRL)
            {

            }
        }

    }
}