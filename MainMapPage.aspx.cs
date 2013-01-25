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
        GoogleMapForASPNet1.GoogleMapObject.Width = "800px"; // You can also specify percentage(e.g. 80%) here
        GoogleMapForASPNet1.GoogleMapObject.Height = "600px";

        //Specify initial Zoom level.
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 13;
        GoogleMapForASPNet1.GoogleMapObject.AutomaticBoundaryAndZoom = false;
        //Specify Center Point for map. Map will be centered on this point.
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 43.66619, -79.44268);

        //Add pushpins for map. 
        //This should be done with intialization of GooglePoint class. 
        //ID is to identify a pushpin. It must be unique for each pin. Type is string.
        //Other properties latitude and longitude.
        GooglePoint GP1 = new GooglePoint();
        GP1.ID = "RedCar";
        GP1.Latitude = 43.65669;
        GP1.Longitude = -79.47268;  //+0.001
        //Specify bubble text here. You can use standard HTML tags here.
        GP1.InfoHTML = "This is Pushpin 1";

        //Specify icon image. This should be relative to root folder.
        GP1.IconImage = "icons/track.png";
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
        
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        // Move Red Car. 
        //Increment longitude value to move car horizontally. 
        GoogleMapForASPNet1.GoogleMapObject.Points["RedCar"].Longitude += 0.001;
    }
}
