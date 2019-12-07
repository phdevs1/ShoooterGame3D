using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace tutoriales
{
    public class GameConfiguration : MonoBehaviour
    {
        static public GameConfiguration Conf { get; private set; }
        static public float EffectsLevel { get { return Conf.AudioMaster * Conf.AudioEffects; } }

        public float AudioMaster = 1f;
        public float AudioEffects = 1f;
        public float AudioMusic = 1f;
        public float AudioVoices = 1f;
        void Start()
        {
            Conf = this;
        }

       
    }

}
