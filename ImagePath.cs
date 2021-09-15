using System;

namespace Platform
{
    public class ImagePath
    {
        private string _path;

        public string Path
        {
            get => _path;
            set
            {
                if (!value.EndsWith(".jpg") && !value.EndsWith(".png"))
                {
                    throw new Exception("Incorrect image path (suffix .jpg or .png does not present)");
                }
                _path = value;
            }
        }

        public override string ToString()
        {
            return _path;
        }
    }
}