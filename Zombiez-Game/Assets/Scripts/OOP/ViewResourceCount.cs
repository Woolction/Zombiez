using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Entities;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class ViewResourceCount : IService, IStart, IDestryable
{
    [SerializeField] private TextMeshProUGUI AppleText;
    [SerializeField] private TextMeshProUGUI ActorText;
    [SerializeField] private TextMeshProUGUI BoardText;
    [SerializeField] private TextMeshProUGUI StoneText;

    private EntityQuery query;
    private Entity player;

    private CancellationTokenSource cts;

    public void OnStart()
    {
        query = ServiceLocator.Instance.entityManager.CreateEntityQuery(
            ComponentType.ReadOnly<LocalPlayerTag>()
            );
        cts = new();

        if (!query.IsEmpty)
        {
            player = query.GetSingletonEntity();

            _ = CheckCount();
        }
    }

    public void OnDestroy()
    {
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }
    
    private async UniTaskVoid CheckCount()
    {
        while (this != null)
        {
            InventoryData inventoryData = ServiceLocator.Instance.entityManager.GetComponentData<InventoryData>(player);

            AppleText.text = $"Apple: {inventoryData.AppleCount}";
            ActorText.text = $"Actor: {inventoryData.ActorCount}";
            BoardText.text = $"Board: {inventoryData.BoardCount}";
            StoneText.text = $"Stone: {inventoryData.StoneCount}";

            await UniTask.Delay(400, cancellationToken: cts.Token);
        }
        
    }

}
