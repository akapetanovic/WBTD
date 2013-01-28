using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for CustomMap
/// </summary>
public class CustomMap
{
	public CustomMap()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static GooglePolygon GetBlankPoligon()
    {
        GooglePolygon BP = new GooglePolygon();

        GooglePoint GP1 = new GooglePoint();
        GP1.ID = "GP1";
        GP1.Latitude = 50.0;
        GP1.Longitude = 0.0;

        GooglePoint GP2 = new GooglePoint();
        GP2.ID = "GP2";
        GP2.Latitude = 50.0;
        GP2.Longitude = 50.0;

        GooglePoint GP3 = new GooglePoint();
        GP3.ID = "GP3";
        GP3.Latitude = 30.00;
        GP3.Longitude = 50.0;

        GooglePoint GP4 = new GooglePoint();
        GP4.ID = "GP4";
        GP4.Latitude = 30.0;
        GP4.Longitude = 0.0;

        GooglePoint GP5 = new GooglePoint();
        GP5.ID = "GP5";
        GP5.Latitude = 49.95;
        GP5.Longitude = 0.0;


        BP.ID = "BLANK";
        //Give Hex code for line color
        Color color = Color.FromName(Color.Black.Name);
        string ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);

        BP.FillColor = ColorCode;
        BP.FillOpacity = 1;
        BP.StrokeColor = ColorCode;
        BP.StrokeOpacity = 1;
        BP.StrokeWeight = 1;

        BP.Points.Add(GP1);
        BP.Points.Add(GP2);
        BP.Points.Add(GP3);
        BP.Points.Add(GP4);
        BP.Points.Add(GP5);


        return BP;
    }

    public static double GetScaleFactor(int ZoomLevel)
    {
        double ScaleFactor = 1;

        switch (ZoomLevel)
        {
            case 1:
                ScaleFactor = 0.2;
                break;
            case 2:
                ScaleFactor = 0.2;
                break;
            case 3:
                ScaleFactor = 0.2;
                break;
            case 4:
                ScaleFactor = 0.2;
                break;
            case 5:
                ScaleFactor = 0.2;
                break;
            case 6:
                ScaleFactor = 0.4;
                break;
            case 7:
                ScaleFactor = 0.5;
                break;
            case 8:
                ScaleFactor = 1;
                break;
            case 9:
                ScaleFactor = 2;
                break;
            case 10:
                ScaleFactor = 4;
                break;
            case 11:
                ScaleFactor = 8;
                break;
            case 12:
                ScaleFactor = 16;
                break;
            case 13:
                ScaleFactor = 32;
                break;
            case 14:
                ScaleFactor = 64;
                break;
            case 15:
                ScaleFactor = 128;
                break;
            case 16:
                ScaleFactor = 256;
                break;
            case 17:
                ScaleFactor = 512;
                break;
            default:
                break;
        }

        return ScaleFactor;
    }
}