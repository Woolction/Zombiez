using System.Collections.Generic;
using System;
using Unity.Entities;

public interface IService {}
public interface IInit { public void OnInit(); }
public interface IStart { public void OnStart(); }
public interface IUpdate { public void OnUpdate(); }
public interface ILateUpdate { public void OnLateUpdate(); }
public interface IDestryable { public void OnDestroy(); }

public class ServiceLocator
{
    public static ServiceLocator Instance = new();

    public EntityManager entityManager;

    public Dictionary<string, IService> _services = new();
    
    public event Action Initer;
    public event Action Starter;
    public event Action Updater;
    public event Action LateUpdater;
    public event Action Destroyator;

    public void ServiceRegistring<T>(T service) where T : IService
    {
        string key = service.GetType().FullName;

        if (_services.ContainsKey(key))
            return;

        _services.Add(key, service);
    }

    public void ServiceUpdater()
    {
        foreach (IService service in _services.Values)
        {
            if (service is IInit initiliazeable)
                Initer += initiliazeable.OnInit;
            if (service is IStart startable)
                Starter += startable.OnStart;
            if (service is IUpdate updateable)
                Updater += updateable.OnUpdate;
            if (service is ILateUpdate lateUpdateable)
                LateUpdater += lateUpdateable.OnLateUpdate;
            if (service is IDestryable disactable)
                Destroyator += disactable.OnDestroy;
        }
    }

    public T Get<T>() where T : IService
    {
        string key = typeof(T).FullName;

        if (!_services.ContainsKey(key))
        {
            return default;
        }

        return (T)_services[key];
    }

    public void Awake() => Initer?.Invoke();
    public void Start() => Starter?.Invoke();
    public void Update() => Updater?.Invoke();
    public void LateUpdate() => LateUpdater?.Invoke();
    public void Destroy()
    {
        Destroyator?.Invoke();


        /*Instance = null;
        _services = null;

        Initer = null;
        Starter = null;
        Updater = null;
        LateUpdater = null;
        Destroyator = null;*/
    }

}