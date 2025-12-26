using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerInputCollector : IService, IInit, IUpdate
{
    private EntityManager entityManager;
    private EntityQuery entityQuery;

    public void OnInit()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        entityQuery = entityManager.CreateEntityQuery(
            ComponentType.ReadOnly<LocalPlayerTag>()
            );
    }

    public void OnUpdate()
    {
        if (entityQuery.IsEmpty)
            return;

        Entity player = entityQuery.GetSingletonEntity();

        MoveSpeed speed = entityManager.GetComponentData<MoveSpeed>(player);
        LocalTransform transform = entityManager.GetComponentData<LocalTransform>(player);
        PlayerInput playerInput = new(); //entityManager.GetComponentData<PlayerInput>(player);

        playerInput.IsDownE = Input.GetKeyDown(KeyCode.E);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerInput.Move.x = horizontal;
        playerInput.Move.y = vertical;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        playerInput.Mouse.x = mouseX;
        playerInput.Mouse.y = mouseY;

        playerInput.MoveTarget = transform.Forward() * playerInput.Move.y +
        transform.Right() * playerInput.Move.x;

        playerInput.MoveTarget *= speed.value;

        playerInput.IsDownShift = Input.GetKeyDown(KeyCode.LeftShift);
        playerInput.IsUpShift = Input.GetKeyUp(KeyCode.LeftShift);

        playerInput.IsDownCTRL = Input.GetKeyDown(KeyCode.LeftControl);
        playerInput.IsUpCTRL = Input.GetKeyUp(KeyCode.LeftControl);

        playerInput.IsDownSpace = Input.GetKeyDown(KeyCode.Space);

        entityManager.SetComponentData(player, playerInput);
    }
}