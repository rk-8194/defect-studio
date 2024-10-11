using System;
using System.Linq.Expressions;

namespace DefectStudio
{
    public static class StructureManager
    {
        private static float[,] cellParameters = new float[3, 3];
        private static int totalCount = 0;
        private static List<Tuple<string, int>> speciesCount = [];
        private static List<Atom> atoms = [];

        public static void LoadXYZ()
        {
            Dictionary<string, int> map = new Dictionary<string, int>() {
                {"HEADER", 0 },
                {"COUNT", 1 },
                {"POSITIONS", 2 }
            };
        }

        public static void LoadVASP()
        {
            DebugMessage.New(DebugMessageType.Normal, "Reading VASP POSCAR file.");

            Dictionary<string, int> map = new Dictionary<string, int>() {
                {"HEADER", 0 },
                {"SCALING", 1 },
                {"CELL_X", 2 },
                {"CELL_Y", 3 },
                {"CELL_Z", 4 },
                {"SPECIES", 5 },
                {"COUNT", 6 },
                {"COORDINATE_SYSTEM", 7 },
                {"POSITIONS", 8 }
            };

            // Set the cell parameters from the vasp POSCAR.
            string[] _cell_x = TaskManager.WorkContents[map["CELL_X"]].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] _cell_y = TaskManager.WorkContents[map["CELL_Y"]].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] _cell_z = TaskManager.WorkContents[map["CELL_Z"]].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            float _scaling = float.Parse(TaskManager.WorkContents[map["SCALING"]].Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]);

            cellParameters = new float[3, 3];

            for (int i = 0; i < 3; i++) {
                cellParameters[i, 0] = float.Parse(_cell_x[i]) * _scaling;
                cellParameters[i, 1] = float.Parse(_cell_y[i]) * _scaling;
                cellParameters[i, 2] = float.Parse(_cell_z[i]) * _scaling;
            }

            // Get the species count.
            string[] _count = TaskManager.WorkContents[map["COUNT"]].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] _species = TaskManager.WorkContents[map["SPECIES"]].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            totalCount = 0;
            speciesCount = [];
            for (int k = 0; k < _species.Length; k++) {
                string _label = _species[k];
                int _value = int.Parse(_count[k]);

                speciesCount.Add(new Tuple<string, int>(_label, _value));
                totalCount += _value;
            }

            // Get a temporary track of the range covered by each species.
            List<Tuple<string, int>> _track = new List<Tuple<string, int>>();
            int _cumulativeCount = 0;
            for (int _k = 0; _k < speciesCount.Count; _k++) {
                _cumulativeCount += speciesCount[_k].Item2;
                Tuple<string, int> _newTrack = new Tuple<string, int>(speciesCount[_k].Item1,
                    _cumulativeCount);
                _track.Add(_newTrack);
            }

            // Get the atom positions. Start at the "POSITIONS" index and run until either end of file or until a blank line
            // is reached.
            for (int l = map["POSITIONS"]; l < TaskManager.WorkContents.Count; l++) {
                string[] _line = TaskManager.WorkContents[l].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                // Break out if the line is blank.
                if (_line.Length == 0) { break; }

                // Which atom number is this?
                int _number = l - map["POSITIONS"] + 1;

                // Get the species based on the cumulative count.
                string _atomSpecies = "??";
                for (int _l = 0; _l < _track.Count; _l++) {
                    if (_track[_l].Item2 < _number) continue;
                    else _atomSpecies = _track[_l].Item1; break;
                }

                // Add the new atom to the list.
                Atom _atom = new Atom(_number,
                    _atomSpecies, 
                    float.Parse(_line[0]), 
                    float.Parse(_line[1]), 
                    float.Parse(_line[2]));

                atoms.Add(_atom);
            }

            // Print the structure that has been loaded.
            PrintCurrentStructure();
        }

        private static void PrintCurrentStructure()
        {
            DebugMessage.New(DebugMessageType.Normal, "");

            PrintCellParameters();
            PrintSpeciesCount();
            PrintAtoms();
        }

        private static void PrintCellParameters()
        {
            DebugMessage.New(DebugMessageType.Normal,
                "\t### Cell Parameters ###\n" +
                $"\t\t\t{cellParameters[0, 0]}\t{cellParameters[1, 0]}\t{cellParameters[2, 0]}\n" +
                $"\t\t\t{cellParameters[0, 1]}\t{cellParameters[1, 1]}\t{cellParameters[2, 1]}\n" +
                $"\t\t\t{cellParameters[0, 2]}\t{cellParameters[1, 2]}\t{cellParameters[2, 2]}\n");
        }

        private static void PrintSpeciesCount()
        {
            DebugMessage.New(DebugMessageType.Normal, "\t### Species Count ###");
            foreach (Tuple<string,int> t in speciesCount)
            {
                DebugMessage.New(DebugMessageType.Normal, $"\t\t{t.Item1}\t{t.Item2}");
            }
        }

        private static void PrintAtoms()
        {
            DebugMessage.New(DebugMessageType.Normal, "\n\t\t\t\t### Positions ###");
            foreach (Atom a in atoms)
            {
                DebugMessage.New(DebugMessageType.Normal, $"{a.Number}" +
                    $"\t{a.Species}" +
                    $"\t{a.PosX}" +
                    $"\t{a.PosY}" +
                    $"\t{a.PosZ}");
            }
        }
    }
}

