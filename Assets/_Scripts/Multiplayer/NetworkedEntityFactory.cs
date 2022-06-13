using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Colyseus;
using GameDevWare.Serialization;
using LucidSightTools;

/// <summary>
/// Responsible for carrying out the creation of network entities and registering them with the Example Manager.
/// </summary>
public class NetworkedEntityFactory
{
    private readonly Dictionary<string, Action<NetworkedEntity>> _creationCallbacks;
    // TODO: replace GameDevWare stuff
    private readonly IndexedDictionary<string, NetworkedEntity> _entities;
    private readonly IndexedDictionary<string, NetworkedEntityView> _entityViews;

    public NetworkedEntityFactory(Dictionary<string, Action<NetworkedEntity>> creationCallbacks, IndexedDictionary<string, NetworkedEntity> entities, IndexedDictionary<string, NetworkedEntityView> entityViews)
    {
        _creationCallbacks = creationCallbacks;
        _entities = entities;
        _entityViews = entityViews;
    }

    /// <summary>
    /// Creates a new <see cref="ExampleNetworkedEntity"/> with the given prefab and attributes
    /// and places it at the provided position and rotation.
    /// </summary>
    /// <param name="room">The room the entity will be added to</param>
    /// <param name="prefab">Prefab you would like to use to create the entity</param>
    /// <param name="position">Position for the new entity</param>
    /// <param name="rotation">Position for the new entity</param>
    /// <param name="attributes">Position for the new entity</param>
    public void InstantiateNetworkedEntity(ColyseusRoom<RoomState> room, string prefab, Vector3 position, Quaternion rotation,
        Dictionary<string, object> attributes = null)
    {
        if (string.IsNullOrEmpty(prefab))
        {
            LSLog.LogError("No Prefab Declared");
            return;
        }

        if (attributes != null)
        {
            attributes.Add("creationPos", new object[3] { position.x, position.y, position.z });
            attributes.Add("creationRot", new object[4] { rotation.x, rotation.y, rotation.z, rotation.w });
        }
        else
        {
            attributes = new Dictionary<string, object>()
            {
                ["creationPos"] = new object[3] { position.x, position.y, position.z },
                ["creationRot"] = new object[4] { rotation.x, rotation.y, rotation.z, rotation.w }
            };
        }

        CreateNetworkedEntity(room, prefab, attributes);
    }

    /// <summary>
    /// Creates a new <see cref="ExampleNetworkedEntity"/> with the given prefab, attributes, and <see cref="ColyseusNetworkedEntityView"/>.
    /// </summary>
    /// <param name="room">The room the entity will be added to</param>
    /// <param name="prefab">Prefab you would like to use</param>
    /// <param name="attributes">Position for the new entity</param>
    /// <param name="viewToAssign">The provided view that will be assigned to the new <see cref="ExampleNetworkedEntity"/></param>
    /// <param name="callback">Callback that will be invoked with the newly created <see cref="ExampleNetworkedEntity"/></param>
    public void CreateNetworkedEntity(ColyseusRoom<RoomState> room, string prefab, Dictionary<string, object> attributes = null, ColyseusNetworkedEntityView viewToAssign = null, Action<NetworkedEntity> callback = null)
    {
        Dictionary<string, object> updatedAttributes = (attributes != null)
            ? new Dictionary<string, object>(attributes)
            : new Dictionary<string, object>();
        updatedAttributes.Add("prefab", prefab);
        CreateNetworkedEntity(room, updatedAttributes, viewToAssign, callback);
    }

    /// <summary>
    /// Creates a new <see cref="ExampleNetworkedEntity"/> attributes and <see cref="ColyseusNetworkedEntityView"/>.
    /// </summary>
    /// <param name="room">The room the entity will be added to</param>
    /// <param name="attributes">Position for the new entity</param>
    /// <param name="viewToAssign">The provided view that will be assigned to the new <see cref="ExampleNetworkedEntity"/></param>
    /// <param name="callback">Callback that will be invoked with the newly created <see cref="ExampleNetworkedEntity"/></param>
    public void CreateNetworkedEntity(ColyseusRoom<RoomState> room, Dictionary<string, object> attributes = null, ColyseusNetworkedEntityView viewToAssign = null, Action<NetworkedEntity> callback = null)
    {
        try
        {
            string creationId = null;

            if (viewToAssign != null || callback != null)
            {
                creationId = Guid.NewGuid().ToString();
                if (callback != null)
                {
                    if (viewToAssign != null)
                    {
                        _creationCallbacks.Add(creationId, (newEntity) =>
                        {
                            RegisterNetworkedEntityView(newEntity, viewToAssign);
                            callback.Invoke(newEntity);
                        });
                    }
                    else
                    {
                        _creationCallbacks.Add(creationId, callback);
                    }
                }
                else
                {
                    _creationCallbacks.Add(creationId,
                        (newEntity) => { RegisterNetworkedEntityView(newEntity, viewToAssign); });
                }
            }

            // send "createEntity" message for the server.
            _ = room.Send("createEntity", new { creationId = creationId, attributes = attributes });
        }
        catch (System.Exception err)
        {
            LSLog.LogError(err.Message + err.StackTrace);
        }

    }

    /// <summary>
    /// Creates a new <see cref="ExampleNetworkedEntity"/> with the given <see cref="ColyseusNetworkedEntityView"/> and attributes
    /// and places it at the provided position and rotation.
    /// </summary>
    /// <param name="room">The room the entity will be added to</param>
    /// <param name="position">Position for the new entity</param>
    /// <param name="rotation">Position for the new entity</param>
    /// <param name="attributes">Position for the new entity</param>
    /// <param name="viewToAssign">The provided view that will be assigned to the new <see cref="ExampleNetworkedEntity"/></param>
    /// <param name="callback">Callback that will be invoked with the newly created <see cref="ExampleNetworkedEntity"/></param>
    public void CreateNetworkedEntityWithTransform(ColyseusRoom<RoomState> room, Vector3 position, Quaternion rotation,
        Dictionary<string, object> attributes = null, ColyseusNetworkedEntityView viewToAssign = null,
        Action<NetworkedEntity> callback = null)
    {
        if (attributes != null)
        {
            attributes.Add("creationPos", new object[3] { position.x, position.y, position.z });
            attributes.Add("creationRot", new object[4] { rotation.x, rotation.y, rotation.z, rotation.w });
        }
        else
        {
            attributes = new Dictionary<string, object>()
            {
                ["creationPos"] = new object[3] { position.x, position.y, position.z },
                ["creationRot"] = new object[4] { rotation.x, rotation.y, rotation.z, rotation.w }
            };
        }

        CreateNetworkedEntity(room, attributes, viewToAssign, callback);
    }

    /// <summary>
    /// Creates a GameObject using the <see cref="ExampleNetworkedEntityView"/>'s prefab.
    /// <para>Requires that the entity has a "prefab" attribute defined.</para>
    /// </summary>
    /// <param name="entity"></param>
    public async Task CreateFromPrefab(NetworkedEntity entity)
    {
        LSLog.LogError($"Factory - Create From Prefab - {entity.id}");

        ResourceRequest asyncItem = Resources.LoadAsync<NetworkedEntityView>(entity.attributes["prefab"]);
        while (asyncItem.isDone == false)
        {
            await Task.Yield();
        }

        NetworkedEntityView view = UnityEngine.Object.Instantiate((NetworkedEntityView)asyncItem.asset);
        if (view == null)
        {
            LSLog.LogError("Instantiated Object is not of VMENetworkedEntityView Type");
            asyncItem = null;
            return;
        }

        RegisterNetworkedEntityView(entity, view);
    }

    /// <summary>
    /// Registers the <see cref="ExampleNetworkedEntityView"/> with the manager for tracking.
    /// <para>Initializes the <see cref="ExampleNetworkedEntityView"/> if it has not yet been initialized.</para>
    /// </summary>
    /// <param name="model"></param>
    /// <param name="view"></param>
    public void RegisterNetworkedEntityView(NetworkedEntity model, ColyseusNetworkedEntityView view)
    {
        if (string.IsNullOrEmpty(model.id) || view == null || _entities.ContainsKey(model.id) == false)
        {
            LSLog.LogError("Cannot Find Entity in Room");
            return;
        }

        NetworkedEntityView entityView = (NetworkedEntityView) view;

        if (entityView && !entityView.HasInit)
        {
            entityView.InitiView(model);
        }

        _entityViews.Add(model.id, (NetworkedEntityView)view);
        view.SendMessage("OnEntityViewRegistered", SendMessageOptions.DontRequireReceiver);
    }

    public void UnregisterNetworkedEntityView(NetworkedEntity model)
    {
        if (string.IsNullOrEmpty(model.id) || _entities.ContainsKey(model.id) == false)
        {
            LSLog.LogError("Cannot Find Entity in Room");
            return;
        }

        NetworkedEntityView view = _entityViews[model.id];

        _entityViews.Remove(model.id);
        view.SendMessage("OnEntityViewUnregistered", SendMessageOptions.DontRequireReceiver);
    }
}
