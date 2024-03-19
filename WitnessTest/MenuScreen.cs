using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WitnessTest
{
    public partial class MenuScreen : UserControl
    {
        #region Variables
        //sets up the playing area
        List<Wall> walls = new List<Wall>();
        List<Wall> outerWalls = new List<Wall>();

        //variables used to generate the trail
        List<Trail> trail = new List<Trail>();
        List<TrailIntersection> intersection = new List<TrailIntersection>();
        Trail trailGen;

        int state = 0;
        int prevState = 0;
        bool runIntersection = false;
        bool inIntersection = false;

        //follow circle
        Rectangle followCircle;
        Rectangle prevFollowCircle = new Rectangle();

        List<barrierGen> barriers = new List<barrierGen>();

        //brushes
        //background
        Brush redBrush = new SolidBrush(Color.Maroon);
        Brush fireBrush = new SolidBrush(Color.Firebrick);
        Brush blackBrush = new SolidBrush(Color.Black);

        //activation
        Brush coralBrush = new SolidBrush(Color.LightCoral);
        Brush yellowBrush = new SolidBrush(Color.MistyRose);

        //individual rectangles within the game
        Rectangle cursor = new Rectangle(0, 0, 1, 1);
        Rectangle bottomCircle = new Rectangle(14, 586, 50, 50);

        //exit rectangles
        Rectangle exit;

        int interX;
        int interY;

        //changes between active state modes
        bool active = false;

        //finds mouse position
        PointF mousePosition;
        PointF prevPosition;

        //bool that resets players' position
        bool wallTouch;

        #endregion

        /// playable area
        /// 24x ---------------------------|
        /// | 24p-------------------------||
        /// | | 120x | 120x | 120x | 120x ||
        /// | |---------------------------||
        /// | | 120x | 120x | 120x | 120x ||
        /// | |---------------------------||
        /// | | 120x | 120x | 120x | 120x ||
        /// | |---------------------------||
        /// | | 120x | 120x | 120x | 120x ||
        /// | |---------------------------||
        /// |------------------------------|

        public MenuScreen()
        {
            InitializeComponent();
            gameTimer.Enabled = true;

            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Wall wallSegment = new Wall(24 * (i + 2) + (i * 120), 24 * (k + 2) + (k * 120), 120, 120);
                    walls.Add(wallSegment);
                }
            }

            Wall outerLayer = new Wall(0, 0, this.Height, 24);
            outerWalls.Add(outerLayer);

            outerLayer = new Wall(0, 0, 24, this.Width);
            outerWalls.Add(outerLayer);

            outerLayer = new Wall(this.Width - 24, 0, this.Height, 24);
            outerWalls.Add(outerLayer);

            outerLayer = new Wall(0, this.Height - 24, 24, this.Width);
            outerWalls.Add(outerLayer);

            exit = new Rectangle(this.Width - 48, 6, 24, 30);
            //Cursor.Hide();


            barriers.Add(new barrierGen("x", 4, 1));
            barriers.Add(new barrierGen("y", 5, 1));
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //stops player from going through walls
            if (active == true)
            {
                wallTouch = false;
                //foreach loops that check for collisions with each rectangle that make up the levels' walls
                //resets players' position if they go out of bounds
                foreach (Wall wall in outerWalls)
                {
                    if (cursor.IntersectsWith(new Rectangle(wall.x, wall.y, wall.width, wall.height))
                        || cursor.X < 0 || cursor.Y < 0 || cursor.X > this.Width || cursor.Y > this.Height)
                    {
                        if (!cursor.IntersectsWith(exit))
                        {
                            resetPosition();
                        }
                    }
                }

                foreach (Wall wall in walls)
                {
                    if (cursor.IntersectsWith(new Rectangle(wall.x, wall.y, wall.width, wall.height)))
                    {
                        resetPosition();
                    }
                }

                for (int i = 0; i < intersection.Count - 2; i++)
                {
                    if (cursor.IntersectsWith(new Rectangle(intersection[i].x, intersection[i].y, 24, 24)))
                    {
                        resetPosition();
                    }
                    if (cursor.IntersectsWith(new Rectangle(trail[i].x, trail[i].y, trail[i].width, trail[i].height)))
                    {
                        resetPosition();
                    }
                }

                if (cursor.Y < 20) //temporary stop on finish //will replace with next level later
                {
                    resetPosition();
                }

                //attempted fix for clipping issue
                /*
                if ((state == 1 || state == 2) && (prevState == 3 || prevState == 4) && inIntersection == false) //from x to y
                {
                    if (prevState == 3 && cursor.X - 100 > intersection.Last().x) //right
                    {
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x + 136, intersection.Last().y + 12));
                    }
                    else if (prevState == 4 && cursor.X + 100 > intersection.Last().x) //left
                    {
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x - 136, intersection.Last().y + 12));
                    }
                    wallTouch = true;
                }
                if ((state == 3 || state == 4) && (prevState == 1 || prevState == 2) && inIntersection == false) //from y to x
                {
                    if (prevState == 1 && cursor.Y - 100 > intersection.Last().y) //down
                    {
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x + 12, intersection.Last().y + 136));
                    }
                    else if (prevState == 2 && cursor.Y + 100 > intersection.Last().y) //up
                    {
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x + 12, intersection.Last().y - 136));
                    }
                    wallTouch = true;
                }
                */
                
                

                inIntersection = false;
                int prevListCount = intersection.Count;

                //intersection point generation
                for (int i = 0; i < 5; i++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        //intersection points generation code
                        if (cursor.X >= 24 + (k * 144) && cursor.X <= 48 + (k * 144) && cursor.Y >= 24 + (i * 144) && cursor.Y <= 48 + (i * 144))
                        {
                            //converts position to an int so that the extact x y can be placed if the player is anywhere in the 24 / 24 square
                            interX = (int)(cursor.X / 144);
                            interY = (int)(cursor.Y / 144);
                            inIntersection = true;

                            //adds to class
                            intersection.Add(new TrailIntersection(24 + (interX * 144), 24 + (interY * 144)));

                            HashSet<TrailIntersection> duplicateFinder = new HashSet<TrailIntersection>(); //create a hashset to contain and compare to the rectangle list

                            foreach (TrailIntersection trailInt in intersection) //add to list
                            {
                                duplicateFinder.Add(trailInt);
                            }

                            if (duplicateFinder.Count != intersection.Count) //checks if any duplicates were detected, and deletes the last count if so to prevent clogging list
                            {
                                intersection.Clear();
                                foreach (TrailIntersection trailInt in duplicateFinder)
                                {
                                    intersection.Add(trailInt);
                                }
                            }

                            goto BREAK;
                        }
                    }
                }
            BREAK:

                if (trail.Count > 2)
                {
                    int hamburgler = 0;
                }

                prevState = state;

                //trail generation code
                for (int i = 0; i < 5; i++)
                {
                    if (inIntersection == false) //checks if the player is at an intersection point
                    {
                        runIntersection = false;
                        if (cursor.X >= 24 + (i * 144) && cursor.X <= 48 + (i * 144)) //within Y regions
                        {
                            if (intersection.Last().y + 24 < cursor.Y) //cursor moving down
                            {
                                state = 1;
                                trailGen = new Trail(24 + (i * 144), intersection.Last().y + 24, 24, cursor.Y - (intersection.Last().y + 24), "down");
                                resetTrail();
                                break;
                            }
                            else if (intersection.Last().y > cursor.Y) //cursor moving up
                            {
                                state = 2;
                                trailGen = new Trail(24 + (i * 144), cursor.Y, 24, intersection.Last().y + 24 - cursor.Y, "up");
                                resetTrail();
                                break;
                            }
                        }
                        else if (cursor.Y >= 24 + (i * 144) && cursor.Y <= 48 + (i * 144)) //within X regions
                        {
                            if (intersection.Last().x + 24 < cursor.X) //moving right
                            {
                                state = 3;
                                trailGen = new Trail(intersection.Last().x + 24, 24 + (i * 144), cursor.X - (intersection.Last().x + 24), 24, "right");
                                resetTrail();
                                break;
                            }
                            else if (intersection.Last().x > cursor.X)
                            {
                                state = 4;
                                trailGen = new Trail(cursor.X, 24 + (i * 144), intersection.Last().x - cursor.X, 24, "left");
                                resetTrail();
                                break;
                            }
                        }
                    }
                    else if (runIntersection == false && intersection.Count > 1 && prevListCount != intersection.Count) //is in intersection point
                    {
                        runIntersection = true;
                        trail.Remove(trail.Last());
                        switch (state)
                        {
                            case 0:
                                break;
                            case 1: //down in x
                                trail.Add(new Trail(intersection[intersection.Count - 2].x, intersection[intersection.Count - 2].y + 24, 24, 144, "down"));
                                break;
                            case 2: //up in x
                                trail.Add(new Trail(intersection.Last().x, intersection.Last().y + 24, 24, 144, "up"));
                                break;
                            case 3: //right in y
                                trail.Add(new Trail(intersection[intersection.Count - 2].x + 24, intersection[intersection.Count - 2].y, 144, 24, "right"));
                                break;
                            case 4: //left in y
                                trail.Add(new Trail(intersection.Last().x + 24, intersection.Last().y, 144, 24, "left"));
                                break;
                        }
                        state = 0;
                        trail.Add(new Trail(0, 0, 0, 0, "null"));
                    }
                }

                //reverse code
                if (trail.Count > 1)
                {
                    if (trail.Last().state == "right" && trail[trail.Count - 2].state == "left")
                    {
                        prevRemoval();
                    }
                    else if (trail.Last().state == "left" && trail[trail.Count - 2].state == "right")
                    {
                        prevRemoval();
                    }
                    else if (trail.Last().state == "up" && trail[trail.Count - 2].state == "down")
                    {
                        prevRemoval();
                    }
                    else if (trail.Last().state == "down" && trail[trail.Count - 2].state == "up")
                    {
                        prevRemoval();
                    }
                }
                


                //gets players previous position if they are in the given playing field
                if (wallTouch == false)
                    {
                        prevPosition = mousePosition;
                    }
            }
            Refresh();
        }

        private void resetPosition()
        {
            Cursor.Position = this.PointToScreen(new Point((int)prevPosition.X, (int)prevPosition.Y));
            wallTouch = true;
        }

        private void resetTrail()
        {
            trail.Remove(trail.Last());
            trail.Add(trailGen);
        }

        private void prevRemoval()
        {
            trail.Remove(trail.Last());
            intersection.Remove(intersection.Last());
        }

        private void MenuScreen_Paint(object sender, PaintEventArgs e)
        {
            //creates walls that the game contains itself in
            foreach (Wall wall in walls)
            {
                e.Graphics.FillRectangle(redBrush, new Rectangle(wall.x, wall.y, wall.width, wall.height));
            }

            foreach (Wall wall in outerWalls)
            {
                e.Graphics.FillRectangle(blackBrush, new Rectangle(wall.x, wall.y, wall.width, wall.height));
            }

            foreach (barrierGen barrier in barriers)
            {
                if (barrier.type == "x")
                {
                    e.Graphics.FillRectangle(redBrush, new Rectangle(80 + ((barrier.xLocation - 1) * 144),
                        24 + ((barrier.yLocation - 1) * 144), 56, 24));
                }
                else
                {
                    e.Graphics.FillRectangle(redBrush, new Rectangle(24 + ((barrier.xLocation - 1) * 144),
                        80 + ((barrier.yLocation - 1) * 144), 24, 56));
                }
            }

            //creates exit ellipse
            e.Graphics.FillEllipse(fireBrush, exit);

            //draws the trail aspects
            foreach (Trail trail in trail)
            {
                e.Graphics.FillRectangle(yellowBrush, new Rectangle(trail.x, trail.y, trail.width, trail.height));
            }

            foreach (TrailIntersection trailInt in intersection)
            {
                e.Graphics.FillRectangle(yellowBrush, new Rectangle(trailInt.x, trailInt.y, trailInt.width, trailInt.height));
            }

            //changes the colour of the starting circle based on its current state
            if (active == true) //active game state
            {
                e.Graphics.FillEllipse(yellowBrush, bottomCircle);
            }
            else if (cursor.IntersectsWith(bottomCircle)) //hovering over the start button
            {
                e.Graphics.FillEllipse(coralBrush, bottomCircle);
            }
            else //not hovering or playing
            {
                e.Graphics.FillEllipse(fireBrush, bottomCircle);
            }

            //circle that follows the player
            if (active == true)
            {
                bool checker;
                checker = findCircle();

                if (checker == true)
                {
                    e.Graphics.FillEllipse(yellowBrush, followCircle);
                }
                else
                {
                    e.Graphics.FillEllipse(yellowBrush, prevFollowCircle);
                }

            }
            else
            {
                e.Graphics.FillEllipse(yellowBrush, new Rectangle(cursor.X - 12, cursor.Y - 12, 24, 24));
            }
        }

        public bool findCircle()
        {
            int yPos;
            int xPos;
            bool output;

            //stops ellipse from clipping into walls
            yPos = cursor.Y - 12;
            if (cursor.Y <= 36 && !(cursor.X >= 600) && !(cursor.X <= 624))
            {
                yPos = 24;
            }
            else if (cursor.Y >= 612)
            {
                yPos = 600;
            }

            xPos = cursor.X - 12;
            if (cursor.X <= 36)
            {
                xPos = 24;
            }
            else if (cursor.X >= 612)
            {
                xPos = 600;
            }

            //checks if the ellipse is in the horizontal or vertical regions of the game and draws the ellipse accordingly
            prevFollowCircle = followCircle;

            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if ((cursor.X - (24 + (k * 144))) * (48 + (k * 144) - cursor.X) >= 0 && (cursor.Y - (24 + (i * 144))) * (48 + (i * 144) - cursor.Y) >= 0) //works
                    {
                        int xCoord = (int)(cursor.X / 144);
                        int yCoord = (int)(cursor.Y / 144);

                        followCircle = new Rectangle(24 + (xCoord * 144), 24 + (yCoord * 144), 24, 24);
                        output = true; return output;
                    }
                }
            }
            //checks otherwise if the cursor is within the x or y quadrants of the game
            for (int i = 0; i < 5; i++)
            {
                if ((cursor.Y - (24 + (i * 144))) * (48 + (i * 144) - cursor.Y) >= 0)
                {
                    followCircle = new Rectangle(xPos, 24 + (i * 144), 24, 24);
                    output = true; return output;
                }
            }
            for (int k = 0; k < 5; k++)
            {
                if ((cursor.X - (24 + (k * 144))) * (48 + (k * 144) - cursor.X) >= 0)
                {
                    followCircle = new Rectangle(24 + (k * 144), yPos, 24, 24);
                    output = true; return output;
                }
            }

            return false;
        }

        private void MenuScreen_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = this.PointToClient(Cursor.Position);

            cursor = new Rectangle(Convert.ToInt16(mousePosition.X), Convert.ToInt16(mousePosition.Y), 1, 1);

            xInput.Text = mousePosition.X.ToString();
            yInput.Text = mousePosition.Y.ToString();

            if (trail.Count > 1)
            {
                rectXPos.Text = trail[1].width.ToString();
                rectYPos.Text = trail[1].height.ToString();
            }

            listLabel.Text = trail.Count.ToString();
            listLabel2.Text = intersection.Count.ToString();
        }

        private void MenuScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && active == true)
            {
                intersection.Clear();
                trail.Clear();
                active = false;
            }

            if (e.Button == MouseButtons.Left && cursor.IntersectsWith(bottomCircle))
            {
                active = true;
                prevPosition = mousePosition;
                Cursor.Position = this.PointToScreen(new Point(bottomCircle.X + bottomCircle.Width / 2, bottomCircle.Y + bottomCircle.Height / 2));

                //adds starting rectangles in each list to prevent empty list errors
                trail.Add(new Trail(0, 0, 0, 0, "null"));
                intersection.Add(new TrailIntersection(24, 600));
            }
        }

        private void MenuScreen_KeyDown(object sender, KeyEventArgs e)
        {
            //closes upon escaping
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}
