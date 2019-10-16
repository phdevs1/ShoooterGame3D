﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tutoriales
{
    [System.Serializable]
    public class InputController 
    {
        public List<InputKey> keys = new List<InputKey>();
        private Dictionary<string, InputKey> mappedKeys = new Dictionary<string, InputKey>();
        bool initialized = false;

        private void Initialize()
        {
            if (initialized)
                return;
            foreach(InputKey key in keys)
            {
                if (!mappedKeys.ContainsKey(key.Name))
                {
                    mappedKeys.Add(key.Name, key);
                }
            }
            initialized = true;
        }
        public bool Check(string name)
        {
            if (!mappedKeys.ContainsKey(name))
                return false;
            return mappedKeys[name].Check();
        }
        public float CheckF(string name)
        {
            if (!mappedKeys.ContainsKey(name))
                return 0f;
            return mappedKeys[name].CheckF();
        }
        // Update is called once per frame
        public void Update()
        {
            Initialize();
            foreach (InputKey key in keys)
                key.Update();
        }
    }
    public enum InputType
    {
        down,
        up,
        release,
        pressed
    }
    [System.Serializable]
    public class InputKey
    {
        public string Name;
        public InputType _Type;
        public float Sensibility = 0.1f;

        private float act;
        private float prev;

        public bool Check()
        {
            switch (_Type)
            {
                case InputType.down: return act > Sensibility;
                case InputType.up: return act <= Sensibility;
                case InputType.release: return act < prev;
                case InputType.pressed: return act > prev;

                default: return false;
            }
        }
        public float CheckF()
        {
            return act;
        }
        public void Update()
        {
            prev = act;
            act = Input.GetAxis(Name);
        }
    }
}
