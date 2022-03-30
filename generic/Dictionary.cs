class DictClass
{
    private static void Main()
    {
        Dictionary<int, List<Entity>> resultDict = new Dictionary<int, List<Entity>>();

        Entity first = new Entity(1, 0, "Root entity");
        Entity second = new Entity(2, 1, "Child of 1 entity");
        Entity third = new Entity(3, 1, "Child of 1 entity");
        Entity fourth = new Entity(4, 2, "Child of 2 entity");
        Entity fifth = new Entity(5, 4, "Child of 4 entity");

        List<Entity> entityList = new List<Entity>();
        entityList.Add(first);
        entityList.Add(second);
        entityList.Add(third);
        entityList.Add(fourth);
        entityList.Add(fifth);

        first.ToDict(entityList, resultDict);

        foreach (var gr in resultDict)
        {
            Console.Write($"Key: {gr.Key},  Value = List: ");
            foreach (var x in gr.Value)
            {
                Console.Write($"Entity [id = {x.Id}] ");
            }
            Console.WriteLine();
        }
    }
}

public class Entity
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }

    public Entity(int id, int parentid, string name)
    {
        Id = id;
        ParentId = parentid;
        Name = name;
    }

    public void ToDict(List<Entity> entityList, Dictionary<int, List<Entity>> dict)
    {
        var result = entityList.GroupBy(p => p.ParentId);
        foreach (var group in result)
        {
            var list = group.ToList();
            dict.Add(group.Key, list);
        }
    }
}