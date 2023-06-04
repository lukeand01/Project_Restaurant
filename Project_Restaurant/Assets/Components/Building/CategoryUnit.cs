using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CategoryUnit : ButtonBase
{
    BuildCategoryType categoryType;

    public void SetUp(BuildCategoryType categoryType)
    {
        this.categoryType = categoryType;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //
        UIHolder.instance.building.ChangeCategory(categoryType);
    }
}
