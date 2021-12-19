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

                tag = "Untagged";                

                break;

            case Cubestate.Lanched:

                if (Rigidbody.velocity == Vector3.zero)
                {                    
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

    private void OnCollisionEnter(Collision collision)
    {
        if(cubestate == Cubestate.Lanched && collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().Damage();
            cubestate = Cubestate.grounded;
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

                //Detect with player owns the cube being garbed
                foreach (var item in temp)
                {                    
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
