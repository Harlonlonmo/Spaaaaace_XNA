using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Spaaaaace.Graphics
{
    public class TextureBank
    {
        private Dictionary<String, Texture2D> textures;

        public TextureBank()
        {
            textures = new Dictionary<String, Texture2D>();
        }

        public void Load(ContentManager Content, String name)
        {
            textures.Add(name, Content.Load<Texture2D>(name));
        }

        public Texture2D getTexture(String name){
            return textures[name];
        }
    }
}
