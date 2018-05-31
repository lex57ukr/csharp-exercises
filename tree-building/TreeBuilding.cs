using System;
using System.Linq;
using System.Collections.Generic;


public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public int Id { get; set; }
    public int ParentId { get; set; }

    public List<Tree> Children { get; set; }

    public bool IsLeaf => Children.Count == 0;
}

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        var recordsArray = records.OrderBy(x => x.RecordId).ToArray();
        AssertNodesIntegrity(recordsArray);

        var nodes = recordsArray.Select(ToTreeNode).ToArray();
        foreach (var child in nodes.Skip(1))
        {
            nodes[child.ParentId].Children.Add(child);
        }

        return nodes[0];
    }

    private static Tree ToTreeNode(TreeBuildingRecord record)
        => new Tree {Id = record.RecordId, ParentId = record.ParentId, Children = new List<Tree>()};

    private static void AssertNodesIntegrity(IReadOnlyList<TreeBuildingRecord> records)
    {
        if (records.Count == 0)
        {
            throw new ArgumentException("Empty records.");
        }

        if (! IsValidRoot(records[0]))
        {
            throw new ArgumentException("Missing root.");
        }

        var allChildrenValid = records.Skip(1).Select(
            (x, i) => IsValidRecordId(x, i + 1) && IsValidParentId(x, records.Count)
        ).All(x => x);

        if (! allChildrenValid)
        {
            throw new ArgumentException("Invalid records.");
        }
    }

    private static bool IsValidRoot(TreeBuildingRecord x)
        => x.ParentId == 0 && IsValidRecordId(x, 0);

    private static bool IsValidRecordId(TreeBuildingRecord x, int id)
        => x.RecordId == id;

    private static bool IsValidParentId(TreeBuildingRecord x, int count)
        => x.ParentId < x.RecordId && x.ParentId < count;
}
