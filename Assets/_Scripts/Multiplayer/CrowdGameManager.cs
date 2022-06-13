using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using LucidSightTools;

public class CrowdGameManager : MonoBehaviour
{
    public ColyseusNetworkedEntityView prefab;

    private void OnEnable()
    {
        CrowdRoomController.onAddNetworkEntity += OnNetworkAdd;
        CrowdRoomController.onRemoveNetworkEntity += OnNetworkRemove;
    }

    private void OnNetworkAdd(NetworkedEntity entity)
    {
        if (MultiPlayerGameManager.Instance.HasEntityView(entity.id))
        {
            LSLog.LogImportant("View found! For " + entity.id);
        }
        else
        {
            LSLog.LogImportant("No View found for " + entity.id);
            CreateView(entity);
        }
    }

    private void OnNetworkRemove(NetworkedEntity entity, ColyseusNetworkedEntityView view)
    {
        RemoveView(view);
    }

    private void CreateView(NetworkedEntity entity)
    {
        LSLog.LogImportant("print: " + JsonUtility.ToJson(entity));
        ColyseusNetworkedEntityView newView = Instantiate(prefab);
        MultiPlayerGameManager.Instance.RegisterNetworkedEntityView(entity, newView);
        newView.gameObject.SetActive(true);
    }

    private void RemoveView(ColyseusNetworkedEntityView view)
    {
        view.SendMessage("OnEntityRemoved", SendMessageOptions.DontRequireReceiver);
    }
}
