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
    public partial class GameScreen : UserControl
    {
        #region Variables
        //sets up the playing area
        List<Wall> walls = new List<Wall>();
        List<Wall> outerWalls = new List<Wall>();
        List<barrierGen> barriers = new List<barrierGen>();

        //variables used to generate the trail
        List<Trail> trail = new List<Trail>();
        List<TrailIntersection> intersection = new List<TrailIntersection>();
        Trail trailGen;

        List<Trail> oppositeTrail = new List<Trail>();
        List<TrailIntersection> oppositeIntersection = new List<TrailIntersection>();

        int state = 0;
        int prevState = 0;
        bool runIntersection = false;
        bool inIntersection = false;

        //follow circle
        Rectangle oppositeFollowCircle = new Rectangle();
        Rectangle followCircle;
        Rectangle prevFollowCircle = new Rectangle();

        //brushes
        //background
        Brush blackBrush = new SolidBrush(Color.Black);

        //level-dependant brushes
        Brush wallBrush;
        Brush backgroundBrush;
        Brush hoverBrush;
        Brush trailBrush;

        //individual rectangles within the game
        Rectangle cursor = new Rectangle(0, 0, 1, 1);
        Rectangle bottomCircle = new Rectangle(14, 586, 50, 50);

        //exit rectangles
        Rectangle exit;
        Rectangle exit2;

        int interX;
        int interY;

        //changes between active state modes
        bool active = false;

        //finds mouse position
        PointF mousePosition;
        PointF prevPosition;

        //bool that resets players' position
        bool wallTouch;

        //tracks player's level
        int level = 0;

        bool initialRunStopListError = false;
        bool invertedDoubleState = false;

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

        public GameScreen()
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

            levelGen();
            Cursor.Hide();
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
                        if (!cursor.IntersectsWith(exit) && !cursor.IntersectsWith(exit2))
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

                foreach (barrierGen barrier in barriers)
                {
                    if (cursor.IntersectsWith(new Rectangle(barrier.x, barrier.y, barrier.width, barrier.height)))
                    {
                        resetPosition();
                    }
                    Rectangle oppositeCursor = new Rectangle(this.Width - cursor.X, this.Height - cursor.Y, 1, 1);
                    if (oppositeCursor.IntersectsWith(new Rectangle(barrier.x, barrier.y, barrier.width, barrier.height)) && invertedDoubleState)
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

                for (int i = 0; i < oppositeIntersection.Count; i++)
                {
                    if (cursor.IntersectsWith(new Rectangle(oppositeIntersection[i].x, oppositeIntersection[i].y, 24, 24)))
                    {
                        resetPosition();
                    }
                }

                if (invertedDoubleState && cursor.IntersectsWith(new Rectangle(308, 308, 32, 32)))
                {
                    resetPosition();
                }

                //attempted fix for clipping issue
                /*
                if ((state == 1 || state == 2) && (prevState == 3 || prevState == 4)) //from x to y
                {
                    if (prevState == 3 && (cursor.X < intersection.Last().x - 60 || intersection.Last().x + 84 < cursor.X)) //right
                    {
                        //Cursor.Position = this.PointToScreen(new Point(intersection.Last().x + 136, intersection.Last().y + 12));
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x + 132, intersection.Last().y + 12));
                    }
                    else if (prevState == 4 && (cursor.X < intersection.Last().x - 60 || intersection.Last().x + 84 < cursor.X)) //left
                    {
                        //Cursor.Position = this.PointToScreen(new Point(intersection.Last().x - 136, intersection.Last().y + 12));
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x - 108, trail.Last().y + 12));
                    }
                    wallTouch = true;
                }
                
                if ((state == 3 || state == 4) && (prevState == 1 || prevState == 2)) //from y to x
                {
                    if (prevState == 1 && (cursor.Y < intersection.Last().y - 60 || intersection.Last().y + 84 < cursor.Y)) //down
                    {
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x + 12, intersection.Last().y + 132));
                    }
                    else if (prevState == 2 && (cursor.Y < intersection.Last().y - 60 || intersection.Last().y + 84 < cursor.Y)) //up
                    {
                        Cursor.Position = this.PointToScreen(new Point(intersection.Last().x + 12, intersection.Last().y - 108));
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

                foreach (TrailIntersection trailInt in intersection)
                {
                    if (trailInt.x == 312 && trailInt.y == 312 && invertedDoubleState)
                    {
                        intersection.Remove(trailInt);
                        oppositeIntersection.Remove(trailInt);
                        resetPosition();
                        break;
                    }
                    if (oppositeIntersection.Contains(trailInt))
                    {
                        intersection.Remove(trailInt);
                        resetPosition();
                        break;
                    }
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
                            case 1: //down in y
                                trail.Add(new Trail(intersection[intersection.Count - 2].x, intersection[intersection.Count - 2].y + 24, 24, 120, "down"));
                                break;
                            case 2: //up in y
                                trail.Add(new Trail(intersection.Last().x, intersection.Last().y + 24, 24, 120, "up"));
                                break;
                            case 3: //right in x
                                trail.Add(new Trail(intersection[intersection.Count - 2].x + 24, intersection[intersection.Count - 2].y, 120, 24, "right"));
                                break;
                            case 4: //left in x
                                trail.Add(new Trail(intersection.Last().x + 24, intersection.Last().y, 120, 24, "left"));
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


                //opposite code
                if (level >= 6 && trail.Count > 0)
                {
                    oppositeTrail.Clear();
                    oppositeIntersection.Clear();

                    if (invertedDoubleState)
                    {
                        oppositeFollowCircle = new Rectangle(this.Width - followCircle.X - 24, this.Height - followCircle.Y - 24, 24, 24);
                        foreach (Trail trails in trail)
                        {
                            switch (trails.state)
                            {
                                case "up":
                                    oppositeTrail.Add(new Trail(this.Width - trails.x - trails.width, this.Height - trails.y - trails.height, 24, trails.height, "down"));
                                    break;
                                case "down":
                                    oppositeTrail.Add(new Trail(this.Width - trails.x - trails.width, this.Height - trails.y - trails.height, 24, trails.height, "up"));
                                    break;
                                case "left":
                                    oppositeTrail.Add(new Trail(this.Width - trails.x - trails.width, this.Height - trails.y - trails.height, trails.width, 24, "right"));
                                    break;
                                case "right":
                                    oppositeTrail.Add(new Trail(this.Width - trails.x - trails.width, this.Height - trails.y - trails.height, trails.width, 24, "left"));
                                    break;
                            }
                        }

                        foreach (TrailIntersection trailInt in intersection)
                        {
                            oppositeIntersection.Add(new TrailIntersection(this.Width - trailInt.x - 24, this.Height - trailInt.y - 24));
                        }
                    }

                    if (oppositeIntersection.Count > 2)
                    {
                        int tester = 0;
                    }
                }

                if (cursor.IntersectsWith(new Rectangle(exit.X, exit.Y - (exit.Height / 2), exit.Width, exit.Height)) && level <= 5)
                {
                    levelGen();
                }
                else if ((cursor.IntersectsWith(new Rectangle(exit.X + 20, exit.Y, exit.Width, exit.Height)) ||
                         cursor.IntersectsWith(new Rectangle(0, exit2.Y, 18, 24))) && level >= 6 && 10 >= level)
                {
                    levelGen();
                }
            }
            Refresh();
        }

        private void resetPosition()
        {
            if (trail.Last().state == "up" || trail.Last().state == "down")
            {
                Cursor.Position = this.PointToScreen(new Point(trail.Last().x + 12, (int)prevPosition.Y));
                wallTouch = true;
            }
            else
            {
                Cursor.Position = this.PointToScreen(new Point((int)prevPosition.X, trail.Last().y + 12));
                wallTouch = true;
            }
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
                e.Graphics.FillRectangle(wallBrush, new Rectangle(wall.x, wall.y, wall.width, wall.height));
            }

            foreach (Wall wall in outerWalls)
            {
                e.Graphics.FillRectangle(blackBrush, new Rectangle(wall.x, wall.y, wall.width, wall.height));
            }

            foreach (barrierGen barrier in barriers)
            {
                e.Graphics.FillRectangle(wallBrush, new Rectangle(barrier.x, barrier.y, barrier.width, barrier.height));
            }

            //creates exit ellipse
            e.Graphics.FillEllipse(backgroundBrush, exit);

            if (invertedDoubleState)
            {
                e.Graphics.FillEllipse(backgroundBrush, new Rectangle(6, 168, 30, 24));
            }

            //draws the trail aspects
            foreach (Trail trail in trail)
            {
                e.Graphics.FillRectangle(trailBrush, new Rectangle(trail.x, trail.y, trail.width, trail.height));
            }

            foreach (Trail trail in oppositeTrail)
            {
                e.Graphics.FillRectangle(trailBrush, new Rectangle(trail.x, trail.y, trail.width, trail.height));
            }

            foreach (TrailIntersection trailInt in intersection)
            {
                e.Graphics.FillRectangle(trailBrush, new Rectangle(trailInt.x, trailInt.y, trailInt.width, trailInt.height));
            }

            foreach (TrailIntersection trailInt in oppositeIntersection)
            {
                e.Graphics.FillRectangle(trailBrush, new Rectangle(trailInt.x, trailInt.y, trailInt.width, trailInt.height));
            }

            //changes the colour of the starting circle based on its current state
            if (active == true) //active game state
            {
                e.Graphics.FillEllipse(trailBrush, bottomCircle);
            }
            else if (cursor.IntersectsWith(bottomCircle)) //hovering over the start button
            {
                e.Graphics.FillEllipse(hoverBrush, bottomCircle);
            }
            else //not hovering or playing
            {
                e.Graphics.FillEllipse(backgroundBrush, bottomCircle);
            }

            //circle that follows the player
            if (active == true)
            {
                bool checker;
                checker = findCircle();

                if (checker == true)
                {
                    e.Graphics.FillEllipse(trailBrush, followCircle);
                }
                else
                {
                    e.Graphics.FillEllipse(trailBrush, prevFollowCircle);
                }
                e.Graphics.FillEllipse(trailBrush, oppositeFollowCircle);
            }
            else
            {
                e.Graphics.FillEllipse(trailBrush, new Rectangle(cursor.X - 12, cursor.Y - 12, 24, 24));
            }
        }

        public bool findCircle()
        {
            int yPos;
            int xPos;
            bool output;

            //stops ellipse from clipping into walls
            yPos = cursor.Y - 12;
            if (cursor.Y <= 36)
            {
                if  (cursor.X >= 600 && cursor.X <= 624 && level >= 5)
                {

                }
                else
                {
                    yPos = 24;
                }
            }
            else if (cursor.Y >= 612)
            {
                yPos = 600;
            }

            xPos = cursor.X - 12;
            if (cursor.X <= 36 && !(cursor.Y >= 168) && !(cursor.Y <= 168) && level >= 6 && 8 >= level)
            {
                xPos = 24;
            }
            else if (cursor.X >= 612 && !(cursor.Y >= 456) && !(cursor.Y <= 480) && level >= 6 && 8 >= level)
            {
                xPos = 600;
            }

            //checks if the ellipse is in the horizontal or vertical regions of the game and draws the ellipse accordingly
            prevFollowCircle = followCircle;

            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if ((cursor.X - (24 + (k * 144))) * (48 + (k * 144) - cursor.X) >= 0 && (cursor.Y - (24 + (i * 144))) * (48 + (i * 144) - cursor.Y) >= 0) //in an intersection zone
                    {
                        int xCoord = (int)(cursor.X / 144);
                        int yCoord = (int)(cursor.Y / 144);

                        followCircle = new Rectangle(24 + (xCoord * 144), 24 + (yCoord * 144), 24, 24);
                        output = true; return output;
                    }
                }
            }
            //checks otherwise if the cursor is within the x or y quadrants of the game
            for (int i = 0; i < 5; i++) //in x quad
            {
                if ((cursor.Y - (24 + (i * 144))) * (48 + (i * 144) - cursor.Y) >= 0)
                {
                    followCircle = new Rectangle(xPos, 24 + (i * 144), 24, 24);
                    output = true; return output;
                }
            }
            for (int k = 0; k < 5; k++) //in y quad
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

            xInput.Text = level.ToString();
            yInput.Text = mousePosition.Y.ToString();

            if (oppositeTrail.Count > 1)
            {
                rectXPos.Text = oppositeTrail[0].width.ToString();
                rectYPos.Text = oppositeTrail[0].height.ToString();
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
                oppositeIntersection.Clear();
                oppositeTrail.Clear();
                active = false;
                AdjustMouseSpeed.SetMouseSpeed(18);
            }

            if (e.Button == MouseButtons.Left && cursor.IntersectsWith(bottomCircle))
            {
                active = true;
                AdjustMouseSpeed.SetMouseSpeed(3);
                prevPosition = mousePosition;
                Cursor.Position = this.PointToScreen(new Point(bottomCircle.X + bottomCircle.Width / 2, bottomCircle.Y + bottomCircle.Height / 2));

                followCircle = new Rectangle(Cursor.Position.X - 12, Cursor.Position.Y - 12, 24, 24);
                oppositeFollowCircle = new Rectangle(this.Width - followCircle.X - 24, this.Height - followCircle.Y - 24, 24, 24);

                //adds starting rectangles in each list to prevent empty list errors
                if (initialRunStopListError)
                {
                    trail.Add(new Trail(0, 0, 0, 0, "null"));
                    intersection.Add(new TrailIntersection(24, 600));
                }
                initialRunStopListError = true;
            }
        }

        private void MenuScreen_KeyDown(object sender, KeyEventArgs e)
        {
            //closes upon escaping
            if (e.KeyCode == Keys.Escape)
            {
                AdjustMouseSpeed.SetMouseSpeed(18);
                Application.Exit();
            }
        }

        private void levelGen()
        {
            level++;
            Cursor.Position = this.PointToScreen(new Point(bottomCircle.X + bottomCircle.Width / 2, bottomCircle.Y + bottomCircle.Height / 2));

            barriers.Clear();
            intersection.Clear();
            trail.Clear();

            trail.Add(new Trail(0, 0, 0, 0, "null"));
            intersection.Add(new TrailIntersection(24, 600));

            switch (level)
            {
                case 1:
                    exit = new Rectangle(this.Width - 48, 6, 24, 30);

                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 1, 1));
                    barriers.Add(new barrierGen("x", 4, 1));
                    barriers.Add(new barrierGen("x", 1, 2));
                    barriers.Add(new barrierGen("x", 3, 2));
                    barriers.Add(new barrierGen("x", 4, 2));
                    barriers.Add(new barrierGen("x", 3, 3));
                    barriers.Add(new barrierGen("x", 2, 4));
                    barriers.Add(new barrierGen("x", 3, 4));
                    barriers.Add(new barrierGen("x", 4, 4));

                    barriers.Add(new barrierGen("y", 3, 1));
                    barriers.Add(new barrierGen("y", 2, 2));
                    barriers.Add(new barrierGen("y", 1, 3));
                    barriers.Add(new barrierGen("y", 4, 3));
                    barriers.Add(new barrierGen("y", 5, 4));
                    break;
                case 2:
                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 2, 1));
                    barriers.Add(new barrierGen("x", 1, 2));
                    barriers.Add(new barrierGen("x", 4, 3));
                    barriers.Add(new barrierGen("x", 2, 4));
                    barriers.Add(new barrierGen("x", 3, 4));
                    barriers.Add(new barrierGen("x", 2, 5));

                    barriers.Add(new barrierGen("y", 2, 1));
                    barriers.Add(new barrierGen("y", 5, 1));
                    barriers.Add(new barrierGen("y", 2, 2));
                    barriers.Add(new barrierGen("y", 3, 2));
                    barriers.Add(new barrierGen("y", 4, 2));
                    barriers.Add(new barrierGen("y", 3, 3));
                    barriers.Add(new barrierGen("y", 4, 4));
                    break;
                case 3:
                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 1, 1));
                    barriers.Add(new barrierGen("x", 1, 2));
                    barriers.Add(new barrierGen("x", 1, 3));
                    barriers.Add(new barrierGen("x", 3, 3));
                    barriers.Add(new barrierGen("x", 2, 4));
                    barriers.Add(new barrierGen("x", 4, 4));

                    barriers.Add(new barrierGen("y", 3, 1));
                    barriers.Add(new barrierGen("y", 4, 1));
                    barriers.Add(new barrierGen("y", 5, 1));
                    barriers.Add(new barrierGen("y", 4, 2));
                    barriers.Add(new barrierGen("y", 2, 3));
                    barriers.Add(new barrierGen("y", 3, 3));
                    barriers.Add(new barrierGen("y", 3, 4));
                    break;
                case 4:
                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 2, 1));
                    barriers.Add(new barrierGen("x", 1, 2));
                    barriers.Add(new barrierGen("x", 3, 2));
                    barriers.Add(new barrierGen("x", 4, 3));
                    barriers.Add(new barrierGen("x", 3, 4));

                    barriers.Add(new barrierGen("y", 4, 1));
                    barriers.Add(new barrierGen("y", 5, 1));
                    barriers.Add(new barrierGen("y", 2, 2));
                    barriers.Add(new barrierGen("y", 3, 2));
                    barriers.Add(new barrierGen("y", 1, 3));
                    barriers.Add(new barrierGen("y", 2, 3));
                    barriers.Add(new barrierGen("y", 3, 3));
                    barriers.Add(new barrierGen("y", 4, 3));
                    barriers.Add(new barrierGen("y", 5, 4));
                    break;
                case 5:
                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 2, 1));
                    barriers.Add(new barrierGen("x", 3, 1));
                    barriers.Add(new barrierGen("x", 4, 1));
                    barriers.Add(new barrierGen("x", 1, 2));
                    barriers.Add(new barrierGen("x", 2, 2));
                    barriers.Add(new barrierGen("x", 4, 2));
                    barriers.Add(new barrierGen("x", 1, 3));
                    barriers.Add(new barrierGen("x", 2, 3));
                    barriers.Add(new barrierGen("x", 3, 3));
                    barriers.Add(new barrierGen("x", 1, 4));
                    barriers.Add(new barrierGen("x", 2, 4));
                    barriers.Add(new barrierGen("x", 3, 4));
                    barriers.Add(new barrierGen("x", 4, 4));
                    barriers.Add(new barrierGen("x", 1, 5));
                    barriers.Add(new barrierGen("x", 3, 5));
                    barriers.Add(new barrierGen("x", 4, 5));
                    break;
                case 6:
                    invertedDoubleState = true;
                    exit = new Rectangle(this.Width - 36, 456, 30, 24);
                    exit2 = new Rectangle(6, 168, 30, 24);

                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 3, 1));
                    barriers.Add(new barrierGen("x", 1, 2));

                    barriers.Add(new barrierGen("y", 2, 1));
                    barriers.Add(new barrierGen("y", 4, 2));
                    barriers.Add(new barrierGen("y", 1, 3));
                    barriers.Add(new barrierGen("y", 2, 4));
                    barriers.Add(new barrierGen("y", 5, 4));
                    break;
                case 7:
                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 1, 2));
                    barriers.Add(new barrierGen("x", 4, 2));
                    barriers.Add(new barrierGen("x", 2, 3));

                    barriers.Add(new barrierGen("y", 3, 1));
                    barriers.Add(new barrierGen("y", 5, 1));
                    barriers.Add(new barrierGen("y", 4, 3));
                    barriers.Add(new barrierGen("y", 2, 4));
                    barriers.Add(new barrierGen("y", 5, 4));
                    break;
                case 8:
                    //set colour scheme
                    trailBrush = new SolidBrush(Color.Orange);
                    wallBrush = new SolidBrush(Color.Maroon);
                    backgroundBrush = new SolidBrush(Color.Firebrick);
                    hoverBrush = new SolidBrush(Color.LightCoral);

                    //add barriers
                    barriers.Add(new barrierGen("x", 3, 1));
                    barriers.Add(new barrierGen("x", 4, 1));
                    barriers.Add(new barrierGen("x", 4, 3));
                    barriers.Add(new barrierGen("x", 2, 4));
                    barriers.Add(new barrierGen("x", 4, 4));

                    barriers.Add(new barrierGen("y", 2, 1));
                    barriers.Add(new barrierGen("y", 1, 3));
                    break;
                case 9:
                    break;
                case 10:
                    break;
            }
        }
    }
}
