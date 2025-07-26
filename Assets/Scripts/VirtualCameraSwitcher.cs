using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;


//
// inizialmente l`avevo concepito per un solo tasto, ma ho seguito l`intuizione di Riccardo per il secondo tasto =)
//

public class VirtualCameraSwitcher : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras;
    [SerializeField] private int _activeCameraIndex = 0;
    [SerializeField] private KeyCode _keyCodeForSwitchOn = KeyCode.D; // <- tasto da premere per switchare alla camera successiva
    [SerializeField] private KeyCode _keyCodeForSwitchBack = KeyCode.A; // <- tasto da premere per tornare alla camera precedente

    private int _defaultPriority = 10;
    private int _activePriority = 11;

    // Start is called before the first frame update
    void Start()
    {
        SetActiveCamera(_activeCameraIndex); // <- predispongo la camera attiva
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyCodeForSwitchOn)) // <- per passare alla camera successiva
        {
            _activeCameraIndex++;

            if (_activeCameraIndex >= _virtualCameras.Count) _activeCameraIndex = 0;

            SetActiveCamera(_activeCameraIndex);
        }

        if (Input.GetKeyDown(_keyCodeForSwitchBack)) // <- per tornare alla camera precedente
        {
            _activeCameraIndex--;

            if (_activeCameraIndex < 0) _activeCameraIndex = _virtualCameras.Count - 1; // <- ìn questo modo l`indice non sarà mai negativo

            SetActiveCamera(_activeCameraIndex);
        }
    }

    private void SetActiveCamera(int index) // <- metodo per predisporre la camera attiva
    {
        for (int i = 0; i < _virtualCameras.Count; i++)
        {
            _virtualCameras[i].Priority = (i == index) ? _activePriority : _defaultPriority;
        }
    }
}
