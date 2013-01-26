using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MapWithAutoMovingPushpins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //You must specify Google Map API Key for this component. You can obtain this key from http://code.google.com/apis/maps/signup.html
        //For samples to run properly, set GoogleAPIKey in Web.Config file.
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"]; 

        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
        GoogleMapForASPNet1.GoogleMapObject.Width = "900px"; // You can also specify percentage(e.g. 80%) here
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";

        //Specify initial Zoom level.
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 8;
        GoogleMapForASPNet1.GoogleMapObject.AutomaticBoundaryAndZoom = false;
        //Specify Center Point for map. Map will be centered on this point.
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 44.00, 18.00);

      

        //Add pushpins for map. 
        //This should be done with intialization of GooglePoint class. 
        //ID is to identify a pushpin. It must be unique for each pin. Type is string.
        //Other properties latitude and longitude.
        GooglePoint GP1 = new GooglePoint();
        GP1.ID = "TRACK";
        GP1.Latitude = 44.00;
        GP1.Longitude = 18.00;  //+0.001
        //Specify bubble text here. You can use standard HTML tags here.
        //GP1.InfoHTML = "This is Pushpin 1";

        GP1.IconImage = "icons/track.png";
        //Specify icon image. This should be relative to root folder.

        TextToImage TI = new TextToImage();
        TI.GenerateAndStore("DHL334" + Environment.NewLine + "340");
        GooglePoint GP2 = new GooglePoint();
        GP2.ID = "LABEL";
        GP2.Draggable = true;
        GP2.IconImage = "icons/label.png";
        GP2.Latitude = GP1.Latitude + 0.2;
        GP2.Longitude = GP1.Longitude - 0.15;

        GooglePolyline PL = new GooglePolyline();
        PL.Points.Add(GP1);
        PL.Points.Add(GP2);
        PL.Width = 3;
       // PL.ColorCode = 
        
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP2);
        GoogleMapForASPNet1.GoogleMapObject.Polylines.Add(PL);
        
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        // Move Red Car. 
        //Increment longitude value to move car horizontally. 
        GoogleMapForASPNet1.GoogleMapObject.Points["TRACK"].Longitude += 0.010;

        // Check if the custom Map is to be built
        if (this.CheckBoxCustomMapEnabled.Checked == true)
        {
           
        }
        else
        {
           
        }
    }


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
}
