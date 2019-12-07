using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tutoriales
{
    public class AutoLobby : MonoBehaviourPunCallbacks
    {
        public Button ConnectButton;
        public Button Join;
        public Text Log;
        public Text PlayerCount;
        public int playersCount;

        public byte maxPlayer = 4;
        public byte minPlayer = 1;
        private bool IsLoading = false;
        public string roomName = "ShooterG";

        public void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.ConnectUsingSettings())
                {
                    Log.text += "\nConnect to server";
                }
                else
                {
                    Log.text += "\nFailing connected to server";
                }
            }
        }
        public override void OnConnectedToMaster()
        {
            ConnectButton.interactable = false;
            Join.interactable = true;
        }
        public void JoinRandom()
        {
            if (!PhotonNetwork.JoinRoom(roomName))
            {
                Log.text += "\nFail joining room";
            }
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {   
            Log.text += "\nNo rooms to join, creating one....";
            if (PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions() { MaxPlayers = maxPlayer }, null))
            {
                Log.text += "\nRoom created";
            }
            else
            {
                Log.text += "\nFail created to room";
            }
        }
        /* public override void OnJoinRandomFailed(short returnCode, string message)
         {
             Log.text += "\nNo rooms to join, creating one....";
             if (PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions() {MaxPlayers = maxPlayer }))
             {
                 Log.text += "\nRoom created";
             }
             else
             {
                 Log.text += "\nFail created to room";
             }
         }*/
        public override void OnJoinedRoom()
        {
            Log.text += "\nJoinned";
            Join.interactable = false;

        }

        private void FixedUpdate()
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
            }

            PlayerCount.text = playersCount + "/" + maxPlayer;

            /*if (!IsLoading && playersCount >= minPlayersPerRoom)
            {
                LoadMap();
            }*/
        }

        /*private void LoadMap()
        {
            IsLoading = true;

            PhotonNetwork.LoadLevel("isla_demo_4");
        }*/

    }

}