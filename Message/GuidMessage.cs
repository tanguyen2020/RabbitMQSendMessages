using System;

namespace Message
{
    [Serializable]
    public class GuidMessage
    {
        public Guid Identifier { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} Content: '{1}'", Identifier, Content);
        }
    }
}
