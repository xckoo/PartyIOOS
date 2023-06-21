using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_3A : MonoBehaviour {

    public Animation referanceAnim;

    [Header("Sirt Axis Jointler")]
    public Transform sirt_x;
    public Transform sirt_y;
    public Transform sirt_z;
    [Header("Sag Omuz Axis Jointler")]
    public Transform sagOmuz_x;
    public Transform sagOmuz_y;
    public Transform sagOmuz_z;
    [Header("Sag Kol Axis Jointler")]
    public Transform sagKol_x;
    public Transform sagKol_y;
    public Transform sagKol_z;
    [Header("Sol Omuz Axis Jointler")]
    public Transform solOmuz_x;
    public Transform solOmuz_y;
    public Transform solOmuz_z;
    [Header("Sol Kol Axis Jointler")]
    public Transform solKol_x;
    public Transform solKol_y;
    public Transform solKol_z;
    [Header("Sag Bacak Axis Jointler")]
    public Transform sagBacak_x;
    public Transform sagBacak_y;
    public Transform sagBacak_z;
    [Header("Sag Kaval Axis Jointler")]
    public Transform sagKaval_x;
    public Transform sagKaval_y;
    public Transform sagKaval_z;
    [Header("Sol Bacak Axis Jointler")]
    public Transform solBacak_x;
    public Transform solBacak_y;
    public Transform solBacak_z;
    [Header("Sol Kaval Axis Jointler")]
    public Transform solKaval_x;
    public Transform solKaval_y;
    public Transform solKaval_z;

    [Header("Sirt Referance Anim")]
    public Transform sirtReferanceAnim;
    [Header("SagOmuz Referance Anim")]
    public Transform sagOmuzReferanceAnim;
    [Header("SagKol Referance Anim")]
    public Transform sagKolReferanceAnim;
    [Header("SolOmuz Referance Anim")]
    public Transform solOmuzReferanceAnim;
    [Header("SolKol Referance Anim")]
    public Transform solKolReferanceAnim;
    [Header("Sag Bacak Referance Anim")]
    public Transform sagBacakReferanceAnim;
    [Header("Sag Kaval Referance Anim")]
    public Transform sagKavalReferanceAnim;
    [Header("Sol Bacak Referance Anim")]
    public Transform solBacakReferanceAnim;
    [Header("Sol Kaval Referance Anim")]
    public Transform solKavalReferanceAnim;

    private Quaternion sirtInitialRotation;
    private Quaternion sagOmuzInitialRotation;
    private Quaternion sagKolInitialRotation;
    private Quaternion solOmuzInitialRotation;
    private Quaternion solKolInitialRotation;
    private Quaternion sagBacakInitialRotation;
    private Quaternion sagKavalInitialRotation;
    private Quaternion solBacakInitialRotation;
    private Quaternion solKavalInitialRotation;
    private void Start()
    {
        InitializeRotations();
    }
    private void FixedUpdate()
    {
        SirtJointEsle();
        SagOmuzJointEsle();
        SagKolJointEsle();
        SolOmuzJointEsle();
        SolKolJointEsle();
        SagBacakJointEsle();
        SagKavalJointEsle();
        SolBacakJointEsle();
        SolKavalJointEsle();
    }

    private void InitializeRotations()
    {
        sirtInitialRotation = Quaternion.Inverse(sirt_z.transform.rotation);
        sagOmuzInitialRotation = Quaternion.Inverse(sagOmuz_z.transform.rotation);
        sagKolInitialRotation = Quaternion.Inverse(sagKol_z.transform.rotation);
        solOmuzInitialRotation = Quaternion.Inverse(solOmuz_z.transform.rotation);
        solKolInitialRotation = Quaternion.Inverse(solKol_z.transform.rotation);
        sagBacakInitialRotation = Quaternion.Inverse(sagBacak_z.transform.rotation);
        sagKavalInitialRotation = Quaternion.Inverse(sagKaval_z.transform.rotation);
        solBacakInitialRotation = Quaternion.Inverse(solBacak_z.transform.rotation);
        solKavalInitialRotation = Quaternion.Inverse(solKaval_z.transform.rotation);
    }

    private void SirtJointEsle()
    {
        
        JointSpring spsirt_x = sirt_x.GetComponent<HingeJoint>().spring;
        spsirt_x.targetPosition = (sirtReferanceAnim.localRotation * sirtInitialRotation).eulerAngles.x-180f;
        sirt_x.GetComponent<HingeJoint>().spring = spsirt_x;

        JointSpring spsirt_y = sirt_y.GetComponent<HingeJoint>().spring;

        spsirt_y.targetPosition = (sirtReferanceAnim.localRotation * sirtInitialRotation).eulerAngles.y - 180f;
        sirt_y.GetComponent<HingeJoint>().spring = spsirt_y;

        JointSpring spsirt_z = sirt_z.GetComponent<HingeJoint>().spring;
        spsirt_z.targetPosition = (sirtReferanceAnim.localRotation * sirtInitialRotation).eulerAngles.z - 180f;
        sirt_z.GetComponent<HingeJoint>().spring = spsirt_z;
    }


    private void SagOmuzJointEsle()
    {
        JointSpring spsagomuz_x = sagOmuz_x.GetComponent<HingeJoint>().spring;
        spsagomuz_x.targetPosition = (sagOmuzReferanceAnim.localRotation * sagOmuzInitialRotation).eulerAngles.x - 180f;
        sagOmuz_x.GetComponent<HingeJoint>().spring = spsagomuz_x;

        JointSpring spsagomuz_y = sagOmuz_y.GetComponent<HingeJoint>().spring;
        spsagomuz_y.targetPosition = (sagOmuzReferanceAnim.localRotation * sagOmuzInitialRotation).eulerAngles.y - 180f;
        sagOmuz_y.GetComponent<HingeJoint>().spring = spsagomuz_y;

        JointSpring spsagomuz_z = sagOmuz_z.GetComponent<HingeJoint>().spring;
        spsagomuz_z.targetPosition = (sagOmuzReferanceAnim.localRotation * sagOmuzInitialRotation).eulerAngles.z - 180f;
        sagOmuz_z.GetComponent<HingeJoint>().spring = spsagomuz_z;
    }

    private void SagKolJointEsle()
    {
        JointSpring spsagkol_x = sagKol_x.GetComponent<HingeJoint>().spring;
        spsagkol_x.targetPosition = (sagKolReferanceAnim.localRotation * sagKolInitialRotation).eulerAngles.x - 180f;
        sagKol_x.GetComponent<HingeJoint>().spring = spsagkol_x;

        JointSpring spsagkol_y = sagKol_y.GetComponent<HingeJoint>().spring;
        spsagkol_y.targetPosition = (sagKolReferanceAnim.localRotation * sagKolInitialRotation).eulerAngles.y - 180f;
        sagKol_y.GetComponent<HingeJoint>().spring = spsagkol_y;

        JointSpring spsagkol_z = sagKol_z.GetComponent<HingeJoint>().spring;
        spsagkol_z.targetPosition = (sagKolReferanceAnim.localRotation * sagKolInitialRotation).eulerAngles.z - 180f;
        sagKol_z.GetComponent<HingeJoint>().spring = spsagkol_z;
    }

    private void SolOmuzJointEsle()
    {
        JointSpring spsolomuz_x = solOmuz_x.GetComponent<HingeJoint>().spring;
        spsolomuz_x.targetPosition = (solOmuzReferanceAnim.localRotation * solOmuzInitialRotation).eulerAngles.x - 180f;
        solOmuz_x.GetComponent<HingeJoint>().spring = spsolomuz_x;

        JointSpring spsolomuz_y = solOmuz_y.GetComponent<HingeJoint>().spring;
        spsolomuz_y.targetPosition = (solOmuzReferanceAnim.localRotation * solOmuzInitialRotation).eulerAngles.y - 180f;
        solOmuz_y.GetComponent<HingeJoint>().spring = spsolomuz_y;

        JointSpring spsolomuz_z = solOmuz_z.GetComponent<HingeJoint>().spring;
        spsolomuz_z.targetPosition = (solOmuzReferanceAnim.localRotation * solOmuzInitialRotation).eulerAngles.z - 180f;
        solOmuz_z.GetComponent<HingeJoint>().spring = spsolomuz_z;
    }

    private void SolKolJointEsle()
    {
        JointSpring spsolkol_x = solKol_x.GetComponent<HingeJoint>().spring;
        spsolkol_x.targetPosition = (solKolReferanceAnim.localRotation * solKolInitialRotation).eulerAngles.x - 180f;
        solKol_x.GetComponent<HingeJoint>().spring = spsolkol_x;

        JointSpring spsolkol_y = solKol_y.GetComponent<HingeJoint>().spring;
        spsolkol_y.targetPosition = (solKolReferanceAnim.localRotation * solKolInitialRotation).eulerAngles.y - 180f;
        solKol_y.GetComponent<HingeJoint>().spring = spsolkol_y;

        JointSpring spsolkol_z = solKol_z.GetComponent<HingeJoint>().spring;
        spsolkol_z.targetPosition = (solKolReferanceAnim.localRotation * solKolInitialRotation).eulerAngles.z - 180f;
        solKol_z.GetComponent<HingeJoint>().spring = spsolkol_z;
    }


    private void SagBacakJointEsle()
    {
        JointSpring spsagbacak_x = sagBacak_x.GetComponent<HingeJoint>().spring;
        spsagbacak_x.targetPosition = (sagBacakReferanceAnim.localRotation * sagKolInitialRotation).eulerAngles.x - 180f;
        sagBacak_x.GetComponent<HingeJoint>().spring = spsagbacak_x;

        JointSpring spsagbacak_y = sagBacak_y.GetComponent<HingeJoint>().spring;
        spsagbacak_y.targetPosition = (sagBacakReferanceAnim.localRotation * sagKolInitialRotation).eulerAngles.y - 180f;
        sagBacak_y.GetComponent<HingeJoint>().spring = spsagbacak_y;

        JointSpring spsagbacak_z = sagBacak_z.GetComponent<HingeJoint>().spring;
        spsagbacak_z.targetPosition = (sagBacakReferanceAnim.localRotation * sagKolInitialRotation).eulerAngles.z - 180f;
        sagBacak_z.GetComponent<HingeJoint>().spring = spsagbacak_z;

    }

    private void SagKavalJointEsle()
    {
        JointSpring spsagkaval_x = sagKaval_x.GetComponent<HingeJoint>().spring;
        spsagkaval_x.targetPosition = (sagKavalReferanceAnim.localRotation * sagKavalInitialRotation).eulerAngles.x - 180f;
        sagKaval_x.GetComponent<HingeJoint>().spring = spsagkaval_x;

        JointSpring spsagkaval_y = sagKaval_y.GetComponent<HingeJoint>().spring;
        spsagkaval_y.targetPosition = (sagKavalReferanceAnim.localRotation * sagKavalInitialRotation).eulerAngles.y - 180f;
        sagKaval_y.GetComponent<HingeJoint>().spring = spsagkaval_y;

        JointSpring spsagkaval_z = sagKaval_z.GetComponent<HingeJoint>().spring;
        spsagkaval_z.targetPosition = (sagKavalReferanceAnim.localRotation * sagKavalInitialRotation).eulerAngles.z - 180f;
        sagKaval_z.GetComponent<HingeJoint>().spring = spsagkaval_z;
    }

    private void SolBacakJointEsle()
    {
        JointSpring spsolbacak_x = solBacak_x.GetComponent<HingeJoint>().spring;
        spsolbacak_x.targetPosition = (solBacakReferanceAnim.localRotation * solBacakInitialRotation).eulerAngles.x - 180f;
        solBacak_x.GetComponent<HingeJoint>().spring = spsolbacak_x;

        JointSpring spsolbacak_y = solBacak_y.GetComponent<HingeJoint>().spring;
        spsolbacak_y.targetPosition = (solBacakReferanceAnim.localRotation * solBacakInitialRotation).eulerAngles.y - 180f;
        solBacak_y.GetComponent<HingeJoint>().spring = spsolbacak_y;

        JointSpring spsolbacak_z = solBacak_z.GetComponent<HingeJoint>().spring;
        spsolbacak_z.targetPosition = (solBacakReferanceAnim.localRotation * solBacakInitialRotation).eulerAngles.z - 180f;
        solBacak_z.GetComponent<HingeJoint>().spring = spsolbacak_z;

    }


    private void SolKavalJointEsle()
    {
        JointSpring spsolkaval_x = solKaval_x.GetComponent<HingeJoint>().spring;
        spsolkaval_x.targetPosition = (solKavalReferanceAnim.localRotation * solKavalInitialRotation).eulerAngles.x - 180f;
        solKaval_x.GetComponent<HingeJoint>().spring = spsolkaval_x;

        JointSpring spsolkaval_y = solKaval_y.GetComponent<HingeJoint>().spring;
        spsolkaval_y.targetPosition = (solKavalReferanceAnim.localRotation * solKavalInitialRotation).eulerAngles.y - 180f;
        solKaval_y.GetComponent<HingeJoint>().spring = spsolkaval_y;

        JointSpring spsolkaval_z = solKaval_z.GetComponent<HingeJoint>().spring;
        spsolkaval_z.targetPosition = (solKavalReferanceAnim.localRotation * solKavalInitialRotation).eulerAngles.z - 180f;
        solKaval_z.GetComponent<HingeJoint>().spring = spsolkaval_z;
    }
}
