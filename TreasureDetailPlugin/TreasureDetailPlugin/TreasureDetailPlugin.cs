﻿namespace TreasureDetailPlugin
{
    using GameFreeText;
    using GameGlobal;
    using GameObjects;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PluginInterface;
    using PluginInterface.BaseInterface;
    using System;
    using System.Drawing;
    using System.Xml;

    public class TreasureDetailPlugin : GameObject, ITreasureDetail, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"GameComponents\TreasureDetail\Data\";
        private string description = "宝物细节显示";
        private GraphicsDevice graphicsDevice;
        private const string Path = @"GameComponents\TreasureDetail\";
        private string pluginName = "TreasureDetailPlugin";
        private TreasureDetail treasureDetail = new TreasureDetail();
        private string version = "1.0.0";
        private const string XMLFilename = "TreasureDetailData.xml";

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.treasureDetail.IsShowing)
            {
                this.treasureDetail.Draw(spriteBatch);
            }
        }

        public void Initialize()
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Font font;
            Microsoft.Xna.Framework.Graphics.Color color;
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.treasureDetail.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.treasureDetail.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.treasureDetail.BackgroundTexture = Texture2D.FromFile(this.graphicsDevice, @"GameComponents\TreasureDetail\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.treasureDetail.PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
            for (int i = 0; i < node.ChildNodes.Count; i += 2)
            {
                LabelText item = new LabelText();
                XmlNode node3 = node.ChildNodes.Item(i);
                Microsoft.Xna.Framework.Rectangle rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Label = new FreeText(this.graphicsDevice, font, color);
                item.Label.Position = rectangle;
                item.Label.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.Label.Text = node3.Attributes.GetNamedItem("Label").Value;
                node3 = node.ChildNodes.Item(i + 1);
                rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Text = new FreeText(this.graphicsDevice, font, color);
                item.Text.Position = rectangle;
                item.Text.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.PropertyName = node3.Attributes.GetNamedItem("PropertyName").Value;
                this.treasureDetail.LabelTexts.Add(item);
            }
            node = nextSibling.ChildNodes.Item(3);
            this.treasureDetail.DescriptionClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.treasureDetail.DescriptionText.ClientWidth = this.treasureDetail.DescriptionClient.Width;
            this.treasureDetail.DescriptionText.ClientHeight = this.treasureDetail.DescriptionClient.Height;
            this.treasureDetail.DescriptionText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.treasureDetail.DescriptionText.Builder.SetFreeTextBuilder(this.graphicsDevice, font);
            this.treasureDetail.DescriptionText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(4);
            this.treasureDetail.InfluenceClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.treasureDetail.InfluenceText.ClientWidth = this.treasureDetail.InfluenceClient.Width;
            this.treasureDetail.InfluenceText.ClientHeight = this.treasureDetail.InfluenceClient.Height;
            this.treasureDetail.InfluenceText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.treasureDetail.InfluenceText.Builder.SetFreeTextBuilder(this.graphicsDevice, font);
            this.treasureDetail.InfluenceText.DefaultColor = color;
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"GameComponents\TreasureDetail\TreasureDetailData.xml");
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.treasureDetail.SetPosition(showPosition);
        }

        public void SetScreen(object screen)
        {
            this.treasureDetail.Initialize(screen as Screen);
        }

        public void SetTreasure(object treasure)
        {
            this.treasureDetail.SetTreasure(treasure as Treasure);
        }

        public void Update(GameTime gameTime)
        {
        }

        public string Author
        {
            get
            {
                return this.author;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public object Instance
        {
            get
            {
                return this;
            }
        }

        public bool IsShowing
        {
            get
            {
                return this.treasureDetail.IsShowing;
            }
            set
            {
                this.treasureDetail.IsShowing = value;
            }
        }

        public string PluginName
        {
            get
            {
                return this.pluginName;
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }
    }
}

