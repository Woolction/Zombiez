using Unity.Entities;
using UnityEngine;

public class ServiceUpdater : MonoBehaviour
{
    [SerializeField] private CameraRotating cameraRotating = new();
    [SerializeField] private ViewResourceCount viewResourceCount = new();
    private PlayerInputCollector playerInputCollector = new();
    void Awake()
    {
        ServicesRegistry();

        ServiceLocator.Instance.ServiceUpdater();
        ServiceLocator.Instance.Awake();
    }

    void Start()
    {
        ServiceLocator.Instance.Start();
    }

    void Update()
    {
        ServiceLocator.Instance.Update();
    }

    void LateUpdate()
    {
        ServiceLocator.Instance.LateUpdate();
    }

    void OnDestroy()
    {
        ServiceLocator.Instance?.Destroy();
    }

    void ServicesRegistry()
    {
        ServiceLocator.Instance.entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        ServiceLocator.Instance.ServiceRegistring(cameraRotating);
        ServiceLocator.Instance.ServiceRegistring(viewResourceCount);
        ServiceLocator.Instance.ServiceRegistring(playerInputCollector);
    }
}