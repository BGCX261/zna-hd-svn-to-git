using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace ZNA
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ZObject : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Vector2 origin;
        protected Vector2 position;
        protected Vector2 scale;
        protected Color color;
        protected bool show = true;
        protected bool active = true;
        protected int width;
        protected int height;
        protected float rotation;

        protected Texture2D tex;

        protected Game g;

        public ZObject(Game game)
            : base(game)
        {
            // TODO: Construct any child components here

            g = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        /// <summary>
        /// Sets the size of the Game Object.
        /// It is generally a bad idea to use this. Instead you should use scale whenever possible.
        /// </summary>
        /// <param name="w">The width of the object</param>
        /// <param name="h">The height of the object</param>
        public void SetSize(int w, int h)
        {
            width = w;
            height = h;
        }

        /// <summary>
        /// Sets the size of the Game Object equal to the dimensions a Texture
        /// </summary>
        /// <param name="t">The Texture to get the width and height of the object.</param>
        public void SetSize(Texture2D t)
        {
            width = t.Width;
            height = t.Height;
        }

        /// <summary>
        /// Gets the width of the game object
        /// </summary>
        /// <returns>the objects width (in pixels)</returns>
        public int Width() { return width; }

        /// <summary>
        /// Gets the height of the game object.
        /// </summary>
        /// <returns>the objects height (in pixels)</returns>
        public int Height() { return height; }

        /// <summary>
        /// Set the width (in pixels) of the current game object
        /// </summary>
        /// <param name="nWidth">the new width of the game object</param>
        public void Width(int nWidth) { width = nWidth; }

        /// <summary>
        /// Set the height (in pixels) of the current game object
        /// </summary>
        /// <param name="nHeight">The new height of the game object</param>
        public void Height(int nHeight) { height = nHeight; }

        /// <summary>
        /// This will set the color of the object to the specified color
        /// </summary>
        /// <param name="newCol">the objects new color</param>
        public void SetColor(Color newCol)
        {
            color = newCol;
        }

        /// <summary>
        /// This will get the color of the object
        /// </summary>
        /// <returns>the Objects color</returns>
        public Color GetColor()
        {
            return color;
        }

        /// <summary>
        /// Sets the alpha of the Game Object
        /// </summary>
        /// <param name="alpha">The game objects new alpha value</param>
        public void Alpha(byte alpha)
        {
            color.A = alpha;
        }

        /// <summary>
        /// Gets the alpha of the Game Object
        /// </summary>
        /// <returns>the alpha of the Game Object</returns>
        public byte Alpha()
        {
            return color.A;
        }

        /// <summary>
        /// This sets the Texture of the Game Object
        /// </summary>
        /// <param name="newTex">The new texture to use</param>
        public void Texture(Texture2D newTex)
        {
            tex = newTex;
        }

        /// <summary>
        /// Gets the current texture of the Game Object
        /// </summary>
        /// <returns>returns the texture of the Game Object</returns>
        public Texture2D Texture()
        {
            return tex;
        }

        /// <summary>
        /// This will set the position of the object to the specified vector
        /// </summary>
        /// <param name="pos">The objects new position</param>
        public void Position(Vector2 pos)
        {
            position = pos;
        }

        /// <summary>
        /// This will get the current position of the object.
        /// </summary>
        /// <returns>The position of the object</returns>
        public Vector2 Position()
        {
            return position;
        }

        //Movement functions:
        /// <summary>
        /// Move the Game Object towards a set vector.
        /// </summary>
        /// <param name="target">the target vector</param>
        /// <param name="stepSize">the maximum step that the ZObjectDrawableDrawable will take to get to the target point</param>
        public void MoveTowardsPoint(Vector2 target, float stepSize)
        {
            Vector2 v = (position - target);
            v.Normalize();
            position -= (v * stepSize);
        }

        /// <summary>
        /// Move the Game Object directly away from a set vector.
        /// </summary>
        /// <param name="target">the target vector</param>
        /// <param name="stepSize">how fast the game object should move away from the target</param>
        public void MoveFromPoint(Vector2 target, float stepSize)
        {
            Vector2 v = (position - target);
            v.Normalize();
            position += (v * stepSize);
        }

        /// <summary>
        /// Move the Game Object towards another.
        /// </summary>
        /// <param name="target">the Game Object to move towards</param>
        /// <param name="stepSize">the maximum step that the Game Object will take to get to the target position</param>
        public void MoveTowardsObject(ZObject target, float stepSize)
        {
            Vector2 t = target.position - position;
            t.Normalize();
            position += (t * stepSize);
        }

        /// <summary>
        /// Move the Game Object away from another
        /// </summary>
        /// <param name="target">the Game Object to move away from</param>
        /// <param name="stepSize">how fast the Game Object should move away from the target</param>
        public void MoveFromObject(ZObject target, float stepSize)
        {
            Vector2 t = target.position - position;
            t.Normalize();
            position -= (t * stepSize);
        }

        /// <summary>
        /// Move the Game Object in a set direction with set StepSize
        /// </summary>
        /// <param name="direction">The direction to move in</param>
        /// <param name="stepSize">The size of the step to take in the specified direction.</param>
        public void MoveDirection(float direction, float stepSize)
        {
            float hMod = (float)(direction < Math.PI/2 || direction > Math.PI*1.5 ? Math.Cos(direction) : -Math.Cos(direction));
            float vMod = (float)(direction < Math.PI ? Math.Sin(direction) : -Math.Sin(direction));
            position += new Vector2(hMod, vMod) * stepSize;
        }

        /// <summary>
        /// This will move the Game Object outside of the Game Object by moving it in the given direction until it is no longer colliding with it.
        /// </summary>
        /// <param name="mo">The Game Object to Move Outside of</param>
        /// <param name="dir">The direction to move in</param>
        public void MoveOutside(ZObject mo, float dir)
        {
            while (Collision(mo))
            {
                MoveDirection(dir, 1);
            }
        }

        /// <summary>
        /// This will move the Game Object outside of another Game Object by moving it in the given direction with a set step size until it is no longer colliding with the object any more.
        /// This will throw an ArgumentException if stepSize is equal to 0.
        /// </summary>
        /// <param name="mo">The Game Object to Move Outise of</param>
        /// <param name="dir">The direction to move in</param>
        /// <param name="stepSize">The size of the step to take between each collision test.</param>
        public void MoveOutside(ZObject mo, float dir, float stepSize)
        {
            if (stepSize == 0) //Test if stepSize is 0 to avoid infinite loops. Throw ArgumentException if it is.
                throw new ArgumentException("Can not execute MoveOutside with a step size of 0");

            while (Collision(mo))
            {
                MoveDirection(dir, stepSize);
            }
        }

        /// <summary>
        /// Moves the Game Object forward along the objects current rotation.
        /// </summary>
        /// <param name="stepSize">The size of the step to take</param>
        public void MoveForward(float stepSize)
        {
            MoveDirection(rotation, stepSize);
        }

        /// <summary>
        /// Moves the Game Object right and down based on the given arguments.
        /// </summary>
        /// <param name="right">The amount of pixels right to move</param>
        /// <param name="down">The amount of pixels down to move</param>
        public void Move(float right, float down)
        {
            position += new Vector2(right, down);
        }

        /// <summary>
        /// Moves the Game Object by X,Y pixels.
        /// </summary>
        /// <param name="move">The vector of the movement to perform.</param>
        public void Move(Vector2 move)
        {
            position += move;
        }

        /// <summary>
        /// This allows you to set the origin of the Game Object.
        /// </summary>
        /// <param name="orig">The new Origin of the object</param>
        public void Origin(Vector2 orig)
        {
            origin = orig;
        }

        /// <summary>
        /// Gets the origin of the game object.
        /// </summary>
        /// <returns>the origin of the game object</returns>
        public Vector2 Origin()
        {
            return origin;
        }

        /// <summary>
        /// Calculates the objects origin by taking it's width and height and dividing them by 2.
        /// </summary>
        public void CenterOrigin()
        {
            origin = new Vector2(width / 2, height / 2);
        }

        /// <summary>
        /// Sets the rotation of the Game Object (in radians)
        /// </summary>
        /// <param name="newRot">the rotation of the Game Object (in radians)</param>
        public void Rotation(float newRot)
        {
            rotation = newRot;
        }

        /// <summary>
        /// Gets the rotation (in radians) of the Game Object
        /// </summary>
        /// <returns>the rotation (in radians) of the Game Object</returns>
        public float Rotation()
        {
            return rotation;
        }

        /// <summary>
        /// Add a set number of radians to the Game Object's rotation
        /// </summary>
        /// <param name="add">The number of radians to add</param>
        public void Rotate(float add)
        {
            rotation += add;
        }

        /// <summary>
        /// Rotates the Game Object towards a specific rotation moving rotate Amount.
        /// </summary>
        /// <param name="rotTowards">The rotation to rotate towards</param>
        /// <param name="rotateAmount">The size of the rotation step (in radians)</param>
        public void RotateTowards(float rotTowards, float rotateAmount)
        {
            rotation = (float)(Math.Abs(rotation) % (2 * Math.PI)); // Get the min rotation of the obj
            float rotDir;
            if (rotation > rotTowards)
                rotDir = -(float)rotateAmount;
            else rotDir = (float)rotateAmount;
            rotation += rotDir;
        }

        /// <summary>
        /// Gets if the Game Object will be drawn or not
        /// </summary>
        /// <returns>Will return true if the Game Object is shown and is active. Otherwise this will return false.</returns>
        public bool isShown()
        {
            return show && active;
        }

        /// <summary>
        /// Sets whether the Game Object should be shown or not.
        /// </summary>
        /// <param name="toShow">If the Game Object should be shown</param>
        public void Show(bool toShow)
        {
            show = toShow;
        }

        /// <summary>
        /// Sets the Game Object's visibility to match that of the given Game Object.
        /// </summary>
        /// <param name="showLike">The Game Object who's visibility will be matched</param>
        public void ShowLike(ZObject showLike)
        {
            show = showLike.show;
        }

        /// <summary>
        /// Scales the Game Object to a specific value.
        /// </summary>
        /// <param name="scaleTo">The new scale of the Game Object</param>
        public void Scale(Vector2 scaleTo)
        {
            scale = scaleTo;
        }

        /// <summary>
        /// Gets the scale of the Game Object
        /// </summary>
        /// <returns>the scale of the Game Object</returns>
        public Vector2 Scale()
        {
            return scale;
        }

        /// <summary>
        /// Multiplies the scale of the Game Object by a set amount
        /// </summary>
        /// <param name="mult">The scalar to be applied to the scale of the Game Object</param>
        public void ScaleMult(float mult)
        {
            scale *= mult;
        }

        /// <summary>
        /// Sets the Game Objects scale to that of another Game Object.
        /// </summary>
        /// <param name="gobj">The game object to match the scale of</param>
        public void ScaleTo(ZObject gobj)
        {
            scale = gobj.scale;
        }

        /// <summary>
        /// Scales the Game Object to match proportions with another.
        /// This is different than ScaleTo as it will make sure both
        /// ZObjectDrawableDrawables have the same width and height in pixels instead of simply matching the scale of both objects.
        /// </summary>
        /// <param name="gobj">The game object with which to match the scale of</param>
        public void RelativeScale(ZObject gobj)
        {
            float hScale = (float)gobj.width / (float)width;
            float vScale = (float)gobj.height / (float)height;
            scale = new Vector2(hScale, vScale);
        }

        /// <summary>
        /// Scales the Game Object to a set size given by an int representing the width and height.
        /// This will scale both the Height and Width equally.
        /// </summary>
        /// <param name="scaleTo">The new size of the Game Object, given in pixels</param>
        public void RelativeScale(int scaleTo)
        {
            float hScale = (float)scaleTo / (float)width;
            float vScale = (float)scaleTo / (float)height;
            scale = new Vector2(hScale, vScale);
        }

        /// <summary>
        /// Scales the Game Object to a set size given by an int representing the width and height.
        /// </summary>
        /// <param name="scaleX">The new width of the Game Object, given in pixels</param>
        /// <param name="scaleY">The new height of the Game Object, given in pixels</param>
        public void RelativeScale(int scaleX, int scaleY)
        {
            float hScale = (float)scaleX / (float)width;
            float vScale = (float)scaleY / (float)height;
            scale = new Vector2(hScale, vScale);
        }

        /// <summary>
        /// Scales the game object to a set size given by a Vector2.
        /// </summary>
        /// <param name="scaleTo">The vector to scale the game object to match.</param>
        public void RelativeScale(Vector2 scaleTo)
        {
            float hScale = (float)scaleTo.X / (float)width;
            float vScale = (float)scaleTo.Y / (float)height;
            scale = new Vector2(hScale, vScale);
        }

        /// <summary>
        /// Activate the Game Object.
        /// </summary>
        public void Activate()
        {
            active = true;
        }

        /// <summary>
        /// Deactivate the Game Object.
        /// </summary>
        public void Deactivate()
        {
            active = false;
        }

        /// <summary>
        /// Set the Game Object to be active or inactive.
        /// </summary>
        /// <param name="isActive">The new active state of the Game Object.</param>
        public void Active(bool isActive)
        {
            active = isActive;
        }

        /// <summary>
        /// Get if the Game Object is Active or not.
        /// </summary>
        /// <param name="isActive">The active state of the Game Object.</param>
        public bool Active()
        {
            return active;
        }

        //Collision Detection Stuff:

        /// <summary>
        /// This tests if a given point is within the Game Object.
        /// </summary>
        /// <param name="point">The point to test</param>
        /// <returns>This returns true if the point is within the rectangle of the Game Object</returns>
        public bool PointWithin(Vector2 point)
        {
            if (point.X > position.X && point.Y > position.Y)
            {
                if (point.X < position.X + width && point.Y < position.Y + height)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This will return true if any of obj's sides are within the current Game Object.
        /// </summary>
        /// <param name="obj">The game object to test collisions with</param>
        /// <returns>If the game object is within this one than true is returned. Otherwise false is.</returns>
        public bool Collision(ZObject obj)
        {
            if (PointWithin(obj.position) || PointWithin(obj.position + new Vector2(width, 0)) ||
                PointWithin(obj.position + new Vector2(0, height)) || PointWithin(obj.position + new Vector2(width,height)))
                    return true;
            return false;
        }

        /// <summary>
        /// Tests if an object is fully within Game Object
        /// </summary>
        /// <param name="obj">The Game Object to test</param>
        /// <returns>Returns true if "obj" is completely within this Game Object</returns>
        public bool Within(ZObject obj)
        {
            if (PointWithin(obj.position) && PointWithin(obj.position + new Vector2(width, 0)) &&
                PointWithin(obj.position + new Vector2(0, height)) && PointWithin(obj.position + new Vector2(width, height)))
                    return true;
            return false;
        }

        /// <summary>
        /// This gets the distance between this object and a set point.
        /// </summary>
        /// <param name="point">The point to find the distance between</param>
        /// <returns>Gets distance between this objects position and the point.</returns>
        public float DistanceTo(Vector2 point)
        {
            return (point - position).Length();
        }

        /// <summary>
        /// This will get the distance between this object's and "obj's" position
        /// </summary>
        /// <param name="obj">The object to compare distances with</param>
        /// <returns>The distance between obj and this Game Object</returns>
        public float DistanceTo(ZObject obj)
        {
            return (obj.position - position).Length();
        }

        /// <summary>
        /// Tests if a point is within the set distance
        /// </summary>
        /// <param name="point">The point to test</param>
        /// <param name="dist">The distance to test if is within</param>
        /// <returns>True if the point is within the distance of dist</returns>
        public bool IsWithin(Vector2 point, float dist)
        {
            return (DistanceTo(point) <= dist);
        }

        /// <summary>
        /// Tests if an Object is within the set distance
        /// </summary>
        /// <param name="gobj">The Game Object to test</param>
        /// <param name="dist">The distance to test if is within</param>
        /// <returns>True if the point is within the distance of dist</returns>
        public bool IsWithin(ZObject gobj, float dist)
        {
            return (DistanceTo(gobj) <= dist);
        }

        public T InstanceNearest<T>() where T: ZObject
        {
            T ret = (T)this;
            float dist = -1;
            
            foreach(T obj in g.Components)
            {
                if (obj == this)
                    continue;
                if (dist == -1)
                    obj.DistanceTo(Position());
                if (obj.DistanceTo(Position()) < dist)
                {
                    ret = obj;
                    dist = obj.DistanceTo(Position());
                }
            }

            return ret;
        }
    }
}