using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ZNAlib.Rooms
{
    /// <summary>
    /// The Room interface contains functions that may be used to construct a Room Object.
    /// 
    /// Rooms act as seperators in a Game. They allow actions to take place within them without affecting other rooms.
    /// This means that Rooms may be used to easily switch between scenes in a game and then switch back.
    /// 
    /// Rooms are controlled by the RoomController. The RoomController allows easy swapping between rooms and controls
    /// all aspects of a room.
    /// 
    /// A Room's Load, Initiate, Initilize, Update, Draw, Unload are all called by the RoomController and should NEVER
    /// be called manually. Calling Room Updates and Draw calls manually may result in terrible things.
    /// 
    /// Definition: A Persistant Room will not revert to it's original state after it is created. Thus, a persistant room
    /// is... persistant.
    /// </summary>
    interface Room
    {
        /// <summary>
        /// Load is called when the roam is initiated. This occurs when the GotoRoom() function is called.
        /// </summary>
        void Load();

        /// <summary>
        /// Initiate is called directly after a level is loaded. This is called after every Room load.
        /// </summary>
        void Initiate();

        /// <summary>
        /// Initilize is called only once, when the game/app is first started.
        /// </summary>
        void Initilize();

        /// <summary>
        /// Update is called once per frame and updates all objects in the room.
        /// If the room should perform something special (effects, testing etc) it should be in here.
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw is used to draw all the objects in a room to the screen.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch that the room will be drawn to</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// A room is unloaded before another may be loaded.
        /// </summary>
        void Unload();

        /// <summary>
        /// A room is persistant if and only if it remains the same as it was when it is reloaded as it was when it was last unloaded.
        /// 
        /// A persistant room will not change or revert to it's original form if it is unloaded and reloaded.
        /// </summary>
        /// <returns></returns>
        Boolean Persistant();
    }
}
