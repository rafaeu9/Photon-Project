using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cubestate
{
    Grabed,
    Lanched,
    grounded
}

public class CubesLogic : MonoBehaviourPunCallbacks, IPunObservable
{



    public Cubestate cubestate = Cubestate.grounded;

    private PhotonView PhotonView;
    private PhotonTransformView PhotonTransformView;
    private Rigidbody Rigidbody;

    private string ParentName = "";

    // Start is called before the first frame update
    void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        PhotonTransformView = GetComponent<PhotonTransformView>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (cubestate)
        {
            case Cubestate.Grabed:

                //ParentName = transform.parent.name;

                tag = "Untagged";
                //PhotonView.enabled = true;
                PhotonTransformView.enabled = true;

                break;
            case Cubestate.Lanched:

                if (Rigidbody.velocity == Vector3.zero)
                {
                    //PhotonView.enabled = false;
                    //PhotonTransformView.enabled = false;

                    cubestate = Cubestate.grounded;

                }

                break;
            case Cubestate.grounded:
                tag = "throwable";

                if (transform.parent)
                    transform.parent = null;

                break;
            default:
                break;
        }
    }

    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(cubestate);
            stream.SendNext(ParentName);

            
        }
        else
        {
            // Network player, receive data
            this.cubestate = (Cubestate)stream.ReceiveNext();

            this.ParentName = (string)stream.ReceiveNext();

            if (cubestate == Cubestate.Grabed && transform.parent)
            {
                GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");

                foreach (var item in temp)
                {
                    Debug.Log(temp);
                    if(item.GetComponent<PhotonView>().Owner == photonView.Owner)
                    {
                        transform.parent = item.transform.GetChild(0).GetChild(0);
                    }
                }               

            }
        }
    }


    #endregion
}
