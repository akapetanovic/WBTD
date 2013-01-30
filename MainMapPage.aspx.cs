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

            this.lblUpdateRateReadout.Text = this.Timer1.Interval.ToString();
            this.TextBoxUpdateRate.Text = this.lblUpdateRateReadout.Text;
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {

        // Each update cycle call TrackerProvider to get new display data
        GoogleMapForASPNet1.GoogleMapObject = TrackProvider.GetDisplayData();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        TrackProvider.CustomMapEnabled_Enabled = this.CheckBoxCustomMapEnabled.Checked;
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
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        TrackProvider.PredictionEngine1_Enabled = this.CheckBox1.Checked;
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        TrackProvider.PredictionEngine2_Enabled = this.CheckBox2.Checked;
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        TrackProvider.PredictionEngine3_Enabled = this.CheckBox3.Checked;
    }
    protected void BtnUpdateRate_Click(object sender, EventArgs e)
    {
        this.Timer1.Interval = int.Parse(this.TextBoxUpdateRate.Text);
        this.lblUpdateRateReadout.Text = this.Timer1.Interval.ToString();
        this.TextBoxUpdateRate.Text = this.lblUpdateRateReadout.Text;
    }
   
}
