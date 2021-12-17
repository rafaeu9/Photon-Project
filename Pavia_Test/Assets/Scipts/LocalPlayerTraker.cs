using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using System.Collections;

using StarterAssets;

public class LocalPlayerTraker : MonoBehaviour
{
    
    

    // Start is called before the first frame update
    void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (GetComponent<PhotonView>().IsMine)
        {
            GameObject Target = GameObject.Find("PlayerCameraRoot");
            Target.name = "PlayerCameraRoot{0}";

            Target.transform.SetParent(gameObject.transform);

            GetComponent<ThirdPersonController>().CinemachineCameraTarget = Target;
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

}
