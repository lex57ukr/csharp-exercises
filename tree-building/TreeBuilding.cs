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
        var orderedRecords = records.OrderBy(x => x.RecordId).ToArray();
        if (! orderedRecords.Valid())
        {
            throw new ArgumentException();
        }

        var nodes = orderedRecords.Select(MakeTreeNode).ToArray();
        foreach (var child in nodes.Skip(1))
        {
            nodes[child.ParentId].Children.Add(child);
        }

        return nodes[0];
    }

    private static Tree MakeTreeNode(TreeBuildingRecord x)
        => new Tree {Id = x.RecordId, ParentId = x.ParentId, Children = new List<Tree>()};

    private static bool Valid(this IReadOnlyList<TreeBuildingRecord> records)
    {
        bool IsValidParentId(TreeBuildingRecord x)
            => x.ParentId < x.RecordId && x.ParentId < records.Count;

        bool IsValidChild(TreeBuildingRecord x, int i)
            => x.RecordId == (i + 1) && IsValidParentId(x);

        return records.Count != 0
            && records[0].RecordId == 0
            && records[0].ParentId == 0
            && records.Skip(1).Select(IsValidChild).All(x => x);
    }
}
