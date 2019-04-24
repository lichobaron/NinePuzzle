using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class MoveTrackerGloves : MonoBehaviour
{
    //Tracker
    private Vector3 posVRPN;
    private float x;
    private float y;
    private float z;
    private Vector3 actualTracker;
    private Vector3 newPos;
    //Gloves
    private string path = "C:\\Users\\Takina\\Documents\\GitHub\\NinePuzzle\\NinePuzzle\\Assets\\DataGloves\\";
    //    "/Users/licho/Documents/Unity/FurnitureSimulator/FurnitureSimulator/Assets/DataGloves/";
    //Right Glove
    private string[] filenames;
    private double[][][] means;
    private int[] info;
    private double[] tuple;
    //Left Glove
    private string[] filenamesl;
    private double[][][] meansl;
    private int[] infol;
    private double[] tuplel;
    //Transformaciones
    private float maxXReal = -0.195f;
    private float maxYReal = -0.206f;
    private float maxZReal = -0.907f;
    private float minXReal = -0.500f;
    private float minYReal = -0.369f;
    private float minZReal = -0.292f;
    private float maxXVir = 50f;
    private float maxYVir = 10f;
    private float maxZVir = 0f;
    private float minXVir = -20f;
    private float minYVir = 0f;
    private float minZVir = -30f;
    private int pasos = 1000;
    private float deltaXReal;
    private float deltaXVir;
    private float deltaYReal;
    private float deltaYVir;
    private float deltaZReal;
    private float deltaZVir;


    // Start is called before the first frame update
    void Start()
    {
        //Data Gloves
        filenames = new string[5];
        //Right gloves
        filenames[0] = path + "finger13.txt";
        filenames[1] = path + "finger23.txt";
        filenames[2] = path + "finger33.txt";
        filenames[3] = path + "finger43.txt";
        filenames[4] = path + "finger53.txt";
        //Left gloves
        filenamesl = new string[5];
        filenamesl[0] = path + "finger13l.txt";
        filenamesl[1] = path + "finger23l.txt";
        filenamesl[2] = path + "finger33l.txt";
        filenamesl[3] = path + "finger43l.txt";
        filenamesl[4] = path + "finger53l.txt";

        tuple = new double[14];
        tuplel = new double[14];

        deltaXReal = (maxXReal - minXReal) / pasos;
        deltaXVir = (maxXVir - minXVir) / pasos;
        deltaYReal = (maxYReal - minYReal) / pasos;
        deltaYVir = (maxYVir - minYVir) / pasos;
        deltaZReal = (maxZReal - minZReal) / pasos;
        deltaZVir = (maxZVir - minZVir) / pasos;
        newPos = new Vector3(15, 15, -35);
    }

    // Update is called once per frame
    void Update()
    {
        InputDataTracker("Tracker0@10.3.137.218");
        InputDataGloves("Glove14Right@10.3.136.131", "Glove14Left@10.3.136.131");
        TransformarRealVirtual();
        transform.position = newPos;
    }

    void InputDataTracker(string address)
    {
        posVRPN = VRPN.vrpnTrackerPos(address, 2);

        x = 1*posVRPN.x;
        y = -1*posVRPN.z;
        z = -1*posVRPN.y;

        actualTracker = new Vector3( x, y, z);
        Debug.Log(new Vector3(x*1000, y*1000, z*1000));
    }

    void TransformarRealVirtual()
    {
        //TransformarX
        float pX = (actualTracker.x - minXReal) / deltaXReal;
        float xT = (pX * deltaXVir) + minXVir;
        //TransformarY
        float pY = Mathf.Abs((actualTracker.y - minYReal) / deltaYReal);
        float yT = (pY * deltaYVir) + minYVir;
        //TransformarZ
        float pZ = Mathf.Abs((actualTracker.z - minZReal) / deltaZReal);
        float zT = (pZ * deltaZVir) + minZVir;

        newPos.x = xT;
        newPos.y = yT;
        newPos.z = zT;
    }

    void InputDataGloves(string addressRight, string addressLeft)
    {
        tuplel[0] = VRPN.vrpnAnalog(addressLeft, 0);
        tuplel[1] = VRPN.vrpnAnalog(addressLeft, 1);
        tuplel[2] = VRPN.vrpnAnalog(addressLeft, 2);
        tuplel[3] = VRPN.vrpnAnalog(addressLeft, 3);
        tuplel[4] = VRPN.vrpnAnalog(addressLeft, 4);
        tuplel[5] = VRPN.vrpnAnalog(addressLeft, 5);
        tuplel[6] = VRPN.vrpnAnalog(addressLeft, 6);
        tuplel[7] = VRPN.vrpnAnalog(addressLeft, 7);
        tuplel[8] = VRPN.vrpnAnalog(addressLeft, 8);
        tuplel[9] = VRPN.vrpnAnalog(addressLeft, 9);
        tuplel[10] = VRPN.vrpnAnalog(addressLeft, 10);
        tuplel[11] = VRPN.vrpnAnalog(addressLeft, 11);
        tuplel[12] = VRPN.vrpnAnalog(addressLeft, 12);
        tuplel[13] = VRPN.vrpnAnalog(addressLeft, 13);

        meansl = GetMeansFromFile(filenamesl, 2); //TODO
        infol = TestFingers(tuplel, meansl);

        /*
        Debug.Log("Glove raw left = " + String.Join(", ",
            new List<double>(tuplel)
            .ConvertAll(i => i.ToString())
            .ToArray()));
        Debug.Log("GloveL = " + String.Join(", ",
            new List<int>(infol)
            .ConvertAll(i => i.ToString())
            .ToArray()));
        */

        tuple[0] = VRPN.vrpnAnalog(addressRight, 0);
        tuple[1] = VRPN.vrpnAnalog(addressRight, 1);
        tuple[2] = VRPN.vrpnAnalog(addressRight, 2);
        tuple[3] = VRPN.vrpnAnalog(addressRight, 3);
        tuple[4] = VRPN.vrpnAnalog(addressRight, 4);
        tuple[5] = VRPN.vrpnAnalog(addressRight, 5);
        tuple[6] = VRPN.vrpnAnalog(addressRight, 6);
        tuple[7] = VRPN.vrpnAnalog(addressRight, 7);
        tuple[8] = VRPN.vrpnAnalog(addressRight, 8);
        tuple[9] = VRPN.vrpnAnalog(addressRight, 9);
        tuple[10] = VRPN.vrpnAnalog(addressRight, 10);
        tuple[11] = VRPN.vrpnAnalog(addressRight, 11);
        tuple[12] = VRPN.vrpnAnalog(addressRight, 12);
        tuple[13] = VRPN.vrpnAnalog(addressRight, 13);

        means = GetMeansFromFile(filenames, 2); //TODO
        info = TestFingers(tuple, means); //TODO

        /*
        Debug.Log("Glove raw right= " + String.Join(", ",
            new List<double>(tuple)
            .ConvertAll(i => i.ToString())
            .ToArray()));
        Debug.Log("GloveR = " + String.Join(", ",
            new List<int>(info)
            .ConvertAll(i => i.ToString())
            .ToArray()));
        */
    }

    public static double[] GetFingersTuple(double[] tuple, int finger)
    {
        double[] r = new double[2];
        finger++;
        switch (finger)
        {
            case 1:
                r[0] = tuple[0];
                r[1] = tuple[1];
                break;
            case 2:
                r[0] = tuple[3];
                r[1] = tuple[4];
                break;
            case 3:
                r[0] = tuple[6];
                r[1] = tuple[7];
                break;
            case 4:
                r[0] = tuple[9];
                r[1] = tuple[10];
                break;
            case 5:
                r[0] = tuple[12];
                r[1] = tuple[13];
                break;
            default:
                r[0] = -1;
                r[1] = -1;
                break;
        }
        return r;
    }

    public static double[][][] GetMeansFromFile(string[] fileNames, int numK)
    {
        double[][][] means = new double[5][][];

        for (int i = 0; i < fileNames.Length; i++)
        {
            means[i] = new double[numK][];
            string filename = fileNames[i];
            String line; try
            {
                StreamReader sr = new StreamReader(filename);
                line = sr.ReadLine();

                int j = 0;
                while (line != null)
                {
                    Regex reg = new Regex(@"([-+]?[0-9]*\.?[0-9]+)");
                    int tam = 0;
                    foreach (Match match in reg.Matches(line))
                    {
                        tam++;
                    }
                    means[i][j] = new double[tam];

                    int k = 0;
                    foreach (Match match in reg.Matches(line))
                    {
                        means[i][j][k] = double.Parse(match.Value, CultureInfo.InvariantCulture.NumberFormat);
                        k++;
                    }

                    j++;
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block files.");
            }
        }

        return means;
    }

    public static int[] TestFingers(double[] tuple, double[][][] means)
    {

        int[] r = new int[5];

        for (int i = 0; i < 5; i++) //num dedos
        {
            int minIndex = 0;
            double min = Distance(GetFingersTuple(tuple, i), means[i][0]);
            for (int j = 1; j < means[i].Length; j++)
            {
                if (Distance(GetFingersTuple(tuple, i), means[i][j]) < min)
                {
                    minIndex = j;
                }
            }
            r[i] = TranslateSensor(minIndex, i);
        }
        return r;
    }

    public static int TranslateSensor(int num, int finger)
    {
        //Debug.Log(num);
        int r = 99;
        if (num == 0)
        {
            r = 0;
        }
        else if (num == 1) //TODO
        {
            r = 1;
        }
        else if (num == 2)
        {
            r = -1;
        }
        return r;
    }

    private static double Distance(double[] tuple, double[] mean)
    {
        double sumSquaredDiffs = 0.0;
        for (int j = 0; j < tuple.Length; ++j)
            sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
        return Math.Sqrt(sumSquaredDiffs);

    }

}
