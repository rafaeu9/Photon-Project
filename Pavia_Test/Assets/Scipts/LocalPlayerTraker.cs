using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using System.Collections;

using Cinemachine;

public class LocalPlayerTraker : MonoBehaviour
{

    public GameObject Target;

    // Start is called before the first frame update
    void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = Target.transform;
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

}
