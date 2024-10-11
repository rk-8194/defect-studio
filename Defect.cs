using System;

namespace DefectStudio
{
    public class Defect
    {
        /// <summary>
        /// The internal name given to the defect.
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }
        private string _name;

        /// <summary>
        /// The general, relative position of the defect in the [100] direction. 
        /// </summary>
        public float OffsetX { get { return _offsetX; } set { _offsetX = Math.Abs(value); } }
        private float _offsetX;

        /// <summary>
        /// The general, relative position of the defect in the [010] direction. 
        /// </summary>
        public float OffsetY { get { return _offsetY; } set { _offsetY = Math.Abs(value); } }
        private float _offsetY;

        /// <summary>
        /// The general, relative position of the defect in the [001] direction. 
        /// </summary>
        public float OffsetZ { get { return _offsetZ; } set { _offsetZ = Math.Abs(value); } }
        private float _offsetZ;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Defect() { }

        /// <summary>
        /// Defines a new defect with the general position relative to the lattice site.
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="offsetZ"></param>
        public Defect(float offsetX, float offsetY, float offsetZ) {  _offsetX = offsetX; _offsetY = offsetY; _offsetZ = offsetZ; }
    }
}

