//Attach this script to your GameObject. This GameObject doesn�t need to have a Collider component
//Set the Layer Mask field in the Inspector to the layer you would like to see collisions in (set to Everything if you are unsure).
//Create a second Gameobject for testing collisions. Make sure your GameObject has a Collider component (if it doesn�t, click on the Add Component button in the GameObject�s Inspector, and go to Physics>Box Collider).
//Place it so it is overlapping your other GameObject.
//Press Play to see the console output the name of your second GameObject

//This script uses the OverlapBox that creates an invisible Box Collider that detects multiple collisions with other colliders. The OverlapBox in this case is the same size and position as the GameObject you attach it to (acting as a replacement for the BoxCollider component).

using UnityEngine;
using TMPro;

public class OverlapBox : MonoBehaviour
{
    public TMP_Text ScoreText;
    [SerializeField] GameObject menu;

    [SerializeField] int PlayerScore;



    bool _mStarted;
    public LayerMask MLayerMask;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        _mStarted = true;
    }

    public void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, MLayerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box

        PlayerScore = hitColliders.Length;

        ScoreText.SetText(PlayerScore.ToString());



        if(PlayerScore >= 20)
        {
            Lose();
        }
    }

    public void Lose()
    {
        menu.SetActive(true);
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}