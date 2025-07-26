using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//
// abbiamo spiegato cos`è ANY STATE in un animator, settando quest`ultimo usando any state:
//  - creare un nuovo empty state
//  - renderlo default
//  - creare tutte le transitions che partono da any state ad ogni animazione (idle, walk, run etc.)
//  - non flaggare Has Exit Time
//  - settare Transition Duration a 0
//  - NB: NON fare transition da default creato a any state (NON ci entrerà mai!)
//
// (rivedere lezione M5-W2-D2 a circ min 30:00 per spiegazione Any State)
//
public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras;

    private int _defaultPriority = 0;
    private int _activePriority = 1;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(AnimationCameraSwitch());
    }

    IEnumerator AnimationCameraSwitch()
    {
        SetActiveCamera(0);
        _animator.SetTrigger("isWalking"); // <- settare il trigger corretto: una volta settato, il trigger RESTA ATTIVO FINCHÈ NON VERRÀ USATO (ecco per`chè NON metto isIdle ma isMoving)
        yield return null; // <- aggiungiamo un primo yield return null per evitare il bug per il quale entra per un frame nella defult state che abbiamo creato
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * .8f); // <- lo 0 tra parentesi indica l`indice dei layers dell`animator

        SetActiveCamera(1);
        _animator.SetTrigger("isRunning");
        yield return null;
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * .5f);

        SetActiveCamera(2);
        _animator.SetTrigger("isIdle");
        yield return null;
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * .25f);
    }

    private void SetActiveCamera(int index) // <- metodo per predisporre la camera attiva
    {
        for (int i = 0; i < _virtualCameras.Count; i++)
        {
            _virtualCameras[i].Priority = (i == index) ? _activePriority : _defaultPriority;
        }
    }
}
