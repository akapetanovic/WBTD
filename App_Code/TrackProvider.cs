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

    public static bool PredictionEngine1_Enabled = false;
    public static bool PredictionEngine2_Enabled = false;
    public static bool PredictionEngine3_Enabled = false;
    public static bool CustomMapEnabled_Enabled = false;

    private static double Lat = 44.0;

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
        public GooglePoint PredictionSymbol_1 = null;
        public GooglePoint PredictionSymbol_2 = null;
        public GooglePoint PredictionSymbol_3 = null;
        public GooglePolyline TrackToPredictionLine1 = new GooglePolyline();
        public GooglePolyline TrackToPredictionLine2 = new GooglePolyline();
        public GooglePolyline TrackToPredictionLine3 = new GooglePolyline();

        public TrackAndLabel(int LineWidth, double Lat, double Lon, string Track_ID, string Label_ID, // This has be a CALLSIGN
                            string ModeC)
        {
            // Track data
            Track.Latitude = Lat;
            Track.Longitude = Lon;
            Track.ID = Track_ID;
            Track.IconImage = "icons/Track_Green.png";
          
            // Label Data
            TextToImage TI = new TextToImage();
            TI.GenerateAndStore(Label_ID, Label_ID + Environment.NewLine + ModeC, Color.Green);
            Label.ID = Label_ID;
            Label.Draggable = true;
            Label.IconImage = "icons/labels/" + Label_ID + ".png";

            // Place the label close the the track symbol factoring in the zoom setting.
            Label.Latitude = Track.Latitude + (0.2 / CustomMap.GetScaleFactor(CurrentZoomLevel));
            Label.Longitude = Track.Longitude - (0.15 / CustomMap.GetScaleFactor(CurrentZoomLevel));
            
            // Leader line
            Color color = Color.FromName(Color.Green.Name);
            LeaderLine.ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            LeaderLine.Width = LineWidth;
            LeaderLine.ID = "L1";
            LeaderLine.Points.Add(Track);
            LeaderLine.Points.Add(Label);

            // Track prediction symbol and line 1
            if (PredictionEngine1_Enabled)
            {
                PredictionSymbol_1 = new GooglePoint();
                PredictionSymbol_1.Latitude = Track.Latitude + 0.050;
                PredictionSymbol_1.Longitude = Track.Longitude + 0.020;
                PredictionSymbol_1.ID = "P1";
                PredictionSymbol_1.IconImage = "icons/Track_Yellow.png";
                color = Color.FromName(Color.Yellow.Name);
                TrackToPredictionLine1.ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
                TrackToPredictionLine1.ID = "L2";
                TrackToPredictionLine1.Width = LineWidth;
                TrackToPredictionLine1.Points.Add(Track);
                TrackToPredictionLine1.Points.Add(PredictionSymbol_1);
            }

            // Track prediction symbol and line 1
            if (PredictionEngine2_Enabled)
            {
                PredictionSymbol_2 = new GooglePoint();
                PredictionSymbol_2.Latitude = Track.Latitude + 0.060;
                PredictionSymbol_2.Longitude = Track.Longitude + 0.040;
                PredictionSymbol_2.ID = "P2";
                PredictionSymbol_2.IconImage = "icons/Track_Blue.png";
                color = Color.FromName(Color.Blue.Name);
                TrackToPredictionLine2.ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
                TrackToPredictionLine2.ID = "L3";
                TrackToPredictionLine2.Width = LineWidth;
                TrackToPredictionLine2.Points.Add(Track);
                TrackToPredictionLine2.Points.Add(PredictionSymbol_2);
            }

            // Track prediction symbol and line 1
            if (PredictionEngine3_Enabled)
            {
                PredictionSymbol_3 = new GooglePoint();
                PredictionSymbol_3.Latitude = Track.Latitude + 0.070;
                PredictionSymbol_3.Longitude = Track.Longitude + 0.060;
                PredictionSymbol_3.ID = "P3";
                PredictionSymbol_3.IconImage = "icons/Track_Pink.png";
                color = Color.FromName(Color.Pink.Name);
                TrackToPredictionLine3.ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
                TrackToPredictionLine3.ID = "L4";
                TrackToPredictionLine3.Width = LineWidth;
                TrackToPredictionLine3.Points.Add(Track);
                TrackToPredictionLine3.Points.Add(PredictionSymbol_3);
            }

        }
    }

    // This is the main data storage for the current update cycle
    // It will get updated at the specified update rate and used by the page update timer
    // to obtain the latest data do be displayed.
    private static System.Collections.Generic.List<TrackAndLabel> DisplayData = new System.Collections.Generic.List<TrackAndLabel>();

    public TrackProvider()
    {
       
    }

    // This method is to be called upon page load to initialise 
    // the data structures
    public static void Initialise()
    {
        DisplayDataOut.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
        DisplayDataOut.Width = "1000px"; // You can also specify percentage(e.g. 80%) here
        DisplayDataOut.Height = "800px";

        //Specify initial Zoom level.
        DisplayDataOut.ZoomLevel = CurrentZoomLevel;
        DisplayDataOut.AutomaticBoundaryAndZoom = false;
        //Specify Center Point for map. Map will be centered on this point.
        DisplayDataOut.CenterPoint = new GooglePoint("1", 44.00, 18.00);

        DisplayDataOut.Polylines.Capacity = 1000;
        DisplayDataOut.Polygons.Capacity = 1000;
        DisplayDataOut.Points.Capacity = 4000;

    }

    public static GoogleObject GetDisplayData()
    {
        DisplayDataOut.Polylines.Clear();
        DisplayDataOut.Points.Clear();

      
        
        if (CustomMapEnabled_Enabled && DisplayDataOut.Polygons.Count == 0)
        {
            DisplayDataOut.Polygons.Add(CustomMap.GetBlankPoligon());
            DisplayDataOut.Polygons.Add(CustomMap.GetStatePoligon());
        }
        else if (CustomMapEnabled_Enabled == false)
        {
            DisplayDataOut.Polygons.Clear();
        }

        DisplayData.Clear();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Here build the new display list.


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DEVELOPMENT TEST code
        //
        // Define track data and add them to the master list
        TrackAndLabel MasterTrack = new TrackAndLabel(2, Lat, 18.00, "REAL", "LABEL1", "285");
       
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Add data to the master list
        DisplayData.Add(MasterTrack);

        Lat = Lat + 0.020;

        foreach (TrackAndLabel Track in DisplayData)
        {
            DisplayDataOut.Points.Add(Track.Track);
            DisplayDataOut.Points.Add(Track.Label);
            DisplayDataOut.Polylines.Add(Track.LeaderLine);

            if (Track.PredictionSymbol_1 != null)
            {
                DisplayDataOut.Points.Add(Track.PredictionSymbol_1);
               DisplayDataOut.Polylines.Add(Track.TrackToPredictionLine1);
            }

            if (Track.PredictionSymbol_2 != null)
            {
                DisplayDataOut.Points.Add(Track.PredictionSymbol_2);
                DisplayDataOut.Polylines.Add(Track.TrackToPredictionLine2);
            }

            if (Track.PredictionSymbol_3 != null)
            {
                DisplayDataOut.Points.Add(Track.PredictionSymbol_3);
                DisplayDataOut.Polylines.Add(Track.TrackToPredictionLine3);
            }
        }
        
        return DisplayDataOut;
    }
}