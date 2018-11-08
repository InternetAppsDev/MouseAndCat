using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MouseAndCat
{
    public partial class MainPage : ContentPage
    {
        // == global constants ==
        private const int NUMBER_R = 8;
        private const int NUMBER_C = 8;



        public MainPage()
        {
            InitializeComponent();
            SetUpTheGrid();
            PutStuffOnTheGrid();
        }

        private void SetUpTheGrid()
        {
            /*
             * height, width, spacing, horizontal and vertical
             *         <Grid x:Name="grdDesign" IsVisible="False" 
              HeightRequest="400" WidthRequest="400" 
              HorizontalOptions="Center" VerticalOptions="Center" >

             */
            grdRuntime.HeightRequest = 400;
            grdRuntime.WidthRequest = 400;
            grdRuntime.RowSpacing = 5;
            grdRuntime.ColumnSpacing = 5;
            grdRuntime.HorizontalOptions = LayoutOptions.Center;
            grdRuntime.VerticalOptions = LayoutOptions.Center;

            // add the row and column definitions
            // create the two collections
            // <Grid.ColumnDefinitions></Grid.ColumnDefinitions>
            grdRuntime.ColumnDefinitions = new ColumnDefinitionCollection();
            // rows
            grdRuntime.RowDefinitions = new RowDefinitionCollection();

            // create the rows and the columns
            for( int r = 0; r < NUMBER_R; r++ )
            {
                // <RowDefinition />
                //RowDefinition rd = new RowDefinition();
                //grdRuntime.RowDefinitions.Add(rd);
                grdRuntime.RowDefinitions.Add(new RowDefinition());
            }

            for( int c = 0; c < NUMBER_C; c++)
            {
                grdRuntime.ColumnDefinitions.Add(new ColumnDefinition());
            }


        }

        private void PutStuffOnTheGrid()
        {
            /*
             * put the frames on the grid.  
             * need a Frame variable that is instantiated to be 
             * placed on the grid.
             *<Frame x:Name="frm_R0_C0" BackgroundColor="Black" 
                   Grid.Row="0" Grid.Column="0" >
                <Frame.GestureRecognizers>
                    <TapGestureRecognizer Tapped="TapGestureRecognizer_Tapped"
                                          NumberOfTapsRequired="1"/>
                </Frame.GestureRecognizers>
            </Frame>
             */
            Frame frame;

            /*
             * loops need to create NxN rows cols.
             * need to decide between b/w colours
             * Use the same tap gesture recogniser on all frames
             *      This means the creation of the tap gesture is not
             *      in the loop
             */
            // create the tap gesture recogniser.
            // set the tap gesture recogniser
            // the recogniser is an object that is added to the frame
            TapGestureRecognizer tapGestureRecognizer;
            tapGestureRecognizer = new TapGestureRecognizer();
            // add the event handler for the tap event to the recogniser
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;


            // set up the two loops to create the frames.
            for( int r = 0; r < NUMBER_R; r++)
            {
                for( int c = 0; c < NUMBER_C; c++)
                {
                    // add a frame in row r, column c
                    frame = new Frame();
                    // set the colour
                    frame.BackgroundColor = Color.White;
                    if( ((r + c) % 2) != 0 )
                    {
                        frame.BackgroundColor = Color.Black;
                        // add the recogniser to the frame
                        frame.GestureRecognizers.Add(tapGestureRecognizer);
                    }
                    // set the row, col coordinates
                    // composite properties - use SetValue
                    frame.SetValue(Grid.RowProperty, r);
                    frame.SetValue(Grid.ColumnProperty, c);

                    // put the frame on the grid.
                    // add to the children collection of some parent object
                    // add to the grdRuntime
                    grdRuntime.Children.Add(frame);
                } // end for(int c = 0;
            } // end for(int r = 0;
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //change the colour to Red
            // get a handle to the sender object
            Frame current = (Frame)sender;
            current.BackgroundColor = Color.Red;

        }
    }
}
