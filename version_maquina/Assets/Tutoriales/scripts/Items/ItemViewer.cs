using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class ItemViewer : MonoBehaviour
    {
        private Transform mtransform;
        public Vector3 viewerOffset;

        public void DrawItemViewer(Transform itemPos, Transform camera)
        {
            mtransform.position = itemPos.position + viewerOffset;
            mtransform.LookAt(camera);
            ShowViewer();
        }

        public bool showingViewer = false;
        private void ShowViewer()
        {
            if (!showingViewer)
            {
                showingViewer = true;
                gameObject.SetActive(true);
            }
        }

        private void HideViewer()
        {
            if (!showingViewer)
            {
                showingViewer = false;
                gameObject.SetActive(false);
            }
        }
    }
}
