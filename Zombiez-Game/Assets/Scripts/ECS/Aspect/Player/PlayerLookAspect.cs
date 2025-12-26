using Unity.Entities;
using Unity.Mathematics;

public readonly partial struct PlayerLookAspect : IAspect
{
    public readonly RefRW<PlayerLook> playerLook;
    public readonly RefRO<PlayerInput> playerInput;
    public readonly RefRO<MouseSensivity> MouseSensevity;

    public void UpdateLook()
    {
        float2 mouse = playerInput.ValueRO.Mouse * MouseSensevity.ValueRO.value;

        playerLook.ValueRW.Look.x += mouse.x;
        playerLook.ValueRW.Look.y -= mouse.y;

        playerLook.ValueRW.Look.y = math.clamp(playerLook.ValueRW.Look.y, -70f, 70f);
    }
}