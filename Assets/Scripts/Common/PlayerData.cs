using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData :Singleton<PlayerData> {
    public int userId;
    public List<PlayerEntity> emptyPlayers;
    public PlayerEntity selectedPlayerEntity;
    public PlayerEntity playerEntity;
	void Start () {
        
    }

    private void SaveGameCallback(RemoteResult<PlayerEntity> result)
    {
        if (result.data != null)
            Debug.Log(result.data.playerId);
    }

    public void ClearInfo()
    {
        playerEntity = null;
    }
}
