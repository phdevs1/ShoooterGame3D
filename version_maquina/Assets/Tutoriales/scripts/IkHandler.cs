﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class IkHandler : MonoBehaviour
    {
        public Transform LookAtPosition;

        public Transform LeftHandPosition;
        public Transform LeftElbowPosition;
        public Transform RightHandPosition;
        public Transform RightElbowPosition;

        public float BodyWeight = 0.5f;

        public float TimeMod = .02f;
        public Vector3 RecoilTarget;
        public float t;

        public bool Aiming;
        public bool Shooting;

        Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            t += Time.deltaTime * TimeMod;
        }

        public void UpdateRecoil(float MaxRecoil, float ShootingModifier, float LeteralMod)
        {
            float r = Mathf.Cos(t) * MaxRecoil;
            RecoilTarget = new Vector3(0, r*(!Shooting ? ShootingModifier : 1f));
        }

        public void OnAnimatorIK(int layerIndex)
        {
            float aim = (Aiming ? 1f : 0);

            anim.SetLayerWeight(anim.GetLayerIndex("UpperBody"), aim);
            anim.SetLookAtWeight(1, BodyWeight, 1, 1, (Aiming ? 0 : 1)); anim.SetLookAtPosition(LookAtPosition.position);

            SetIk(AvatarIKGoal.RightHand, RightHandPosition, AvatarIKHint.RightElbow, RightElbowPosition, aim, RecoilTarget);
            if (LeftHandPosition != null)
            {
                SetIk(AvatarIKGoal.LeftHand, LeftHandPosition, AvatarIKHint.LeftElbow, LeftElbowPosition, aim, Vector3.zero);
            }
        }

        private void SetIk(AvatarIKGoal goal, Transform target, AvatarIKHint hint, Transform restraint, float weight, Vector3 recoil)
        {
            anim.SetIKHintPositionWeight(hint, weight);
            anim.SetIKPositionWeight(goal, weight);
            anim.SetIKRotationWeight(goal, weight);
            anim.SetIKPosition(goal, target.position + recoil);
            anim.SetIKRotation(goal, target.rotation);
            anim.SetIKHintPosition(hint, restraint.position);
        }
    }
}
