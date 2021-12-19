using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using System.Collections;

using Cinemachine;
using TMPro;

public class LocalPlayerTraker : MonoBehaviour
{

    public GameObject Target;
    public TMP_Text Name;

    private PhotonView localPhoton;

    // Start is called before the first frame update
    void Awake()
    {

        localPhoton = GetComponent<PhotonView>();

        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (localPhoton.IsMine)
        {
            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = Target.transform;

            Name.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Name.text = localPhoton.Owner.NickName;
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

}
