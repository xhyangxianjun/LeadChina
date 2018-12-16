﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimMotionADLink
{
    public class PrimTypeAttributes : IPrimTypeAttributes
    {
        public string Name => nameof(PrimADLink);

        public string DisplayName => "MotionCard";

        public string Description => "";

        public string Group => "Dev";

        public string DisplayGroup => "Dev";

        public byte MajorVersion => 1;

        public byte MinorVersion => 0;

        public Image Icon
        {
            get
            {
                var manager = new ResourceManager(typeof(ResourceADlink));
                return (Image) manager.GetObject("icn_轴运动");
            }
        }

        public object this[string attrName]
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<KeyValuePair<string, object>> Attributes
        {
            get { throw new NotImplementedException(); }
        }
    }
}