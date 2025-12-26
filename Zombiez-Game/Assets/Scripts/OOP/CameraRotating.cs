using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[Serializable]
public class CameraRotating : IService, IInit, ILateUpdate
{
    public Transform CameraTransform;

    private EntityQuery playerQuery;

    private Entity player;

    public void OnInit()
    {
        playerQuery = ServiceLocator.Instance.entityManager.CreateEntityQuery(
            ComponentType.ReadOnly<LocalPlayerTag>()
            );
    }

    public void OnLateUpdate()
    {
        player = playerQuery.GetSingletonEntity();

        PlayerLook playerLook = ServiceLocator.Instance.entityManager.GetComponentData<PlayerLook>(player);
        BodyParts bodyParts = ServiceLocator.Instance.entityManager.GetComponentData<BodyParts>(player);

        Quaternion targetRot = Quaternion.Euler(playerLook.Look.y, playerLook.Look.x, 0f);

        Vector3 targetPos = ServiceLocator.Instance.entityManager.GetComponentData<LocalToWorld>(bodyParts.Eyes).Position;

        
        CameraTransform.SetPositionAndRotation(targetPos, targetRot);
    }
}