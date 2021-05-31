﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTypeNode : MonoBehaviour
{
    public GameObject nodeToDisable;
    public GameObject viveNode;
    public GameObject oculusNode;

    public void SelectNode()
    {
        GameObject nodeToEnable = null;
        DetectVR.VRController controller = DetectVR.GetControllerTypeToEnum();
        switch (controller)
        {
            case DetectVR.VRController.vive_controller:
                nodeToEnable = this.viveNode;
                break;
            case DetectVR.VRController.oculus_touch:
                nodeToEnable = this.oculusNode;
                break;
        }

        if (nodeToEnable == null)
            return;
        
        nodeToEnable.SetActive(true);
        this.nodeToDisable.SetActive(false);
    }
}