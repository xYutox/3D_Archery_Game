using System.Collections;
using UnityEngine;

public class BowControl : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowParent;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private Transform moveToPoint;
    [SerializeField] private Transform restPoint;

    [Header("Attributes")]
    [SerializeField] private float launchVelocity = 700f;
    [SerializeField] private float speed = 2f;

    bool drawn = false;
    Vector3 arrowState;

    // Start is called before the first frame update
    void Start()
    {
        //arrowState = restPoint.transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("Fire", false);
            drawn = true;
            animator.SetBool("Pulling", true);

            //arrowState = moveToPoint.transform.position;
            arrowParent.SetActive(true);
            arrowParent.transform.position = moveToPoint.transform.position;            
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            drawn = false;
            animator.SetBool("Pulling", false);

            //arrowState = restPoint.transform.position;
            arrowParent.transform.position = restPoint.transform.position;
            
        }
        
        if (drawn == true && Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Pulling", false);

            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.transform.position, arrowSpawnPoint.transform.rotation);
            arrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity, 0));

            StartCoroutine(ArrowTimer());

            //arrowState = restPoint.transform.position;
            arrowParent.transform.position = restPoint.transform.position;
            arrowParent.SetActive(false);
        }

        //float step = speed * Time.deltaTime;
        //arrowParent.transform.position = Vector3.MoveTowards(arrowParent.transform.position, arrowState, step);
    }

    private IEnumerator ArrowTimer()
    {
        yield return new WaitForSeconds(0.5f);

        arrowParent.SetActive(true);
    }

}
