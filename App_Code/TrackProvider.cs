using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Configuration;

/// <summary>
/// Summary description for TrackProvider
/// </summary>
public class TrackProvider
{
    private static GoogleObject DisplayDataOut = new GoogleObject();
    private static int CurrentZoomLevel = 8;

    public static void SetZoomLevel(int Zoom)
    {
        CurrentZoomLevel = Zoom;
    }
    
    private class TrackAndLabel
    {
        public GooglePoint Track = new GooglePoint();
        public GooglePoint Label = new GooglePoint();
        public string TrackColor;
        public GooglePolyline LeaderLine = new GooglePolyline();
        // Applicable only for drawing line from the
        // master (present) track to the predicted position
        public GooglePolyline TrackToPredictionLine = new GooglePolyline();

        public TrackAndLabel(Color T_Color, int LineWidth, double Lat, double Lon, string Track_ID, string Label_ID, // This has be a CALLSIGN
            string ImagePath, string ModeC)
        {
            // Track data
            Track.Latitude = Lat;
            Track.Longitude = Lon;
            Track.ID = Track_ID;
            Track.IconImage = ImagePath;

            // Label Data
            TextToImage TI = new TextToImage();
            TI.GenerateAndStore(Label_ID + Environment.NewLine + ModeC, Color.Cyan);
            Label.ID = Label_ID;
            Label.Draggable = true;
            Label.IconImage = "icons/label.png";
            
            // Please the label close the the track symbol factoring the zoom setting.
            Label.Latitude = Track.Latitude + (0.2 / CustomMap.GetScaleFactor(CurrentZoomLevel));
            Label.Longitude = Track.Longitude - (0.15 / CustomMap.GetScaleFactor(CurrentZoomLevel));
            
            // Leader line
            Color color = Color.FromName(T_Color.Name);
            LeaderLine.ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            LeaderLine.Width = LineWidth;
            LeaderLine.Points.Add(Track);
            LeaderLine.Points.Add(Label);

            // Leader line
            color = Color.FromName(T_Color.Name);
            TrackToPredictionLine.ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            TrackToPredictionLine.Width = LineWidth;
            TrackToPredictionLine.Points.Add(Track);
            
            // Here extract the predicted data based on the callsign
            // and include the predicted position s the end line
            TrackToPredictionLine.Points.Add(Label);
        }
    }

    private class TrackLabel_and_Predicted
    {
        public TrackAndLabel MasterTrack;
        public TrackAndLabel Predicted1;
        public TrackAndLabel Predicted2;
        public TrackAndLabel Predicted3;
    }

    // This is the main data storage for the current update cycle
    // It will get updated at the specified update rate and used by the page update timer
    // to obtain the latest data do be displayed.
    private static System.Collections.Generic.List<TrackLabel_and_Predicted> DisplayData = new System.Collections.Generic.List<TrackLabel_and_Predicted>();

    public TrackProvider()
    {
       
    }

    // This method is to be called upon page load to initialise 
    // the data structures
    public static void Initialise()
    {
        DisplayDataOut.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
        DisplayDataOut.Width = "900px"; // You can also specify percentage(e.g. 80%) here
        DisplayDataOut.Height = "700px";

        //Specify initial Zoom level.
        DisplayDataOut.ZoomLevel = CurrentZoomLevel;
        DisplayDataOut.AutomaticBoundaryAndZoom = false;
        //Specify Center Point for map. Map will be centered on this point.
        DisplayDataOut.CenterPoint = new GooglePoint("1", 44.00, 18.00);
    }

    public static GoogleObject GetDisplayData()
    {
        DisplayDataOut.Points.Clear();
        DisplayDataOut.Polylines.Clear();

        DisplayData.Clear();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Here build the new display list.



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DEVELOPMENT TEST code
        //
        // Define track data and add them to the master list
        TrackLabel_and_Predicted Data = new TrackLabel_and_Predicted();
        TrackAndLabel MasterTrack = new TrackAndLabel(Color.SkyBlue, 3, 44.00, 18.00, "TRACK", "LABEL", "icons/Track_Cyan.png", "285");
        TrackLabel_and_Predicted TrackWithPredicted = new TrackLabel_and_Predicted();
        TrackWithPredicted.MasterTrack = MasterTrack;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Add data to the master list
        DisplayData.Add(TrackWithPredicted);
        
        foreach (TrackLabel_and_Predicted Track in DisplayData)
        {
            DisplayDataOut.Points.Add(Track.MasterTrack.Track);
            DisplayDataOut.Points.Add(Track.MasterTrack.Label);
            DisplayDataOut.Polylines.Add(Track.MasterTrack.LeaderLine);

            if (Track.Predicted1 != null)
            {
                DisplayDataOut.Points.Add(Track.Predicted1.Track);
                DisplayDataOut.Points.Add(Track.Predicted1.Label);
                DisplayDataOut.Polylines.Add(Track.Predicted1.LeaderLine);
                DisplayDataOut.Polylines.Add(Track.Predicted1.TrackToPredictionLine);
            }

            if (Track.Predicted2 != null)
            {
                DisplayDataOut.Points.Add(Track.Predicted2.Track);
                DisplayDataOut.Points.Add(Track.Predicted2.Label);
                DisplayDataOut.Polylines.Add(Track.Predicted2.LeaderLine);
                DisplayDataOut.Polylines.Add(Track.Predicted2.TrackToPredictionLine);
            }

            if (Track.Predicted3 != null)
            {
                DisplayDataOut.Points.Add(Track.Predicted3.Track);
                DisplayDataOut.Points.Add(Track.Predicted3.Label);
                DisplayDataOut.Polylines.Add(Track.Predicted3.LeaderLine);
                DisplayDataOut.Polylines.Add(Track.Predicted3.TrackToPredictionLine);
            }
        }
        
        return DisplayDataOut;
    }
}