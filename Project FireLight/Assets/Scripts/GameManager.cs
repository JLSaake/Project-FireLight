using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Player player;
    public Camera overheadCamera; // TODO: get main camera via script and tags

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOverheadCamera();
    }

    // Update the position of the overhead camera
    void UpdateOverheadCamera() {
        Vector3 playerPos = player.GetPlayerPosition();
        overheadCamera.transform.position = new Vector3(playerPos.x, 10, playerPos.z);
        //TODO: work in edge of room cases to keep gameplay on screen
    }
}
