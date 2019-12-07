using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace tutoriales
{
    public class ItemViewer : MonoBehaviour
    {
        private Transform mTransform;
        public Vector3 viewerOffset;
        public TextMeshProUGUI Text;

        public Image Icon;

        private void Start()
        {
            mTransform = this.transform;
        }
        public void DrawItemViewer(ItemStats stats, Transform itemPos, Transform camera)
        {
            mTransform.position = itemPos.position + viewerOffset;
            mTransform.LookAt(camera);
            Text.text = stats.ItemName;
            Icon.sprite = stats.Icon;
            ShowViewer();
        }
        private bool showingViewer = false;
        public void ShowViewer()
        {
            if (!showingViewer)
            {
                showingViewer = true;
                gameObject.SetActive(true);
            }
        }
        public void HideViewer()
        {
            if (showingViewer)
            {
                showingViewer = false;
                gameObject.SetActive(false);
            }
        }
    }
}

