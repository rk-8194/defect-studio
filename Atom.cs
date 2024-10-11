using System;

namespace DefectStudio
{
    public class Atom
    {
        public int Number { get => number; set => number = value; }
        public string Species { get => species; set => species = value; }
        public float PosX { get => posX; set => posX = value; }
        public float PosY { get => posY; set => posY = value; }
        public float PosZ { get => posZ; set => posZ = value; }

        private string species;
        private int number;
        private float posX;
        private float posY;
        private float posZ;

        public Atom(int _number, string _species, float _posX, float _posY, float _posZ)
        {
            number = _number;
            species = _species;
            posX = _posX;
            posY = _posY;
            posZ = _posZ;
        }


    }
}
