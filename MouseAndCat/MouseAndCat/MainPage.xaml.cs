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
        private const int NUMBER_CATS = 4;
        Image currentPiece;

        public MainPage()
        {
            InitializeComponent();
            SetUpTheGrid();
            PutStuffOnTheGrid();
            AddPlayingPieces();

            //AddPiecesToGrid();
        }

        private void AddPlayingPieces()
        {
            Image piece;
            TapGestureRecognizer tapGR = new TapGestureRecognizer();
            tapGR.Tapped += MouseCat_Tapped;
            var assembly = typeof(MainPage);

            // create the mouse
            piece = new Image();
            piece.Source = ImageSource.FromResource("MouseAndCat.Assets.Images.mouse.png", assembly);
            CreatePiece(piece, tapGR, NUMBER_R - 1, 2, "mouse");
            // add it to the board
            grdRuntime.Children.Add(piece);

            for(int i = 1; i < NUMBER_C; i+=2)
            {
                piece = new Image();
                piece.Source = ImageSource.FromResource("MouseAndCat.Assets.Images.cats.png", assembly);
                CreatePiece(piece, tapGR, 0, i, "cat" + i.ToString());
                // add it to the board
                grdRuntime.Children.Add(piece);

            }

        }

        private static void CreatePiece(Image piece, 
                                        TapGestureRecognizer tapGR, 
                                        int r,
                                        int c,
                                        string styleId)


        {
            piece.GestureRecognizers.Add(tapGR);
            piece.StyleId = styleId;
            piece.HeightRequest = 90;
            piece.WidthRequest = 90;
            piece.Aspect = Aspect.AspectFit;
            // set the grid.row and grid.col
            piece.SetValue(Grid.RowProperty, r);
            piece.SetValue(Grid.ColumnProperty, c);
        }


        #region Set Up the Grid and the Frames
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
            grdRuntime.RowSpacing = 0;
            grdRuntime.ColumnSpacing = 0;
            grdRuntime.Margin = 5;
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
            tapGestureRecognizer.Tapped += EmptySquare_Tapped;


            // set up the two loops to create the frames.
            for( int r = 0; r < NUMBER_R; r++)
            {
                for( int c = 0; c < NUMBER_C; c++)
                {
                    // add a frame in row r, column c
                    frame = new Frame();
                    // set the Id
                    frame.StyleId = "R" + r + "_C" + c;
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
        #endregion



        #region Other Stuff to Add

        private void AddPiecesToGrid()
        {
            // black squares are where to place
            // add the mouse at the bottom
            var assembly = typeof(MainPage);
            Image image;
            TapGestureRecognizer tapGestureRecognizer;
            tapGestureRecognizer = new TapGestureRecognizer();
            // add the event handler for the tap event to the recogniser
            tapGestureRecognizer.Tapped += MouseCat_Tapped;

            image = new Image();
            image.WidthRequest = 100;
            image.HeightRequest = 100;
            image.Aspect = Aspect.AspectFit;
            image.SetValue(Grid.RowProperty, NUMBER_R - 1);
            image.SetValue(Grid.ColumnProperty, 4);
            image.StyleId = "mouse";

            image.Source = ImageSource.FromResource("MouseAndCat.Assets.Images.mouse.png", assembly);
            image.GestureRecognizers.Add(tapGestureRecognizer);
            grdRuntime.Children.Add(image);

        }

        private void MouseCat_Tapped(object sender, EventArgs e)
        {
            // which squares light up
            Image piece = (Image)sender;
            currentPiece = piece;
            int r, c;
            int highlightR = -1, highlightCLeft, highlightCRight;
            r = (int)piece.GetValue(Grid.RowProperty);
            c = (int)piece.GetValue(Grid.ColumnProperty);

            if( piece.StyleId == "mouse")
            {
                if ((r - 1) >= 0)
                {
                    highlightR = r - 1;
                }
                else
                {
                    return;
                }
            }
            else
            {
                // it's a cat
                if ((r - 1) <= NUMBER_R - 1)
                {
                    highlightR = r + 1;
                }
                else
                {
                    return;
                }
            }
            // work out the columns
            if (c == 0)
            {
                highlightCLeft = -1;
                highlightCRight = c + 1;
                HightlightSquare(highlightR, highlightCRight);
            }
            else if (c == (NUMBER_C - 1))
            {   // if piece is on the right
                highlightCLeft = c - 1;
                highlightCRight = -1;
                HightlightSquare(highlightR, highlightCLeft);
            }
            else if ((c > 0) && (c < NUMBER_C - 1))
            {
                highlightCRight = c + 1;
                highlightCLeft = c - 1;
                HightlightSquare(highlightR, highlightCLeft);

                HightlightSquare(highlightR, highlightCRight);
            }


        }

        private void HightlightSquare(int highlightR, int highlightCRight)
        {
            // R0_C0
            string idName = "R" + highlightR + "_C" + highlightCRight;
            foreach (var item in grdRuntime.Children)
            {
                if (item.StyleId == idName)
                {
                    item.BackgroundColor = Color.Red;
                }
            }
        }

        private void EmptySquare_Tapped(object sender, EventArgs e)
        {
            //change the colour to Red
            // get a handle to the sender object
            Frame current = (Frame)sender;
            if( current.BackgroundColor == Color.Red)
            {
                currentPiece.SetValue(Grid.RowProperty, current.GetValue(Grid.RowProperty));
                currentPiece.SetValue(Grid.ColumnProperty, current.GetValue(Grid.ColumnProperty));
            }
            foreach (var item in grdRuntime.Children)
            {
                if( item.BackgroundColor == Color.Red)
                {
                    item.BackgroundColor = Color.Black;
                }
            }

        }
        #endregion
    }
}
