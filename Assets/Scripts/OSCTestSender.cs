using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Simple OSC test communication script
/// </summary>
[AddComponentMenu("Scripts/OSCTestSender")]
public class OSCTestSender : MonoBehaviour
{

    private Osc oscHandler;

    public string remoteIp;
    public int sendToPort;
    public int listenerPort;

    public float mouseX;
    public float mouseY;

    public int[] kb1 = new int[27];
    public int[] kb2 = new int[27];


    ~OSCTestSender()
    {
        if (oscHandler != null)
        {            
            oscHandler.Cancel();
        }

        // speed up finalization
        oscHandler = null;
        System.GC.Collect();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Debug.LogWarning("time = " + Time.time);
       
        OscMessage oscM = Osc.StringToOscMessage("/test1 TRUE 23 0.501 bla");
        oscHandler.Send(oscM);  

    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
       
    }

    void OnDisable()
    {
        // close OSC UDP socket
        Debug.Log("closing OSC UDP socket in OnDisable");
        oscHandler.Cancel();
        oscHandler = null;
    }

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
        UDPPacketIO udp = GetComponent<UDPPacketIO>();
        udp.init(remoteIp, sendToPort, listenerPort);
        
	    oscHandler = GetComponent<Osc>();
        oscHandler.init(udp);
        
        oscHandler.SetAddressHandler("/mouseX", getMouseX);
        oscHandler.SetAddressHandler("/mouseY", getMouseY);
        
        #region
        oscHandler.SetAddressHandler("/kb1/a", kb1getA);
        oscHandler.SetAddressHandler("/kb1/b", kb1getB);
        oscHandler.SetAddressHandler("/kb1/c", kb1getC);
        oscHandler.SetAddressHandler("/kb1/d", kb1getD);
        oscHandler.SetAddressHandler("/kb1/e", kb1getE);
        oscHandler.SetAddressHandler("/kb1/f", kb1getF);
        oscHandler.SetAddressHandler("/kb1/g", kb1getG);
        oscHandler.SetAddressHandler("/kb1/h", kb1getH);
        oscHandler.SetAddressHandler("/kb1/i", kb1getI);
        oscHandler.SetAddressHandler("/kb1/j", kb1getJ);
        oscHandler.SetAddressHandler("/kb1/k", kb1getK);
        oscHandler.SetAddressHandler("/kb1/l", kb1getL);
        oscHandler.SetAddressHandler("/kb1/m", kb1getM);
        oscHandler.SetAddressHandler("/kb1/n", kb1getN);
        oscHandler.SetAddressHandler("/kb1/o", kb1getO);
        oscHandler.SetAddressHandler("/kb1/p", kb1getP);
        oscHandler.SetAddressHandler("/kb1/q", kb1getQ);
        oscHandler.SetAddressHandler("/kb1/r", kb1getR);
        oscHandler.SetAddressHandler("/kb1/s", kb1getS);
        oscHandler.SetAddressHandler("/kb1/t", kb1getT);
        oscHandler.SetAddressHandler("/kb1/u", kb1getU);
        oscHandler.SetAddressHandler("/kb1/v", kb1getV);
        oscHandler.SetAddressHandler("/kb1/w", kb1getW);
        oscHandler.SetAddressHandler("/kb1/x", kb1getX);
        oscHandler.SetAddressHandler("/kb1/y", kb1getY);
        oscHandler.SetAddressHandler("/kb1/z", kb1getZ);  
        oscHandler.SetAddressHandler("/kb1/space", kb1getSpace);  

        oscHandler.SetAddressHandler("/kb2/a", kb2getA);
        oscHandler.SetAddressHandler("/kb2/b", kb2getB);
        oscHandler.SetAddressHandler("/kb2/c", kb2getC);
        oscHandler.SetAddressHandler("/kb2/d", kb2getD);
        oscHandler.SetAddressHandler("/kb2/e", kb2getE);
        oscHandler.SetAddressHandler("/kb2/f", kb2getF);
        oscHandler.SetAddressHandler("/kb2/g", kb2getG);
        oscHandler.SetAddressHandler("/kb2/h", kb2getH);
        oscHandler.SetAddressHandler("/kb2/i", kb2getI);
        oscHandler.SetAddressHandler("/kb2/j", kb2getJ);
        oscHandler.SetAddressHandler("/kb2/k", kb2getK);
        oscHandler.SetAddressHandler("/kb2/l", kb2getL);
        oscHandler.SetAddressHandler("/kb2/m", kb2getM);
        oscHandler.SetAddressHandler("/kb2/n", kb2getN);
        oscHandler.SetAddressHandler("/kb2/o", kb2getO);
        oscHandler.SetAddressHandler("/kb2/p", kb2getP);
        oscHandler.SetAddressHandler("/kb2/q", kb2getQ);
        oscHandler.SetAddressHandler("/kb2/r", kb2getR);
        oscHandler.SetAddressHandler("/kb2/s", kb2getS);
        oscHandler.SetAddressHandler("/kb2/t", kb2getT);
        oscHandler.SetAddressHandler("/kb2/u", kb2getU);
        oscHandler.SetAddressHandler("/kb2/v", kb2getV);
        oscHandler.SetAddressHandler("/kb2/w", kb2getW);
        oscHandler.SetAddressHandler("/kb2/x", kb2getX);
        oscHandler.SetAddressHandler("/kb2/y", kb2getY);
        oscHandler.SetAddressHandler("/kb2/z", kb2getZ);     
        oscHandler.SetAddressHandler("/kb2/space", kb2getSpace);     
        #endregion


        
    }

    public bool getkb1(char desire){
        return Convert.ToBoolean(kb1[(int)desire - 97]);
    }
    public bool getkb2(char desire){
        return Convert.ToBoolean(kb2[(int)desire - 97]);
    }

    public bool getkb1(string desire){
        switch(desire){
            case "space":
                return Convert.ToBoolean(kb1[26]);
            default:
                return false;
        }
    }
    public bool getkb2(string desire){
        switch(desire){
            case "space":
                return Convert.ToBoolean(kb2[26]);
            default:
                return false;
        }
    }

    #region
    public void getMouseX(OscMessage m)
    {
        //Debug.Log("--------------> OSC example message received: ("+m+")");
        //print(m.Values[0]);
        mouseX = float.Parse(m.Values[0].ToString());
    }

    public void getMouseY(OscMessage m)
    {
        //Debug.Log("--------------> OSC example message received: ("+m+")");
        //print(m.Values[0]);
        mouseY = float.Parse(m.Values[0].ToString());
    }

    public void kb1getA(OscMessage m){
    kb1[0] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getB(OscMessage m){
        kb1[1] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getC(OscMessage m){
        kb1[2] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getD(OscMessage m){
        kb1[3] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getE(OscMessage m){
        kb1[4] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getF(OscMessage m){
        kb1[5] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getG(OscMessage m){
        kb1[6] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getH(OscMessage m){
        kb1[7] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getI(OscMessage m){
        kb1[8] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getJ(OscMessage m){
        kb1[9] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getK(OscMessage m){
        kb1[10] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getL(OscMessage m){
        kb1[11] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getM(OscMessage m){
        kb1[12] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getN(OscMessage m){
        kb1[13] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getO(OscMessage m){
        kb1[14] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getP(OscMessage m){
        kb1[15] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getQ(OscMessage m){
        kb1[16] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getR(OscMessage m){
        kb1[17] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getS(OscMessage m){
        kb1[18] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getT(OscMessage m){
        kb1[19] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getU(OscMessage m){
        kb1[20] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getV(OscMessage m){
        kb1[21] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getW(OscMessage m){
        kb1[22] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getX(OscMessage m){
        kb1[23] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getY(OscMessage m){
        kb1[24] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getZ(OscMessage m){
        kb1[25] = int.Parse(m.Values[0].ToString());
    }

    public void kb1getSpace(OscMessage m){
        kb1[26] = int.Parse(m.Values[0].ToString());
    }



        public void kb2getA(OscMessage m){
    kb2[0] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getB(OscMessage m){
        kb2[1] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getC(OscMessage m){
        kb2[2] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getD(OscMessage m){
        kb2[3] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getE(OscMessage m){
        kb2[4] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getF(OscMessage m){
        kb2[5] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getG(OscMessage m){
        kb2[6] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getH(OscMessage m){
        kb2[7] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getI(OscMessage m){
        kb2[8] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getJ(OscMessage m){
        kb2[9] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getK(OscMessage m){
        kb2[10] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getL(OscMessage m){
        kb2[11] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getM(OscMessage m){
        kb2[12] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getN(OscMessage m){
        kb2[13] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getO(OscMessage m){
        kb2[14] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getP(OscMessage m){
        kb2[15] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getQ(OscMessage m){
        kb2[16] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getR(OscMessage m){
        kb2[17] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getS(OscMessage m){
        kb2[18] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getT(OscMessage m){
        kb2[19] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getU(OscMessage m){
        kb2[20] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getV(OscMessage m){
        kb2[21] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getW(OscMessage m){
        kb2[22] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getX(OscMessage m){
        kb2[23] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getY(OscMessage m){
        kb2[24] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getZ(OscMessage m){
        kb2[25] = int.Parse(m.Values[0].ToString());
    }

    public void kb2getSpace(OscMessage m){
        kb2[26] = int.Parse(m.Values[0].ToString());
    }

    #endregion
}
