using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NoteButtonsController : MonoBehaviour
{
    enum Note
    {
        OTHERS,
        D5,
        C5,
        A4,
        G4,
        F4,
        D4,
    };

    private Queue<Note> clickNotes;
    private List<Note> rightNotes = new List<Note>{
        Note.D5, Note.D5, Note.D5,
        Note.C5, Note.D5, Note.A4,
        Note.G4, Note.A4, Note.F4, Note.D4, Note.A4
    };
    public DoorController Door;
    // audio source
    public List<AudioSource> NoteAudioSources;
    public AudioSource WinAudioSource;
    public AudioSource TomoSource; 
    public AudioSource SoyoSource;
    public AudioSource PaiSource;
    public Animator FurinaAnimator; 
    public Animator TomorinAnimator; 
    //public Animator SoyorinAnimator; 
    public Animator PaimonAnimator; 
    private float frnMovingTime = 0;
    private float tmrMovingTime = 0;
    //private float soyMovingTime = 0;
    private float paiMovingTime = 0;
    public GameObject Prompt1;
    public GameObject Prompt2;
    // public GameObject Prompt3;
    private void updateNote(Note note)
    {
        while(clickNotes.Count >= 11)
        {
            clickNotes.Dequeue();
        }
        clickNotes.Enqueue(note);
    }
    private void playNote(Note note)
    {
        NoteAudioSources[(int)(note)].Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        clickNotes = new Queue<Note>();
        Prompt1.SetActive(false);
        Prompt2.SetActive(false);
        // Prompt3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // furina moving
        if(frnMovingTime > Time.deltaTime*2){
            frnMovingTime = 0;
            FurinaAnimator.SetBool("ClickToMove", false); 
        }
        else if(frnMovingTime > 0){
            frnMovingTime += Time.deltaTime;
        }
        // tomorin moving
        
        tmrMovingTime += Time.deltaTime;
        if(tmrMovingTime >= 14)
        {
            tmrMovingTime = 0;
            TomorinAnimator.SetBool("ClickToMove", true);
        }
        else if(tmrMovingTime > 2*Time.deltaTime)
        {
            TomorinAnimator.SetBool("ClickToMove", false);
        }
        // soyorin moving

        //if(soyMovingTime > Time.deltaTime*2){
        //    soyMovingTime = 0;
        //    SoyorinAnimator.SetBool("ClickToMove", false); 
        //}
        //else if(soyMovingTime > 0){
        //    soyMovingTime += Time.deltaTime;
        //}
        // paimon moving

        if(paiMovingTime > Time.deltaTime*2){
            paiMovingTime = 0;
            PaimonAnimator.SetBool("ClickToMove", false); 
        }
        else if(paiMovingTime > 0){
            paiMovingTime += Time.deltaTime;
        }


        // door open

        if(clickNotes.Count != 11) { return; }
        int i = 0;
        foreach(Note note in clickNotes)
        {
            if (note != rightNotes[i])
            {
                return;
            }
            i++;
        }
        // open
        Door.Open();
        clickNotes.Clear();
        WinAudioSource.Play();
    }

    public void SelectOthers()
    {
        updateNote(Note.OTHERS);
        playNote(Note.OTHERS);
    }
    public void SelectD5()
    {
        updateNote(Note.D5);
        playNote(Note.D5);
    }
    public void SelectC5()
    {
        updateNote(Note.C5);
        playNote(Note.C5);
    }
    public void SelectA4()
    {
        updateNote(Note.A4);
        playNote(Note.A4);
    }
    public void SelectG4()
    {
        updateNote(Note.G4);
        playNote(Note.G4);
    }
    public void SelectF4()
    {
        updateNote(Note.F4);
        playNote(Note.F4);
    }
    public void SelectD4()
    {
        updateNote(Note.D4);
        playNote(Note.D4);
    }
    public void SelectSound()
    {
        WinAudioSource.Play();
        frnMovingTime = Time.deltaTime;
        FurinaAnimator.SetBool("ClickToMove", true);
        Prompt1.SetActive(true);
    }
    public void SelectTomorin()
    {
        TomoSource.Play();
        Prompt2.SetActive(true);
    }
    //public void SelectSoyorin()
    //{
    //    SoyoSource.Play();
    //    soyMovingTime = Time.deltaTime;
    //    SoyorinAnimator.SetBool("ClickToMove", true);
    //    // Prompt3.SetActive(true);
    //}
    public void SelectPaimon()
    {
        PaiSource.Play();
        paiMovingTime = Time.deltaTime;
        PaimonAnimator.SetBool("ClickToMove", true);
    }
    public void GrabLeft()
    {

    }
}
