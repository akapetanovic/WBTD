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
using System.Drawing;

public partial class MapWithAutoMovingPushpins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GoogleMapForASPNet1.ZoomChanged += new GoogleMapForASPNet.ZoomChangedHandler(OnZoomChanged);

        if (!IsPostBack)
        {
            // Set Zoom level Label
            lblZoomLevel.Text = "8";
            TrackProvider.SetZoomLevel(8);

            TrackProvider.Initialise();

            // Each update cycle call TrackerProvider to get new display data
            GoogleMapForASPNet1.GoogleMapObject = TrackProvider.GetDisplayData();
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        // Each update cycle call TrackerProvider to get new display data
       GoogleMapForASPNet1.GoogleMapObject = TrackProvider.GetDisplayData();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        // Check if the custom Map is to be built
        if (this.CheckBoxCustomMapEnabled.Checked == true)
        {
            if (GoogleMapForASPNet1.GoogleMapObject.Polygons.Count == 0)
                GoogleMapForASPNet1.GoogleMapObject.Polygons.Add(CustomMap.GetBlankPoligon());
        }
        else
        {
            if (GoogleMapForASPNet1.GoogleMapObject.Polygons.Count > 0)
                GoogleMapForASPNet1.GoogleMapObject.Polygons.Remove("BLANK");
        }
    }

    //Add event handler for PushpinDrag event
    void OnZoomChanged(int pZoomLevel)
    {
        TrackProvider.SetZoomLevel(pZoomLevel);
        //pID is ID of pushpin which was moved.
        lblZoomLevel.Text = pZoomLevel.ToString();

        // Update tracks on each zoom level change
        GoogleMapForASPNet1.GoogleMapObject = TrackProvider.GetDisplayData();
    }
}
