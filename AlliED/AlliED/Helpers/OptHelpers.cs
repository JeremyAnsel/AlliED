using JeremyAnsel.Xwa.Opt;
using JeremyAnsel.Xwa.Workspace;
using System.IO;

namespace AlliED.Helpers;

internal static class OptHelpers
{
    public static string GetXwaDirectory()
    {
        return AlliedVariables.s_XWADirLabSetting;
    }

    private static XwaWorkspace? _xwaWorkspace;
    private static bool _xwaWorkspaceNotFoundMessageShown = false;

    public static XwaWorkspace? XwaWorkspace
    {
        get
        {
            if (_xwaWorkspace is null)
            {
                try
                {
                    _xwaWorkspace = new(GetXwaDirectory());
                }
                catch
                {
                    if (!_xwaWorkspaceNotFoundMessageShown)
                    {
                        _xwaWorkspaceNotFoundMessageShown = true;
                        System.Windows.MessageBox.Show("XWA directory not found.");
                    }
                }
            }

            return _xwaWorkspace;
        }
    }

    public record OptCraftData(float size, List<Tuple<Vector, Vector>> geometry);

    private static readonly Dictionary<int, OptCraftData> CraftData = new();

    private static string GetOptFileNameByCraftIndex(int craftIndex)
    {
        if (XwaWorkspace is null)
        {
            return string.Empty;
        }

        int modelIndex = XwaWorkspace.SpeciesTable.Entries[craftIndex].Value;
        return XwaWorkspace.GetModelName(modelIndex);
    }

    public static OptCraftData GetCraftData(int craftIndex)
    {
        if (XwaWorkspace is null)
        {
            return new OptCraftData(0.0f, new List<Tuple<Vector, Vector>>());
        }

        if (CraftData.TryGetValue(craftIndex, out var data))
        {
            return data;
        }

        string optName = GetOptFileNameByCraftIndex(craftIndex);
        string optPath = Path.Combine(XwaWorkspace.WorkingDirectory, XwaWorkspace.FlightModelDirectory, optName + ".opt");

        if (!File.Exists(optPath))
        {
            data = new OptCraftData(0.0f, new List<Tuple<Vector, Vector>>());
        }
        else
        {
            OptFile opt = OptFile.FromFile(optPath, false);
            opt.Scale(OptFile.ScaleFactor);

            data = new OptCraftData(opt.Size, new List<Tuple<Vector, Vector>>());

            CreateGeometry(opt, data.geometry);
        }


        CraftData[craftIndex] = data;
        return data;
    }

    private static void CreateGeometry(OptFile opt, List<Tuple<Vector, Vector>> geometry)
    {
        foreach (var mesh in opt.Meshes)
        {
            if (mesh.Lods.Count == 0)
            {
                continue;
            }

            if (mesh.Vertices is null)
            {
                continue;
            }

            void addLine(int a, int b)
            {
                Vector pointA = mesh.Vertices![a];
                Vector pointB = mesh.Vertices![b];
                geometry.Add(Tuple.Create(pointA, pointB));
            }

            var lod = mesh.Lods[0];

            foreach (var faceGroup in lod.FaceGroups)
            {
                if (faceGroup.Faces is null)
                {
                    continue;
                }

                foreach (var face in faceGroup.Faces)
                {
                    Indices positionsIndex = face.VerticesIndex;

                    addLine(positionsIndex.A, positionsIndex.B);
                    addLine(positionsIndex.B, positionsIndex.C);

                    if (positionsIndex.D < 0)
                    {
                        addLine(positionsIndex.C, positionsIndex.A);
                    }
                    else
                    {
                        addLine(positionsIndex.C, positionsIndex.D);
                        addLine(positionsIndex.D, positionsIndex.A);
                    }
                }
            }
        }
    }
}
