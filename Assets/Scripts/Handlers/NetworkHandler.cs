using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class NetworkHandler : MonoBehaviour
{
    private GameServerMock _gameServer = new GameServerMock();
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public bool ResponseUpToDate = false;

    public string ServerResponse = "";

    public void UpdateItemList()
    {
        var result = _gameServer.GetItemsAsync(_cancellationTokenSource.Token).ContinueWith(x => _onUpdateComplete(x.Result));
    }

    private void _onUpdateComplete(string result)
    {
        ServerResponse = result;
        ResponseUpToDate = true;
    }

    private void _onItemsUpdated()
    {
        ResponseUpToDate = true;
    }
}
