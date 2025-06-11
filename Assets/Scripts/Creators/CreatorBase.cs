using UnityEngine;

public class CreatorBase : Creator<Base>
{
    public override Base Create(Vector3 position)
    {
        Base @base = Instantiate(Prefab, position, Quaternion.identity);
        @base.gameObject.SetActive(true);

        return @base;
    }
}